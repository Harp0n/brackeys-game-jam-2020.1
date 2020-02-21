using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Systems
{
    public class MovementSystem : ISystem
    {
        private readonly float SPEED_DECREASE = 0.3f;
        private readonly float INCREMENT_HOLE_SIZE = 0.001f;

        public void Update(GameSystem gameSystem, float deltaTime)
        {
            Boat boat = gameSystem.Boat;
            float sumOfHoleSizes = 0;

            foreach (Hole hole in boat.HolesHull)
            {
                hole.Size += INCREMENT_HOLE_SIZE * deltaTime;
                sumOfHoleSizes += hole.Size;
            }

            boat.Speed = 1 - (SPEED_DECREASE * sumOfHoleSizes);
        }
    }
}
