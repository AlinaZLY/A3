
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    public static GoldSystem Instance { get; private set; }
    public int CurrentGold { get; private set; }

    void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            CurrentGold = 100; // 初始金币
            DontDestroyOnLoad(gameObject); // 跨场景保留（可选）
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        CurrentGold += amount;
        Debug.Log($"当前金币：{CurrentGold}"); // 现在明确指向UnityEngine.Debug
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
}