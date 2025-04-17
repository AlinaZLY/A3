using System.Collections;
using TMPro;
using UnityEngine;

public class GoldUIDisplay : MonoBehaviour
{
    [Header("UI组件")]
    public TMP_Text goldText;

    void Start()
    {
        StartCoroutine(InitializeWithDelay());
    }
    IEnumerator InitializeWithDelay()
    {
        // 等待1帧确保GoldSystem已初始化
        yield return null;

        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.OnGoldChanged += UpdateGoldDisplay;
            UpdateGoldDisplay(GoldSystem.Instance.CurrentGold);
        }
        else
        {
            Debug.LogError("金币系统初始化失败！");
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