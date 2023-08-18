using Autohand.Demo;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TVP;
using UnityEngine;
using UnityEngine.XR;


namespace TVP
{
    public class InputHandlerOfDream : MonoSinglethon<InputHandlerOfDream>
    {
        //public InputDevice _targetDevice;
        //public List<InputDevice> devices;
        public Vector2 Primary2DAxis;
        public Vector2 Secondary2DAxis;
        public bool Triger;
        public float TrigerValue;


        [SerializeField] private XRHandControllerLink _left;
        [SerializeField] private XRHandControllerLink _right; 
        
        // Update is called once per frame
        void Update()
        {

            Primary2DAxis = _left.GetAxis2D(Common2DAxis.primaryAxis);
            Secondary2DAxis = _right.GetAxis2D(Common2DAxis.primaryAxis);
            TrigerValue = _right.GetAxis(CommonAxis.trigger);
            Triger = TrigerValue != 0;
             
        }

        // hard code shit ->>

        /*
        IEnumerator GetDevices()
        {
            InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
            devices = new List<InputDevice>();
            while (true)
            {
                yield return new WaitForSeconds(1);
                //InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Right, devices);
                InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
                names = devices.Select(d => d.characteristics.ToString()).ToList();
            }

        }
        private void XRRareInput()
        {
            if (devices.Count == 0)
                return;

            foreach (var devcie in devices)
            {
                devcie.TryGetFeatureValue(CommonUsages.primary2DAxis, out Primary2DAxis);
                devcie.TryGetFeatureValue(CommonUsages.secondary2DAxis, out Secondary2DAxis);
                devcie.TryGetFeatureValue(CommonUsages.triggerButton, out Triger);
                devcie.TryGetFeatureValue(CommonUsages.trigger, out TrigerValue);

            }
        }
        */

    }
}