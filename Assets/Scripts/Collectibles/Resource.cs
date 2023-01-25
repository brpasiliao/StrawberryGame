using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Collectible {
    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);
        if (collision.GetComponent<StrawbertBehavior>()) {
            EventBroker.CallResourceCollection();
        }
    }
}
