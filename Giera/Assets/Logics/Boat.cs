using Assets.Logics;
using Assets.Logics.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public List<Hole> HolesMast { get; set; }
    public List<Hole> HolesHull { get; set; }
    public bool IsDrown { get; set; }
    private float waterOnBoard;
    public float Speed {
        get { 
            return _speed;
        }
        set
        {
            _ = value > 1 ? _speed = 1 : 
                value < 0 ? _speed = 0 : _speed = value; 
        }
    }
    private float _speed;

    public void SetWaterOnBoard(float value)
    {
        if ((waterOnBoard + value) < 0.0f)
        {
            waterOnBoard = 0.0f;
            //throw new ArgumentException();
        }
        else if ((waterOnBoard + value) > 1.0f)
        {
            Debug.Log("GAME OVERS");
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
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("holeHull"))
        {
            HolesHull.Add(gameObject.GetComponent<Hole>());
        }
        HolesMast = new List<Hole>();
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("holeMast"))
        {
            HolesMast.Add(gameObject.GetComponent<Hole>());
        }
        IsDrown = false;
        waterOnBoard = 0.0f;
        Speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
