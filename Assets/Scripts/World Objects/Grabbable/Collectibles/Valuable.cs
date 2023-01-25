using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valuable : Collectible {
    private void Start() {
        CollectEvent = EventBroker.CallValuableCollection;
    }
}
