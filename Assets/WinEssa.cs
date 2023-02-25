using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEssa : MonoBehaviour
{
    [SerializeField] private GameEvent end;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            end.Raise();
        }
    }
}
