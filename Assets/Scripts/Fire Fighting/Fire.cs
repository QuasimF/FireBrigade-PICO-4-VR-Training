using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float currentIntensity = 1.0f;
    [SerializeField] private ParticleSystem [] fireParticleSystems = new ParticleSystem[0];
    [SerializeField] private float amountExtinguishedPerSecond = 1.0f;


    private float [] startIntensities = new float[0];

    private void Start()
    {
        startIntensities = new float[fireParticleSystems.Length];
        
        for (int i = 0; i < fireParticleSystems.Length; i++)
        {
            startIntensities[i] = fireParticleSystems[i].emission.rateOverTime.constant;
        }
        
    }

    private void ChangeIntensity()
    {
        for (int i = 0; i < fireParticleSystems.Length; i++)
        {
            var emission = fireParticleSystems[i].emission;
            emission.rateOverTime = currentIntensity * startIntensities[i];
        }
    }

    public bool TryExtinguish(float amount)
    {
        currentIntensity -= amount;

        ChangeIntensity();

        return currentIntensity <= 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
        }
    }
        private void Update()
    {
        //ChangeIntensity();
    }

}

