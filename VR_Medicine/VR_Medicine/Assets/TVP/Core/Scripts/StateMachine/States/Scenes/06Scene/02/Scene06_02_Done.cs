using Common.UI;

namespace TVP.Scene06
{
    public class Scene06_02_Done : SimDomenState
    {
        public Scene06_02_Done(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsDone_06_02;
            Description = "Подготовка след. сцены!";
            LevelTittle = "06.02 Обработка";
            FullDesription = "Инструкция:\r\n1. Обработать (вымыть + антисептиĸ) руĸи - стандарт EN-1500, надеть перчатĸи стерильные неопудренные.\r\n2. Обработка промежности и влагалища";

            CurrentLevelNum = 2;
            TotalSteps = 2;
            CurrentStep = 2;
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
            stateMachine.TimeMachine.OnStateFinihsh?.Invoke();
            NextScenLevel();

            IsDoneAtTime = stateMachine.TimeMachine.IsDoneAtTime;


            OutlineManager.CurrentSimulation.HideAll(); 
        }
        public void NextScenLevel()
        {
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsStart_06_03);
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_02();
        }
    }
}