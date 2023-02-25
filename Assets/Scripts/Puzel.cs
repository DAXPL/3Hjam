using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzel : MonoBehaviour
{
    PuzzleManager pm;
    private void Start()
    {
        pm = GetComponentInParent<PuzzleManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pm)
            {
                pm.NextStep(this);
            }
        }
    }
}
