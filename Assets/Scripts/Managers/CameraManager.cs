using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {
    StrawbertBehavior strawbertB;

    private void Start() {
        strawbertB = GameObject.FindWithTag(Tags.PLAYER).GetComponent<StrawbertBehavior>();
    }

    private void OnEnable() {
        EventBroker.onCameraTarget += ChangeTarget;
        EventBroker.onPlayerCamera += BackToPlayer;
    }

    private void OnDisable() {
        EventBroker.onCameraTarget -= ChangeTarget;
        EventBroker.onPlayerCamera -= BackToPlayer;
    }

    public void ChangeTarget(Transform target) {
        GetComponent<CinemachineVirtualCamera>().Follow = target;
    }

    public void BackToPlayer() {
        GetComponent<CinemachineVirtualCamera>().Follow = strawbertB.transform;
    }
}
