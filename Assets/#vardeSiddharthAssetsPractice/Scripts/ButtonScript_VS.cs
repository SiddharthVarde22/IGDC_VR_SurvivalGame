using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript_VS : MonoBehaviour
{
    Vector3 initialPosition;
    [SerializeField][Tooltip("Local Distance The Button Should Move On Y Axis " +
        " Positive To Move Up And negative To move down")]
    float DistanceToMove;
    [SerializeField]
    float buttonMoveSpeed = 10;

    [SerializeField]
    UnityEvent onButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
             if(onButtonPressed != null)
            {
                onButtonPressed.Invoke();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                new Vector3(initialPosition.x, initialPosition.y + DistanceToMove, initialPosition.z),
                buttonMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            transform.localPosition = initialPosition;
        }
    }
}
