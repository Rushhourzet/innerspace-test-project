using UnityEditor.Rendering;
using UnityEngine;

//30 minutes of staring at the screen and contemplating life
public class PlatformManager : MonoBehaviour
{
    [SerializeField][Range(0.01f,0.02f)] private float initialSpeed;
    [SerializeField][Range(1f, 1.01f)] private float speedIncreaseOverTime;
    private const float MIN_HOLE_SIZE = 1.2f;
    private const float PLATFORM_DEPTH = 4f;
    private const float FIELD_WIDTH = 11.4f;
    private const float FIELD_HEIGHT = 16.4f;
    [SerializeField][Range (MIN_HOLE_SIZE, FIELD_WIDTH)] private float holeSize;
    [SerializeField][Range(0.8f, 1.2f)] private float spawnStartingFrequency;
    [SerializeField][Range(1f, 1.01f)] private float frequencyIncreaseOverTime;
    private static Vector3 spawnposition => new Vector3(0.0f, -(FIELD_HEIGHT-0.4f) / 2);

    private int platformCounter;
    private float timeSinceLastSpawn;

    private const int playerLayer = 10;


    [SerializeField] private GameObject platformPrefab;

    private SessionScore levelManager => FindObjectOfType<SessionScore>();
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
        //30 mins of playing around with frequency values, also doubled gravity
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
        RandomizePlatform(instantiatedPlatform);
        callback("Platform has been spawned");
    }


    //15 mins + 15mins refactoring
    void RandomizePlatform(GameObject go) {
        GameObject platformLeft = go.transform.Find("PlatformLeft").gameObject;
        GameObject platformRight = go.transform.Find("PlatformRight").gameObject;
        BoxCollider scoreCollider = go.transform.Find("Collider").GetComponent<BoxCollider>();
        scoreCollider.isTrigger = true;

        float halfLengthWithoutHole = (FIELD_WIDTH - holeSize) / 2;
        float holePosition = RandomizeHolePosition(holeSize, scoreCollider, halfLengthWithoutHole);
        AdjustLeftAndRightPlatform(platformLeft, platformRight, holePosition, halfLengthWithoutHole);
    }
    private float RandomizeHolePosition(float holeSize, BoxCollider scoreCollider, float halfLengthWithoutHole) {
        float randomHolePosition = Random.Range(-halfLengthWithoutHole, halfLengthWithoutHole);
        scoreCollider.center = new Vector3(randomHolePosition, 0.0f, 0f);
        scoreCollider.size = new Vector3(holeSize, 1f, PLATFORM_DEPTH);
        return randomHolePosition;
    }
    private void AdjustLeftAndRightPlatform(GameObject platformLeft, GameObject platformRight, float holePosition, float halfLengthWithoutHole) {
        float platformLengthLeft = halfLengthWithoutHole + holePosition;
        float platformLengthRight = halfLengthWithoutHole - holePosition;
        platformLeft.transform.localScale = new Vector3(platformLengthLeft, 1f, PLATFORM_DEPTH);
        platformLeft.transform.position = new Vector3(-(FIELD_WIDTH / 2) + platformLengthLeft / 2, platformLeft.transform.position.y);
        platformRight.transform.localScale = new Vector3(platformLengthRight, 1f, PLATFORM_DEPTH);
        platformRight.transform.position = new Vector3(FIELD_WIDTH / 2 - platformLengthRight / 2, platformLeft.transform.position.y);
    }

}