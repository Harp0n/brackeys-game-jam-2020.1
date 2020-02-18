using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    [Header("Controls")]
    public string axisName = "Horizontal";
    public string jumpButton = "";
    public string actionButton = "";

    [Header("Settings")]
    public float movementForce;
    public float maxSpeed;
    public float stoppingForce;
    public float jumpStrength;

    public Transform arm;
    private Rigidbody2D rigid;
    private bool isGrounded = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jumping();
        Movement();
        //Aiming();
        Action();
    }

    private void Movement()
    {
        //Debug.Log("Player: velocity = " + rigid.velocity.x);

        if (rigid.velocity.sqrMagnitude < movementForce * movementForce)
        {
            float input = Input.GetAxis("Horizontal");

            if (input != 0)
                if(rigid.velocity.x * input >= 0)
                    rigid.AddForce(input * movementForce * Vector2.right);
                else
                    rigid.AddForce(input * movementForce * Vector2.right + -rigid.velocity.x * Vector2.right * stoppingForce);
            else
                rigid.AddForce(-rigid.velocity.x * Vector2.right * stoppingForce);
        }
    }

    private void Jumping()
    {
        //Debug.Log("Player: grounded = " + isGrounded);

        if (!isGrounded) return;

        if (Input.GetButtonDown(jumpButton))
            rigid.AddForce(Vector2.up * jumpStrength);
    }

    private void Aiming()
    {
        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 forward = mouseInput - (Vector2)transform.position;
        forward.Normalize();
        float rotation = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
        arm.rotation = Quaternion.Euler(0f, 0f, rotation);
    }

    private void Action()
    {

    }

    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
    }
}
