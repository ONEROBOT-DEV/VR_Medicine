﻿using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_03_05_UziEmulation : SimDomenState
    {
        public Scene06_03_05_UziEmulation(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.UziEmulation_06_03_05;
            CurrentStep = 5;
            TotalSteps = 6;
            CurrentLevelNum = 3;
            LevelTittle = "06.03 Подготовка";
            FullDesription = "Инструкция:\r\n1.Взять со столика гель - отĸрыть паĸетиĸ, нанести на УЗИ датчиĸ\r\n2.Взять презерватив - надеть на УЗИ датчиĸ, \r\n3.Взять адаптер - приĸрепить ĸ УЗИ датчиĸу - теперь адаптер в сборе \r\n4.Введение во влагалище - \r\n5.Эмуляция УЗИ\r\n6.Исследования ";

            Description = "Исследование";
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
            stateMachine.ResetToScene06_03();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}