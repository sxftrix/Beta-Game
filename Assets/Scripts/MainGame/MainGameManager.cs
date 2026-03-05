using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainGameManager : SerializedMonoBehaviour
{
    public static MainGameManager Instance;

    [Header("REQUIRED: Prefabs and Parameters")] 
    [SerializeField] private GameObject mainSettlementPrefab;
    [DictionaryDrawerSettings(KeyLabel = "Prefab", ValueLabel = "Unlocks at Main Level")]
    [SerializeField] private Dictionary<GameObject, int> prefabList = new Dictionary<GameObject, int>();
    [SerializeField] private GameObject ConstructButton;
    
    [Header("REQUIRED: Build Locations")]
    [SerializeField] private List<BuildingLocation> buildingLocations = new List<BuildingLocation>();
    
    [Header("Turbo Mode: Turn on to boost Resource Gain by 2x")]
    [SerializeField] public bool turboModeOn;
    
    [Header("REFERENCE ONLY DON'T EDIT")]
    public float prestigeMultiplier;
    
    private int buildingLevelCap;
    private GameObject toConstruct;
    private Transform constructLocation;
    
    /// <summary>
    /// MONOBEHAVIOR METHODS
    /// </summary>
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Building.OnBuildingLevelUp += OnMainLevelUp;
        EnableConstruction(mainSettlementPrefab);
    }
    
    /// <summary>
    /// BUILDING CONSTRUCTION Methods
    /// </summary>
    
    private void OnMainLevelUp(Building eventSource, int level)
    {
        if (eventSource.GetName == "Main")
        {
            buildingLevelCap = level;
            foreach (var building in prefabList)
            {
                if (building.Value <= level)
                {
                    EnableConstruction(building.Key);
                }
            }
        }
    }

    private void EnableConstruction(GameObject buildingPrefab)
    {
        toConstruct = buildingPrefab;
        var buildingData = toConstruct.GetComponent<Building>();
        foreach (var loc in buildingLocations)
        {
            if (buildingData.GetName == loc.Building)
            {
                constructLocation = loc.location;
                ConstructButton.transform.position = loc.buttonLocation.position;
                ConstructButton.SetActive(true);
            }
        }
    }

    public void ConstructBuilding()
    {
        if (toConstruct != null && constructLocation != null)
        {
            var building = Instantiate(toConstruct, constructLocation);
            building.transform.position = constructLocation.position;
        }
        ConstructButton.SetActive(false);
        ConstructButton.transform.position = Vector2.zero;
    }

    /// <summary>
    /// GET Methods
    /// </summary>

    public int GetLevelCap() => buildingLevelCap;
}
