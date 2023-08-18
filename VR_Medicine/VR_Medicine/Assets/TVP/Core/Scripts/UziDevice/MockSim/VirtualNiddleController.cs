using Common.DataManager;
using Common.UI;
using System.Collections;
using System.Collections.Generic;
using TVP.UI;
using UnityEngine;
using UnityEngine.Events;

namespace TVP.MockSim
{


    public class VirtualNiddleController : MonoBehaviour
    {
        [SerializeField] DeviceToVirtualNiddle _device;
        [SerializeField] bool _doesPenBloodPipe;

        public bool DoesPenBloodPipe { get => _doesPenBloodPipe; set => _doesPenBloodPipe = value; }


        private void Awake()
        {
            DoesPenBloodPipe = false;
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.GetComponent<FolikulController>())
            {
                var folikul = other.gameObject.GetComponent<FolikulController>();

                if(PompController.CurrentSimulation.PressaureMultiplayer > 0)
                { 
                    folikul.Decrese();
                    _device.DoAspirate();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {

            if (other.gameObject.GetComponent<FolikulController>())
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.AspiratingIsDone_07_01_02);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (DoesPenBloodPipe)
                return;

 

        }
    }
}