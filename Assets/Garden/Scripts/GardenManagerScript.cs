using UnityEngine;

public class GardenManagerScript : MonoBehaviour
{
    public int gardenLevel; // private variables reference garden level
    public int MAX_POTS = 20;
    public GameObject potObject;

    private int currentPotCount;
    private int plantYield;

    private void Start()
    {
        currentPotCount = gardenLevel;
        plantYield = gardenLevel;

        CreatePotField(currentPotCount);
    }

    private void CreatePotField(int potCount)
    {
        
    }
}
