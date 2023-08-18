using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Common.DataManager.UI
{
    public class StudentListManagerUI : MonoBehaviour
    {
        [SerializeField] List<StudentExaminationTestInfo> _listOfStudents = new List<StudentExaminationTestInfo>();

        [SerializeField] List<StudentDataInfoController> _studentDataRows = new List<StudentDataInfoController>();
        [SerializeField] GameObject _studentDataRootObject;
        [SerializeField] int _studentNums;

        public int StudentNums { get => _studentNums; set => _studentNums = value; }
        public List<StudentExaminationTestInfo> ListOfStudents { get => _listOfStudents; set => _listOfStudents = value; }
        public List<StudentDataInfoController> StudentDataRows { get => _studentDataRows; set => _studentDataRows = value; }
        public GameObject StudentDataRootObject { get => _studentDataRootObject; set => _studentDataRootObject = value; }

        // Start is called before the first frame update
        void Start()
        { 
            StudentNums = StudentDataRows.Count();
            ListOfStudents = DataManager.SimulationStaticDataManager.LIST_OF_STUDENTS;
            InnitRows();
        }

        [ContextMenu("Innit")]
        public void InnitRows()
        {
            if (ListOfStudents.Count > 0)
            {
                for (int i = 0; i < StudentNums && i <  ListOfStudents.Count(); i++)
                { 
                    StudentDataRows[i].Inntit(ListOfStudents[i]);
                    if(i%2==0)
                    {
                        StudentDataRows[i].DoGrey();
                    }
                }
            }
            else
            {
                Debug.Log("Ошибка чтения баззы данных");
            }


        }
    }
}