// BuildManager.cs �޸ĺ�Ĵ���
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public GameObject towerPrefab;
    public int towerCost = 50;
    public LayerMask buildLayer;

    [Header("������Ч")]
    public ParticleSystem buildEffect;

    void Awake() => Instance = this;

    public void BuildTower(Vector3 position)
    {
        // ��ά�ռ���ײ���
        Collider[] colliders = Physics.OverlapBox(position,
            new Vector3(0.4f, 0.1f, 0.4f),
            Quaternion.identity,
            buildLayer
        );

        if (colliders.Length > 0)
        {
            Debug.LogWarning("��λ�����н�����");
            return;
        }

        if (GoldSystem.Instance.SpendGold(towerCost))
        {
            Instantiate(towerPrefab, position, Quaternion.identity);
            if (buildEffect != null)
            {
                Instantiate(buildEffect, position, Quaternion.identity);
            }
        }
    }
}