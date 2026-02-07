using UnityEngine;

public class Tree : MonoBehaviour, IChop
{
    public int scrapYield;

    public void Chop()
    {
        //insert Scrap function here
        Destroy(gameObject);
    }

    public void SetScrapYield (int newYield)
    {
        scrapYield = newYield;
    }
}
