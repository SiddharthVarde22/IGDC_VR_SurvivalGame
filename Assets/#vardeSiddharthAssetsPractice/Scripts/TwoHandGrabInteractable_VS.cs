using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[CanSelectMultiple(true)]
public class TwoHandGrabInteractable_VS : XRGrabInteractable
{
    [SerializeField]
    Transform secondAttachedTransform;

    Rigidbody plankRigidBody;

    protected override void Awake()
    {
        base.Awake();
        selectMode = InteractableSelectMode.Multiple;
        plankRigidBody = GetComponent<Rigidbody>();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        
        if(interactorsSelecting.Count == 2)
        {
            processDoubleHandGrip();
        }
    }

    void processDoubleHandGrip()
    {
        Transform firstAttachment = GetAttachTransform(null);
        Transform secondAttachment = secondAttachedTransform;
        Transform firstHand = interactorsSelecting[0].transform;
        Transform secondHand = interactorsSelecting[1].transform;

        Vector3 directionBetweenHands = secondHand.position - firstHand.position;
        Quaternion targetDirection = Quaternion.LookRotation(directionBetweenHands);

        Vector3 localDirectionbetweenBaseAndFirstAttachment =
            transform.InverseTransformDirection(transform.position - firstAttachment.position);

        Vector3 targetPosition = firstHand.position + targetDirection * localDirectionbetweenBaseAndFirstAttachment;

        transform.SetPositionAndRotation(targetPosition,
            targetDirection * firstAttachment.localRotation * Quaternion.Euler(-firstHand.localEulerAngles.x, 0, 0));

    }
    protected override void Drop()
    {
        base.Drop();
        plankRigidBody.useGravity = true;
    }
}
