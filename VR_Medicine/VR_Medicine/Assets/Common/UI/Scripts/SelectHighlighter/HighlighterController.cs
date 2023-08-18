using Autohand; 
using UnityEngine; 
using UnityEngine.EventSystems;
using DG.Tweening;

namespace Common.UI
{
    public class HighlighterController : MonoBehaviour 
    {
        [SerializeField] GameObject _shape;
        [SerializeField] Grabbable _grabale;
        [SerializeField] Renderer _view;
         


        private void Awake()
        {
            if(_grabale==null)
            {
                _grabale = GetComponentInParent<Grabbable>();
                Debug.LogError("Не поулчается найти родительсктй грабабл");
            } 
            else
            {
                _grabale.onHighlight.AddListener(HighlightStart);
                _grabale.onUnhighlight.AddListener(HighlightFinish);
            }
        }

        private void Start()
        {
            UnhighlightLogic();
        }
        private void  HighlightStart(Hand hand, Grabbable grab)
        {
            HighlightLogc();

        }
        private void HighlightFinish(Hand hand, Grabbable grab)
        {
            UnhighlightLogic();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UnhighlightLogic();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            HighlightLogc();
        }

        public void HighlightLogc()
        {
            _view.material.SetFloat("_Glow", 0);
            _shape.SetActive(true);
            _view.transform.DOShakeScale(1);
            _view.material.DOFloat(1, "_Glow",1);
        }
        public void UnhighlightLogic()
        {

            _shape.SetActive(false);
        }
    }
}