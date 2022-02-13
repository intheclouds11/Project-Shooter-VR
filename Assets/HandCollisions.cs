using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCollisions : MonoBehaviour
{
    [SerializeField] private Collider handCollider;

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Physics.IgnoreCollision(handCollider, other.collider, true);
            
        }
    }
}