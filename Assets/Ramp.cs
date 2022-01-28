using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour, IInteractable
{
    public void DoThing()
    {
        transform.Rotate(0,90,0);
    }

    public void Interact()
    {
        DoThing();
    }
}
