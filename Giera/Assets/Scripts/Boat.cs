using Assets.Logics.Systems;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{

    public List<Hole> HolesMast { get; set; }
    public List<Hole> HolesHull { get; set; }
    public bool IsDrown { get; set; }
    private float waterOnBoard;
    private UIManager uiManager;
    public float Speed {
        get { 
            return _speed;
        }
        set
        {
            _ = value > 1 ? _speed = 1 : 
                value < 0 ? _speed = 0 : _speed = value;
            uiManager.SetSpeedValue(_speed);
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
            uiManager.GameOver();
        }
        else
        {
            _ = waterOnBoard + value > 1 ? waterOnBoard = 1 : waterOnBoard += value;
        }
        uiManager.SetWaterPercentage(waterOnBoard);
    }

    public float GetWaterOnBoard()
    {
        return this.waterOnBoard;
    }


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
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
        Reset();
    }

    public void Reset() //reverts ship to the initial state
    {
        foreach (Hole hole in HolesHull)
        {
            hole.IsPatchedUp = true;
        }
        foreach (Hole hole in HolesHull)
        {
            hole.IsPatchedUp = true;
        }
        IsDrown = false;
        waterOnBoard = 0.0f;
        Speed = 1.0f;
    }
}
