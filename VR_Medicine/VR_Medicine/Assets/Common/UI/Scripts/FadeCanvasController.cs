using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Common.UI
{
    public class FadeCanvasController : MonoBehaviour
    {
        [SerializeField] Color _black;
        [SerializeField] Color _transperent;
        [SerializeField] Image _image; 

        private void Start()
        {
            FadeToTransperent();
            SimSceneManager.CurrentSimulation.OnSceneChanged += FadeToBlack;
            _image.gameObject.SetActive(true);
        }

        public void FadeToBlack()
        {
            _image.gameObject.SetActive(true);
            _image.DOColor(_black, 3);
        }

        public void FadeToTransperent() 
        {
            _image.DOColor(_transperent, 3);
        }

        private void OnDestroy()
        { 
            //SimSceneManager.CurrentSimulation.OnSceneChanged -= FadeToBlack;
        }
    }
}