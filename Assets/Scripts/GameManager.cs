using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject spawner;

    public int totalScrapEarned;
    public float timeElapsed;
    public float beastDifficultyModifier;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("IncreaseDifficulty", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public void GainScrap(int gain)
    {
        totalScrapEarned += gain;
        UIManager.Instance.UpdateScrapText(totalScrapEarned);
    }

    public void IncreaseDifficulty()
    {
        EntitySpawner beastSpawner = spawner.GetComponent<EntitySpawner>();
        beastSpawner.UpdateBeastSpeed(beastDifficultyModifier);
        if (beastSpawner.spawnDelay > 0.10f)
        {
            beastSpawner.spawnDelay -= 0.10f;
        }
    }
}
