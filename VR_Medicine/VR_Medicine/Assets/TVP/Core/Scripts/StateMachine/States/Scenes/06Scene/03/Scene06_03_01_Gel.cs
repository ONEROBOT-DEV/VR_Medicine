using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_03_01_Gel : SimDomenState
    {
        public Scene06_03_01_Gel(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.GelIsApplied_06_03_01;
            CurrentStep = 1;
            TotalSteps = 6;
            CurrentLevelNum = 3;
            FullDesription = "Инструкция:\r\n1.Взять со столика гель - отĸрыть паĸетиĸ, нанести на УЗИ датчиĸ\r\n2.Взять презерватив - надеть на УЗИ датчиĸ, \r\n3.Взять адаптер - приĸрепить ĸ УЗИ датчиĸу - теперь адаптер в сборе \r\n4.Введение во влагалище - \r\n5.Эмуляция УЗИ\r\n6.Исследования ";

            LevelTittle = "06.03 Подготовка";
            Description = "Взять презерватив надеть на узи датчик"; 
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();

            OutlineManager.CurrentSimulation.HideAll(); 
            OutlineManager.CurrentSimulation.ShowModel(OutlineManager.CurrentSimulation.Prez); 
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