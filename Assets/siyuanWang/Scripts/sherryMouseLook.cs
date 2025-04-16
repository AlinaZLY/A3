using UnityEngine;

public class sherryMouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    void Start()
    {
        // 隐藏并锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 获取鼠标移动量
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 垂直方向旋转（上下看）
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // 限制上下最大角度

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // 水平方向旋转（左右看）
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
