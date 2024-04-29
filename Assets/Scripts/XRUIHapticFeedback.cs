using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class HapticSettings
{
    public bool active;
    [Range(0f, 1f)]
    public float intensity;
    public float duration;
}


public class XRUIHapticFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public HapticSettings OnHoverEnter;
    public HapticSettings OnHoverExit;
    public HapticSettings OnSelectEnter;
    public HapticSettings OnSelectExit;

    private XRUIInputModule InputModule => EventSystem.current.currentInputModule as XRUIInputModule;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnHoverEnter.active)
        {
            TriggerHaptic(eventData, OnHoverEnter);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnHoverExit.active)
        {
            TriggerHaptic(eventData, OnHoverExit);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnSelectEnter.active)
        {
            TriggerHaptic(eventData, OnSelectEnter);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnSelectEnter.active)
        {
            TriggerHaptic(eventData, OnSelectExit);
        }
    }

    private void TriggerHaptic(PointerEventData eventData, HapticSettings hapticSettings)
    {
        XRRayInteractor interactor = InputModule.GetInteractor(eventData.pointerId) as XRRayInteractor;

        if (!interactor) { return; }

        interactor.xrController.SendHapticImpulse(hapticSettings.intensity, hapticSettings.duration);
    }
}
