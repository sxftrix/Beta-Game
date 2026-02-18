using System;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    [Header("Garden Initialization")]
    public GameObject potPrefab; 
    public Transform gridParent;
    public int gardenLevel = 1;
    public int MAX_POTS = 20;
    private List<PlantPot> pots = new List<PlantPot>();

    [Header("Pot Grid Settings")]
    public int columns;
    public float spacingX;
    public float spacingY;

    [Header("Global Mutliplier Variables")]
    /// NOTE:
    /// PLACEHOLDER TO SIMULATE SETTLER MULTIPLIERS AND BOOST MULTIPLIERS.
    /// TO BE REPLACED WITH THE PROPER SYSTEM LATER IN DEVELOPMENT.
    public float totalMultiplier;
    public float[] settlerMultipliers = new float[2];
    public List<float> boostMultipliers = new List<float>();

    public static GardenManager Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        InitializeGarden();
        LoadGardenState();
    }

    /// SUMMARY
    /// This method is used to initialize the plant pots in a grid. With variable spacing, to be adjusted
    /// based on the size of the sprite used.
    /// Reference to [gardenLevel] variable changes the amount of pots. Maximum is declared by [MAX_POTS]
    void InitializeGarden()
    {
        // Clearing logic for testing purposes
        foreach (PlantPot pot in pots) Destroy(pot); 
        pots.Clear();

        // Determines the amount of pots to spawn based on [gardenLevel]
        int potsToSpawn = Mathf.Min(gardenLevel, MAX_POTS);

        // loop to spawn pots
        for (int i = 0; i < potsToSpawn; i++)
        {
            int column = i % columns;
            int row = i / columns;

            GameObject newPot = Instantiate(potPrefab, gridParent);
            
            float posX = column * spacingX;
            float posY = -row * spacingY; 
            
            newPot.transform.localPosition = new Vector3(posX, posY, 0);
            newPot.name = $"Pot_{i}";
            PlantPot potScript = newPot.GetComponent<PlantPot>();
            pots.Add(potScript);
        }
    }

    /// SUMMARY
    /// This method is for calculation of the boosts provided. Placeholder variables for testing.
    /// To be replaced with the proper systems later in development.
    public float TotalGrowthMultiplier()
    {
        float settlerSum = 1.0f;
        float boostProduct = 1.0f;
        foreach (float val in settlerMultipliers) settlerSum += val;
        foreach (float val in boostMultipliers) if (val > 1) boostProduct *= val;
        totalMultiplier = settlerSum * boostProduct;
        return totalMultiplier;
    }

    /// SUMMARY
    /// This method is used to save the state of the garden, including which pots are planted and their growth progress.
    /// Currently uses PlayerPrefs for simplicity.
    void SaveGardenState()
    {
        for (int i = 0; i < pots.Count; i++)
        {
            int planted = pots[i].currentState != PlantPot.PlantState.EMPTY ? 1 : 0;
            PlayerPrefs.SetInt($"Pot_{i}_Planted", planted);

            PlayerPrefs.SetFloat($"Pot_{i}_Progress", pots[i].growthProgress);
        }
        PlayerPrefs.Save();
    }

    void LoadGardenState()
    {
        for (int i = 0; i < pots.Count; i++)
        {
            if (PlayerPrefs.HasKey($"Pot_{i}_Planted"))
            {
                int planted = PlayerPrefs.GetInt($"Pot_{i}_Planted");
                if (planted == 1)
                {
                    pots[i].currentState = PlantPot.PlantState.GROWING;
                    pots[i].growthProgress = PlayerPrefs.GetFloat($"Pot_{i}_Progress");
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveGardenState();
    }
}
