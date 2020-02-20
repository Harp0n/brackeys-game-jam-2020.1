using Assets.Logics.Map;
using Assets.Logics.Systems;
using Graphs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Logics
{
    class GameSystem : MonoBehaviour
    {
        public List<ISystem> Systems { get; set; }
        public Boat Boat { get; set; }
        public PlayerScript Player { get; set; }
        private int HowHard { get; set; }
        private int HowLong { get; set; }

        private GameStateEnum _gameState;
        public GameStateEnum GameState
        {
            get => _gameState;
            set
            {
                switch (value)
                {
                    case GameStateEnum.PLAYING:
                        FindObjectOfType<Canvas>().enabled = false;
                        break;
                    case GameStateEnum.PATH_SELECTION:
                        FindObjectOfType<Canvas>().enabled = true;
                        break;
                }
                _gameState = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Systems = new List<ISystem>
            {
                new WaterSystem(),
                new EventSystem()
            };
            Boat = FindObjectOfType<Boat>();
            Player = FindObjectOfType<PlayerScript>();
            GameState = GameStateEnum.PATH_SELECTION;
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
                foreach (var system in Systems)
                {
                    system.Update(Boat);
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
