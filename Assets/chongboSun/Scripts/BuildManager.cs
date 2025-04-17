
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [Header("��ǰѡ��")]
    public TowerData selectedTowerData;

    [Header("UI����")]
    public Transform towerSelectionPanel;  // ������ק��ֵ
    public GameObject towerButtonPrefab;   // ������ק��ֵ

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

    // ��ʼ��ʱ������ť�����ӿ�ֵ��飩
    public void InitializeTowerButtons(TowerData[] towerTypes)
    {
        if (towerButtonPrefab == null)
        {
            Debug.LogError("��ťԤ����δ���䣡");
            return;
        }

        if (towerSelectionPanel == null)
        {
            Debug.LogError("UI��������δ���䣡");
            return;
        }

        if (towerTypes == null || towerTypes.Length == 0)
        {
            Debug.LogWarning("δ�����������ͣ�");
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
                Debug.LogError("��ťԤ����ȱ��TowerButton�����");
            }
        }
    }

    public void BuildTower(Vector3 position)
    {
        if (selectedTowerData == null)
        {
            Debug.LogWarning("δѡ���������ͣ�");
            return;
        }

        // �ϲ���Ҽ���߼�
        if (!GoldSystem.Instance.SpendGold(selectedTowerData.buildCost))
        {
            Debug.LogWarning($"��Ҳ��㣡��Ҫ {selectedTowerData.buildCost}����ǰ {GoldSystem.Instance.CurrentGold}");
            return;
        }

        if (selectedTowerData.towerPrefab == null)
        {
            Debug.LogError($"���ô���{selectedTowerData.towerName} δ����Ԥ����");
            return;
        }

        GameObject tower = Instantiate(selectedTowerData.towerPrefab, position, Quaternion.identity);

        // �ؼ��޸�����ʼ����������
        Tower towerComponent = tower.GetComponent<Tower>();
        if (towerComponent != null)
        {
            towerComponent.Initialize(selectedTowerData);
        }
        else
        {
            Debug.LogError($"����Ԥ����ȱ�� Tower ���: {selectedTowerData.towerName}");
        }

        Debug.Log($"�ɹ����� {selectedTowerData.towerName}��");
    }
}