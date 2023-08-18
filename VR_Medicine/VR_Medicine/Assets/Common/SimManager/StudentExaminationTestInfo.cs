using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Common.DataManager
{
    [System.Serializable]
    public class StudentExaminationTestInfo
    {

        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _totalSteps;
        [SerializeField] private int _errors;
        [SerializeField] private string _errorsDesription;
        [SerializeField] private int _passedSteps;
        [SerializeField] private int _id;
        [SerializeField] private float _time;
        [SerializeField] private string _date;

        public StudentExaminationTestInfo(int ID)
        {
            _id = ID;
            _name = "";
            _description = "";
            _errorsDesription = "";
            _totalSteps = 0;
            _errors = 0;
            _passedSteps = 0;
            _time = 0;
            _date = System.DateTime.Today.ToString("d");
        }

        public StudentExaminationTestInfo(int ID, string name, string description, int totalSteps, int errors, int passedSteps, float time, string date)
        {
            _id = ID;
            _name = name;
            _description = description;
            _totalSteps = totalSteps;
            _errors = errors;
            _passedSteps = passedSteps;
            _time = time;
            _date = date;
        }

        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int TotalSteps { get => _totalSteps; set => _totalSteps = value; }
        public int Errors { get => _errors; set => _errors = value; }
        public int CompletedSteps { get => _passedSteps; set => _passedSteps = value; }
        public int ID { get => _id; set => _id = value; }
        public float Time { get => _time; set => _time = value; }
        public string Date { get => _date; set => _date = value; }
        public string ErrorsDesription { get => _errorsDesription; set => _errorsDesription = value; }

        internal string GetText()
        {
            TimeSpan t = TimeSpan.FromSeconds(Time);

            return
                "Режим " + " " + Description
                + "\n" + $"Имя <color=orange>{Name}</color>"
                + "\n" + $"Пройденые этапы <color=orange> {CompletedSteps}/ {TotalSteps}</color>"
                + "\n" + $"Ошибки <color=orange>{Errors}</color>"
                + "\n" + $"Время <color=orange> {t.Minutes}:{t.Seconds}</color>"
                + "\n" + $"Дата <color=orange>{Date}</color>";
        }
        internal string GetTextSingleRow()
        {
            return
                ID + ". " + Description
                + $" <color=orange>{Name}</color>"
                + $" пройденые этапы <color=orange> {CompletedSteps} / {TotalSteps}</color>"
                + $" ошибки <color=orange>{Errors} / {TotalSteps}</color>"
                + $" время <color=orange>{Time} </color>"
                + $" дата  <color=orange>{Date} </color>";
        }
    }
}