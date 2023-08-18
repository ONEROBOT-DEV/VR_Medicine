using Common.DataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Common.UI
{
    public class SelectorSceneManager : MonoBehaviour
    {
        [SerializeField] GameObject _icsiMenu;
        [SerializeField] GameObject _tvpMenu;

        private void Start()
        {
            _icsiMenu.SetActive(false);
            _tvpMenu.SetActive(false);
        }

        public void ICSIBtnClicked()
        {
            _tvpMenu.SetActive(false);
            var nextState = !_icsiMenu.activeInHierarchy && !_tvpMenu.activeInHierarchy;
            _icsiMenu.SetActive(nextState);
        }
        public void TVPBtnClicked()
        {
            _icsiMenu.SetActive(false);
            var nextState = !_icsiMenu.activeInHierarchy && !_tvpMenu.activeInHierarchy;
            _tvpMenu.SetActive(nextState);
        }


        public void TVPSceneSelected(int mode)
        {
            ModeInnit(mode); 
            SimSceneManager.CurrentSimulation.LoadTVP();
        }

        private static void ModeInnit(int mode)
        {
            switch (mode)
            {

                case 1:
                    SimulationManagerDataContainer.IsTestMode = true;
                    SimulationManagerDataContainer.IsLightTrainingMode = false;
                    SimulationManagerDataContainer.IsHardTrainingMode = false;
                    break;
                case 2:
                    SimulationManagerDataContainer.IsTestMode = false;
                    SimulationManagerDataContainer.IsLightTrainingMode = true;
                    SimulationManagerDataContainer.IsHardTrainingMode = false;
                    break;
                case 3:
                    SimulationManagerDataContainer.IsTestMode = false;
                    SimulationManagerDataContainer.IsLightTrainingMode = false;
                    SimulationManagerDataContainer.IsHardTrainingMode = true;
                    break;
            }
        }

        public void ICSIceneSelected(int mode)
        {
            ModeInnit(mode);
            SimSceneManager.CurrentSimulation.LoadICSI();
        }
    }
}