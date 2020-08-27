using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour {
    ScoreManager score => FindObjectOfType<ScoreManager>();
    [SerializeField] private float speed;
    GameObject previousObject;

    debugLogDel dbg = (messages) => Debug.Log(messages);
    delegate void debugLogDel(string message);
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        /*
        eww no input manager used x3
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed;
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed;
        }
        */

        //left and right movement
        //5 mins
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, 0.0f);
    }

    public void GameOver() {
        //show Game Over screen
        Time.timeScale = 0f;
        Debug.Log("ME DED");
        Destroy(this.gameObject);
    }

    public void PlayerHitScoreTrigger() {
        score.IncrementScore();
    }

    //1hr - I had problems figuring out why it would trigger 2 times, had the 2 if statements the wrong way aroundand created GameObject previousObject = null in the function, which makes it so the if will always be true ^^"
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name.Equals("Collider")) {
            GameObject platformMotherClass = other.transform.parent.gameObject;
            if (previousObject == null || platformMotherClass != (previousObject)) {
                score.IncrementScore();
                Debug.Log(platformMotherClass.name);
                previousObject = platformMotherClass;
            }
        }
    }
}