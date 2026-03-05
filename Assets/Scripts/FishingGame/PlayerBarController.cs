using UnityEngine;

/// <summary>
/// Controls the player-driven bar movement and detects overlap with the fish via triggers.
/// </summary>
public class PlayerBarController : MonoBehaviour
{
    [Header("Physics Constants")]
    [SerializeField] private float gravityForce = -20f;
    [SerializeField] private float liftForce = 45f;
    [SerializeField] private float terminalVelocity = 15f;

    private float _velocity = 0f;
    private SpriteRenderer _spriteRenderer;

    void Start() => _spriteRenderer = GetComponent<SpriteRenderer>();

    void Update()
    {
        ApplyMovement();
        ClampToManagerBounds();
    }

    /// <summary>
    /// Calculates upward or downward velocity based on mobile touch or mouse input.
    /// </summary>
    private void ApplyMovement()
    {
        bool isInputHeld = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);
        float force = isInputHeld ? (gravityForce + liftForce) : gravityForce;

        _velocity += force * Time.deltaTime;
        _velocity = Mathf.Clamp(_velocity, -terminalVelocity, terminalVelocity);
        transform.position += new Vector3(0, _velocity * Time.deltaTime, 0);
    }

    /// <summary>
    /// Keeps the bar within the Manager's bounds, accounting for custom sprite height.
    /// </summary>
    private void ClampToManagerBounds()
    {
        float halfHeight = _spriteRenderer.bounds.size.y / 2f;
        float clampedY = Mathf.Clamp(transform.position.y, FishingManager.Instance.MinY + halfHeight, FishingManager.Instance.MaxY - halfHeight);

        if (clampedY != transform.position.y) _velocity = 0;
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish")) FishingManager.Instance.CurrentState = FishingState.REELING;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fish")) FishingManager.Instance.CurrentState = FishingState.FISHING;
    }
}