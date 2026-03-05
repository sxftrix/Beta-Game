using UnityEngine;

public class Beast : MonoBehaviour, IChop
{
    public Transform player;
    [SerializeField]
    private float moveSpeed = 15f;
    [SerializeField]
    private float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    public void Chop()
    {
        health--;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Upgrade()
    {
        health++;
        moveSpeed += 0.5f;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        PlayerControl playerCode = other.gameObject.GetComponent<PlayerControl>();
        if (playerCode != null)
        {
            playerCode.Hit();
        }
    }
}
