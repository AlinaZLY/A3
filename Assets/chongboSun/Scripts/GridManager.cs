using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public GameObject gridPrefab;
    public int gridSize = 10;  // 总网格尺寸（中心对称）
    public float cellSize = 1f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // 计算起始偏移量实现中心对称生成
        float offset = (gridSize % 2 == 0) ? cellSize * 0.5f : 0f;

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 pos = new Vector3(
                    (x - gridSize / 2) * cellSize + offset,
                    0,
                    (z - gridSize / 2) * cellSize + offset
                );

                GameObject cell = Instantiate(gridPrefab, pos, Quaternion.identity, transform);
                cell.name = $"GridCell_{x}_{z}";
            }
        }
    }
}