using System.Collections;
using UnityEngine;

public class PlantPotScript : MonoBehaviour
{

    public enum growthState
    {
        EMPTY,
        IMMATURE,
        RIPE
    }

    public Sprite[] growthSprites;
    public growthState state;

    public float currentGrowthTime = 0f;
    public float maturityTime = 5.0f;
    public float savedGrowthTime; // saved when player exits game

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = growthState.EMPTY;
        StartCoroutine(checkGrowth(state));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            state = growthState.IMMATURE;
        }
    }

    private IEnumerator checkGrowth(growthState state)
    {
        while (true) {
            switch (state)
            {
                case growthState.EMPTY:
                    Debug.Log("No plant");
                    break;

                case growthState.IMMATURE:

                    if (currentGrowthTime < maturityTime)
                    {
                        currentGrowthTime += Time.deltaTime;
                    }
                    else
                    {
                        state = growthState.RIPE;
                    }
                    Debug.Log("Plant Growing");
                    break;
                case growthState.RIPE:
                    Debug.Log("Plant Ripe");
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
