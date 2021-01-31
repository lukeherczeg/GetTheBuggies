using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : ObjectEntity
{
    public GameObject ballToy, hairtieToy, mousieToy, redDotToy, starToy;
    public List<GameObject> collectables;

    [SerializeField] private Ground ground;

    private float timeSinceLastSpawn;

    private float screenTopY = 5.8f;
    private float screenBottomY = -1.4f;


    public void SpawnRandomCollectable() {
        timeSinceLastSpawn = Time.time;
        int choice = Random.Range(0, 5);
        float ySpawnLocation;

        // The red dot can be higher!
        if (choice == 3)
            ySpawnLocation = Random.Range(3f, screenTopY);
        else
            ySpawnLocation = Random.Range(screenBottomY, 4f);

        Vector3 spawnPosition = new Vector3(12, ySpawnLocation, 0);


        SpawnObjectEntityByType(collectables[choice].transform, spawnPosition);
    }



    // Start is called before the first frame update
    void Start()
    {
        collectables.Add(ballToy);
        collectables.Add(hairtieToy);
        collectables.Add(mousieToy);
        collectables.Add(redDotToy);
        collectables.Add(starToy);
        SpawnRandomCollectable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("There are " + spawnedObjectEntities.Count + " buggies.");6

        if (Time.time - timeSinceLastSpawn > 3)
        {
            SpawnRandomCollectable();

            if (CanDeleteObjectEntity(spawnedObjectEntities[0].position))
            {
                DeleteLeftMostObjectEntity();
            }
        }

    }

    void Update() {
        spawnedObjectEntities.ForEach((objectEntity) =>
        {
            float yChange = Random.Range(.004f, .006f);

            objectEntity.position += new Vector3(-ground.scrollSpeedVariable, yChange * Mathf.Sin(Time.time * 1.5f), 0);
        }
        );
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.name == "Star Toy(Clone)" ||
            trig.gameObject.name == "Ball Toy(Clone)" ||
            trig.gameObject.name == "Hairtie Toy(Clone)" ||
            trig.gameObject.name == "Red Dot Toy(Clone)" ||
            trig.gameObject.name == "Mousie Toy(Clone)")
        {
            DeleteObjectEntity(trig.gameObject);
        }
    }
}
