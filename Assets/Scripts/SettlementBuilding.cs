using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class SettlementBuilding : MonoBehaviour
{
    [Header("Required Parameters")]
    [SerializeField] private string buildingName;
    [SerializeField] private string resourceType;
    [SerializeField] private int upgradeMultiplier;
    [SerializeField] private int startingGain;
    [SerializeField] private bool isGenerator;
    [SerializeField] private bool isMain;
    [SerializeField] private int unlockLevel;
    
    //will contain all the resource costs needed to upgrade to next level
    private Dictionary<string, int> _upgradeCosts = new Dictionary<string, int>();
    
    //Events for updating ui text
    public static event Action<Dictionary<string, int>, string> OnCostChanged;
    public static event Action<string, int> OnBuildingUpgraded;
    public static event Action<string> OnBuildingEnable;
    
    private int _gainPerSecond;
    private int _currentLevel;
    private int _upgradeCost;

    private void Awake()
    {
        GlobalGameManager.OnMainLevelUp += UnlockBuilding;
    }
    
    private void InitializeSettlement()
    {
        Resources.OnUnlockResource += OnUnlockResource;
        LevelUp();
        if (isGenerator)
        {
            Resources.Instance.AddNewResource(resourceType);
            _gainPerSecond = startingGain;
            StartGainPerSecond();
        }
        InitializeCosts();
    }

    private void InitializeCosts()
    {
        foreach (var resource in Resources.Instance.GetResourceNames())
        {
            _upgradeCosts.TryAdd(resource, 1);
        }
        SetAllCosts();
    }

    private void SetAllCosts()
    {
        foreach (var key in _upgradeCosts.Keys.ToList())
        {
            _upgradeCosts[key] = SetNextUpgradeCost();
        }
    }
    
    public void StartGainPerSecond()
    {
        InvokeRepeating(nameof(GainResource), 1, 1);
    }
    
    public void GainResource()
    {
        Resources.Instance.GainResource(resourceType, _gainPerSecond);
        Debug.Log("Gain " + resourceType + ": " + _gainPerSecond);
    }
    
    public void StopGain()
    {
        CancelInvoke("GainResource");
    }

    private int SetNextUpgradeCost()
    {
        var exponent = _currentLevel - 1;
        int cost = (int)Math.Round((upgradeMultiplier * (Math.Pow(1.5, exponent))));
        OnCostChanged?.Invoke(_upgradeCosts, buildingName);
        return cost;
    }

    public void UpgradeBuilding()
    {
        //Cant upgrade if even one resource is lacking
        foreach (var cost in _upgradeCosts)
        {
            if (Resources.Instance.GetResource(cost.Key.ToString()) < cost.Value)
            {
                Debug.Log("Not enough Resources to upgrade to Level " + _currentLevel);
                return;
            }
        }
        
        //If resources pass checks, upgrading is done
        foreach (var cost in _upgradeCosts.Keys.ToList())
        {
            Resources.Instance.LoseResource(cost, _upgradeCosts[cost]);
            Debug.Log("Pay " + cost + ": " + _upgradeCosts[cost]);
        }
        LevelUp();
        SetAllCosts();
        OnBuildingUpgraded?.Invoke(buildingName, _currentLevel);
        Debug.Log("Building is now Level: " + _currentLevel);
        Debug.Log("Building now gains " + resourceType + " at " + _gainPerSecond + " per second.");
    }
    
    
    //when new resource is unlocked (i.e, when a new generator is enabled for player) all buildings will now require the new resource to be upgraded).
    private void OnUnlockResource(string resourceName)
    {
        _upgradeCosts.Add(resourceName, SetNextUpgradeCost());
    }

    private void LevelUp()
    {
        _currentLevel++;
        if (isMain)
        {
            GlobalGameManager.instance.UpdateLeveL(_currentLevel);
        }

        if (isGenerator)
        {
            _gainPerSecond += startingGain;
        }
    }
    
    //unlocks settlement
    private void UnlockBuilding(int mainLevel)
    {
        if (mainLevel == unlockLevel)
        {
            this.gameObject.SetActive(true);
            InitializeSettlement();
            OnBuildingEnable?.Invoke(buildingName);
        }
    }
}
