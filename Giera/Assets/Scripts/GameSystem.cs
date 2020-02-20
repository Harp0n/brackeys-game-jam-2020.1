using Assets.Logics.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Logics
{
    class GameSystem : MonoBehaviour
    {
        public List<ISystem> Systems { get; set; }
        public Boat Boat { get; set; }
        public PlayerScript Player { get; set; }
        private Transform waterTransform;
        private Vector2 waterTransformInitialPosition;

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
            waterTransform = GameObject.Find("movingWaterTransform").transform;
            waterTransformInitialPosition = waterTransform.position;
            waterTransform.localPosition = Vector3.zero;
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
                system.Update(Boat);
            }
            waterTransform.localPosition = new Vector3(0.0f, -Boat.GetWaterOnBoard() * waterTransformInitialPosition.y, 0.0f);
        }
    }
}
