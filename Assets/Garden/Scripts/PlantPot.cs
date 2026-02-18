using TMPro;
using UnityEngine;

public class PlantPot : MonoBehaviour
{
    // PlantState enum for clearer switch statements
    public enum PlantState { EMPTY, GROWING, RIPE }
    public PlantState currentState = PlantState.EMPTY;

    [Header("Growth Settings")]
    public float growthProgress = 0f;
    public float fullGrowthTime = 10f;

    [Header("Visuals")]
    public SpriteRenderer potRenderer;
    public TextMeshPro timerText;


    void Update()
    {
        if (currentState == PlantState.GROWING)
        {
            UpdateGrowth();
        }
        UpdateVisuals(); 
    }

    /// SUMMARY
    /// Simple visual indicator of growth state. 
    /// To be replaced with proper sprites and sprite changing in official ver.
    void UpdateVisuals()
    {
        switch (currentState)
        {
            case PlantState.EMPTY:
                potRenderer.color = Color.red;
                timerText.text = ""; 
                break;

            case PlantState.GROWING:
                potRenderer.color = Color.yellow;
                
                // Dynamic text display of time remaining
                // 1. Calculate seconds left
                float totalSecondsLeft = fullGrowthTime - (growthProgress / 100f * fullGrowthTime);
                
                // 2. Ensure it doesn't show negative numbers
                totalSecondsLeft = Mathf.Max(0, totalSecondsLeft);

                // 3. Format to hh:mm:ss
                System.TimeSpan t = System.TimeSpan.FromSeconds(totalSecondsLeft);
                
                // "D2" ensures a leading zero (e.g., "05" instead of "5")
                timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", 
                    t.Hours, 
                    t.Minutes, 
                    t.Seconds);
                timerText.fontSize = 2;
                break;

            case PlantState.RIPE:
                potRenderer.color = Color.green;
                timerText.fontSize = 2;
                timerText.text = "READY!";
                break;
        }
    }

    /// SUMMARY
    /// Constantly updates growth in a percentage form. 
    /// It references [fullGrowthTime] and [totalMultiplier] to allow for variable changes in growth progress.
    /// When growthProgress is 100%, it changes the [currentState] to Ripe to allow for collection.
    void UpdateGrowth()
    {
        float multiplier = GardenManager.Instance.TotalGrowthMultiplier();

        float growthThisFrame = (100f / fullGrowthTime) * multiplier * Time.deltaTime;
        growthProgress += growthThisFrame;

        if (growthProgress >= 100f)
        {
            growthProgress = 100f;
            currentState = PlantState.RIPE;
            Debug.Log($"<color=green>{gameObject.name}: Plant is fully grown!</color>");
        }
    }

    /// SUMMARY
    /// Switch for the plant state.
    /// StartPlanting() changes the [currentState] to GROWING to start growth and consume 1 resource.
    /// CollectPlant() changes the [currentState] to EMPTY to clear the pot and allow for planting the next plant and provide 2 resources.
    void OnMouseDown()
    {

        switch (currentState)
        {
            case PlantState.EMPTY:
                StartPlanting();
                break;
            case PlantState.GROWING:
                Debug.Log($"{gameObject.name}: Still growing... ({Mathf.Round(growthProgress)}%)");
                break;
            case PlantState.RIPE:
                CollectPlant();
                break;
        }
    }

    void StartPlanting()
    {
        if (!ResourceManager.Instance.CanAfford(1)) { Debug.Log("Not Enough Food!"); return; }
        
        ResourceManager.Instance.SpendResources(1);
        growthProgress = 0f;
        currentState = PlantState.GROWING;
    }

    void CollectPlant()
    {
        int level = GardenManager.Instance.gardenLevel;
        int totalYield = 2 + (level - 1);

        ResourceManager.Instance.AddResources(totalYield);

        growthProgress = 0f;
        currentState = PlantState.EMPTY;
    }
}
