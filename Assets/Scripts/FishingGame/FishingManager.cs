using System.Collections;
using UnityEngine;

public enum FishingState { IDLE, FISHING, REELING }

/// <summary>
/// Manages the global state of the fishing minigame, progress tracking, and centralized boundary math.
/// 
/// Current Issues:
/// - Clamping logic may not work for custom sprites. I haven't tested it with anything other than the default bar sprite, but it uses 
/// the SpriteRenderer's bounds to calculate the MinY and MaxY. If the sprite has a frame or border, it might clip through that frame.
/// 
/// - The progress fill is centered in the progress bar and scales from there to indicate progress. Might want to change it to a bottom
/// to top fill instead.
/// </summary>
public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance { get; private set; }

    [Header("State Management")]
    public FishingState CurrentState = FishingState.IDLE;

    [Header("References")]
    [SerializeField] private SpriteRenderer fishingBarBG;
    [SerializeField] private Transform progressFill;
    [SerializeField] private GameObject fishObject;
    [SerializeField] private GameObject playerBarObject;

    [Header("Progress Settings")]
    [SerializeField] private float fillSpeed = 0.3f;
    [SerializeField] private float drainSpeed = 0.2f;
    [SerializeField] private float winThreshold = 1.1f;
    [SerializeField] private float respawnTimeMin = 3f;
    [SerializeField] private float respawnTimeMax = 5f;

    [Header("Calculated Bounds")]
    public float MinY;
    public float MaxY;

    private float _currentProgress = 0f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CalculateGlobalBounds();
    }

    void Update()
    {
        HandleProgress();
        CheckWinCondition();
    }

    /// <summary>
    /// Calculates the world-space Y limits based on the FishingBar's SpriteRenderer.
    /// </summary>
    private void CalculateGlobalBounds()
    {
        float bgHeight = fishingBarBG.bounds.size.y;
        float halfHeight = bgHeight / 2f;
        float centerOrder = fishingBarBG.transform.position.y;

        MaxY = centerOrder + halfHeight;
        MinY = centerOrder - halfHeight;
    }

    /// <summary>
    /// Updates progress bar scale and value based on whether the state is REELING or FISHING.
    /// </summary>
    private void HandleProgress()
    {
        if (CurrentState == FishingState.IDLE) return;

        if (CurrentState == FishingState.REELING)
            _currentProgress += fillSpeed * Time.deltaTime;
        else
            _currentProgress -= drainSpeed * Time.deltaTime;

        _currentProgress = Mathf.Clamp(_currentProgress, 0f, winThreshold);
        progressFill.localScale = new Vector3(1, Mathf.Clamp01(_currentProgress), 1);
    }

    private void CheckWinCondition()
    {
        if (_currentProgress >= winThreshold && CurrentState != FishingState.IDLE)
        {
            StartCoroutine(HandleSuccess());
        }
    }

    private IEnumerator HandleSuccess()
    {
        CurrentState = FishingState.IDLE;
        Debug.Log("Fish Caught!");
        fishObject.SetActive(false);
        _currentProgress = 0f;

        yield return new WaitForSeconds(Random.Range(respawnTimeMin, respawnTimeMax));
        RespawnFish();
    }

    /// <summary>
    /// Resets the fish position to a random valid spot within the bar's boundaries upon respawning.
    /// </summary>
    private void RespawnFish()
    {
        // Calculate the fish's half-height to ensure it stays in bounds
        float fishHalfHeight = fishObject.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        
        // Pick a random Y value between the manager's centralized Min and Max
        float randomSpawnY = Random.Range(MinY + fishHalfHeight, MaxY - fishHalfHeight);
        
        // Apply the new position and re-enable visuals
        fishObject.transform.position = new Vector3(fishObject.transform.position.x, randomSpawnY, fishObject.transform.position.z);
        
        fishObject.SetActive(true);
        CurrentState = FishingState.FISHING;
        Debug.Log("A new fish has appeared at a random depth!");
    }
}