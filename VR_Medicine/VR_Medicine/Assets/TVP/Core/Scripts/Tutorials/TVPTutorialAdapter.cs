using Autohand;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace TVP
{
    public class TVPTutorialAdapter : MonoSinglethon<TVPTutorialAdapter>
    {
        [SerializeField] TutorialController _tutorialControlelr;

        [SerializeField] HandControllerTutorialData _right;
        [SerializeField] HandControllerTutorialData _left;


        [SerializeField] Grabbable _niddle;


        [SerializeField] Grabbable _uzi;

        [SerializeField] GameObject _leftGObject;
        [SerializeField] GameObject _rightGObject;


        public TutorialController TutorialControlelr { get => _tutorialControlelr; set => _tutorialControlelr = value; }
        public bool IsShowTutorial { get; private set; }

        private void Awake()
        {
            TutorialControlelr = GameObject.FindAnyObjectByType<TutorialController>();
            IsShowTutorial = false;
        }

        private void Start()
        { 
            Invoke("CustomSticksTutorial", 0.5f);
        }

        public void CustomSticksTutorial()
        {
          

            _left.HorizontalStick.gameObject.SetActive(true);
            _left.VerticalStick.gameObject.SetActive(true);

            _right.HorizontalStick.gameObject.SetActive(true);
            _right.VerticalStick.gameObject.SetActive(true);

            _left.HorizontalStick.StartTutorial();
            _left.VerticalStick.StartTutorial();
            _right.HorizontalStick.StartTutorial();
            _right.VerticalStick.StartTutorial(); 
            
            _leftGObject.SetActive(true);
            _rightGObject.SetActive(true);
        }

        public void ShowNiddleSelectTutorial()
        {
            TutorialControlelr.ShowTriggerButton(_niddle);
        }
        public void ShowDispenceAspirateTutorial()
        { 
            TutorialControlelr.ShowTriggerButton(_uzi);
            TutorialControlelr.ShowPrimaryButton(_uzi);
            TutorialControlelr.ShowSecondButton(_uzi);
        }
        

        internal void ShowDontRealeasePedalTutorial()
        {
 
        }
    }
}