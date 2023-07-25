using UnityEngine;


[System.Serializable]
class MapTransforms
{
    public Transform vrTarget, ikTarget;

    public Vector3 trackingPositionOffset, trackingRotationOffset;

    public void VrMapping()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}


public class AvatarAnimationController_VS : MonoBehaviour
{
    [SerializeField]
    MapTransforms head, LeftHand, rightHand;

    [SerializeField]
    float turnSmoothness;
    [SerializeField]
    Transform ikHead;
    [SerializeField]
    Vector3 headBodyOffset;

    private void LateUpdate()
    {
        transform.position = ikHead.position + headBodyOffset;
        transform.forward = Vector3.Lerp(ikHead.forward, 
            Vector3.ProjectOnPlane(ikHead.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.VrMapping();
        LeftHand.VrMapping();
        rightHand.VrMapping();

    }
}
