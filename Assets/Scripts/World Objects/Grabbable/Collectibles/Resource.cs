using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Collectible {
    private void Start() {
        CollectEvent = EventBroker.CallResourceCollection;
    }
}
