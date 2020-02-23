using Assets.Logics.Map;
using Assets.Scripts.MissionsGenerator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem
{
    private UIManager uiManager;
    public bool IsQuestActive;
    public QuestSystem(UIManager uiManager)
    {
        this.uiManager = uiManager;
        IsQuestActive = false;
    }
    public void UpdateQuest() // called after returning
    {
        IsQuestActive = true;
        if (uiManager.BoatData.Cargo.Quantity == 0.0f) // if returned with nothing
        {
            Debug.Log("Returned with nothing");
            StartQuest();
            IsQuestActive = false;
        }
        else
        {
            Location targetLocation = uiManager.BoatData.Cargo.Destination;
            Location currentLocation = uiManager.WorldMap.CurrentLocation.Location;
            if (currentLocation.Equals(targetLocation)) // finish quest
            {
                IsQuestActive = false;
                uiManager.BoatData.UnloadCargo();
                StartQuest();
            }
        }
    }
    private void StartQuest()
    {
        Debug.Log("Starting quest");
        Mission mission = GeneratorMission.GenerateMission(uiManager.WorldMap, uiManager.costPerCargo);
        uiManager.BoatData.Cargo = mission.Cargo;
    }
}
