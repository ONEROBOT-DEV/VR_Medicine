using Common;
using Common.DataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    public class UIElementsVisibilityController : MonoBehaviour
    {
        [SerializeField] List<GameObject> _testModeUIElements = new List<GameObject>();
        [SerializeField] List<GameObject> _lightTraingModeUIElements = new List<GameObject>();
        [SerializeField] List<GameObject> _hardTraingModeUIElements = new List<GameObject>();

        public void UpdateVisibility(GameObject element, bool state)
        {
            element.SetActive(state);  
        }


        private void Start() => UpdateCanvas();

        [ContextMenu("Update visibility")]
        public  void UpdateCanvas()
        {
            if(SimulationManagerDataContainer.IsTestMode)
                foreach (GameObject element in _testModeUIElements) { UpdateVisibility(element, !SimulationManagerDataContainer.IsTestMode); }

            if (SimulationManagerDataContainer.IsLightTrainingMode)
                foreach (GameObject element in _lightTraingModeUIElements) { UpdateVisibility(element, !SimulationManagerDataContainer.IsLightTrainingMode); }

            if (SimulationManagerDataContainer.IsHardTrainingMode)
                foreach (GameObject element in _hardTraingModeUIElements) { UpdateVisibility(element, !SimulationManagerDataContainer.IsHardTrainingMode); }
        }
    }
}