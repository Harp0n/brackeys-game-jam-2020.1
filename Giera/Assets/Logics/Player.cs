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
    public bool IsInsideShip { get; set; }
    [SerializeField]
    public float WaterInBucket { get; set; }


    public void UseBucket()
    {
        float maxAmount = 0.1f;
        if (WaterInBucket > 0.0f)
        {
            if (IsInsideShip) //pour inside, water increases
            {
                GameObject.FindObjectOfType<Boat>().SetWaterOnBoard(WaterInBucket);
            }
            WaterInBucket = 0.0f;
        }
        else //empty bucket
        {
            if (CanCollectWater) //water decreases
            {
                Boat boat = GameObject.FindObjectOfType<Boat>();
                float pickedUpWater = Mathf.Min(boat.GetWaterOnBoard(), maxAmount);
                WaterInBucket = pickedUpWater;
                boat.SetWaterOnBoard(-pickedUpWater);
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
        WaterInBucket = 0.0f;
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
