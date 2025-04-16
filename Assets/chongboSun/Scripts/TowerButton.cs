using UnityEngine;
using UnityEngine.UI; // 保留UI命名空间
using TMPro;
//using static System.Net.Mime.MediaTypeNames;

public class TowerButton : MonoBehaviour
{
    [Header("UI元素")]
    public Image iconImage; // 明确使用UnityEngine.UI.Image
    public TMP_Text costText;
    public Button button;

    private TowerData towerData;

    public void Initialize(TowerData data)
    {
        towerData = data;
        iconImage.sprite = data.towerIcon;
        costText.text = data.buildCost.ToString();

        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        BuildManager.Instance.selectedTowerData = towerData;
        // 添加选中效果（明确使用UI的Image组件）
        GetComponent<Image>().color = Color.yellow;
    }
}