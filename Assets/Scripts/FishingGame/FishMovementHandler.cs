using UnityEngine;

/// <summary>
/// Handles erratic fish AI movement using smoothed random targeting within centralized bounds.
/// 
/// Current Issues:
/// - Fish can still clip through the bar while wobbling.
/// </summary>
public class FishMovementHandler : MonoBehaviour
{
    [Header("AI Behavior")]
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float movementSmoothTime = 0.4f;
    [SerializeField] private float decisionIntervalMin = 1f;
    [SerializeField] private float decisionIntervalMax = 3f;
    [SerializeField] private float microWobbleIntensity = 0.002f;

    private float _targetY;
    private float _currentVelocity;
    private float _timer;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _targetY = transform.position.y;
    }

    void Update()
    {
        if (FishingManager.Instance.CurrentState == FishingState.IDLE) return;
        
        UpdateAIStep();
        ApplyPosition();
    }

    /// <summary>
    /// Picks a new target position within the manager bounds at fixed intervals.
    /// </summary>
    private void UpdateAIStep()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            float halfHeight = _spriteRenderer.bounds.size.y / 2f;
            _targetY = Random.Range(FishingManager.Instance.MinY + halfHeight, FishingManager.Instance.MaxY - halfHeight);
            float decisionInterval = Random.Range(decisionIntervalMin, decisionIntervalMax);
            _timer = decisionInterval;
        }
    }

    /// <summary>
    /// Smoothly interpolates to the target position and applies a subtle wobble.
    /// </summary>
    private void ApplyPosition()
    {
        float nextY = Mathf.SmoothDamp(transform.position.y, _targetY, ref _currentVelocity, movementSmoothTime, maxMoveSpeed);
        float wobble = Mathf.Sin(Time.time * 10f) * microWobbleIntensity;
        
        transform.position = new Vector3(transform.position.x, nextY + wobble, transform.position.z);
    }
}