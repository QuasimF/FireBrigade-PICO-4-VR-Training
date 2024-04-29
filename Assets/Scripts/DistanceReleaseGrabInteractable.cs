using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DistanceReleaseGrabInteractable : XRGrabInteractable
{
    public float maxGrabDistance = 0.25f; //Max distance before auto-release
    private IXRSelectInteractor cachedInteractor; // To keep track of the interactor

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        cachedInteractor = args.interactorObject; //Cache the interactor when grabbed
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        cachedInteractor = null; //Clear cache when released
    }

    void Update()
    {
        if (cachedInteractor != null) //Only check distance if currently grabbed 
        {
            if (Vector3.Distance(cachedInteractor.transform.position, colliders[0].transform.position) > maxGrabDistance)
            {
                //If the interactor is too far, force release
                interactionManager.SelectExit(cachedInteractor, this);
            }
        }
    }

    public void DetachInteractor()
    {
        if (cachedInteractor != null)
        {
            interactionManager.SelectExit(cachedInteractor, this);
        }
    }
}
