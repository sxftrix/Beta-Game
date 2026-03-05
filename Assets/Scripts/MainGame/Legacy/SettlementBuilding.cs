using Sirenix.OdinInspector;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Rendering.LookDev;

public class SettlementBuilding : SerializedMonoBehaviour
{
    [Header("REQUIRED: Settlement Parameters")]
    [SerializeField] private string buildingName;
    [SerializeField] private string resourceType;
    [SerializeField] private int baseMultiplier;
    [SerializeField] private int startingGain;
    [SerializeField] private bool isGenerator;
    [SerializeField] private bool isMainSettlement;
    [SerializeField] private GameObject uiSet;
    
    [Header("Upgrade Costs Dictionary (For Reference Only")]
    [SerializeField] private Dictionary<string, int> _upgradeCosts = new Dictionary<string, int>();
    
    [Header("Turbo Mode: Turn on to boost Resource Gain by 2x")]
    [SerializeField] private bool TurboMode;

    //For communication between scripts w/o coupling
    public static event Action<int> OnMainLevelUp;
    public static event Action<string, Dictionary<string, int>> OnCostChanged;
    public static event Action<string, int> OnBuildingUpgraded;

    //For Displaying Messages
    public static event Action<string> OnMessageTrigger;
    private string message;

    private int _gainPerSecond;
    private int _currentLevel;
    private int _upgradeCost;
    
    private void OnEnable()
    {
        uiSet.SetActive(true);
        InitializeSettlement();
        ResourceManager.OnResourceUnlocked += AddCosts;
    }
    
    private void InitializeSettlement()
    {
        _currentLevel = 1;
        OnBuildingUpgraded?.Invoke(buildingName, _currentLevel);
        {
            _gainPerSecond = startingGain;
            ResourceManager.Instance.AddNewResource(resourceType);
            StartGainPerSecond();
            if (isMainSettlement)
            {
                OnMainLevelUp?.Invoke(_currentLevel);
            }
        }
        InitializeCosts();
    }
    
    public void StartGainPerSecond()
    {
        InvokeRepeating(nameof(GainResource), 1, 1);
    }

    public void StopGain()
    {
        CancelInvoke("GainResource");
    }
    public void GainResource()
    {
        if (TurboMode)
        {
            ResourceManager.Instance.GainResource(resourceType, (_gainPerSecond * 2));
            Debug.Log("Gain " + resourceType + ": " + (_gainPerSecond * 2));
        }
        else
        {
            ResourceManager.Instance.GainResource(resourceType, _gainPerSecond);
            Debug.Log("Gain " + resourceType + ": " + _gainPerSecond);
        }
    }

    private int SetNextUpgradeCost()
    {
        var exponent = _currentLevel - 1;
        var nextCost = (int)Math.Round((baseMultiplier * (Math.Pow(1.5, exponent))));
        return nextCost;
    }
    
    private void InitializeCosts()
    {
        foreach (var key in ResourceManager.Instance.GetCurrentResourceTypes())
        {
            _upgradeCosts.TryAdd(key, 1);
        }
        SetNewCosts();
    }

    private void AddCosts(string resourceName)
    {
        _upgradeCosts.TryAdd(resourceName, 1);
        SetNewCosts();
    }
    
    private void SetNewCosts()
    {
        var keys = new List<string>(_upgradeCosts.Keys);
        foreach (var key in keys)
        {
            _upgradeCosts[key] = SetNextUpgradeCost();
        }
        OnCostChanged?.Invoke(buildingName, _upgradeCosts);
    }
    
    private bool Upgradeable()
    {
        var keys = new List<string>(_upgradeCosts.Keys);
        foreach (var key in keys)
        {
            if (ResourceManager.Instance.GetResource(key) < _upgradeCosts[key])
            {
                return false;
            }
        }
        return true;
    }

    public void UpgradeBuilding()
    {
        if (Upgradeable())
        {
            var keys = new List<string>(_upgradeCosts.Keys);
            foreach (var key in keys)
            {
                ResourceManager.Instance.LoseResource(key, _upgradeCosts[key]);
                Debug.Log("Pay " + key + ": " + _upgradeCosts[key]);
            }
            _currentLevel++;
            if (isMainSettlement)
            {
                Debug.Log("Building is now Level: " + _currentLevel);
                Debug.Log("Building now gains " + resourceType + " at " + _gainPerSecond + " per second.");
                OnMainLevelUp?.Invoke(_currentLevel);
            }
            OnBuildingUpgraded?.Invoke(buildingName, _currentLevel);
            _gainPerSecond += startingGain;
            SetNewCosts();
        }
        else
        {
            message = "Not enough Resources to upgrade " + buildingName + " to Level " + (_currentLevel + 1);
            OnMessageTrigger?.Invoke(message);
            Debug.Log(message);
        }
    }
}
