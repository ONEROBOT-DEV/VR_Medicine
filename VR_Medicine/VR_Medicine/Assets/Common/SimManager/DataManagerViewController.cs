using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Common.DataManager
{
    public class DataManagerViewController : MonoBehaviour
    {
        #region DEBUG
        [SerializeField] private List<StudentExaminationTestInfo> _list;
        [Sirenix.OdinInspector.Button]
        public void RefreshStates()
        {
            _levels = Common.DataManager.SimulationStaticDataManager.LEVEL_PASS_STATE.Values.ToList();
        }
        [Header("Progress")]
        [SerializeField] List<LevelProgressInfo> _levels = new List<LevelProgressInfo>();
        #endregion DEBUG
        [SerializeField]
        [Header("Events:")]
        public UnityEvent OnStart = new UnityEvent();
        public UnityEvent OnUpdate = new UnityEvent();
        public UnityEvent OnFinish = new UnityEvent();

        // Start is called before the first frame update
        void Start() => OnStart?.Invoke();
        void Update() => OnUpdate?.Invoke();
        void OnDestroy() => OnFinish?.Invoke();


        public void DisplayResult(TextMeshProUGUI _text)
        {
            _text.text = SimulationStaticDataManager.GetCurrentStudentFromPlayerPrefs()?.GetText();
        }

        public void DisplayErrors(TextMeshProUGUI _text)
        {
            _text.text = SimulationStaticDataManager.GetCurrentStudentFromPlayerPrefs().ErrorsDesription;
        }

        [ContextMenu("ReadData")]
        public void ReadData()
        {
            SimulationStaticDataManager.GetListOfStudentsFromPlayerPrefs(100);
            _list = SimulationStaticDataManager.LIST_OF_STUDENTS;
        }
        [ContextMenu("Save Random")]
        public void SaveData()
        {
            SimulationStaticDataManager.SaveDataToPlayerPrefs();
        }

        public void SaveNickName(TMPro.TMP_InputField _inputField)
        {
            var nickName = _inputField.text;
            SimulationStaticDataManager.PlayerNickNameInnit(nickName);
        }
        [ContextMenu("Clear data")]
        public void ClearDataBase() => PlayerPrefs.DeleteAll();
        public void SetTVPMode()
        {
            SimulationStaticDataManager.CurrentStudet.Description = "TVP";
            SimulationStaticDataManager.TotalStepsInnit(7);
        }
        public void SetICSIMode()
        {
            SimulationStaticDataManager.CurrentStudet.Description = "ICSI";
            SimulationStaticDataManager.TotalStepsInnit(12);
        }
        //  

    }
}