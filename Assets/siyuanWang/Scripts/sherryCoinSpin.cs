using UnityEngine;

public class sherryCoinSpin : MonoBehaviour
{
    // 控制旋转速度，可以在 Inspector 中调节
    public float rotateSpeed = 100f;

    void Update()
    {
        // 让金币在 Y 轴持续旋转
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
