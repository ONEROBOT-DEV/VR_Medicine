using TVP.UI;
using Unity.VisualScripting;
using UnityEditor;

namespace TVP.Scene06
{
    public class Scene06_03_Start : SimDomenState
    {
        public Scene06_03_Start(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            PlayerMovemetnManager.CurrentSimulation.FreezePlayer();
            StateType = StateTypeEnum.SceneIsStart_06_03;
            CurrentStep = 0;
            TotalSteps = 6;
            CurrentLevelNum = 3;
            FullDesription = "Инструкция:\r\n1.Взять со столика гель - отĸрыть паĸетиĸ, нанести на УЗИ датчиĸ\r\n2.Взять презерватив - надеть на УЗИ датчиĸ, \r\n3.Взять адаптер - приĸрепить ĸ УЗИ датчиĸу - теперь адаптер в сборе \r\n4.Введение во влагалище - \r\n5.Эмуляция УЗИ\r\n6.Исследования ";
            LevelTittle = "06.03 Подготовка";
            Description = "Взять со столика гель - отĸрыть паĸетиĸ, нанести на УЗИ датчиĸ";
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
            SimStateCanvas.CurrentSimulation.NewSceneConfig(); 
            stateMachine.TimeMachine.StartTimer(60);



            OutlineManager.CurrentSimulation.HideAll();
            OutlineManager.CurrentSimulation.ShowModel(OutlineManager.CurrentSimulation.Gel); 
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_03();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}