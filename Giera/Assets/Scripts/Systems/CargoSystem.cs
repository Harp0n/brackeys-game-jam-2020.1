﻿using Assets.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Systems
{
    public class CargoSystem : ISystem
    {
        private float HowMuchCargoToLose { get; set; } = 0.0001f;
        public CargoSystem(){ }

        public CargoSystem(float howMuchCargoToLose)
        {
            if(howMuchCargoToLose > 0) HowMuchCargoToLose = howMuchCargoToLose;
        }
        public void Update(GameSystem gameSystem, float deltaTime)
        {
            float waterPercentageOnBoard = gameSystem.Boat.GetWaterOnBoard();
            int result = (int)(waterPercentageOnBoard * gameSystem.Boat.Cargo.Quantity * HowMuchCargoToLose);
            gameSystem.Boat.Cargo.Quantity -= result;
        }
    }
}