using System;
using UnityEngine;
using System.Collections;

public static class EventBroker {
    public static event Action onResourceCollection;
    public static event Action onValuableCollection;
    public static event Action<Transform> onCameraTarget;
    public static event Action<Collider2D> onConfinerSwitch;
    public static event Action onPlayerCamera;
    public static event Action onFlowerReach;

    public static event Action<bool> onSetCanMove;

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

    public static void CallSetCanMove(bool can) {
        onSetCanMove?.Invoke(can);
    }

    public static void CallFlowerReach() {
        onFlowerReach?.Invoke();
    }

    public static void CallConfinerSwitch(Collider2D confiner) {
        onConfinerSwitch?.Invoke(confiner);
    }
}
