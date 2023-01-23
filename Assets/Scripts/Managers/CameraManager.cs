using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public GameObject strawbertPlayer;

    private void OnEnable()
    {
        EventBroker.onCameraTarget += changeTarget;
        EventBroker.onPlayerCamera += backToPlayer;
    }

    private void OnDisable()
    {
        EventBroker.onCameraTarget -= changeTarget;
        EventBroker.onPlayerCamera -= backToPlayer;
    }

    public void changeTarget(Transform target)
    {
        GetComponent<CinemachineVirtualCamera>().Follow = target;
    }

    public void backToPlayer()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = strawbertPlayer.transform;
    }
}
