using Assets.Logics.Map;

public class Cargo
{
    public float Quantity { 
        get
        {
            return _quantity;
        } 
        set
        {
            _ = value > 0 ? _quantity = value : _quantity = Quantity;
        }
    }
    public float _quantity;
    private float _pricePerUnit;
    public float PricePerUnit {
        get
        {
            return _pricePerUnit;
        } 
        set
        {
            _ = value > 0 ? _pricePerUnit = value : _pricePerUnit = PricePerUnit;
        }
    }
    public Location Destination { get; set; }
}