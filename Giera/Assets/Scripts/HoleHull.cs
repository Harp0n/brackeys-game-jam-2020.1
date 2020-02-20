using Assets.Logics.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleHull : Hole
{
    private readonly float INITIAL_SIZE = 0.00001f;
    private Vector3 minScale = new Vector3(0.4f, 0.5f, 1.0f);

    protected override void OnStateChange(bool futurePatchUp)
    {
        if (IsPatchedUp && !futurePatchUp) //open a hole
        {
            Size = INITIAL_SIZE;
        }
        else if (!IsPatchedUp && futurePatchUp)//close a hole
        {

        }
        SetDisplay(futurePatchUp);
    }

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

    void SetDisplay(bool isPatchedUp)
    {
        Debug.Log("SET DISPLAY "+isPatchedUp);
        gameObject.GetComponent<SpriteRenderer>().enabled = isPatchedUp;
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.GetComponent<SpriteRenderer>().enabled = !isPatchedUp;
        }
    }
}
