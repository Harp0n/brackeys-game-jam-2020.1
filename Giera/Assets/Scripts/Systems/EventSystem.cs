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
        public static float easyThresholdHolesHull = 0.3f; // how many hull holes per second on easiest level
        public static float hardThresholdHolesHull = 0.7f;  // how many hull holes per second on hardest level

        public static float easyThresholdHolesMast = 0.1f; // how many mast holes per second on easiest level
        public static float hardThresholdHolesMast = 0.3f;  // how many mast holes per second on hardest level

        public static float easyThresholdPirates = 0.05f; // how many pirates per second on easiest level
        public static float hardThresholdPirates = 0.15f; // how many pirates per second on hardest level

        const float precision = 1000000f;

        public void Update(GameSystem gameSystem, float deltaTime)
        {
            Boat boat = gameSystem.Boat;
            int HowHard = gameSystem.HowHard;

            //holes hull
            if (ShouldEventOccur(easyThresholdHolesHull, hardThresholdHolesHull, HowHard, deltaTime))
            {
                boat.HolesHull[UnityEngine.Random.Range(0, boat.HolesHull.Count)].IsPatchedUp = false;
            }

            //holes mast
            if (ShouldEventOccur(easyThresholdHolesMast, hardThresholdHolesMast, HowHard, deltaTime))
            {
                boat.HolesMast[UnityEngine.Random.Range(0, boat.HolesMast.Count)].IsPatchedUp = false;
            }

            //pirates
            if(ShouldEventOccur(easyThresholdPirates, hardThresholdPirates, HowHard, deltaTime))
            {
                gameSystem.SpawnPirate();
            }
        }

        private bool ShouldEventOccur(float easyThreshold, float hardThreshold, int HowHard, float deltaTime)
        {
            float chance = (hardThreshold - easyThreshold) * (HowHard - 1) / 9.0f + easyThreshold;
            return UnityEngine.Random.Range(0, precision) < deltaTime * chance * precision;
        }
    }
}