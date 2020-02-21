using Assets.Logics.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMask : Hole
{
    void Start()
    {
        IsPatchedUp = true;
        INITIAL_SIZE = 100.5f;
    }
}
