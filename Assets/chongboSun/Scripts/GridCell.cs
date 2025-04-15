
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [Header("材质设置")]
    public Material highlightMaterial; // 单独的高亮材质（需赋值）
    private Renderer rend;
    private Material originalMaterial;
    private Vector3 cellCenter;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;

        // 获取碰撞体中心点（确保碰撞体组件存在）
        cellCenter = GetComponent<BoxCollider>().bounds.center;
    }

    void OnMouseEnter()
    {
        rend.material = highlightMaterial;
    }

    void OnMouseExit()
    {
        rend.material = originalMaterial;
    }

    void OnMouseDown()
    {
        if (!BuildManager.Instance) return;

        // 直接使用预计算的中心坐标
        BuildManager.Instance.BuildTower(cellCenter);

        // 调试输出
        Debug.Log($"建造位置：{cellCenter}");
    }
}