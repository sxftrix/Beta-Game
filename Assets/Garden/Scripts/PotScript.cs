using UnityEngine;

public class PotScript : MonoBehaviour
{
    public bool hasPlant;
    public GameObject plant;
    public PotObjectState potState;

    [SerializeField] private float plantMaxGrowthTime;
    [SerializeField] private float plantCurrentGrowthTime;
    [SerializeField] private float plantGrowthSpeed;

    private void Start()
    {
        if (!hasPlant) potState = PotObjectState.EMPTY;
        else if (plantCurrentGrowthTime != plantMaxGrowthTime) potState = PotObjectState.PLANT_GROWING;
        else potState = PotObjectState.PLANT_FULL_GROWN;
    }

    private void Update()
    {
        switch (potState) 
        { 
            case PotObjectState.EMPTY:
                break;
            case PotObjectState.PLANT_GROWING:
                continueGrowingPlant(plantCurrentGrowthTime);
                break;
            case PotObjectState.PLANT_FULL_GROWN:
                showClaimablePlant();
                break;
            default:
                break;
        }
    }

    private void continueGrowingPlant(float currentGrowthTime)
    {
        currentGrowthTime += Time.deltaTime;
        if (currentGrowthTime == plantMaxGrowthTime) potState = PotObjectState.PLANT_FULL_GROWN;
    }

    private void showClaimablePlant()
    {
        
    }
}
