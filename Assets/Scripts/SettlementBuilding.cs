using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Building : MonoBehaviour
{
    [Header("Required Parameters")]
    [SerializeField] private string buildingName;
    [SerializeField] private string resourceType;
    [SerializeField] private int upgradeMultiplier;
    [SerializeField] private int startingGain;
    [SerializeField] private bool isGenerator;
    
    private Dictionary<string, int> _upgradeCosts = new Dictionary<string, int>();
    
    public static event Action<string, int> OnCostChanged;
    public static event Action<string, int> OnBuildingUpgraded;
    
    private int _gainPerSecond;
    private int _currentLevel;
    private int _upgradeCost;
    
    private void InitializeSettlement()
    {
        _currentLevel = 1;
        if (isGenerator)
        {
            _gainPerSecond = startingGain;
            Resources.Instance.AddNewResource(resourceType);
            StartGainPerSecond();
            SetNextUpgradeCost();
        }
    }

    void Start()
    {
        InitializeSettlement();
    }
    
    public void GainResource()
    {
        Resources.Instance.GainResource(resourceType, _gainPerSecond);
        Debug.Log("Gain " + resourceType + ": " + _gainPerSecond);
    }

    private void SetNextUpgradeCost()
    {
        var exponent = _currentLevel - 1;
        _upgradeCost = (int)Math.Round((upgradeMultiplier * (Math.Pow(1.5, exponent))));
        OnCostChanged?.Invoke(buildingName, _upgradeCost);
        OnBuildingUpgraded?.Invoke(buildingName, _currentLevel);
    }

    public void StartGainPerSecond()
    {
        InvokeRepeating(nameof(GainResource), 1, 1);
    }

    public void StopGain()
    {
        CancelInvoke("GainResource");
    }

    public void UpgradeBuilding()
    {
        if (Resources.Instance.GetResource(resourceType) >= _upgradeCost)
        {
            Resources.Instance.LoseResource(resourceType, _upgradeCost);
            Debug.Log("Pay " + resourceType + ": " + _upgradeCost);
            _currentLevel++;
            _gainPerSecond += startingGain;
            Debug.Log("Building is now Level: " + _currentLevel);
            Debug.Log("Building now gains " + resourceType + " at " + _gainPerSecond + " per second.");
            SetNextUpgradeCost();
        }
        else
        {
            Debug.Log("Not enough Resources to upgrade to Level " + _currentLevel);
        }
    }
}
