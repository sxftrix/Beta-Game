using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Building))]
public class ResourceGenerator : MonoBehaviour
{
    [Header("REQUIRED: Generator Parameters")]
    [SerializeField] private Resource targetResource;
    [SerializeField] private int baseGainPerSecond;

    [Header("REFERENCE ONLY DON'T EDIT")] 
    [SerializeField] private Building thisBuilding;
    [SerializeField] private int _gainPerSecond;
    
    /// <summary>
    /// MONOBEHAVIOR METHODS
    /// </summary>
    
    private void OnEnable()
    {
        Building.OnBuildingLevelUp += UpdateGain;
        thisBuilding = GetComponent<Building>();
        if (thisBuilding == null)
        {
            Debug.Log("Resource Generator in " + thisBuilding.GetName + ": No Building Component found in GameObject");
        }
    }

    private void Start()
    {
        targetResource.Unlock();
        if (MainGameManager.Instance.turboModeOn)
        {
            baseGainPerSecond *= 5;
        }
        _gainPerSecond = baseGainPerSecond;
        StartGenerator();
    }

    private void OnDisable()
    {
        Building.OnBuildingLevelUp -= UpdateGain;
        StopGenerator();
    }
    
    /// <summary>
    /// RESOURCE GENERATION METHODS
    /// </summary>
    
    private void StartGenerator()
    {
        InvokeRepeating(nameof(Gain), 1, 1);
    }
    
    private void Gain()
    {
        targetResource.Gain(_gainPerSecond);
    }
    
    private void UpdateGain(Building eventSource, int level)
    {
        if (eventSource == thisBuilding)
        {
            _gainPerSecond = baseGainPerSecond * level;
        }
    }

    private void StopGenerator()
    {
        CancelInvoke(nameof(Gain));
    }
}
