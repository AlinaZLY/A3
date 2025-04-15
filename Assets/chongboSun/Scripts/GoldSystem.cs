
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    public static GoldSystem Instance { get; private set; }
    public int CurrentGold { get; private set; }

    void Awake()
    {
        // ����ģʽ��ʼ��
        if (Instance == null)
        {
            Instance = this;
            CurrentGold = 100; // ��ʼ���
            DontDestroyOnLoad(gameObject); // �糡����������ѡ��
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddGold(int amount)
    {
        CurrentGold += amount;
        Debug.Log($"��ǰ��ң�{CurrentGold}"); // ������ȷָ��UnityEngine.Debug
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