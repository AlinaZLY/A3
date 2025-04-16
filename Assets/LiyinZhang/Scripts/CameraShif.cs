using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShif : MonoBehaviour
{
    public Transform y_Axis;
    //控制上下旋转
    public Transform x_Axis;
    //控制左右倾斜
    public Transform z_Axis;
    //控制远近距离
    public Transform zoom_Axis;
 
    //物体对象
    public Transform player;
 
    //旋转速度
    public float roSpeed = 180;
    //缩放速度
    public float scSpeed = 50;
    //限定角度
    public float limitAngle = 45;
 
    //鼠标左右滑动数值、滚动数值
    private float hor, ver, scrollView;
    float x = 0,sc = 10;
 
    //是否跟随物体对象
    public bool followFlag;
    //是否控制物体旋转
    public bool turnFlag;
 
    private void LateUpdate()
    {
        //输入获取
        hor = Input.GetAxis("Mouse X");
        ver = Input.GetAxis("Mouse Y");
 
        //鼠标滚动数值
        scrollView = Input.GetAxis("Mouse ScrollWheel");
 
        //左右滑动鼠标
        if (hor!=0)
        {
            //围绕Y轴旋转，注意Up是本地坐标的位置
            y_Axis.Rotate(Vector3.up*roSpeed*hor*Time.deltaTime);
        }
 
        //上下滑动鼠标
        if (ver!=0)
        {
            //Y轴移动值
            x += -ver * Time.deltaTime * roSpeed;
            //设置鼠标移动范围
            x = Mathf.Clamp(x, -limitAngle,limitAngle);
            //Quaternion.identity：单位旋转
            Quaternion q = Quaternion.identity;
            //Quaternion.Euler：返回一个旋转，它围绕 z 轴旋转 z 度、围绕 x 轴旋转 x 度、围绕 y 轴旋转 y 度
            q = Quaternion.Euler(new Vector3(x,x_Axis.eulerAngles.y,x_Axis.eulerAngles.z));
            //Quaternion.Lerp：在 a 和 b 之间插入 t，然后对结果进行标准化处理。参数 t 被限制在 [0, 1] 范围内。
            //将旋转值赋值给摄像机坐标对象      
            x_Axis.rotation = Quaternion.Lerp(x_Axis.rotation,q, Time.deltaTime*roSpeed);
        }
 
        //缩放远近
        if (scrollView!=0)
        {
            //远近移动值
            sc -= scrollView * scSpeed;
            //设置鼠标移动范围
            sc = Mathf.Clamp(sc,3,10);
            //将缩放值赋值给控制远近距离对象
            zoom_Axis.transform.localPosition = new Vector3(0,0,-sc);
        }
 
        //跟随物体对象  当指定物体后
        if (followFlag&&player!=null)
        {
            //Vector3.Lerp:在两个点之间进行线性插值。
            //Y轴方向的坐标同步
            y_Axis.position = Vector3.Lerp(y_Axis.position,player.position+Vector3.up, Time.deltaTime * 10f);
        }
 
 
        //旋转物体对象   当指定物体后
        if (followFlag && player != null)
        {
            //旋转值同步
            player.transform.forward = new Vector3(transform.forward.x,0,transform.forward.z);
        }
    }
 
 
}
