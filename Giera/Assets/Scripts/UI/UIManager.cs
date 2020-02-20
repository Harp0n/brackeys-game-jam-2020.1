using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu;
    public Slider boatProgress;
    public TextMeshProUGUI speedValue;
    public TextMeshProUGUI waterPercentage;
    public string waterSurfix = "%";
    public string speedSurfix = "kn";

    private void Start()
    {
        if(gameOverMenu != null)
            gameOverMenu.SetActive(false);
    }

    public void ChangeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void SetWaterPercentage(float percentage)
    {
        if (waterPercentage == null) return;
        waterPercentage.SetText(percentage.ToString() + "%");
    }

    public void SetSpeedValue(float speed)
    {
        if (speedValue == null) return;
        speedValue.SetText(speed.ToString() + speedSurfix);
    }

    public void SetBoatProgress(float progress)
    {
        if (boatProgress == null) return;
        boatProgress.value = progress;
    }

    public void OpenGameOverMenu(int totalScore, bool isHighScore)
    {
        if (gameOverMenu == null) return;
        gameOverMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(totalScore.ToString());
        if (isHighScore)
            gameOverMenu.transform.GetChild(1).gameObject.SetActive(true);
        gameOverMenu.SetActive(true);
    }
}