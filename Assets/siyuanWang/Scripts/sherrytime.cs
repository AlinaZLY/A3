using UnityEngine;
using TMPro;

public class sherrytime : MonoBehaviour
{
    public float timeLeft = 60f; // 倒计时时间
    public int coinsCollected = 0;
    public int targetCoins = 4;

    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI resultText;

    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        // 更新倒计时
        timeLeft -= Time.deltaTime;
        countdownText.text =  Mathf.Ceil(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            EndGame(false);
        }
    }

    public void CollectCoin()
    {
        if (gameEnded) return;

        coinsCollected++;
        if (coinsCollected >= targetCoins)
        {
            EndGame(true);
        }
    }

    void EndGame(bool won)
    {
        gameEnded = true;
        if (won)
        {
            resultText.text = "胜利！你吃到了4个金币！";
        }
        else
        {
            resultText.text = "失败！时间到了。";
        }
        resultText.gameObject.SetActive(true);
    }
}
