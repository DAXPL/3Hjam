using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Wase : MonoBehaviour
{

    [SerializeField] private GameObject WaseFire;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Torch"))
        {
            WaseFire.SetActive(true);
        }
    }


}
