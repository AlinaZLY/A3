// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("������������")]
    public TowerData[] towerTypes; // ������Inspector�и�ֵ��

    void Start()
    {
        if (BuildManager.Instance != null && towerTypes != null)
        {
            BuildManager.Instance.InitializeTowerButtons(towerTypes);
        }
        else
        {
            Debug.LogError("��ʼ��ʧ�ܣ�BuildManager����������δ���ã�");
        }
    }
}