using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {
    public GameObject strawbertPlayer;

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
        GetComponent<CinemachineVirtualCamera>().Follow = strawbertPlayer.transform;
    }
}
