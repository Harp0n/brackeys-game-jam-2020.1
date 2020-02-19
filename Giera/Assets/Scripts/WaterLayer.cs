using UnityEngine;

public class WaterLayer : MonoBehaviour
{
    public string playerTag = "Player";
    private PlayerScript player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(playerTag))
        {
            player.canCollectWater = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            player.canCollectWater = false;
        }
    }
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }
}
