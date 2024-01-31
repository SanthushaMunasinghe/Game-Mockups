using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridGenerator : MonoBehaviour
{
    public int gridSizeX = 5; // Number of cells in the X-axis
    public int gridSizeZ = 5; // Number of cells in the Z-axis
    public float cellSize = 1.0f; // Size of each cell

    [SerializeField] private GameObject cellPrefab;
    public List<GameObject> gridItemPrefabs = new List<GameObject>();

    public List<GameObject> CurrentCells = new List<GameObject>();

#if UNITY_EDITOR
    [CustomEditor(typeof(GridGenerator))]
    public class GridGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GridGenerator gridGenerator = (GridGenerator)target;

            if (GUILayout.Button("Generate"))
            {
                gridGenerator.ClearAndGenerateGrid();
            }
        }
    }
#endif

    private void Start()
    {
        GenerateGridItem(0.1f);
    }

    void GenerateGrid()
    {
        ClearGrid();

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * cellSize, 0f, z * cellSize);
                GameObject cube = SpawnCell(spawnPosition);

                cube.transform.parent = transform;

                //Add to the list
                CurrentCells.Add(cube);
            }
        }
    }

    private void GenerateGridItem(float yPos)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                Vector3 spawnPosition = new Vector3(x * cellSize, yPos, z * cellSize);
                GameObject itemClone = SpawnItem(gridItemPrefabs[Random.Range(0, gridItemPrefabs.Count)], spawnPosition);

                itemClone.transform.parent = transform;
            }
        }
    }

    GameObject SpawnCell(Vector3 position)
    {
        GameObject cube = Instantiate(cellPrefab, position, Quaternion.identity);
        return cube;
    }
    
    GameObject SpawnItem(GameObject itemPrefab, Vector3 position)
    {
        GameObject cube = Instantiate(itemPrefab, position, Quaternion.identity);
        return cube;
    }

    void ClearGrid()
    {
        foreach (var cube in CurrentCells)
        {
            DestroyImmediate(cube);
        }
        CurrentCells.Clear();
    }

    public void ClearAndGenerateGrid()
    {
        ClearGrid();
        GenerateGrid();
    }
}
