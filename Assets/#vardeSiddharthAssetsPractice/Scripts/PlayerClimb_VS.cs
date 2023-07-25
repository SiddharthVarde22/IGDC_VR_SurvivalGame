using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerClimb_VS : MonoBehaviour
{
    [SerializeField]
    CharacterController playerCharacterController;

    public static XRController currentHandController;
    private DeviceBasedContinuousMoveProvider moveProvider;
    DeviceBasedContinuousTurnProvider turnProvider;

    [SerializeField]
    float GravityToAdd = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerCharacterController = GetComponentInChildren<CharacterController>();
        moveProvider = GetComponent<DeviceBasedContinuousMoveProvider>();
        turnProvider = GetComponent<DeviceBasedContinuousTurnProvider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(currentHandController)
        {
            moveProvider.enabled = false;
            turnProvider.enabled = false;
            
            Climb();
        }
        else
        {
            moveProvider.enabled = true;
            turnProvider.enabled = true;
            if (!playerCharacterController.isGrounded)
            {
                playerCharacterController.Move((transform.up * -1) * GravityToAdd * Time.fixedDeltaTime);
            }
        }


    }

    void Climb()
    {
        InputDevices.GetDeviceAtXRNode(currentHandController.controllerNode).
            TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        playerCharacterController.Move(transform.rotation * (-1 * velocity) * Time.fixedDeltaTime);
    }
}
