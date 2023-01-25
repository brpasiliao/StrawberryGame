using System;
using UnityEngine;
using System.Collections;

public static class EventBroker {
    public static event Action onResourceCollection;
    public static event Action onValuableCollection;
    public static event Action<Transform> onCameraTarget;
    public static event Action onPlayerCamera;

    public static void CallResourceCollection() {
        onResourceCollection?.Invoke();
    }

    public static void CallValuableCollection() {
        onValuableCollection?.Invoke();
    }

    public static void CallCameraTarget(Transform target) {
        onCameraTarget?.Invoke(target);
    }

    public static void CallPlayerCamera() {
        onPlayerCamera?.Invoke();
    }

}
