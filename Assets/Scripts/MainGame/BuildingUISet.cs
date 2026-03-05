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

    private Building sourceBuilding;
    public void InitializeUI(Building source)
    {
        sourceBuilding = source;
        gameObject.SetActive(true);
        upgradeButton.onClick.AddListener(OnUpgradePress);
    }

    public void UpdateLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void UpdateCosts(Dictionary<Resource, int> newCosts)
    {
        foreach (var cost in newCosts)
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
        sourceBuilding.TryUpgrade();
    }
    
}
