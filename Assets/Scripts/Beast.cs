using UnityEngine;

public class Beast : MonoBehaviour, IChop
{
    public Transform player;
    public float moveSpeed = 15f;

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
        Destroy(gameObject);
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
