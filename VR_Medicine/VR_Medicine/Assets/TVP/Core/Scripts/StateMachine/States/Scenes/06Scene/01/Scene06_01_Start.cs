using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_01_Start : SimDomenState
    {


        public Scene06_01_Start(SimDomenStateMachine stateMachine) : base(stateMachine)
        {

            PlayerMovemetnManager.CurrentSimulation.FreezePlayer();

            StateType = StateTypeEnum.SceneIsStart_06_01;
            FullDesription = "Инструкция:\r\n1.Включить УЗИ аппарат с монитором\r\n2.Подготовить аппарат УЗИ\r\n3.Подготовить помпу УЗИ\r\n4.Проверить уровень разрежения.\r\n5.Проверить работу ножной педали\r\n6.Проверить работу помпы";

            Description = "Настроить изображение на узи мониторе";
            LevelTittle = "06.01 Подготовка УЗИ аппарата";
            TotalSteps = 6;
            CurrentStep = 0;
            CurrentLevelNum = 1;

            OutlineManager.CurrentSimulation.HideAll();
            OutlineManager.CurrentSimulation.ShowModel(OutlineManager.CurrentSimulation.UziDEviceTable); 

        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
            SimStateCanvas.CurrentSimulation.NewSceneConfig(); 

            stateMachine.TimeMachine.StartTimer(120);
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {  
        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_01();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}