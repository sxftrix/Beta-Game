
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{

    // Plant Pot containers
    public GameObject plantArea;
    [SerializeField] private List<GameObject> allPots;

    public int gardenLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializePots(gardenLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializePots(int gardenLevel)
    {
        allPots.Clear();

        foreach (Transform row in plantArea.transform)
        {
            foreach (Transform pot in row.transform)
            {
                allPots.Add(pot.gameObject);
                pot.gameObject.SetActive(false);
            }
        }

        for (int i = 0;i < allPots.Count; i++)
        {
            allPots[i].SetActive(i < gardenLevel);
        }
    }
}
