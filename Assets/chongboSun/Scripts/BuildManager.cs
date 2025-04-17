
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [Header("当前选中")]
    public TowerData selectedTowerData;

    [Header("UI配置")]
    public Transform towerSelectionPanel;  // 必须拖拽赋值
    public GameObject towerButtonPrefab;   // 必须拖拽赋值

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 初始化时创建按钮（增加空值检查）
    public void InitializeTowerButtons(TowerData[] towerTypes)
    {
        if (towerButtonPrefab == null)
        {
            Debug.LogError("按钮预制体未分配！");
            return;
        }

        if (towerSelectionPanel == null)
        {
            Debug.LogError("UI父级容器未分配！");
            return;
        }

        if (towerTypes == null || towerTypes.Length == 0)
        {
            Debug.LogWarning("未配置炮塔类型！");
            return;
        }

        foreach (TowerData data in towerTypes)
        {
            if (data == null) continue;

            GameObject buttonObj = Instantiate(towerButtonPrefab, towerSelectionPanel);
            TowerButton button = buttonObj.GetComponent<TowerButton>();
            if (button != null)
            {
                button.Initialize(data);
            }
            else
            {
                Debug.LogError("按钮预制体缺少TowerButton组件！");
            }
        }
    }

    public void BuildTower(Vector3 position)
    {
        if (selectedTowerData == null)
        {
            Debug.LogWarning("未选择炮塔类型！");
            return;
        }

        // 合并金币检查逻辑
        if (!GoldSystem.Instance.SpendGold(selectedTowerData.buildCost))
        {
            Debug.LogWarning($"金币不足！需要 {selectedTowerData.buildCost}，当前 {GoldSystem.Instance.CurrentGold}");
            return;
        }

        if (selectedTowerData.towerPrefab == null)
        {
            Debug.LogError($"配置错误：{selectedTowerData.towerName} 未关联预制体");
            return;
        }

        GameObject tower = Instantiate(selectedTowerData.towerPrefab, position, Quaternion.identity);

        // 关键修复：初始化炮塔数据
        Tower towerComponent = tower.GetComponent<Tower>();
        if (towerComponent != null)
        {
            towerComponent.Initialize(selectedTowerData);
        }
        else
        {
            Debug.LogError($"炮塔预制体缺少 Tower 组件: {selectedTowerData.towerName}");
        }

        Debug.Log($"成功建造 {selectedTowerData.towerName}！");
    }
}