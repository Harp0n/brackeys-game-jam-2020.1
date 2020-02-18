using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourOutLayer : MonoBehaviour
{
    Player player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.IsInsideShip = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player.IsInsideShip = true;
        }
    }
    void Start()
    {
        player = FindObjectOfType<Player>();
    }
}
