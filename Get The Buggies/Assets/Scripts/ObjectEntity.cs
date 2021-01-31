using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEntity : MonoBehaviour
{

    protected const float PLAYER_DISTANCE_SPAWN_RADIUS = 25f;

    [SerializeField] protected List<Transform> spawnedObjectEntities;
    [SerializeField] protected List<Transform> objectEntityList;
    [SerializeField] protected Transform objectEntity;
    [SerializeField] protected CharacterController2D player;

    [HideInInspector] 
    public float scrollSpeedVariable = 0;
    

    private void Awake()
    {
    }

    private void Start() {
    }

    private void Update() {
    }

    protected Transform SpawnObjectEntity(Vector3 spawnPosition)
    {
        Transform newObjectEntity = Instantiate(objectEntity, spawnPosition, Quaternion.identity);
        spawnedObjectEntities.Add(newObjectEntity);
        return newObjectEntity;
    }

    protected Transform SpawnObjectEntityByType(Transform objectEntity, Vector3 spawnPosition)
    {
        Transform newObjectEntity = Instantiate(objectEntity, spawnPosition, Quaternion.identity);
        spawnedObjectEntities.Add(newObjectEntity);
        return newObjectEntity;
    }


    /* protected Transform SpawnObjectEntities(Vector3[] spawnPositions)
     {
         objectEntityList.ForEach((objectEntity) => {
             Transform newObjectEntity = Instantiate(objectEntity, spawnPosition, Quaternion.identity);
             spawnedObjectEntities.Add(newObjectEntity);
             return newObjectEntity;
         });

     }*/

    protected bool CanSpawnNewObjectEntity(Vector3 position) { 
        return (Vector3.Distance(player.m_Rigidbody2D.transform.position, position) < PLAYER_DISTANCE_SPAWN_RADIUS);
    }
    
    protected bool CanDeleteObjectEntity(Vector3 position) { 
        return (Vector3.Distance(player.m_Rigidbody2D.transform.position, position) > PLAYER_DISTANCE_SPAWN_RADIUS);
    }

    protected void DeleteLeftMostObjectEntity() {
        if (spawnedObjectEntities[0] != null)
        {
            Transform temp = spawnedObjectEntities[0];
            spawnedObjectEntities.Remove(temp);
            Destroy(temp.gameObject);
        }
    }

    protected void DeleteObjectEntity(GameObject entity)
    {
        if (entity != null)
        {
            spawnedObjectEntities.Remove(entity.transform);
            Destroy(entity);
        }
    }



    protected void MoveObjectEntities() {
        spawnedObjectEntities.ForEach((objectEntity) => {
            objectEntity.position -= new Vector3(scrollSpeedVariable, 0, 0);
            }
        );
    }
}
