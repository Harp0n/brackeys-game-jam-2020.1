using Assets.Logics.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMask : Hole
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "hammer")
        {
            IsPatchedUp = true;
        }
    }
}
