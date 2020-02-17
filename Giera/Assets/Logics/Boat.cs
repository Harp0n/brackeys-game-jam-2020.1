using Assets.Logics;
using Assets.Logics.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public List<Hole> HolesMast { get; set; }
    public List<ISystem> Systems { get; set; }
    public List<Hole> HolesHull { get; set; }
    public bool IsDrown { get; set; }
    private float waterOnBoard;
    public float Speed { get; set; }

    public void SetWaterOnBoard(float value)
    {
        if ((waterOnBoard + value) < 0)
        {
            throw new ArgumentException();
        }
        else
        {
            _ = waterOnBoard + value > 1 ? waterOnBoard = 1 : waterOnBoard += value;
        }
    }

    public float GetWaterOnBoard()
    {
        return this.waterOnBoard;
    }


    // Start is called before the first frame update
    void Start()
    {
        HolesHull = new List<Hole>();
        HolesMast = new List<Hole>();
        Systems = new List<ISystem>();
        IsDrown = false;
        waterOnBoard = 0.0f;
        Speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
