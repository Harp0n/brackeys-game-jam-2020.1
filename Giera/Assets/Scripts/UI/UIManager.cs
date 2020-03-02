using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private readonly float startingMoney = 1000f;
    private readonly int startingCargoLimit = 50;

    public readonly float costPerCargo = 100.0f;
    private readonly float speedDecreasePerCargo = 1.3f;

    public Slider boatProgress;
    public TextMeshProUGUI speedValue;
    public TextMeshProUGUI waterPercentage;
    public string waterSurfix = "%";
    public string speedSurfix = "kn";

    public int HowHard { get; set; }
    public int HowLong { get; set; }

    private Canvas canvas;
    [SerializeField]
    private GameObject mainMenu, gameOverMenu, shipMenu;

    private GameStateEnum _gameState;
    public GameStateEnum GameState
    {
        get => _gameState;
        set
        {
            HideEverything();
            switch (value)
            {
                case GameStateEnum.MENU:
                    Debug.Log("MENU");
                    ChangeScene(0);
                    mainMenu.SetActive(true);
                    break;
                case GameStateEnum.PLAYING:
                    shipMenu.SetActive(true);
                    ChangeScene(1);
                    break;
                case GameStateEnum.GAMEOVER:
                    gameOverMenu.SetActive(true);
                    break;
            }
            _gameState = value;
        }
    }

    private void HideEverything()
    {
        LoadObjects();
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        shipMenu.SetActive(false);
    }

    public void GameOver()
    {
        GameState = GameStateEnum.GAMEOVER;
    }

    public void Sail()
    {
        GameState = GameStateEnum.PLAYING;
    }

    private void RestartGame()
    {
    }

    public void StartNewGame()
    {
        RestartGame();
        GameState = GameStateEnum.PLAYING;
    }

    public void ReturnToMenu()
    {
        GameState = GameStateEnum.MENU;
        RestartGame();
    }

    private void Start()
    {
        RestartGame();
        canvas = GameObject.FindObjectOfType<Canvas>();
        LoadObjects();
        DontDestroyOnLoad(canvas.gameObject);
        DontDestroyOnLoad(gameObject);
        GameState = GameStateEnum.MENU;
    }

    void LoadObjects()
    {
        if (mainMenu == null)
        {
            mainMenu = GameObject.Find("MainMenu");
        }

        if(gameOverMenu == null)
        {
            gameOverMenu = GameObject.Find("GameOverMenu");
        }
        if(shipMenu == null)
        {
            shipMenu = GameObject.Find("ShipMenu");
        }
    }

    public void ChangeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    public void SetWaterPercentage(float percentage)
    {
        if (waterPercentage == null) return;
        waterPercentage.SetText((int)(percentage * 100) + "%");
    }

    public void SetSpeedValue(float speed)
    {
        if (speedValue == null) return;
        speedValue.SetText((speed * 10.0f).ToString("0.00") + speedSurfix);
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