using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace Common.DataManager.UI
{ 
    public class StudentDataInfoController : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] Image _background;
        public void Inntit(StudentExaminationTestInfo info)
        {
            _text.text = info.GetTextSingleRow();
        }

        internal void DoGrey()
        {
            _background.color = Color.grey;
        }
    }
}