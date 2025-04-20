using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public GameObject popupPanel; // 拖拽Hierarchy中的Panel到此字段
    public float displayTime = 5f; // 弹窗显示时长

    void Start()
    {
        // 启动时显示弹窗
        ShowPopup();
    }

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }

    private System.Collections.IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        popupPanel.SetActive(false);
    }
}
