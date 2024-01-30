using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject darkPlayerTilePrefab;
    [SerializeField] private GameObject lightPlayerTilePrefab;

    [SerializeField] private GameObject darkEnemyTilePrefab;
    [SerializeField] private GameObject lightEnemyTilePrefab;

    [SerializeField] private int gridSizeX = 5;
    [SerializeField] private int gridSizeZ = 5;
    [SerializeField] private float cellSize = 1.0f;

    [SerializeField] private GameObject[] mobPrefabs;

    void Start()
    {
        GenerateCheckerboard();
    }

    void GenerateCheckerboard()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int z = 0; z < gridSizeZ; z++)
            {
                // Calculate spawn position for player tiles
                Vector3 playerSpawnPosition = new Vector3(x * cellSize, 0, z * cellSize);
                GameObject playerTilePrefab = (x + z) % 2 == 0 ? darkPlayerTilePrefab : lightPlayerTilePrefab;
                GameObject playerTile = Instantiate(playerTilePrefab, playerSpawnPosition, Quaternion.identity);
                playerTile.transform.localScale = new Vector3(cellSize, playerTile.transform.localScale.y, cellSize);
                playerTile.transform.parent = transform;

                //SpawnMob
                GameObject mobClone = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)], 
                    new Vector3(playerSpawnPosition.x, 0.2f, playerSpawnPosition.z), Quaternion.identity);

                // Calculate spawn position for enemy tiles
                Vector3 enemySpawnPosition = new Vector3(x * cellSize, 0, (z + gridSizeZ) * cellSize); // Offset in the Z-axis
                GameObject enemyTilePrefab = (x + z) % 2 == 0 ? darkEnemyTilePrefab : lightEnemyTilePrefab;
                GameObject enemyTile = Instantiate(enemyTilePrefab, enemySpawnPosition, Quaternion.identity);
                enemyTile.transform.localScale = new Vector3(cellSize, enemyTile.transform.localScale.y, cellSize);
                enemyTile.transform.parent = transform;
            }
        }
    }

}
