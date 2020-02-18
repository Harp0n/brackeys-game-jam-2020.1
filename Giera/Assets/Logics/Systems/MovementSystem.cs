using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Systems
{
    public class MovementSystem : ISystem
    {
        private readonly float SPEED_DECREASE = 0.001f;
        private readonly float INCREMENT_HOLE_SIZE = 0.05f;

        public void Update(Boat boat)
        {
            float sumOfHoleSizes = 0;

            foreach (Hole hole in boat.HolesHull)
            {
                hole.Size += (hole.Size * INCREMENT_HOLE_SIZE);
                sumOfHoleSizes += hole.Size;
            }

            boat.Speed = 1 - (SPEED_DECREASE * sumOfHoleSizes);
        }
    }
}
