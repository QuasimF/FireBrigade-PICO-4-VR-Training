using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorControllerBasedOnMovement : MonoBehaviour
{

    public GameObject doorHandle;
    public DistanceReleaseGrabInteractable grabInteractable;
    public float movementThreshold = 0.25f; // The distance the handle needs to move to trigger the door

    private Animator _doorAnimator;
    private Vector3 _startPosition;
    private bool _handleGrabbed = false;
    private bool _doorOpened = false;

    private void Awake()
    {
        if (!doorHandle)
        {
            Debug.LogError("Door handle not assigned in the inspector.");
            enabled = false;
            return;
        }
        _doorAnimator = GetComponent<Animator>();
        if(!_doorAnimator)
        {
            Debug.LogError("Door Controller missing Animator");
            enabled = false;
            return;
        }
        if (!grabInteractable)
        {
            Debug.LogError("Door Controller missing grabInteractable in the inspector");
            enabled = false;
            return;
        }

        grabInteractable.selectEntered.AddListener(HandleGrabbed);
        grabInteractable.selectExited.AddListener(HandleReleased);
    }

    private void OnDestroy()
    {
        if (grabInteractable)
        {
            grabInteractable.selectEntered.RemoveListener(HandleGrabbed);
            grabInteractable.selectExited.RemoveListener(HandleReleased);
        }
    }

    private void HandleGrabbed(SelectEnterEventArgs arg)
    {
        _startPosition = doorHandle.transform.position;
        _handleGrabbed = true;
    }

    private void HandleReleased(SelectExitEventArgs arg)
    {
        _handleGrabbed = false;
    }

    private void Update()
    {
        if (_handleGrabbed)
        {
            float distanceMoved = Vector3.Distance(doorHandle.transform.position, _startPosition);
            if (distanceMoved > movementThreshold && !_doorOpened)
            {
                grabInteractable.DetachInteractor();
                //Open the door
                _doorAnimator.SetBool("isOpen", true);
                _doorOpened = true;
            }
            else if (distanceMoved > movementThreshold && _doorOpened)
            {
                grabInteractable.DetachInteractor();

                // Close the door
                _doorAnimator.SetBool("isOpen", false);
                _doorOpened = false;
            }
        }
    }
}
