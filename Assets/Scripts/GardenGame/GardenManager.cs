using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public static GardenManager Instance { get; private set; }

    [Header("Garden Settings And Initialization")]
    public int gardenLevel = 1;
    public int maxPots = 20;
    public Transform gridParent;
    public GameObject potPrefab;
    private List<PotScript> pots = new List<PotScript>();

    [Header("Pot Grid Settings")]
    public int columns;
    public float spacingX;
    public float spacingY;

    [Header("Global Multiplier Variables")]
    // Placeholder to simulate settler multipliers and boost multipliers.
    // To be replaced with the proper system later in development.
    public float totalMultiplier;
    public List<float> boostMultipliers = new List<float>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        calculateTotalMultiplier();
        InitializeGarden(); 
        RefreshGarden(); 
    }

    /// <summary>
    /// Initializes the full garden layout immediately, then disables them after initialization.
    /// </summary>
    private void InitializeGarden()
    {
        for (int i = 0; i < maxPots; i++)
        {
            int column = i % columns;
            int row = i / columns;

            GameObject newPot = Instantiate(potPrefab, gridParent);
            newPot.transform.localPosition = new Vector3(column * spacingX, -row * spacingY, 0);
            
            PotScript potScript = newPot.GetComponent<PotScript>();
            potScript.potID = i; 
            potScript.multiplier = totalMultiplier;
            pots.Add(potScript);
            
            // Hide them by default
            newPot.SetActive(false); 
        }
    }

    /// <summary>
    /// Calculates growth multiplier. Settler multipliers are additive, while boost multipliers are multiplicative.
    /// </summary>
    public void calculateTotalMultiplier()
    {
        float boostProduct = 1.0f;
        foreach (float val in boostMultipliers) if (val > 1) boostProduct *= val;
        totalMultiplier = boostProduct;
    }

    /// <summary>
    /// Activates a number of pots based on the current garden level. Loads the saved growth state for each active pot.
    /// </summary>
    private void RefreshGarden()
    {
        for (int i = 0; i < pots.Count; i++)
        {
            // Only activate pots within the current level range
            bool isUnlocked = i < gardenLevel;
            pots[i].gameObject.SetActive(isUnlocked);

            if (isUnlocked)
            {
                pots[i].LoadSavedGrowth(); // Load saved growth for active pots
            }
        }
    }
}
