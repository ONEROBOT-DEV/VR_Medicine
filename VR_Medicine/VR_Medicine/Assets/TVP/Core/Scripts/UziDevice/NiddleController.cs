using Autohand;
using DG.Tweening;
using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using TVP.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace TVP
{
    public enum NiddleType
    {
        G17,
        G18,
        G19,
        G20,
        G21,
        G22
    }


    public class NiddleController : MonoBehaviour
    {
        [SerializeField] private bool _isMounted;
        [SerializeField] Transform _shape;
        [SerializeField] Transform _virtualShape;
        [SerializeField] private NiddleType _type;
        private BiopsyAdapterController _adapter;
        [SerializeField] private bool canDispanseOrAspirate;
        [SerializeField] GameObject _otsOkayPressaure;
        Grabbable _thisGrababable;
        public Dictionary<NiddleType, int> PressuareRules = new Dictionary<NiddleType, int>();


        public bool IsMounted
        {
            get => _isMounted;
            set
            {
                _isMounted = value;
                if (!_isMounted)
                {
                    if (_adapter != null)
                        _adapter = null;
                }
            }
        }

        public bool CanDispanseOrAspirate { get => canDispanseOrAspirate; set => canDispanseOrAspirate = value; }
        public bool IsOkayNiddle { get; private set; }

        public FluidBase Fluid;



        private void Awake()
        {
            PressuareRules.Add(NiddleType.G17, 150);
            PressuareRules.Add(NiddleType.G18, 170);
            PressuareRules.Add(NiddleType.G19, 190);
            PressuareRules.Add(NiddleType.G20, 90);
            PressuareRules.Add(NiddleType.G21, 90);
            PressuareRules.Add(NiddleType.G22, 90);
            PompController.CurrentSimulation.OnPressuareChanged.AddListener(CheckCorrectPressuareOnDevice);
            _thisGrababable = GetComponent<Grabbable>();
        }

        public void InnitNiddle(string niddle)
        {
            Enum.TryParse(niddle, out _type);
            CheckCorrectPressuareOnDevice();
            SizeInnit();
        }

        private void SizeInnit()
        {
            Vector3 size;
            float virtualNidlleSize = 0.075f;
            switch (_type)
            {
                case NiddleType.G17:
                    size = Vector3.one;
                    virtualNidlleSize = 0.055f;
                    break;

                case NiddleType.G18:
                    size = Vector3.one * .85f;
                    virtualNidlleSize = 0.045f;
                    break;

                case NiddleType.G19:
                    size = Vector3.one * .70f;
                    virtualNidlleSize = 0.035f;
                    break;

                case NiddleType.G20:
                    size = Vector3.one * .55f;
                    virtualNidlleSize = 0.025f;
                    break;

                case NiddleType.G21:
                    size = Vector3.one * .40f;
                    virtualNidlleSize = 0.017f;
                    break;

                case NiddleType.G22:
                    size = Vector3.one * .25f;
                    virtualNidlleSize = 0.01f;
                    break;

                default:
                    size = Vector3.one;
                    virtualNidlleSize = 0.075f;
                    break;

            }

            _shape.DOScaleZ(size.z, 1);
            _shape.DOScaleX(size.x, 1);
            _virtualShape.DOScaleX(virtualNidlleSize, 1);
        }

        public void CheckCorrectPressuareOnDevice(PompController device)
        {

            IsOkayNiddle = device.Pressuare >= PressuareRules[_type];

            if (IsOkayNiddle)
            {
                _otsOkayPressaure.GetComponent<Renderer>().material.color = Color.green;
                ErrorsManager
                .CurrentSimulation
                    .LevelIsPassedWithoutError(SimDomenStateMachine.CurrentSimulation.CurrentStateNum);
            }
            else
            {

                var conditionToShowMag = _isMounted && device.IsActive;


                if (conditionToShowMag)
                    WrongNiddleInfo(device); // hardcode

                _otsOkayPressaure.GetComponent<Renderer>().material.color = Color.red;
            }
        }

        private void WrongNiddleInfo(PompController device)
        {


            int correctPressaure = PressuareRules[_type];

            string msg = $"Для данной иглы {_type.ToString()} нужно выставить {correctPressaure} мм. рт. ст.";


            UnityAction SetCoorectPressuare = () => { device.Pressuare = correctPressaure; };

            var nidleSelectLEvelNum = 4;

            ErrorsManager
                .CurrentSimulation
                    .LevelIsPassedWithError(nidleSelectLEvelNum, "Вы установили неправльное давление для иглы", SetCoorectPressuare, null, msg, "Выставить давление", "Продолжить");


        }

        public void CheckCorrectPressuareOnDevice() //  hardcode shit
        {

            IsOkayNiddle = PompController.CurrentSimulation.Pressuare >= PressuareRules[_type];

            if (IsOkayNiddle)
            {
                _otsOkayPressaure.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {

                var conditionToShowMag = PompController.CurrentSimulation.IsActive;


                if (conditionToShowMag)
                    WrongNiddleInfo(PompController.CurrentSimulation); // hardcode

                _otsOkayPressaure.GetComponent<Renderer>().material.color = Color.red;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            IsMounted = false;
        }

        public void MountOnDevice()
        {
            IsMounted = true;
        }


        private void OnTriggerEnter(Collider other)
        {
            if (IsMounted)
            {
                if (other.gameObject.GetComponent<FluidBase>())
                {
                    CanDispanseOrAspirate = true;
                    Fluid = other.gameObject.GetComponent<FluidBase>();

                    TVPTutorialAdapter.CurrentSimulation.ShowDispenceAspirateTutorial();

                    if (other.gameObject.GetComponent<FluidBase>().IsWoman)
                        SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SurgeryIsStart_07_01_01);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (IsMounted)
            {
                if (other.gameObject.GetComponent<FluidBase>())
                {
                    CanDispanseOrAspirate = false;
                    Fluid = null;
                }
            }
        }


        public void Aspirate()
        {

            _adapter.OnAspirate.Invoke(1);
        }

        public void Dispanse()
        {
            _adapter.OnAspirate.Invoke(1);
        }

        internal void Innit(BiopsyAdapterController biopsyAdapterController)
        {
            _adapter = biopsyAdapterController;
        }

        internal void Disapose(BiopsyAdapterController biopsyAdapterController)
        {
            _adapter = null;
        }
    }
}