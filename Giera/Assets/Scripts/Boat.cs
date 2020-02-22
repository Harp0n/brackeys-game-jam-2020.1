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
    public float Money { get { return _money; } set { _ = value > 0 ? _money = value : _money = 0; } }
    private float _money { get; set; }
    public Cargo Cargo { get; set; }
    public int quantityOfCargoLimit { get; set; } = 500; //?

    /// <summary>
    /// Loading cargo, returns false if cargo is too big, and true if loading is successful
    /// </summary>
    /// <param name="cargo"></param>
    /// <returns></returns>
    public bool LoadCargo(Cargo cargo)
    {
        if (quantityOfCargoLimit < Cargo.Quantity)
        {
            this.Cargo = Cargo;
            return true;
        }
        else return false;
    }
    /// <summary>
    /// Function unloads the cargo (sets it to null) and adds money to the boat object
    /// </summary>
    /// <returns></returns>
    public bool UnloadCargo()
    {
        if (Cargo != null)
        {
            float money = Cargo.PricePerUnit * Cargo.Quantity;

            Money = money + Money;
            Cargo = null;

            return true;
        }
        else return false;
    }
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
