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

        public UIManager UIManager { get; set; }

        private float _journeyPrecentage;
        public float JourneyPercentage
        {
            get => _journeyPrecentage;
            set
            {
                _journeyPrecentage = value;
                UIManager.SetBoatProgress(_journeyPrecentage);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            UIManager = GameObject.FindObjectOfType<UIManager>();
            HowHard = UIManager.HowHard;
            HowLong = UIManager.HowLong;
            Systems = new List<ISystem>
            {
                new WaterSystem(),
                new EventSystem(),
                new TravelSystem(),
                new MovementSystem()
            };
            Boat = FindObjectOfType<Boat>();
            Boat.Reset();
            Player = FindObjectOfType<PlayerScript>();
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

            foreach (var system in Systems)
            {
                system.Update(this, Time.deltaTime);
            }
        }
    }
}
