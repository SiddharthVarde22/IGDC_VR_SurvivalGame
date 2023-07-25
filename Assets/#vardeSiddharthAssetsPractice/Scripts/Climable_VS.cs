using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climable_VS : XRBaseInteractable
{
    
    protected override void OnSelectEntered(XRBaseInteractor interactor)
    {
        base.OnSelectEntered(interactor);

        PlayerClimb_VS.currentHandController = interactor.GetComponent<XRController>();
        
    }

    protected override void OnSelectExited(XRBaseInteractor interactor)
    {
        base.OnSelectExited(interactor);

        if(PlayerClimb_VS.currentHandController && (PlayerClimb_VS.currentHandController.name == interactor.name))
        {
            PlayerClimb_VS.currentHandController = null;
            

        }
    }
}
