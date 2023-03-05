using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerSwitch : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == Tags.PLAYER) {
            EventBroker.CallConfinerSwitch(GetComponent<Collider2D>());
        }
    }
}
