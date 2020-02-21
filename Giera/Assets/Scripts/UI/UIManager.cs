using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Graphs;

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
    private GameObject mainMenu, portMenu, pathMenu, gameOverMenu;

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

                    HideEverything();
                    if (canvas)
                        canvas.enabled = true;
                    if (mainMenu)
                        mainMenu.SetActive(true);
                    break;
                case GameStateEnum.PLAYING:
                    ChangeScene(1);
                    break;
                case GameStateEnum.PATH_SELECTION:
                    Debug.Log("Selection");
                    canvas.enabled = true;
                    if (pathMenu)
                        pathMenu.SetActive(true);
                    break;
                case GameStateEnum.DOCKING:
                    if (_gameState.Equals(GameStateEnum.PLAYING))
                    {
                        ChangeScene(0);
                        HideEverything();
                    }
                    canvas.enabled = true;
                    if (portMenu)
                        portMenu.SetActive(true);
                    Debug.Log("DOCKING");
                    break;
                case GameStateEnum.GAMEOVER:
                    canvas.enabled = true;
                    if (gameOverMenu != null)
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
            canvas.enabled = false;
    }

    public void FinishJourney()
    {
        Debug.Log("END OF JOURNEY");
        if (GameState.Equals(GameStateEnum.PLAYING))
        {
            GameState = GameStateEnum.DOCKING;
        }
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

    private void Start()
    {
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