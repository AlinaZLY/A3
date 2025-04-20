using UnityEngine;
using TMPro;

public class sherrytime : MonoBehaviour
{

    public float timeLeft = 60f; // 倒计时时间
    public TextMeshProUGUI countdownText;

    /*
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(timeLeft).ToString();
        }
        else
        {
            countdownText.text = "时间到！";
        }
    }*/
}
