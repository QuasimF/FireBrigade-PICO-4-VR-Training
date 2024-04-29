using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Extinguisher : MonoBehaviour
{

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Fire"))
        {
            other.GetComponent<ParticleSystem>().Stop();
            other.GetComponent<AudioSource>().Stop();
        }
    }
}
