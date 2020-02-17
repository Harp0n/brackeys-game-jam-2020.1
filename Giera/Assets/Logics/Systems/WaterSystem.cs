using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Systems
{
    public class WaterSystem : ISystem
    {
        private readonly float SPEED_OF_DROWNIG = 0.0001f;
        private readonly float INCREMENT_HOLE_SIZE = 0.05f;

        public void Update(Boat boat)
        {
            float sumOfHoleSizes = 0;

            foreach(Hole hole in boat.HolesMast)
            {
                hole.Size += (hole.Size * INCREMENT_HOLE_SIZE);
                sumOfHoleSizes += hole.Size;
            }

            boat.SetWaterOnBoard(boat.GetWaterOnBoard() + (SPEED_OF_DROWNIG * sumOfHoleSizes));
        }
    }
}
