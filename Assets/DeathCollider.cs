using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    MainCharacter player => GameObject.Find("MainCharacter").GetComponent<MainCharacter>();
    private const int playerLayer = 10;
    private const int platformLayer = 11;
    private const int roofLayer = 8;
    private const int floorLayer = 12;

//1hr
    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.name.Equals("PlatformLeft") ) {
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        if(other.gameObject.layer == playerLayer) {
            player.GameOver();
        }
    }
}
