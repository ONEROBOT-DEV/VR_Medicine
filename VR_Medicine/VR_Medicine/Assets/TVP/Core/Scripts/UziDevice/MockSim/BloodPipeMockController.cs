using Common.DataManager;
using System;
using System.Collections;
using System.Collections.Generic;
using TVP.UI;
using UnityEngine;
using UnityEngine.Events;

namespace TVP.MockSim
{
    public class BloodPipeMockController : MonoBehaviour
    {
        [SerializeField] GameObject _outline;

        public bool IsPenetrated { get; private set; }
        public bool DoesPenBloodPipe { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<VirtualNiddleController>())
            {
                DoesPenBloodPipe = true;
                
                UnityAction revert = () => 
                { 
                    SimDomenStateMachine.CurrentSimulation.ResetCurrentScene(); 
                    DoesPenBloodPipe = false; 
                    ErrorsManager.CurrentSimulation.LevelIsPassedWithoutError(SimDomenStateMachine.CurrentSimulation.CurrentState.CurrentLevelNum); 
                };


                var surgeryLevelNum = 5;

                ErrorsManager
                    .CurrentSimulation
                        .LevelIsPassedWithError(surgeryLevelNum,"Вы задели сосуд при операции", null, revert, "Вы задели сосуд", "Продолжить", "Еще раз");


            }

            if (other.gameObject.GetComponent<GuideLineController>())
            {
                StartCoroutine(Blinking());
            }
        }

        IEnumerator Blinking()
        {
            IsPenetrated = true;
            while (IsPenetrated)
            {
                _outline.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                _outline.SetActive(false);
            }
            _outline.SetActive(false);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetComponent<GuideLineController>())
            {
                IsPenetrated = false;
                StopAllCoroutines();
                _outline.SetActive(false);

            }
        }
    }
}