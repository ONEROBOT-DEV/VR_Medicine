﻿using Common.UI;

namespace TVP.Scene06
{
    public class Scene06_01_02_UziDisplaySettedUp : SimDomenState
    {


        public Scene06_01_02_UziDisplaySettedUp(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.UziDispalySettedUp_06_01_02;
            TotalSteps = 6;
            CurrentStep = 2;
            CurrentLevelNum = 1;
            FullDesription = "Инструкция:\r\n1.Включить УЗИ аппарат с монитором\r\n2.Подготовить аппарат УЗИ\r\n3.Подготовить помпу УЗИ\r\n4.Проверить уровень разрежения.\r\n5.Проверить работу ножной педали\r\n6.Проверить работу помпы";

            Description = "Подготовить помпу";
            LevelTittle = "06.01 Подготовка УЗИ аппарата";
             
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
            OutlineManager.CurrentSimulation.HideAll();
            OutlineManager.CurrentSimulation.ShowModel(OutlineManager.CurrentSimulation.PompDeviece);
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