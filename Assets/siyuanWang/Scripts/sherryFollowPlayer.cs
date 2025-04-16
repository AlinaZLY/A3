using UnityEngine;

public class sherryFollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 10f, -12f);
    public float lookHeight = 1.8f; // 朝角色上半身靠近头部的位置看

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target.position + Vector3.up * lookHeight);
        }
    }
}
