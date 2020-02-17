using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Logics
{
    class GameSystem : MonoBehaviour
    {
        public Boat boat { get; set; }
        public Player player { get; set; }
        private Transform waterTransform;
        private Vector2 waterTransformInitialPosition;

        // Start is called before the first frame update
        void Start()
        {
            boat = GameObject.FindObjectOfType<Boat>();
            waterTransform = GameObject.Find("movingWaterTransform").transform;
            waterTransformInitialPosition = waterTransform.position;
            waterTransform.localPosition = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey("up"))
            {
                boat.SetWaterOnBoard(0.01f);
                print("up arrow key is held down");
            }

            if (Input.GetKey("down"))
            {
                boat.SetWaterOnBoard(-0.01f);
                print("down arrow key is held down");
            }
            foreach (var system in boat.Systems)
            {
                system.Update(boat);
            }
            waterTransform.localPosition = new Vector3(0.0f, -boat.GetWaterOnBoard() * waterTransformInitialPosition.y, 0.0f);
        }
    }
}
