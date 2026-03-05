using UnityEngine;

public class Axe : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IChop chopObject = collision.gameObject.GetComponent<IChop>();
        if (chopObject != null)
        {
            Debug.Log("Axe Hit Object");
            chopObject.Chop();
        }
    }
}
