using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Common.DataManager
{
    [System.Serializable]
    public class LevelProgressInfo
    {
        public int ID;
        public int level;
        public int totalLevels;
        public bool started;
        public bool passed;
        public bool failed;
        public float time;
        internal string errorDesription;

        public LevelProgressInfo(int level, int totalLevels, bool passed)
        {
            this.level = level;
            this.totalLevels = totalLevels;
            this.passed = passed;
            time = 0;
        }
    }


    public static class SimulationStaticDataManager
    {

        #region PLAYER_PREFS_KEYS
        public static string NICKNAME_KEY = "NICKNAME";
        public static string TOTALSTEPS_KEY = "TOTAL_STEPS";
        public static string COMPLETESTEPS_KEY = "COMPLETESTEPS";
        public static string ERRORS_KEY = "ERRORS";
        public static string LAST_ID_KEY = "LAST_ID";
        public static string DESCRIPTION_KEY = "DESCRIPTION";
        public static string CURRENT_DATE = "CURRENT_DATE";
        #endregion PLAYER_PREFS_KEYS

        public static List<StudentExaminationTestInfo> LIST_OF_STUDENTS = new List<StudentExaminationTestInfo>();

        public static Dictionary<int, LevelProgressInfo> LEVEL_PASS_STATE;


        // nick -> des -> errors -> total -> complete -> time -> date

        public static StudentExaminationTestInfo CurrentStudet;

        static SimulationStaticDataManager()
        {
            if (LIST_OF_STUDENTS.Count == 0)
                GetListOfStudentsFromPlayerPrefs(100);
        }


        public static void PlayerNickNameInnit(string nickName)
        {

            GetListOfStudentsFromPlayerPrefs(100);

            int currentID = 0;

            if (LIST_OF_STUDENTS.Select(s => s.ID).Count() > 0)
            {
                currentID = LIST_OF_STUDENTS.Select(s => s.ID).Max();
            }
            currentID++;
            CurrentStudet = new StudentExaminationTestInfo(currentID);
            nickName = nickName.Replace("-", "");
            nickName = nickName == "" ? "NO-NAME" : nickName;
            CurrentStudet.Name = nickName;
            Debug.Log(CurrentStudet.Name + " registated");
        }
        public static void TotalStepsInnit(int totalStepsOfSimulation)
        {
            LEVEL_PASS_STATE = new Dictionary<int, LevelProgressInfo>();
            CurrentStudet.TotalSteps = totalStepsOfSimulation;
            CurrentStudet.Errors = 0;

            for (int x = 1; x < totalStepsOfSimulation + 1; x++)
            {
                LEVEL_PASS_STATE[x] = new LevelProgressInfo(x, totalStepsOfSimulation, false);

                LEVEL_PASS_STATE[x].passed = false;
                LEVEL_PASS_STATE[x].started = false;
                LEVEL_PASS_STATE[x].failed = false;

            }
        }
        public static void LevelIsPassedWithError(int level, string description = "")
        {
            if (LEVEL_PASS_STATE == null)
                return;
            Log("Уровень" + level + " пройден с ошибокой");
            if (LEVEL_PASS_STATE.TryGetValue(level, out LevelProgressInfo data))
            {
                data.failed = true;
                data.errorDesription = description;
                int error = LEVEL_PASS_STATE.Select(L => L.Value).Where(L => L.failed == true).Count();
                CurrentStudet.Errors = error;

                if (description != "")
                    CurrentStudet.ErrorsDesription += CurrentStudet.Errors + "." + description + "\n";
            }
        }

        public static void LevelIsPassedWithoutError(int level)
        {
            if (LEVEL_PASS_STATE == null)
                return;

            Log("Уровень" + level + " пройден без ошибок");
            if (LEVEL_PASS_STATE.TryGetValue(level, out LevelProgressInfo data))
            {
                // re-passing logic
                data.failed = false;
                int error = LEVEL_PASS_STATE.Select(L => L.Value).Where(L => L.failed == true).Count();
                CurrentStudet.Errors = error;

                data.passed = true;
                int passed = LEVEL_PASS_STATE.Select(L => L.Value).Where(L => L.passed == true).Count();
                passed = Mathf.Clamp(passed, 0, CurrentStudet.TotalSteps);
                CurrentStudet.CompletedSteps = passed;
            }
        }
        public static void CheckLevelIsPassedWithoutError(int level)
        {
            if (LEVEL_PASS_STATE == null)
                return;

            Log("Уровень" + level + " пройден без ошибок");
            if (LEVEL_PASS_STATE.TryGetValue(level, out LevelProgressInfo data) && !data.passed)
            {
                LevelIsPassedWithoutError(level);
            }
        }


        public static StudentExaminationTestInfo GetCurrentStudentFromPlayerPrefs()
        {
            return CurrentStudet;
        }

        public static void GetListOfStudentsFromPlayerPrefs(int amount)
        {

            Log("Читаю данные из Player Prefs");

            // nick -> des -> errors -> total -> complete -> time -> date
            string record = "NO_DATA";
            LIST_OF_STUDENTS = new List<StudentExaminationTestInfo>();
            LIST_OF_STUDENTS.Clear();
            for (int x = amount - 1; x > 0; x--)
            {
                record = PlayerPrefs.GetString(x.ToString());

                if (record.Split('_').Length < 7)
                    continue;

                var ID = x;

                var nick = record.Split('_')[0];

                var description = record.Split('_')[1];

                int errors = 0;
                Int32.TryParse(record.Split('_')[2], out errors);

                int total = 0;
                Int32.TryParse(record.Split('_')[3], out total);

                int complete = 0;
                Int32.TryParse(record.Split('_')[4], out complete);

                float time = 0;
                Single.TryParse(record.Split('_')[5], out time);

                string date = record.Split('_')[6];

                var newItem = new StudentExaminationTestInfo(ID, nick, description, total, errors, complete, time, date);

                LIST_OF_STUDENTS.Add(newItem);
            }
        }

        public static void FinishLevel(int level)
        {
            if (LEVEL_PASS_STATE == null)
                return;
            Log("Уровень" + level + " закончен");
            if (LEVEL_PASS_STATE.TryGetValue(level, out LevelProgressInfo data))
            {
                data.passed = true;
                int passed = LEVEL_PASS_STATE.Select(L => L.Value).Where(L => L.passed == true).Count();
                passed = Mathf.Clamp(passed, 0, CurrentStudet.TotalSteps);
                CurrentStudet.CompletedSteps = passed;
            }


        }
        public static void StartLevel(int level)
        {
            if (LEVEL_PASS_STATE == null)
                return;

            if (LEVEL_PASS_STATE.TryGetValue(level, out LevelProgressInfo data))
            {
                data.started = true;
            }
        }

        public static void SimulationIsFinished(float time)
        {
            CurrentStudet.Time = time;
            // nick -> des -> errors -> total -> complete -> time -> date
            LIST_OF_STUDENTS.Insert(0, CurrentStudet);
            SaveDataToPlayerPrefs();
        }

        public static void SaveDataToPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Log("Созраняю данные в PlayerPrefs");
            foreach (StudentExaminationTestInfo data in LIST_OF_STUDENTS)
            {
                string record = $"{data.Name}_{data.Description}_{data.Errors}_{data.TotalSteps}_{data.CompletedSteps}_{data.Time}_{data.Date}";
                PlayerPrefs.SetString(data.ID.ToString(), record);
            }
        }

        public static void Log(string msg)
        {
            UnityEngine.Debug.Log(msg);
        }
    }
}