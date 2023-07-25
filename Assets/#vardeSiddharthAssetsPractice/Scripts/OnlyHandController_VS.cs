using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public enum ControllerHandType
{
    Right ,
    Left
}

public class OnlyHandController_VS : MonoBehaviour
{
    [SerializeField]
    ControllerHandType handType;

    Animator handAnimator;
    InputDevice inputDevice;
    float indexValue, threeFingersValue, thumbValue , thumbMovespeed = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
        handAnimator = GetComponent<Animator>();
        inputDevice = GetInputDevice();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateTheHands();
    }

    InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics inputDeviceCharacteristics = InputDeviceCharacteristics.HeldInHand
            | InputDeviceCharacteristics.Controller;

        if(handType == ControllerHandType.Left)
        {
            inputDeviceCharacteristics = inputDeviceCharacteristics | InputDeviceCharacteristics.Left;
        }
        else
        {
            inputDeviceCharacteristics = inputDeviceCharacteristics | InputDeviceCharacteristics.Right;
        }

        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, inputDevices);

        return inputDevices[0];
    }

    void AnimateTheHands()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out indexValue);
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out threeFingersValue);

        inputDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out bool primaryTouch);
        inputDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out bool secondaryTouch);
        inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool primary2DAxisTouched);

        if (primaryTouch || secondaryTouch || primary2DAxisTouched)
        {
            thumbValue += thumbMovespeed;
            if(thumbValue >= 1)
            {
                thumbValue = 1;
            }
        }
        else
        {
            thumbValue -= thumbMovespeed;
            if(thumbValue <= 0)
            {
                thumbValue = 0;
            }
        }

        Mathf.Clamp(thumbValue, 0, 1);

        handAnimator.SetFloat("Index", indexValue);
        handAnimator.SetFloat("ThreeFingers", threeFingersValue);
        handAnimator.SetFloat("Thumb", thumbValue);
    }
}
