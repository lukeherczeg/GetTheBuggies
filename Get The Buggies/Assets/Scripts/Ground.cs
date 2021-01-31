using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : ObjectEntity
{
    [SerializeField] private Transform groundBlockStart;

    private float groundBlockYPos;
    private Vector3 lastEndPosition;
    private float timeAtStart;
    private float scrollSpeedMax = .035f;
    private bool reachedMaxScrollSpeed = false;

    private void Awake() {
        spawnedObjectEntities.Add(groundBlockStart);
        groundBlockYPos = groundBlockStart.position.y;
        lastEndPosition = groundBlockStart.Find("EndPosition").position;
        SpawnGroundBlock();
    }

    private void SpawnGroundBlock() {
        lastEndPosition.y = groundBlockYPos;
        Transform lastGroundBlockTransform = SpawnObjectEntity(lastEndPosition);
        lastEndPosition = lastGroundBlockTransform.Find("EndPosition").position;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeAtStart = Time.time;
        scrollSpeedVariable = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timeAtStart < 3)
        {
            scrollSpeedVariable += .00005f;
        }
        else
        {
            if (scrollSpeedVariable < scrollSpeedMax && !reachedMaxScrollSpeed)
            {
                scrollSpeedVariable += .0005f;
            }
            else
            {
                reachedMaxScrollSpeed = true;
            }
        }

        if (reachedMaxScrollSpeed) {
            scrollSpeedVariable += .000005f;
        }

        MoveObjectEntities();

        lastEndPosition = spawnedObjectEntities[spawnedObjectEntities.Count-1].Find("EndPosition").position;

        if (CanSpawnNewObjectEntity(lastEndPosition)) { 
            SpawnGroundBlock();
        }

        if (CanDeleteObjectEntity(spawnedObjectEntities[0].position)) {
            DeleteLeftMostObjectEntity();
        }
    }
}
