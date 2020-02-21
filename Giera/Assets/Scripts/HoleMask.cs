using Assets.Logics.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMask : Hole
{
    void Start()
    {
        IsPatchedUp = true;
        INITIAL_SIZE = 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "hammer")
        {
            IsPatchedUp = true;
        }
    }
}
