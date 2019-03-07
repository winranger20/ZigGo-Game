using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject platform;
    // Platform's last known position
    Vector3 platformLastPos;
    // Size of the platform
    float platformSize; 
    public bool gameOver;
    public GameObject diamond;

    // Use this for initialization
    void Start()
    {
        platformLastPos = platform.transform.position;
        platformSize = platform.transform.localScale.x;

        for (int i = 0; i < 5; i++)
        {
            SpawnPlatformInZ();
        }

        for (int r = 0; r < 50; r++)
        {
            SpawnPlatformInX();
        }

    }

    // Spawns platforms every 2 seconds 
    public void StartSpawningPlatform()
    {
        InvokeRepeating("SpawnPlatformInX", 2f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.gameOver == true)
        {
            CancelInvoke("SpawnPlatformInX");
        }
    }

    // Spawn platforms in Z-axis
    void SpawnPlatformInZ() 
    {

        int rand = Random.Range(0, 1);
        if (rand < 1)
        {
            SpawnZ();
        }
        else if (rand >= 1)
        {
            SpawnX();
        }
    }

    // Spawns platforms randomly 
    void SpawnPlatformInX() 
    {
        int rand = Random.Range(0, 2);
        if (rand < 1)
        {
            SpawnX();
        }
        else if (rand >= 1)
        {
            SpawnZ();
        }
    }

    // Spawn platforms to X-axis
    void SpawnX() 
    {
        Vector3 pos = platformLastPos;
        pos.x += platformSize;
        platformLastPos = pos;
        Instantiate(platform, pos, Quaternion.identity);

        // Random diamond Generator in X direction
        int rand = Random.Range(0, 4); 
        if (rand < 1)
        {
            Instantiate(diamond, new Vector3(pos.x, pos.y + 2, pos.z), diamond.transform.rotation);
        }
    }

    // Spawn platforms to Z-axis
    void SpawnZ() 
    {
        Vector3 pos = platformLastPos;
        pos.z += platformSize;
        platformLastPos = pos;
        // Creates a new platform "but doesn't change its direction its facing = (Quaternion.identity)"
        Instantiate(platform, pos, Quaternion.identity);

        // Random diamond generator in Z direction
        int rand = Random.Range(0, 4); 
        if (rand < 1)
        {
            Instantiate(diamond, new Vector3(pos.x, pos.y + 2, pos.z), diamond.transform.rotation);
        }
    }
}