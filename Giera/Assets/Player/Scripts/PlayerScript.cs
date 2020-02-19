using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour
{
    [Header("Movement")]
    public string axisName = "Horizontal";
    public float movementForce;
    public float maxSpeed;
    public float stoppingForce;

    [Header("Jumping")]
    public string jumpButton = "";
    public float jumpStrength;
    private bool isGrounded = true;

    [Header("Actions")]
    public string actionButton = "";

    private Rigidbody2D rigid;
    private Animator animator;
    private Vector2 rightScale;
    private Vector2 leftScale;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rightScale = new Vector2(transform.localScale.x, transform.localScale.y);
        leftScale = new Vector2(-transform.localScale.x, transform.localScale.y);
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
        
        if (rigid.velocity.sqrMagnitude < movementForce * movementForce)    //sprawdzamy, czy nie poruszamy sie juz z maks. predkoscia
        {
            float input = Input.GetAxis("Horizontal");  //wczytujemy wejscie z klawiatury (lewo - prawo)

            if (input != 0) //jesli jest 0 to nie bylo wejscia
            {
                //jesli trzeba to odwracamy postac w lewo badz prawo
                if (input < 0) transform.localScale = leftScale;
                else transform.localScale = rightScale;
                //sprawdzamy, czy gracz nie zmienil kierunku w trakcie ruchu
                if (rigid.velocity.x * input >= 0)
                {
                    //jesli nie zmienil to dodajemy sile w kierunku chodzenia
                    rigid.AddForce(input * movementForce * Vector2.right);
                }
                else
                {
                    //a jesli zmienil to dodajemy zwiekszona sile, co by sie nie slizgal spowalniajac i potem zawracajac
                    rigid.AddForce(input * movementForce * Vector2.right + -rigid.velocity.x * Vector2.right * stoppingForce);
                }
            }
            else
            {
                rigid.AddForce(-rigid.velocity.x * Vector2.right * stoppingForce);  //hamujemy, jesli nie bylo wejscia
            }
        }
        animator.SetFloat("walk_speed", Mathf.Abs(rigid.velocity.x));   //dajemy animacji chodzenia nasza predkosc - jak bedzie wieksza od 0 to wlaczy sie animacja
    }

    private void Jumping()
    {
        //Debug.Log("Player: grounded = " + isGrounded);

        if (!isGrounded) return;    //jesli nie jestesmy na ziemi to nie ma co sprawdzac

        if (Input.GetButtonDown(jumpButton))
        {
            rigid.AddForce(Vector2.up * jumpStrength);  //po wcisnieciu guzika skoku rzucamy graczem w gore
            animator.SetTrigger("jump");    //wlaczamy animacje skoku
            animator.ResetTrigger("land");  //wylaczamy animacje ladowania (czasami sie zacina z jakiegos powodu, wiec trzeba ja tu wylaczyc xd)
        }

    }
    //podazanie reka za myszka, w sumie tego nie uzywam teraz, ale moze sie przyda to zostawiam
    private void Aiming()
    {
        Vector2 mouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 forward = mouseInput - (Vector2)transform.position;
        forward.Normalize();
        float rotation = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
        //arm.rotation = Quaternion.Euler(0f, 0f, rotation);
    }

    private void Action()
    {

    }
    //to jest wzywane przez GroundCheck, w zaleznosci od tego czy gracz opusci ziemie czy spadnie na nia
    public void SetGrounded(bool grounded)
    {
        if (!isGrounded && grounded) animator.SetTrigger("land");
        isGrounded = grounded;
    }
}
