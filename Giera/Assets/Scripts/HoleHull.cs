using Assets.Logics.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleHull : Hole
{
    private Vector3 minScale = new Vector3(0.4f, 0.5f, 1.0f);

    protected override void OnSizeChange(float futureSize)
    {
        foreach (Transform child in gameObject.transform)
        {
            child.localScale = futureSize / INITIAL_SIZE * minScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "hammer")
        {
            IsPatchedUp = true;
        }
    }

}
