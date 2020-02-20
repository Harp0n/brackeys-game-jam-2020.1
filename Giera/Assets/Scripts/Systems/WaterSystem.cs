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
        const float INCREMENT_HOLE_SIZE = 0.0005f;

        public void Update(Boat boat)
        {
            List<Hole> holes = boat.HolesHull;
            float waterAmount = 0.0f;
            foreach (Hole hole in holes)
            {
                if (!hole.IsPatchedUp)
                {
                    hole.Size *= (1+INCREMENT_HOLE_SIZE);
                    waterAmount += hole.Size;
                }
            }
            boat.SetWaterOnBoard(waterAmount);
        }
    }
}
