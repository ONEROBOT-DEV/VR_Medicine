namespace TVP.Scene06
{
    public class Scene06_03_Done : SimDomenState
    {
        public Scene06_03_Done(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsDone_06_03;
            Description = "Подготовка след. сцены";
            LevelTittle = "06.03 Подготовка";
            FullDesription = "Инструкция:\r\n1.Взять со столика гель - отĸрыть паĸетиĸ, нанести на УЗИ датчиĸ\r\n2.Взять презерватив - надеть на УЗИ датчиĸ, \r\n3.Взять адаптер - приĸрепить ĸ УЗИ датчиĸу - теперь адаптер в сборе \r\n4.Введение во влагалище - \r\n5.Эмуляция УЗИ\r\n6.Исследования ";

            TotalSteps = 6;
            CurrentLevelNum = 3;
            CurrentStep = 6;
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            stateMachine.TimeMachine.OnStateFinihsh?.Invoke();
            IsDoneAtTime = stateMachine.TimeMachine.IsDoneAtTime;
            PrintInfo();
            NextScenLevel();
        }
        public void NextScenLevel()
        {
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsStart_06_04);
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_03();
        }
    }
}