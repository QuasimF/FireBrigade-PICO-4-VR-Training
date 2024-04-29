using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireFoam : MonoBehaviour
{

    public ParticleSystem particles;

    [Space, SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(x => ShootFoam());
        grabbable.deactivated.AddListener(x => StopFoam());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootFoam()
    {
        particles.Play();
    }

    public void StopFoam()
    {
        particles.Stop();
    }

    public void ShotAudio()
    {
        audioSource.Play();
    }
}
    