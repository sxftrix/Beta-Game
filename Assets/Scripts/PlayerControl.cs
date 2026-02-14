using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        transform.position += new Vector3(moveX, moveY, 0).normalized * moveSpeed * Time.deltaTime;
    }

    public void SetMoveSpeed(float newSpeed)
    {
        this.moveSpeed = newSpeed;
    }

    public void Hit()
    {
        Debug.Log("Player is Hit by Beast");
    }
}
