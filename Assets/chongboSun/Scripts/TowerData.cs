// TowerData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "TowerDefense/Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("基础属性")]
    public string towerName = "基础炮塔";
    public GameObject towerPrefab;
    public int buildCost = 50;
    public Sprite towerIcon;

    [Header("战斗属性")]
    public float attackRange = 3f;
    public float attackRate = 1f;
    public int damage = 10;

    [Header("弹道设置")]
    public bool useLaser = false;
    public bool useProjectile = true;

    [Header("特殊效果")]
    public float slowAmount = 0f;    // 减速比例 (0-1)
    public float slowDuration = 2f;  // 减速持续时间

    [Header("抛射物设置")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
}