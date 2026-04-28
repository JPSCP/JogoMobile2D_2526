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

    private void Start()
    {
        // Spawn FIRST guaranteed chunk
        SpawnFirstPiece();

        // Then spawn initial random chunks
        for (int i = 0; i < 4; i++)
        {
            SpawnRandomPiece();
        }
    }

    private void Update()
    {
        float camX = Camera.main.transform.position.x;

        // Spawn ahead
        if (camX + 30f > nextSpawnX)
        {
            SpawnRandomPiece();
        }

        // Destroy old chunks
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

    void SpawnFirstPiece()
    {
        GameObject chunk = Instantiate(firstLevelPrefab, new Vector2(nextSpawnX, 0f), Quaternion.identity);

        activeChunks.Add(chunk);

        nextSpawnX += pieceLength;
    }

    void SpawnRandomPiece()
    {
        int index = Random.Range(0, levelPrefabs.Length);

        GameObject chunk = Instantiate(levelPrefabs[index], new Vector2(nextSpawnX, 0f), Quaternion.identity);

        activeChunks.Add(chunk);

        nextSpawnX += pieceLength;
    }
}
