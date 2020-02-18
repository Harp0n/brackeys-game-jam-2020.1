using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public List<Equipment> MyProperty { get; set; }
    public int Health { get; set; }
    public bool IsDead { get; set; }
    [SerializeField]
    public bool CanCollectWater { get; set; }
    [SerializeField]
    public bool IsInsideShip;
    [SerializeField]
    public bool HasWater { get; set; }


    public void UseBucket()
    {
        float amount = 0.1f;
        if (HasWater)
        {
            HasWater = false;
            if (IsInsideShip) //pour inside, water increases
            {
                GameObject.FindObjectOfType<Boat>().SetWaterOnBoard(amount);
            }
        }
        else //empty bucket
        {
            if (CanCollectWater) //water decreases
            {
                HasWater = true;
                GameObject.FindObjectOfType<Boat>().SetWaterOnBoard(-amount);
            }
        }
    }

    public void PatchUp()
    {
        Collider2D coll = GameObject.FindGameObjectWithTag("hammer").GetComponent<Collider2D>();
        coll.enabled = !coll.enabled;
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        IsDead = false;
        CanCollectWater = false;
        IsInsideShip = true;
        HasWater = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseBucket();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PatchUp();
        }
    }
}
