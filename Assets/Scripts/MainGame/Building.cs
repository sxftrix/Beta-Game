using System;
using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Building : SerializedMonoBehaviour
{
    [Header("Required Parameters")] 
    [SerializeField] private string buildingName;
    [SerializeField] private int baseCostMultiplier;
    [SerializeField] private BuildingUISet UISet;
    [SerializeField] private bool isMain;
    
    [Header("REFERENCE ONLY DON'T EDIT")] 
    [SerializeField] private int buildingLevel;
    [SerializeField] private Dictionary<Resource, int> upgradeCosts = new Dictionary<Resource, int>(); 
    
    private int currentCostMultipler;
    private int _gainPerSecond;
    private int _upgradeCost;
    
    public static event Action<Building, int> OnBuildingLevelUp;

    /// <summary>
    /// MONOBEHAVIOR METHODS
    /// </summary>
    
    private void Awake()
    {
        Resource.OnUnlockResource += UpdateCosts;
        if (isMain)
        {
            var mainComponent = gameObject.AddComponent<MainSettlement>();
        }
        LevelUp();
        InitializeCosts();
        UISet.InitializeUI(this);
    }

    /// <summary>
    /// UPGRADE COSTS METHODS
    /// </summary>
    
    private void InitializeCosts()
    {
        foreach (Resource resource in InventoryManager.Instance.Resources)
        {
            upgradeCosts.TryAdd(resource, 0);
        }
        UpdateCosts();
    }
    
    private void UpdateCosts()
    {
        var resourceKeys = new List<Resource>(upgradeCosts.Keys);
        
        foreach (var resource in resourceKeys)
        {
            if (resource.IsUnlocked())
            {
                upgradeCosts[resource] = SetNextUpgradeCost();
            }
            else
            {
                upgradeCosts[resource] = 0;
            }
        }
        UISet.UpdateCosts(upgradeCosts);
    }
    
    private int SetNextUpgradeCost()
    {
        var exponent = buildingLevel - 1;
        var nextCost = (int)Math.Round((baseCostMultiplier * (Math.Pow(1.5, exponent))));
        return nextCost;
    }
    
    /// <summary>
    /// UPGRADE METHODS
    /// </summary>

    public void TryUpgrade()
    {
        if (Upgradable())
        {
            var resourceKeys = new List<Resource>(upgradeCosts.Keys);
            foreach (var resource in resourceKeys)
            {
                resource.Spend(upgradeCosts[resource]);
            }
            LevelUp();
            UpdateCosts();
            return;
        }
        Debug.LogWarning("Couldn't upgrade building");
    }

    private void LevelUp()
    {
        buildingLevel++;
        OnBuildingLevelUp.Invoke(this, buildingLevel);
        UISet.UpdateLevel(buildingLevel);
        if (isMain)
        {
            MainSettlement.Instance.updateMainLevel(buildingLevel);
        }
    }

    private bool Upgradable()
    {
        if (!isMain && MainSettlement.Instance.GetMainLevel <=  buildingLevel)
        {
            return false;
        }
        foreach (var cost in upgradeCosts)
        {
            if (!cost.Key.CanSpend(cost.Value))
            {
                return false;
            }
        }
        
        return true;
    }

    /// <summary>
    /// GET METHODS
    /// </summary>
    
    public int GetLevel => buildingLevel;
    
    public string GetName => buildingName;
}
