using Assets.Logics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelSystem : ISystem
{
    public static float minDistance = 1f;
    public static float maxDistance = 1;
    private float travelledDistance;

    public TravelSystem()
    {
        Reset();
    }

    private void Reset()
    {
        travelledDistance = 0.0f;
    }

    public void Update(GameSystem gameSystem, float deltaTime)
    {
        int HowLong = gameSystem.HowLong;
        float distanceToTravel = (maxDistance - minDistance) * (HowLong - 1) / 9.0f + minDistance;
        float speed = gameSystem.Boat.Speed;
        travelledDistance += speed * deltaTime;
        gameSystem.JourneyPercentage = travelledDistance / distanceToTravel;
        if(travelledDistance >= distanceToTravel)
        {
            gameSystem.UIManager.FinishJourney();
            Reset();
        }

    }
}
