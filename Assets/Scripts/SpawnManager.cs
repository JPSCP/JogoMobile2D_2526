using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("First Chunk")]
    public GameObject firstLevelPrefab;

    [Header("Random Chunks")]
    public GameObject[] levelPrefabs;

    public float pieceLength = 20f;
    public float destroyDistance = 40f;

    private float nextSpawnX = 0f;

    private List<GameObject> activeChunks = new List<GameObject>();

    private bool firstChunkSpawned = false;

    private void Start()
    {
        // Spawn inicial (inclui o primeiro fixo automaticamente)
        for (int i = 0; i < 5; i++)
        {
            SpawnPiece();
        }
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;

        float camX = Camera.main.transform.position.x;

        // Distância dinâmica baseada na velocidade
        float spawnDistance = 30f + GameManager.Instance.GetSpeed();

        // Spawn à frente
        if (camX + spawnDistance > nextSpawnX)
        {
            SpawnPiece();
        }

        // Destruir chunks antigos
        float destroyX = camX - destroyDistance;

        for (int i = activeChunks.Count - 1; i >= 0; i--)
        {
            if (activeChunks[i].transform.position.x < destroyX)
            {
                Destroy(activeChunks[i]);
                activeChunks.RemoveAt(i);
            }
        }
    }

    void SpawnPiece()
    {
        GameObject prefabToSpawn;

        // Primeiro chunk SEMPRE igual
        if (!firstChunkSpawned)
        {
            prefabToSpawn = firstLevelPrefab;
            firstChunkSpawned = true;

            nextSpawnX = 0f; // garante início correto
        }
        else
        {
            int index = Random.Range(0, levelPrefabs.Length);
            prefabToSpawn = levelPrefabs[index];
        }

        GameObject chunk = Instantiate(prefabToSpawn, new Vector2(nextSpawnX, 0f), Quaternion.identity);

        activeChunks.Add(chunk);

        nextSpawnX += pieceLength;
    }
}
