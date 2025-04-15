
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [Header("��������")]
    public Material highlightMaterial; // �����ĸ������ʣ��踳ֵ��
    private Renderer rend;
    private Material originalMaterial;
    private Vector3 cellCenter;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;

        // ��ȡ��ײ�����ĵ㣨ȷ����ײ��������ڣ�
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

        // ֱ��ʹ��Ԥ�������������
        BuildManager.Instance.BuildTower(cellCenter);

        // �������
        Debug.Log($"����λ�ã�{cellCenter}");
    }
}