using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject BeastSpawner, TreeSpawner;

    public int maxScrapEarnable;
    public int totalScrapEarned;
    public int totalGoldEarned;
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

    void Start()
    {
        InvokeRepeating("IncreaseDifficulty", 2.5f, 2.5f);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    public void GainScrap(int gain)
    {
        totalScrapEarned += gain;
        UIManager.Instance.UpdateScrapText(totalScrapEarned);
    }

    public void GainGold(int gain)
    {
        totalGoldEarned += gain;
    }

    public void IncreaseDifficulty()
    {
        EntitySpawner beastSpawner = BeastSpawner.GetComponent<EntitySpawner>();

    }

    public bool reachedMaxScrap()
    {
        return totalScrapEarned == maxScrapEarnable;
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }
}
