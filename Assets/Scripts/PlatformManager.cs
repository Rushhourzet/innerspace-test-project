﻿using UnityEditor.Rendering;
using UnityEngine;

//5 minutes of staring at the screen and contemplating life
public class PlatformManager : MonoBehaviour
{
    [SerializeField][Range(0.01f,0.02f)] private float initialSpeed;
    [SerializeField][Range(1f, 1.01f)] private float speedIncreaseOverTime;
    private const float minHoleSize = 1.2f;
    private const float platformDepth = 4f;
    private const float fieldWidth = 11.4f;
    private const float fieldHeight = 16.4f;
    [SerializeField][Range (minHoleSize, fieldWidth)] private float holeSize;
    [SerializeField][Range(0.8f, 1.2f)] private float spawnStartingFrequency;
    [SerializeField][Range(1f, 1.01f)] private float frequencyIncreaseOverTime;
    private static Vector3 spawnposition => new Vector3(0.0f, -fieldHeight / 2);

    private int platformCounter;
    private float timeSinceLastSpawn;

    private const int playerLayer = 10;


    [SerializeField] private GameObject platformPrefab;

    private ScoreManager levelManager => FindObjectOfType<ScoreManager>();
    private MainCharacter player => FindObjectOfType<MainCharacter>();

    //Just recently learned how to use delegates so im using them for Debug.Log just to show that i can use them a bit
    //Can also be used as callback
    debugLogDel dbg = (messages) => Debug.Log(messages);
    delegate void debugLogDel(string message);

    void Start()
    {
        //5 mins of debugging messages
        if (!platformPrefab.name.Equals("MovingPlatform")){
            dbg("You are not using the \" MovingPlatform \" Prefab");
        }
        if(initialSpeed == 0) {
            initialSpeed = 0.02f;
            dbg("speed was set to 0.02f, because no value was inserted");
        }
        //15 mins
        //holeSize = holeSize < minHoleSize ? minHoleSize : holeSize;
        //holeSize = holeSize > minHoleSize ? fieldWidth : holeSize;

        timeSinceLastSpawn = spawnStartingFrequency; //initialize with startingfrequency for instant spawn
    }

    // Update is called once per frame
    void Update()
    {
        float speed = initialSpeed * Mathf.Pow(speedIncreaseOverTime, platformCounter);
        transform.position += Vector3.up * speed;
    }

    private void FixedUpdate() {
        timeSinceLastSpawn += Time.fixedDeltaTime;
        float frequency = spawnStartingFrequency / Mathf.Pow(frequencyIncreaseOverTime, platformCounter);
        //30 mins of playing around with frequency values
        if (frequency < 0.5f) frequency = 0.5f; //i think this can be done more performant? outside of update
        if(timeSinceLastSpawn >= frequency) {
            SpawnPlatform(dbg);
            platformCounter++;
            timeSinceLastSpawn = 0f;
        }
        //dbg("Frequency: " + frequency);

        
    }

    //1hr 15mins
    //had to make it instantiate the prefab and grab onto platformleft and right on the prefab
    void SpawnPlatform(debugLogDel callback) {
        GameObject instantiatedPlatform = Instantiate(platformPrefab, spawnposition, new Quaternion(0f, 0f, 0f, 1f), transform);
        instantiatedPlatform.name = platformPrefab.name + platformCounter;
        RandomizeHole(instantiatedPlatform);
        callback("Platform has been spawned");
    }


    //15 mins + 5 mins
    void RandomizeHole(GameObject go) {
        GameObject platformLeft = go.transform.Find("PlatformLeft").gameObject;
        GameObject platformRight = go.transform.Find("PlatformRight").gameObject;
        BoxCollider collider = go.transform.Find("Collider").GetComponent<BoxCollider>();

        collider.isTrigger = true;

        float halfLengthWithoutHole = (fieldWidth - holeSize) / 2;
        float randomHolePosition = Random.Range(-halfLengthWithoutHole, halfLengthWithoutHole);

        collider.center = new Vector3(randomHolePosition, 0.0f, 0f);
        collider.size = new Vector3(holeSize, 1f, platformDepth);
        float platformLengthLeft = halfLengthWithoutHole + randomHolePosition;
        float platformLengthRight = halfLengthWithoutHole - randomHolePosition;
        platformLeft.transform.localScale = new Vector3 (platformLengthLeft, 1f, platformDepth);
        //platformLeft.transform.position = new Vector3(-(platformLengthLeft / 2), platformLeft.transform.position.y);  //mistake in calcs adds onto time
        platformLeft.transform.position = new Vector3(-(fieldWidth/2) + platformLengthLeft/2, platformLeft.transform.position.y); //fixed in 5 minutes :D
        platformRight.transform.localScale = new Vector3 (platformLengthRight, 1f, platformDepth);
        //platformRight.transform.position = new Vector3(platformLengthRight / 2, platformRight.transform.position.y);
        platformRight.transform.position = new Vector3(fieldWidth / 2 - platformLengthRight / 2, platformLeft.transform.position.y);  

    }
}