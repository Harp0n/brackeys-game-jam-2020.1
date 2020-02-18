using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Logics.Systems
{
    class WaterSystem : ISystem
    {
        const float sizeIncrement = 0.00000001f;

        public void Update(Boat boat)
        {
            List<Hole> holes = boat.HolesHull;
            foreach (Hole hole in holes)
            {
                if (!hole.IsPatchedUp)
                {
                    hole.Size += sizeIncrement;
                    boat.SetWaterOnBoard(hole.Size);
                }
            }
        }
    }
}
