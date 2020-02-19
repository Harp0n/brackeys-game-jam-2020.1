using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GroundChecker : MonoBehaviour
{
    public string groundTag = "Ground";
    public string playerTag = "Player";
    private BoxCollider2D col;
    private PlayerScript player;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        player = transform.parent.GetComponent<PlayerScript>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(groundTag))
            player.SetGrounded(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
            player.SetGrounded(false);
    }
}
