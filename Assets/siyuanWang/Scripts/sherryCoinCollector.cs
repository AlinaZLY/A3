using UnityEngine;
using TMPro;

public class sherryCoinCollector : MonoBehaviour
{
    public int coinCount = 0;              // 当前吃了多少
    public int totalCoins = 5;             // 一共要吃多少
    public TextMeshProUGUI coinText;       // UI文字对象

    void Start()
    {
        UpdateCoinUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(other.gameObject);
            UpdateCoinUI();
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = coinCount + " / " + totalCoins;
        }
    }
}
