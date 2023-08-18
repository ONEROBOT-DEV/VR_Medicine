using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TVP.MockSim
{
    public class FolikulManager : MonoBehaviour
    {
        [SerializeField] FolikulController[] _folukils;

        public bool SurgeryDone;

        // Start is called before the first frame update
        void Start()
        {
            _folukils = GetComponentsInChildren<FolikulController>();


            foreach(FolikulController folikul in _folukils)
            {
                folikul.OnAspirating.AddListener(UpdateManager);
            }

        }

        private void  UpdateManager()
        {

            SurgeryDone = _folukils.Where(f => !f.IsDone).Count() <= 0;

            print("sfsdfsadfsadfsdasdasdasdasdsa");
            if(SurgeryDone)
            { 
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SurgeryIsDone_07_01_03);
                SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsDone_07_01);
            }

        }
    }
}