// BuildManager.cs 修改后的代码
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    public GameObject towerPrefab;
    public int towerCost = 50;
    public LayerMask buildLayer;

    [Header("建造特效")]
    public ParticleSystem buildEffect;

    void Awake() => Instance = this;

    public void BuildTower(Vector3 position)
    {
        // 三维空间碰撞检测
        Collider[] colliders = Physics.OverlapBox(position,
            new Vector3(0.4f, 0.1f, 0.4f),
            Quaternion.identity,
            buildLayer
        );

        if (colliders.Length > 0)
        {
            Debug.LogWarning("该位置已有建筑！");
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