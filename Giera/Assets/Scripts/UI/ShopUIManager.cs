using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopUIManager : MonoBehaviour
{
    public string cargoSurfix;
    public string speedDecreaseSurfix;
    public string moneySurfix;
    public TextMeshProUGUI cargoTxt;
    public TextMeshProUGUI speedDecreaseTxt;
    public TextMeshProUGUI costTxt;
    public TextMeshProUGUI moneyTxt;
    public Slider cargoSlider;
    public GameObject confirmButton;

    private int selectedCargo;
    private float costPerCargo = 100.0f;
    private float speedDecreasePerCargo = 0.13f;
    private float playerMoney = 2;
    private bool enoughMoney = true;

    //przypisac kase gracza tutaj - moze z playerpref korzystajac?
    private void Awake()
    {
       // playerMoney = PlayerPrefs.GetFloat("playerMoney", 0f);
    }

    private void Start()
    {
        UpdateUI();
    }

    //dla slidera funkcja
    public void UpdateCargo()
    {
        selectedCargo = (int)cargoSlider.value;
        enoughMoney = selectedCargo * costPerCargo <= playerMoney;
        UpdateUI();
    }
    public void SetPlayerMoney(float money)
    {
        playerMoney = money;
    }
    //to modyfikowac z zewnatrz
    public void SetMinCargo(int minCargo)
    {
        cargoSlider.minValue = minCargo;
    }

    public void SetMaxCargo(int maxCargo)
    {
        cargoSlider.maxValue = maxCargo;
    }

    public void SetCostPerCargo(float costPerCargo)
    {
        this.costPerCargo = costPerCargo;
    }

    public void SetSpeedDecreasePerCargo(float decreasePerCargo)
    {
        speedDecreasePerCargo = decreasePerCargo;
    }

    //wezwana przy pomyslnym zakonczeniu transakcji
    public void ConfirmCargo()
    {
        float totalCost = selectedCargo * costPerCargo;
        float totalSlowdown = selectedCargo * speedDecreasePerCargo;
        GameObject.FindObjectOfType<UIManager>().BuyCargo(selectedCargo, totalCost, totalSlowdown);
    }

    private void UpdateUI()
    {
        confirmButton.SetActive(enoughMoney);
        if (!enoughMoney) costTxt.color = Color.red;
        else costTxt.color = Color.black;
        cargoTxt.SetText(selectedCargo + cargoSurfix);
        speedDecreaseTxt.SetText(selectedCargo * speedDecreasePerCargo + speedDecreaseSurfix);
        costTxt.SetText(selectedCargo * costPerCargo + moneySurfix);
        moneyTxt.SetText(playerMoney + moneySurfix);
    }
}
