using Common.UI;

namespace TVP.Scene06
{
    public class Scene06_01_05_PompPedalCheck : SimDomenState
    {


        public Scene06_01_05_PompPedalCheck(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.PompPedalIsWorking_06_01_05;
            TotalSteps = 6;
            CurrentStep = 5;
            CurrentLevelNum = 1;
            FullDesription = "Инструкция:\r\n1.Включить УЗИ аппарат с монитором\r\n2.Подготовить аппарат УЗИ\r\n3.Подготовить помпу УЗИ\r\n4.Проверить уровень разрежения.\r\n5.Проверить работу ножной педали\r\n6.Проверить работу помпы";

            Description = "Проверить помпу вкл / выкл"; 
            LevelTittle = "06.01 Подготовка УЗИ аппарата";
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
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