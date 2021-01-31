using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : ObjectEntity
{
    public GameObject backPack, catTower, couch, table, tvTunnel, wardrobe;
    public List<GameObject> obstacles;

    [SerializeField] private Ground ground;

    private float timeSinceLastSpawn; 

    public void SpawnRandomObstacle(float xVal = 15)
    {
        timeSinceLastSpawn = Time.time;
        int choice = Random.Range(0, 6);
        float ySpawnLocation = 0;

        // The red dot can be higher!

        switch (choice) {
            case 0:
                timeSinceLastSpawn = Time.time;
                ySpawnLocation = .35f;
                break;
            case 1:
                timeSinceLastSpawn = Time.time;
                ySpawnLocation = .82f;
                break;
            case 2:
                timeSinceLastSpawn = Time.time - 1.5f;
                ySpawnLocation = Random.Range(2.48f, 5.64f);
                break;
            case 3:
                timeSinceLastSpawn = Time.time;
                ySpawnLocation = .09f;
                break;
            case 4:
                timeSinceLastSpawn = Time.time;
                ySpawnLocation = .42f;
                break;
            case 5:
                timeSinceLastSpawn = Time.time;
                ySpawnLocation = .84f;
                break;
        }

        Vector3 spawnPosition = new Vector3(xVal, ySpawnLocation, 0);

        SpawnObjectEntityByType(obstacles[choice].transform, spawnPosition);
    }



    // Start is called before the first frame update
    void Start()
    {
        obstacles.Add(backPack);
        obstacles.Add(catTower);
        obstacles.Add(couch);
        obstacles.Add(table);
        obstacles.Add(tvTunnel);
        obstacles.Add(wardrobe);
        SpawnRandomObstacle(6);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - timeSinceLastSpawn > 3 - ground.scrollSpeedVariable*10)
        {
            SpawnRandomObstacle();

            if (CanDeleteObjectEntity(spawnedObjectEntities[0].position))
            {
                DeleteLeftMostObjectEntity();
            }
        }

    }

    void Update()
    {
        spawnedObjectEntities.ForEach((objectEntity) =>
        {
            objectEntity.position += new Vector3(-ground.scrollSpeedVariable, 0, 0);
        }
        );
    }
}
