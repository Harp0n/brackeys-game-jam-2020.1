using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Logics.Systems
{
    public class WaterSystem : ISystem
    {
        private readonly float WATER_MULTIPLIER = 100;
        const float INCREMENT_HOLE_SIZE = 0.000002f;

        private Transform waterTransform;
        private Vector2 waterTransformInitialPosition;

        public WaterSystem()
        {
            waterTransform = GameObject.Find("movingWaterTransform").transform;
            waterTransformInitialPosition = waterTransform.localPosition;
            waterTransform.localPosition = Vector3.zero;
        }

        public void Update(GameSystem gameSystem, float deltaTime)
        {
            Boat boat = gameSystem.Boat;
            List<Hole> holes = boat.HolesHull;
            float waterAmount = 0.0f;
            foreach (Hole hole in holes)
            {
                if (!hole.IsPatchedUp)
                {
                    hole.Size += INCREMENT_HOLE_SIZE*deltaTime;
                    waterAmount += hole.Size * deltaTime;
                }
            }
            boat.SetWaterOnBoard(waterAmount * WATER_MULTIPLIER);
            waterTransform.localPosition = new Vector3(0.0f, boat.GetWaterOnBoard() * waterTransformInitialPosition.y, 0.0f);
        }
    }
}
