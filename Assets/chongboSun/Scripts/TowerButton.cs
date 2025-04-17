using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�
using TMPro;
//using static System.Net.Mime.MediaTypeNames;

public class TowerButton : MonoBehaviour
{
    [Header("UIԪ��")]
    public Image iconImage; // ��ȷʹ��UnityEngine.UI.Image
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
        // ���ѡ��Ч������ȷʹ��UI��Image�����
        GetComponent<Image>().color = Color.yellow;
    }
}