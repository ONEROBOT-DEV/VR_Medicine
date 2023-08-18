using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.DataManager
{
    public static class SimulationManagerDataContainer  
    {
        public static bool IsTestMode = false ;
        public static bool IsLightTrainingMode = true;
        public static bool IsHardTrainingMode = false;

        public static bool TVPTest = false;
        public static bool TVPNegativeTest = false;

    }
}