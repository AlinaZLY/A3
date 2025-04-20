using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
 
public class RoleController : MonoBehaviour
{
    float h; //水平轴系数
    float v; //垂直轴系数
    public float speed = 2;//速度
    public float turnSpeed = 10;//旋转速度
    public Transform camTransform; //相机
    Vector3 camForward; //临时三维坐标
    public Rigidbody rd;
    public int score = 0;
    public TMP_Text ui;
    public GameObject menu;
    public TMP_Text TimerTXT;
    public float timeleft;
    private bool IsCounting;
    public GameObject menu2;

    void Start()
    {
        //Debug.Log("游戏开始了！");
        rd = GetComponent<Rigidbody>(); // 调用刚体组件
        IsCounting = true;
    }

    void Update()
    {
        Move();
        if(IsCounting)
        {
            timeleft -= Time.deltaTime;
        }
        TimerTXT.text = timeleft.ToString(format:"0");
        if (timeleft <= 0 && IsCounting)
        {
            IsCounting = false;
            menu2.SetActive(true);
        }
    }
    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        transform.Translate(camTransform.right * h * speed * Time.deltaTime + camForward * v * speed * Time.deltaTime , Space.World);
        //水平垂直方向系数不为0表示需要进行旋转
        if (h != 0 || v != 0)
        {
            Rotating(h, v);
        }
        rd.AddForce(new Vector3(h, 0, v));
    }
    //旋转
    void Rotating(float hh, float vv)
    {
        camForward = Vector3.Cross(camTransform.right, Vector3.up);
        Vector3 targetDir = camTransform.right * hh + camForward * vv;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("发生碰撞了");

        //给Food模板设置好标签，检测到物体对应标签就销毁
        if (collision.gameObject.tag == "Score")
        {
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().Play ();
            score++;
            ui.text = score+"/6 ";
            if(score >= 6) 
            {
                menu.SetActive(true);
                IsCounting = false;
                menu2.SetActive(false);
            }
        }
    }

}