using System; 
using UnityEngine;
using DG.Tweening; 
using UnityEngine.Events;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using TVP.MockSim;
using System.Collections;

namespace TVP
{
    public class FolikulController : MonoBehaviour
    {

        [Header("Domain")]

        [SerializeField] private float _volume;
        [SerializeField] bool _isDone = false;

        [Header("View")]

        [SerializeField] GameObject _outline;

        public float Volume 
        { 
            get => _volume; 
            set
            {
                _volume = Mathf.Clamp01(value);
                transform.DOScale(_volume, 1);
            }
        
        
        }

        public bool IsDone { get => _isDone; set => _isDone = value; }
        public bool IsPenetrated { get; private set; }

        public UnityEvent OnAspirating = new UnityEvent();

        private void Awake()
        {
            _volume = transform.localScale.x ;
            OnAspirating.AddListener(ShowOutline);

        }
        internal void Decrese()
        {

            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SurgeryIsStart_07_01_01);
            float pressuare = (float)PompController.CurrentSimulation.TotalPressaure / 100000; 
            Volume -= pressuare;

            IsDone = Volume <= 0.1f;
            OnAspirating?.Invoke(); 
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<GuideLineController>())
            {
                StartCoroutine(Blinking());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<GuideLineController>())
            {
                IsPenetrated = false;
                StopAllCoroutines();
                HideOutline();
            } 
        }


        IEnumerator Blinking()
        {
            IsPenetrated = true;
            while (IsPenetrated)
            {
                ShowOutline();
                yield return new WaitForSeconds(0.4f);
                HideOutline();
            }

            HideOutline();
        }

        [Sirenix.OdinInspector.Button]
        public void ShowOutline()
        {
            _outline.SetActive(true); 
        }
        public void HideOutline()
        {
            _outline.SetActive(false);
        }

    }
}