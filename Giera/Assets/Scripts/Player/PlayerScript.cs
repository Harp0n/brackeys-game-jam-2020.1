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
        public bool canShoot;
        public float waterCapacity;
        public int swapToAfterAction = -1;
    }

    [Header("Movement")]
    public string axisName = "Horizontal";
    public float movementSpeed;
    [Range(0f, 1f)]
    public float airMovementSlowdown;

    [Header("Jumping")]
    public string jumpButton = "";
    public float jumpStrength;
    private bool isGrounded = true;

    [Header("Actions")]
    public string actionButton = "";
    public string nextItemButton = "";
    public string prevItemButton = "";
    public SpriteRenderer itemHolder;
    public Item[] items;
    public float shootCooldown;
    public GameObject bulletPrefab;

    private List<int> selectable = new List<int>();
    private int currentItem = 0;
    private bool isInAction = false;
    private bool canChangeItem = true;
    private bool canShoot = true;
    private float coolDownTimer = 0f;
    private List<GameObject> bulletsPool = new List<GameObject>();

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
    private Transform bulletsContainer;

    private void Awake()
    {
        bulletsContainer = new GameObject("bullets").transform;
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boat = FindObjectOfType<Boat>();
        rightScale = new Vector2(transform.localScale.x, transform.localScale.y);
        leftScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i].itemCollider != null)
                items[i].itemCollider.enabled = false;
            if (items[i].isSelectable) selectable.Add(i);
        }
    }

    private void Start()
    {
        itemHolder.sprite = items[currentItem].itemSprite;
    }

    void Update()
    {
        if (isInAction) return;
        Jumping();
        Movement();
        SwapItem();
        Action();
        Timer();
    }

    private void Movement()
    {
        float input = Input.GetAxis("Horizontal");
        if (input < 0) transform.localScale = leftScale;
        else if (input > 0) transform.localScale = rightScale;
        rigid.velocity = new Vector2(movementSpeed * input * (isGrounded ? 1f : (1f - airMovementSlowdown)), rigid.velocity.y);
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
        int prevItem = currentItem;
        Debug.Log(selectable.Count);
        for (int i = 0; i < selectable.Count; i++)
            if (Input.GetKey((i+1).ToString())) currentItem = selectable[i];

        if (prevItem != currentItem)
            itemHolder.sprite = items[currentItem].itemSprite;
    }

    private void Action()
    {
        if (!isGrounded) return;
        if(Input.GetButton(actionButton))
        {
            if (items[currentItem].waterCapacity > 0 && !canCollectWater) return;

            if (items[currentItem].canShoot)
                if (!canShoot) return;
                else Shoot();
            else
                items[currentItem].itemCollider.enabled = true;
            isInAction = true;
            rigid.velocity = Vector2.zero;
            animator.SetTrigger(items[currentItem].animationTrigger);
        }
    }

    private GameObject GetBullet()
    {
        foreach (GameObject g in bulletsPool)
        {
            if (!g.activeSelf) return g;
        }
        GameObject result = Instantiate(bulletPrefab, bulletsContainer);
        bulletsPool.Add(result);
        return result;
    }

    private void Shoot()
    {
        if (!canShoot) return;
        canShoot = false;
        coolDownTimer = shootCooldown;
        GameObject bullet = GetBullet();
        bullet.transform.position = itemHolder.transform.position;
        bullet.SetActive(true); //bullet ma w skrypcie OnEnable, więc nie trzeba nic więcej robić
    }

    private void Timer()
    {
        if (coolDownTimer > 0f)
            coolDownTimer -= Time.deltaTime;
        else
            canShoot = true;
    }

    public void SetGrounded(bool grounded)
    {
        if (!isGrounded && grounded) animator.SetTrigger("land");
        isGrounded = grounded;
    }

    public void FinishAction()
    {
        Item item = items[currentItem];
        if(item.itemCollider !=null)
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
