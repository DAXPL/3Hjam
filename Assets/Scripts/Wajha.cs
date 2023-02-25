using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wajha : MonoBehaviour, Interact
{
    public bool state = false;
    public void Interaction()
    {
        Debug.Log("U¿yto wajhê");
        state = !state;
    }
}
