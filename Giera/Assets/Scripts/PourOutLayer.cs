using UnityEngine;

public class PourOutLayer : MonoBehaviour
{
    public string playerTag = "Player";
    private PlayerScript player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(playerTag))
        {
            player.isInsideShip = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            player.isInsideShip = true;
        }
    }
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }
}
