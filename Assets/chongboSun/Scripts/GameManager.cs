// GameManager.cs
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("炮塔类型配置")]
    public TowerData[] towerTypes; // 必须在Inspector中赋值！

    void Start()
    {
        if (BuildManager.Instance != null && towerTypes != null)
        {
            BuildManager.Instance.InitializeTowerButtons(towerTypes);
        }
        else
        {
            Debug.LogError("初始化失败：BuildManager或炮塔类型未配置！");
        }
    }
}