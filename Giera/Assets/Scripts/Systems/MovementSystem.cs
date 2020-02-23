using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Systems
{
    public class MovementSystem : ISystem
    {
        private readonly float SPEED_DECREASE = 6000;
        private readonly float INCREMENT_HOLE_SIZE = 0.000002f;

        public void Update(GameSystem gameSystem, float deltaTime)
        {
            Boat boat = gameSystem.Boat;
            float sumOfHoleSizes = 0;

            foreach (Hole hole in boat.HolesMast)
            {
                if (!hole.IsPatchedUp)
                {
                    hole.Size += INCREMENT_HOLE_SIZE * deltaTime;
                    sumOfHoleSizes += hole.Size;
                }
            }
           
            boat.Speed = (1 - (SPEED_DECREASE * sumOfHoleSizes)) * gameSystem.UIManager.BoatData.BoatSpeed;
        }
    }
}
