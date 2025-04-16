// TowerData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "TowerDefense/Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("��������")]
    public string towerName = "��������";
    public GameObject towerPrefab;
    public int buildCost = 50;
    public Sprite towerIcon;

    [Header("ս������")]
    public float attackRange = 3f;
    public float attackRate = 1f;
    public int damage = 10;

    [Header("��������")]
    public bool useLaser = false;
    public bool useProjectile = true;

    [Header("����Ч��")]
    public float slowAmount = 0f;    // ���ٱ��� (0-1)
    public float slowDuration = 2f;  // ���ٳ���ʱ��

    [Header("����������")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
}