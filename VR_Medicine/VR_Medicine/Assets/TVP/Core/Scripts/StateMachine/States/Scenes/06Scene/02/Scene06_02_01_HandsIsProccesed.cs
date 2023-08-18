namespace TVP.Scene06
{
    public class Scene06_02_01_HandsIsProccesed : SimDomenState
    {
        public Scene06_02_01_HandsIsProccesed(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.HandsIsProccesed_06_02_01;
            Description = "Обработать промежность влагалища";
            LevelTittle = "06.02 Обработка";
            FullDesription = "Инструкция:\r\n1. Обработать (вымыть + антисептиĸ) руĸи - стандарт EN-1500, надеть перчатĸи стерильные неопудренные.\r\n2. Обработка промежности и влагалища";

            TotalSteps = 2;
            CurrentStep = 1;
            CurrentLevelNum = 2;
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();

            OutlineManager.CurrentSimulation.HideAll(); 
            OutlineManager.CurrentSimulation.ShowModel(OutlineManager.CurrentSimulation.Korzang);
            OutlineManager.CurrentSimulation.ShowModel(OutlineManager.CurrentSimulation.Rag);

        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_02();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}