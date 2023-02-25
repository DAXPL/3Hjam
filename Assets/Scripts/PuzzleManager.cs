using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private Puzel[] puzle;
    private int counter;
    [SerializeField] private GameEvent deathEvent;

    public void NextStep(Puzel p)
    {
        if (counter < puzle.Length && puzle[counter] == p)
        {
            counter++;
        }
        else
        {
            deathEvent.Raise();
        }
    }
}
