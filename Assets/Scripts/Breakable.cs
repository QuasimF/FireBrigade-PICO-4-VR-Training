using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Breakable : MonoBehaviour
{

    public GameObject destroyedVersion;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Axe")
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }


}
