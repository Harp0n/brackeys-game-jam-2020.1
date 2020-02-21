using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Assets.Logics.Systems
{

    public class EventSystem : ISystem
    {
        public static float easyThreshold = 0.996f;
        public static float hardThreshold = 0.98f;
        public void Update(GameSystem gameSystem, float deltaTime)
        {
            Boat boat = gameSystem.Boat;
            int HowHard = gameSystem.HowHard;
            float chance = (hardThreshold - easyThreshold) * (HowHard - 1) / 9.0f + easyThreshold;
            
            if (UnityEngine.Random.Range(0, 1000) > chance*1000)
            {

                if (UnityEngine.Random.Range(0, 20) < 1)
                {
                    if (boat.HolesMast.Count > 0)
                        boat.HolesMast[UnityEngine.Random.Range(0, boat.HolesMast.Count)].IsPatchedUp = false;
                }

                if (boat.HolesHull.Count > 0)
                    boat.HolesHull[UnityEngine.Random.Range(0, boat.HolesHull.Count)].IsPatchedUp = false;
            }
        }
    }
}