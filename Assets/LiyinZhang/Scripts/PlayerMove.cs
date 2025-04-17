using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Transform m_transform;
    //速度
    public float m_movSpeed = 4;
 
    void Start()
    {
        //获取绑定物体的坐标对象
        m_transform = GetComponent<Transform>();
    }
    void Update()
    {
        Control();
    }
 
    void Control() {
        //定义3个值控制移动
        float xm = 0, ym = 0, zm = 0;
 
        //跳 = C
        if (Input.GetKey(KeyCode.Space))
        {
            ym += m_movSpeed * Time.deltaTime;
        }
        //落 = X
        if (Input.GetKey(KeyCode.X) && m_transform.position.y >= 1)
        {
            ym -= m_movSpeed * Time.deltaTime;
        }
        //更新物体坐标位置
        m_transform.Translate(new Vector3(xm,ym,zm),Space.Self);
    }
}
