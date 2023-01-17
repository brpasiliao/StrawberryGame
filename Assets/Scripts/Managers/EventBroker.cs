using System;
using UnityEngine;
using System.Collections;

public static class EventBroker {
    public static event Action onResourceCollection;
    public static event Action onValuableCollection;
    public static event Action<SpringLeafDirections, SpringLeafObject, int> onSpringleafActivation;
    public static event Action onSpringleafDeactivation;

    public static void CallResourceCollection() {
        onResourceCollection?.Invoke();
    }

    public static void CallValuableCollection() {
        onValuableCollection?.Invoke();
    }

    public static void CallSpringleafActivation(SpringLeafDirections direction, SpringLeafObject ThrownObject, int distance) {
        onSpringleafActivation?.Invoke(direction, ThrownObject, distance);
    }

    public static void CallSpringleafdeactivation() {
        onSpringleafDeactivation?.Invoke();
    }

}
