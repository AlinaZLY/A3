using System.Collections;
using TMPro;
using UnityEngine;

public class GoldUIDisplay : MonoBehaviour
{
    [Header("UI���")]
    public TMP_Text goldText;

    void Start()
    {
        StartCoroutine(InitializeWithDelay());
    }
    IEnumerator InitializeWithDelay()
    {
        // �ȴ�1֡ȷ��GoldSystem�ѳ�ʼ��
        yield return null;

        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.OnGoldChanged += UpdateGoldDisplay;
            UpdateGoldDisplay(GoldSystem.Instance.CurrentGold);
        }
        else
        {
            Debug.LogError("���ϵͳ��ʼ��ʧ�ܣ�");
        }
    }
    void UpdateGoldDisplay(int amount)
    {
        if (goldText != null)
        {
            goldText.text = amount.ToString();
        }
    }

    void OnDestroy()
    {
        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.OnGoldChanged -= UpdateGoldDisplay;
        }
    }
}