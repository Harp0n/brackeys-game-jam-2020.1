using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Graphs;
using Assets.Logics.Map;

public class UIManager : MonoBehaviour
{
    public Slider boatProgress;
    public TextMeshProUGUI speedValue;
    public TextMeshProUGUI waterPercentage;
    public string waterSurfix = "%";
    public string speedSurfix = "kn";

    public int HowHard { get; set; }
    public int HowLong { get; set; }

    private Canvas canvas;
    [SerializeField]
    private GameObject mainMenu, portMenu, pathMenu, gameOverMenu, shipMenu;

    public WorldMap WorldMap { get; set; }

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
                case GameStateEnum.PATH_SELECTION:
                    Debug.Log("Selection");
                    pathMenu.SetActive(true);
                    GameObject.FindObjectOfType<MapSelection>().GenerateMapGui(WorldMap);
                    break;
                case GameStateEnum.DOCKING:
                    if (_gameState.Equals(GameStateEnum.PLAYING))
                    {
                        ChangeScene(0);
                    }
                    portMenu.SetActive(true);
                    Debug.Log("DOCKING");
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
        portMenu.SetActive(false);
        pathMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        shipMenu.SetActive(false);
    }

    public void FinishJourney()
    {
        Debug.Log("END OF JOURNEY");
        if (GameState.Equals(GameStateEnum.PLAYING))
        {
            GameState = GameStateEnum.DOCKING;
        }
    }

    public void GameOver()
    {
        GameState = GameStateEnum.GAMEOVER;
    }

    public void SelectPath(Edge edge)
    {
        Debug.Log("SELECT PATH");
        if (GameState.Equals(GameStateEnum.PATH_SELECTION))
        {
            HowHard = edge.Route.HowHard;
            HowLong = edge.Route.HowLong;
            GameState = GameStateEnum.PLAYING;
        }
    }

    public void Sail()
    {
        GameState = GameStateEnum.PATH_SELECTION;
    }

    private void RestartGame()
    {
        WorldMap = new WorldMap();
        WorldMap.CreateMap(howBigMap: 5, minIslandsInArray: 2, maxIslandsInArray: 4, howManyConnectionsInSameArrDivider: 2);
    }

    public void StartNewGame()
    {
        RestartGame();
        Sail();
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
        if (portMenu == null) {
            portMenu = GameObject.Find("PortMenu");
        }
        if(pathMenu == null){
            pathMenu = GameObject.Find("PathMenu");
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