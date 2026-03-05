using UnityEngine;
using System;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/ResourceData")]
public class Resource : ScriptableObject
{
    [SerializeField] private string ResourceName;
    [SerializeField] private string ResourceID;
    [SerializeField] private Sprite ResourceIcon;
    [SerializeField] private int Total;
    [SerializeField] private bool Unlocked;
    
    public static event Action OnUnlockResource;
    public static event Action<Resource> OnTotalChanged;
    
    public string GetName() => ResourceName;
    
    public Sprite GetIcon() => ResourceIcon;
    
    public string GetID() => ResourceID;
    
    public double GetTotal() => Total;
    
    public bool CanSpend(int cost) => Total >= cost;
    
    public void Gain(int amount)
    {
        Total += amount;
        OnTotalChanged?.Invoke(this);
    }

    public void Spend(int amount)
    {
        Total -= amount;
        OnTotalChanged?.Invoke(this);
    }
    
    public bool IsUnlocked() => Unlocked;

    public void Unlock()
    {
        if (!Unlocked)
        {
            Unlocked = true;
            OnUnlockResource?.Invoke();
        }
        else
        {
            Debug.Log("Resource already unlocked");
        }
    }

    public void Reset()
    {
        Unlocked = false;
        Total = 0;
    }
}
