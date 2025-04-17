
using System;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    public static GoldSystem Instance;
    public event Action<int> OnGoldChanged;
    private int currentGold = 100;

    void Awake()
    {
        // 单例初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int CurrentGold
    {
        get => currentGold;
        private set
        {
            currentGold = value;
            OnGoldChanged?.Invoke(currentGold);
        }
    }

    public void AddGold(int amount)
    {
        CurrentGold += amount;
    }

    public bool SpendGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            return true;
        }
        return false;
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}