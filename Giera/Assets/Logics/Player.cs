using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public List<Equipment> MyProperty { get; set; }
    public int Health { get; set; }
    public bool IsDead { get; set; }
    public bool CanCollectWater { get; set; }
    public bool IsInsideShip { get; set; }
    public bool HasWater { get; set; }

    public void UseBucket()
    {

    }

    public void PatchUp()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        IsDead = false;
        CanCollectWater = false;
        IsInsideShip = false;
        HasWater = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
