using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaceThePlank_VS : MonoBehaviour
{
    XRSocketInteractor socketInteractor;
    Transform plankTransform;
    public void PositionThePlank()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();

        plankTransform = socketInteractor.interactablesSelected[0].transform;
        plankTransform.position = transform.position;
        plankTransform.rotation = transform.rotation;
    }

    public void OnDeselectedFromSocket()
    {
        if (plankTransform != null)
        {
            Rigidbody plankRigidBody = plankTransform.GetComponent<Rigidbody>();
            plankRigidBody.isKinematic = false;
            plankRigidBody.useGravity = true;
        }
    }
}
