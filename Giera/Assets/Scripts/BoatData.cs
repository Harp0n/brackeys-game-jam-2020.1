using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatData
{
    public float QuantityOfCargoLimit { get; set; } = 500f; //?
    public Cargo Cargo { get; set; }
    public float BoatSpeed { get; set; }
    private float _money { get; set; }
    public float Money { get { return _money; } set { _ = value > 0 ? _money = value : _money = 0; } }

    public BoatData(float cargoLimit, float money)
    {
        BoatSpeed = 1.0f;
        Cargo = new Cargo();
        Cargo.Quantity = 0.0f;
        QuantityOfCargoLimit = cargoLimit;
        Money = money;
    }


    /// <summary>
    /// Loading cargo, returns false if cargo is too big, and true if loading is successful
    /// </summary>
    /// <param name="cargo"></param>
    /// <returns></returns>
    /// 
    public bool LoadCargo(Cargo cargo)
    {
        if (QuantityOfCargoLimit < Cargo.Quantity)
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
            BoatSpeed = 1.0f;
            return true;
        }
        else return false;
    }
}
