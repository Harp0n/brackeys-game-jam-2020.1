using Assets.Logics.Map;
using Assets.Logics.Systems;
using Graphs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Logics
{
    public class GameSystem : MonoBehaviour
    {
        public List<ISystem> Systems { get; set; }
        public Boat Boat { get; set; }
        public PlayerScript Player { get; set; }
        public int HowHard { get; set; }
        public int HowLong { get; set; }


        public float journeyPercentage { get; set; }
        private GameObject gameObjectPathSelection, gameObjectDocking;

        private GameStateEnum _gameState;
        public GameStateEnum GameState
        {
            get => _gameState;
            set
            {
                HideEverything();
                switch (value)
                {
                    case GameStateEnum.PLAYING:
                        Player.enabled = true;
                        break;
                    case GameStateEnum.PATH_SELECTION:
                        FindObjectOfType<Canvas>().enabled = true;
                        gameObjectPathSelection.SetActive(true);
                        Player.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                        break;
                    case GameStateEnum.DOCKING:
                        FindObjectOfType<Canvas>().enabled = true;
                        gameObjectDocking.SetActive(true);
                        break;
                }
                _gameState = value;
            }
        }

        private void HideEverything()
        {
            gameObjectDocking.SetActive(false);
            gameObjectPathSelection.SetActive(false);
            FindObjectOfType<Canvas>().enabled = false;
            Player.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            Player.enabled = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            Systems = new List<ISystem>
            {
                new WaterSystem(),
                new EventSystem(),
                new TravelSystem(),
                new MovementSystem()
            };
            gameObjectDocking = GameObject.FindGameObjectWithTag("docking");
            gameObjectPathSelection = GameObject.FindGameObjectWithTag("pathSelection");
            Boat = FindObjectOfType<Boat>();
            Player = FindObjectOfType<PlayerScript>();
            GameState = GameStateEnum.PATH_SELECTION;
        }

        void StartRound()
        {
            Boat.Reset();
        }

        public void FinishJourney()
        {
            if (GameState.Equals(GameStateEnum.PLAYING))
            {
                GameState = GameStateEnum.DOCKING;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey("up"))
            {
                Boat.SetWaterOnBoard(0.01f);
                print("up arrow key is held down");
            }

            if (Input.GetKey("down"))
            {
                Boat.SetWaterOnBoard(-0.01f);
                print("down arrow key is held down");
            }
            //--------------------------------------------

            if (GameState.Equals(GameStateEnum.PLAYING))
            {
                Debug.Log(journeyPercentage);
                foreach (var system in Systems)
                {
                    system.Update(this, Time.deltaTime);
                }
            }
        }

        public void SelectPath(Edge edge)
        {
            Debug.Log("SELECT W GAMESYSTEM");
            if (GameState.Equals(GameStateEnum.PATH_SELECTION))
            {
                HowHard = edge.Route.HowHard;
                HowLong = edge.Route.HowLong;
                GameState = GameStateEnum.PLAYING;
            }
        }
    }
}
