using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/ResourceData")]
public class Resource : ScriptableObject
{
    public string ResourceName;
    public string ResourceID;
    public string IsPremium;
    public bool Unlocked;
    public Sprite ResourceIcon;
    public int Total;
    public int gainPerSecond;
}
