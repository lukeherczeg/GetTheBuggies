using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Obstacle : ObjectEntity
{
    public GameObject backPack, catTower, couch, table, tvTunnel, wardrobe, shelf, picture;
    public List<GameObject> obstacles;

    protected const float OBSTACLE_DISTANCE_SPAWN_RADIUS = 19f;

    [SerializeField] private Ground ground;

    private float timeSinceLastObstacleSpawn;
    private float timeSinceLastFlavorObject;

    private enum Obstacles {
        backPack, catTower, couch, table, tvTunnel, wardrobe, shelf, picture
    }

    private Obstacles lastSpawnedObstacleIndex;

    public void SpawnRandomObstacle(float xVal = 15)
    {
        timeSinceLastObstacleSpawn = Time.time;
        Obstacles choice = (Obstacles) Random.Range(0, 8);
        float ySpawnLocation = 0;
        bool spawned = false;

        while (!spawned) {
            switch (choice)
            {
                case Obstacles.backPack:
                    timeSinceLastObstacleSpawn = Time.time - 1;
                    ySpawnLocation = .35f;
                    spawned = true;
                    break;
                case Obstacles.catTower:
                    if (lastSpawnedObstacleIndex != Obstacles.catTower)
                    {
                        timeSinceLastObstacleSpawn = Time.time;
                        ySpawnLocation = .82f;
                        spawned = true;
                    }
                    else
                    {
                        // Reset choice
                        choice = (Obstacles) Random.Range(0, 8);
                    }
                    break;
                case Obstacles.couch:
                    timeSinceLastObstacleSpawn = Time.time - .5f;
                    ySpawnLocation = 1.68f;
                    spawned = true;
                    break;
                case Obstacles.table:
                    timeSinceLastObstacleSpawn = Time.time;
                    ySpawnLocation = .09f;
                    spawned = true;
                    break;
                case Obstacles.tvTunnel:
                    timeSinceLastObstacleSpawn = Time.time;
                    ySpawnLocation = .42f;
                    spawned = true;
                    break;
                case Obstacles.wardrobe:
                    if (lastSpawnedObstacleIndex != Obstacles.wardrobe)
                    {
                        timeSinceLastObstacleSpawn = Time.time;
                        ySpawnLocation = .84f;
                        spawned = true;
                    }
                    else
                    {
                        // Reset choice
                        choice = (Obstacles) Random.Range(0, 8);
                    }
                    break;
            }
                
            // Assigns more random weight to the shelves!
            if (choice >= Obstacles.shelf)
            {
                timeSinceLastObstacleSpawn = Time.time;

                // Spawn shelves higher if it's easy mode! 
                if(PlayerPrefs.GetInt("Difficulty") == 0) {
                    ySpawnLocation = Random.Range(-.5f, 2.2f);
                }
                else {
                    ySpawnLocation = Random.Range(-.5f, 1.5f);
                }

                choice = Obstacles.shelf;
                spawned = true;
            }
        }


        Vector3 spawnPosition = new Vector3(xVal, ySpawnLocation, 0);

        SpawnObjectEntityByType(obstacles[(int)choice].transform, spawnPosition);

        lastSpawnedObstacleIndex = choice;
    }


    public void SpawnFlavorObject(int index, float xVal = 15) {
        if ((Obstacles)index == Obstacles.picture) {
            float ySpawnLocation = Random.Range(1.5f, 4.2f);
            Vector3 spawnPosition = new Vector3(xVal, ySpawnLocation, 0);
            SpawnObjectEntityByType(obstacles[index].transform, spawnPosition);
            timeSinceLastFlavorObject = Time.time;
        }
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
        obstacles.Add(shelf);
        obstacles.Add(picture);
        lastSpawnedObstacleIndex = (Obstacles) (-1);
        // 6 is the distance btw :)
        SpawnRandomObstacle(6);
    }

    void Update() {
        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            if (!ground.reachedMaxScrollSpeed)
            {
                Spawn(3, timeSinceLastObstacleSpawn, true);
            }
            else
            {
                Spawn(1.5f, timeSinceLastObstacleSpawn, true);
            }
        }
        else {
            if (!ground.reachedMaxScrollSpeed)
            {
                Spawn(3, timeSinceLastObstacleSpawn, true);
            }
            else
            {
                Spawn(2.2f, timeSinceLastObstacleSpawn, true);
            }
        }

        Spawn(Random.Range(10, 15), timeSinceLastFlavorObject, false);
    }

    void Spawn(float timeIncrement, float timeSince, bool isObstacle)
    {
        if (Time.time - timeSince > timeIncrement)
        {
            if (isObstacle) {
                SpawnRandomObstacle();
            }
            else { 
                SpawnFlavorObject((int)Obstacles.picture); 
            }

            // This makes the collectables despawn only every 3 seconds :) (it's bad, deal with it)
            if (CanDeleteObjectEntity(spawnedObjectEntities[0].position))
            {
                DeleteLeftMostObjectEntity();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        spawnedObjectEntities.ForEach((objectEntity) =>
        {
            objectEntity.position += new Vector3(-ground.scrollSpeedVariable, 0, 0);
        }
       );

    }
}
