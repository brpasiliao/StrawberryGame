using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : Collectible {
    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);
        if (collision.GetComponent<StrawbertBehavior>()) {
            EventBroker.CallValuableCollection();      
        }
    }
}
