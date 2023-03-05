using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour {
    StrawbertBehavior strawbertB;
    public Collider2D firstArea;

    private void Start() {
        strawbertB = GameObject.FindWithTag(Tags.PLAYER).GetComponent<StrawbertBehavior>();
        GetComponent<CinemachineConfiner>().m_BoundingShape2D = firstArea;
    }

    private void OnEnable() {
        EventBroker.onCameraTarget += ChangeTarget;
        EventBroker.onPlayerCamera += BackToPlayer;
        EventBroker.onConfinerSwitch += ConfinerChange;
    }

    private void OnDisable() {
        EventBroker.onCameraTarget -= ChangeTarget;
        EventBroker.onPlayerCamera -= BackToPlayer;
        EventBroker.onConfinerSwitch -= ConfinerChange;
    }

    public void ChangeTarget(Transform target) {
        GetComponent<CinemachineVirtualCamera>().Follow = target;
    }

    public void BackToPlayer() {
        GetComponent<CinemachineVirtualCamera>().Follow = strawbertB.transform;
    }

    public void ConfinerChange(Collider2D confiner) {
        GetComponent<CinemachineConfiner>().m_BoundingShape2D = confiner;
    }
}
