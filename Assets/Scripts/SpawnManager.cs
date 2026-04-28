using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] levelPrefabs;

    public float pieceLength = 20f;
    public float destroyDistance = 40f;

    private float nextSpawnX = 0f;

    private List<GameObject> activeChunks = new List<GameObject>();

    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;

        // Spawn initial chunks
        for (int i = 0; i < 5; i++)
        {
            SpawnPiece();
        }
    }

    private void Update()
    {
        // Spawn new chunks ahead of camera
        if (nextSpawnX < cam.position.x + 30f)
        {
            SpawnPiece();
        }

        // Destroy old chunks behind camera
        for (int i = activeChunks.Count - 1; i >= 0; i--)
        {
            if (activeChunks[i].transform.position.x < cam.position.x - destroyDistance)
            {
                Destroy(activeChunks[i]);
                activeChunks.RemoveAt(i);
            }
        }
    }

    void SpawnPiece()
    {
        int index = Random.Range(0, levelPrefabs.Length);

        Vector2 spawnPos = new Vector2(nextSpawnX, 0);

        GameObject newChunk = Instantiate(levelPrefabs[index], spawnPos, Quaternion.identity);

        activeChunks.Add(newChunk);

        nextSpawnX += pieceLength;
    }
}
