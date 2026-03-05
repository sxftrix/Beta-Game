using System;
using TMPro;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    public enum PlantState { EMPTY, GROWING, RIPE }
    public PlantState currentState = PlantState.EMPTY;
    public int potID;

    [Header("Settings")]
    public float fullGrowthTimeMinutes = 5f;

    // De-comment below when implementing proper sprites for each growth stage
    // public Sprite emptySprite, growingSprite, ripeSprite;

    public float multiplier; // To be set by GardenManager when initializing pots
    
    [Header("References")]
    public SpriteRenderer potRenderer;
    public TextMeshPro timerText;
    private DateTime plantStartTime;

    /// <summary>
    /// Handles the growth process of the plant. When in the GROWING state, it calculates the elapsed time and 
    /// updates the growth progress. Once the required growth time is reached, it transitions to the RIPE state. 
    /// The visual representation and timer are updated accordingly.
    /// </summary>
    void Update()
    {
        if (currentState == PlantState.GROWING)
        {
            Grow();
        }
    }

    /// <summary>
    /// When the pot is empty, the player can plant. It checks if the player has enough resources, then starts the planting process.
    /// When the pot is ripe, the player can collect the plant, which adds resources and resets the pot to empty.
    /// </summary>
    public void OnMouseDown() {
        switch (currentState) {
            case PlantState.EMPTY: StartPlanting(); break;
            case PlantState.RIPE: CollectPlant(); break;
        }
    }

    void Grow()
    {
        TimeSpan elapsed = DateTime.Now - plantStartTime;
        double totalRequiredSeconds = (fullGrowthTimeMinutes * 60) / multiplier;
        
        if (elapsed.TotalSeconds >= totalRequiredSeconds) {
            SetState(PlantState.RIPE);
        } else {
            UpdateTimerUI(totalRequiredSeconds - elapsed.TotalSeconds);
        }
    }

    private void SetState(PlantState newState) {
        currentState = newState;
        switch (newState) {
            case PlantState.EMPTY:
                potRenderer.color = Color.red;
                timerText.text = "";
                break;
            case PlantState.GROWING:
                potRenderer.color = Color.yellow;
                break;
            case PlantState.RIPE:
                potRenderer.color = Color.green;
                timerText.text = "READY!";
                break;
        }
    }

    /// <summary>
    /// Updates the timer UI to show the remaining time until the plant is ripe. Formats the time in hh:mm:ss.
    /// </summary>
    /// <param name="secondsLeft"> Amount of seconds left before the plant finishes growing. </param>
    void UpdateTimerUI(double secondsLeft) {
        TimeSpan t = TimeSpan.FromSeconds(secondsLeft);
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
    }

    void StartPlanting() {
        if (Resources.Instance.SpendResources(1)) {
            plantStartTime = DateTime.Now;
            SavePlantGrowth();
            SetState(PlantState.GROWING);
        }
    }

    void CollectPlant() {
        Resources.Instance.AddResources(GetYield());
        PlayerPrefs.DeleteKey($"Pot_{potID}_Time"); 
        SetState(PlantState.EMPTY);
    }

    int GetYield()
    {
        int baseYield = 2;
        int totalYield = baseYield + (GardenManager.Instance.gardenLevel - 1);
        return totalYield;
    }

    /// <summary>
    /// Saves the plant's growth start time to PlayerPrefs, allowing the growth progress to be maintained across game sessions.
    /// The time is stored as a binary string representation of the DateTime object. When loading, it retrieves this value and
    /// converts it back to a DateTime object to calculate the growth progress based on the elapsed time since planting.
    /// 
    /// Using PlayerPerfs for now. Preferably, we should use file persistence or a proper save system for better performance.
    /// </summary>
    public void SavePlantGrowth() {
        PlayerPrefs.SetString($"Pot_{potID}_Time", plantStartTime.ToBinary().ToString());
    }

    public void LoadSavedGrowth() {
        string savedTime = PlayerPrefs.GetString($"Pot_{potID}_Time", "");
        if (!string.IsNullOrEmpty(savedTime)) {
            plantStartTime = DateTime.FromBinary(Convert.ToInt64(savedTime));
            SetState(PlantState.GROWING);
            Grow();
        } else {
            SetState(PlantState.EMPTY);
        }
    }
}
