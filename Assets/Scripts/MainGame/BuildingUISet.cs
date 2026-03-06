using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class BuildingUISet : MonoBehaviour
{
    [Header("REQUIRED: Parameters")] 
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private Button upgradeButton;

    private Building targetBuilding;
    
    public void InitializeUI(Building source)
    {
        targetBuilding = source;
        gameObject.SetActive(true);
        upgradeButton.onClick.AddListener(OnUpgradePress);
        UpdateLevel(targetBuilding, targetBuilding.GetLevel);
    }

    public void OnEnable()
    {
        Building.OnBuildingLevelUp += UpdateLevel;
        Building.OnCostsChange += UpdateCosts;
    }
    
    public void OnDisable()
    {
        Building.OnBuildingLevelUp -= UpdateLevel;
        Building.OnCostsChange -= UpdateCosts;
    }

    private void UpdateLevel(Building eventSource, int level)
    {
        if (eventSource == targetBuilding)
        {
            levelText.text = level.ToString();
        }
    }

    public void UpdateCosts(Building eventSource, Dictionary<Resource, int> newCosts)
    {
        if (eventSource == targetBuilding)
        {
            upgradeCostText.text = "";
            foreach (var key in newCosts.Keys)
            {
                if (key.IsUnlocked())
                {
                    upgradeCostText.text += newCosts[key] + " " + key.GetName() + " ";
                }
            }
        }
    }

    void OnUpgradePress()
    {
        targetBuilding.TryUpgrade();
    }
    
}
