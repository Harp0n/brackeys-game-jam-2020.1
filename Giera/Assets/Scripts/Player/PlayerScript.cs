using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public Collider2D itemCollider;
        public Sprite itemSprite;
        public string animationTrigger;
        public bool lockItemAfterAction;
        public bool isSelectable;
        public float waterCapacity;
        public int swapToAfterAction = -1;
    }

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
    public string scrollWheelAxis = "";
    public SpriteRenderer itemHolder;
    public Item[] items;
    private List<int> selectable = new List<int>();
    private int currentItem = 0;
    private bool isInAction = false;
    private bool canChangeItem = true;
    [HideInInspector]
    public bool canCollectWater;
    private float collectedWater = 0f;
    [HideInInspector]
    public bool isInsideShip;

    private Boat boat;
    private Rigidbody2D rigid;
    private Animator animator;
    private Vector2 rightScale;
    private Vector2 leftScale;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boat = FindObjectOfType<Boat>();
        rightScale = new Vector2(transform.localScale.x, transform.localScale.y);
        leftScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        for (int i = 0; i < items.Length; i++)
        {
            items[i].itemCollider.enabled = false;
            if (items[i].isSelectable) selectable.Add(i);
        }
    }

    void Update()
    {
        if (isInAction) return;
        Jumping();
        Movement();
        SwapItem();
        Action();
    }

    private void Movement()
    {
        if (rigid.velocity.magnitude < movementForce)
        {
            float input = Input.GetAxis("Horizontal");

            if (input != 0)
            {
                if (input < 0) transform.localScale = leftScale;
                else transform.localScale = rightScale;
                if (rigid.velocity.x * input >= 0)
                {
                    rigid.AddForce(input * movementForce * Vector2.right);
                }
                else
                {
                    rigid.AddForce(input * movementForce * Vector2.right + -rigid.velocity.x * Vector2.right * stoppingForce);
                }
            }
            else
            {
                rigid.AddForce(-rigid.velocity.x * Vector2.right * stoppingForce);
            }
        }
        animator.SetFloat("walk_speed", Mathf.Abs(rigid.velocity.x));
    }

    private void Jumping()
    {
        if (!isGrounded) return;

        if (Input.GetButtonDown(jumpButton))
        {
            rigid.AddForce(Vector2.up * jumpStrength);
            animator.SetTrigger("jump");
            animator.ResetTrigger("land"); 
        }

    }

    private void SwapItem()
    {
        if (!canChangeItem) return;
        float scrollWheel = Input.GetAxis(scrollWheelAxis);
        if (scrollWheel < 0f)
            currentItem = selectable[(currentItem - 1 + selectable.Count) % selectable.Count];
        else if (scrollWheel > 0f)
            currentItem = selectable[(currentItem + 1) % selectable.Count];

        itemHolder.sprite = items[currentItem].itemSprite;
    }

    private void Action()
    {
        if (!isGrounded) return;
        if(Input.GetButton(actionButton))
        {
            isInAction = true;
            items[currentItem].itemCollider.enabled = true;
            rigid.velocity = Vector2.zero;
            animator.SetTrigger(items[currentItem].animationTrigger);
        }
    }

    public void SetGrounded(bool grounded)
    {
        if (!isGrounded && grounded) animator.SetTrigger("land");
        isGrounded = grounded;
    }

    public void FinishAction()
    {
        Item item = items[currentItem];
        item.itemCollider.enabled = false;
        isInAction = false;
        canChangeItem = true;
        if (item.lockItemAfterAction) canChangeItem = false;
        if (item.waterCapacity > 0f)
        {
            if (canCollectWater)
            {
                float pickedUpWater = Mathf.Min(boat.GetWaterOnBoard(), item.waterCapacity);
                collectedWater = pickedUpWater;
                boat.SetWaterOnBoard(-pickedUpWater);
            }
        }
        else if (collectedWater > 0f) 
        {
            if (isInsideShip) boat.SetWaterOnBoard(collectedWater);
            collectedWater = 0f;
        }
        if(item.swapToAfterAction!=-1)
        {
            currentItem = item.swapToAfterAction;
            itemHolder.sprite = items[currentItem].itemSprite;
        }

    }
}
