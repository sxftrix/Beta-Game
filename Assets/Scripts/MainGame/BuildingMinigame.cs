using System;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class BuildingMinigame : MonoBehaviour
{
    [Header("REQUIRED: Parameters")] 
    [SerializeField] private string minigameName; //must be the exact same as the name in GlobalMinigameHandler.minigameList
    [SerializeField] private GameObject minigameButton; //UI button to activate/deactivate depending on whether the building is unlocked.
    
    private Building sourceBuilding;

    private void Awake()
    {
        sourceBuilding = GetComponent<Building>();
        if (sourceBuilding != null)
        {
            Debug.LogWarning("BuildingMinigame in" + sourceBuilding.GetName + ": No Building Script");
        }
    }
    
    public static event Action<string> OnChooseMinigame;

    private void ChooseMinigame(string gameName)
    {
        OnChooseMinigame?.Invoke(gameName);
    }
}
