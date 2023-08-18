using Common.DataManager;
using Common.UI;
using System.Collections;
using System.Collections.Generic;
using TVP.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace TVP
{
    class LevelInfo
    {
        bool PassedAtTime = false;
        bool Passed = false;
        bool PassedWithError = false;
    }
    public class ErrorsManager : MonoSinglethon<ErrorsManager>
    {



        public void LevelIsPassedWithError(int level, string errordesription, UnityAction positiveAction, UnityAction negative, string textOferror, string positiveButtonText, string negativeButtonText)
        {
            SimulationStaticDataManager.LevelIsPassedWithError(level, errordesription);

            if (SimulationManagerDataContainer.IsTestMode)
            {
                positiveAction = () => SimSceneManager.CurrentSimulation.LoadShowResult();
                negative = () => SimSceneManager.CurrentSimulation.LoadShowResult();
                Invoke("DelayLoadScene", 2);
            }


            SimStateCanvas.CurrentSimulation.QuestionBoxController.Innit(textOferror, positiveAction, negative, positiveButtonText, negativeButtonText);

        }
        public void DelayLoadScene()
        {
            SimSceneManager.CurrentSimulation.LoadShowResult();
        }
        public void LevelIsPassedWithoutError(int level)
        {

            SimulationStaticDataManager.LevelIsPassedWithoutError(level);
        }
    }
}
