using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpringLeafDirections {UpDown, LeftRight}

public class SpringLeaf : MonoBehaviour {
    public RasBehavior ras;
    public GameObject pointerGO;
    public Pointer pointerC;
    public SpringLeafDirections orientation;

    public int distance = 4;
    public float smoothing = 1;

    private List<Transform> nearbyObjects;
    public Transform upTarget, downTarget, leftTarget, rightTarget;
    private GameObject launchedObject;

    // private Transform selected;
    private Vector2 target;
    private Vector3 objOriginalPos;
    private bool clickedDirection;

    void Start() {
        nearbyObjects = new List<Transform>();
    }

    void Update() {}

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<StrawbertBehavior>() != null) {
            if (nearbyObjects.Count == 0) nearbyObjects.Add(collider.transform);
            else nearbyObjects.Insert(0, collider.transform);

            if (ras.withStrawbert) {
                if (nearbyObjects.Count == 1) nearbyObjects.Add(ras.transform);
                else nearbyObjects.Insert(1, ras.transform);
            }

            StartCoroutine("SelectObjectToLaunch");

        } else if (collider.gameObject.GetComponent<Light>() != null) {
            nearbyObjects.Add(collider.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<StrawbertBehavior>() != null) {
            nearbyObjects.Remove(collider.transform);
            nearbyObjects.Remove(ras.transform);
            StopCoroutine("SelectObjectToLaunch");
            pointerGO.SetActive(false);

        } else if (collider.gameObject.GetComponent<Light>() != null) {
            nearbyObjects.Remove(collider.transform);
        }
    }

    IEnumerator SelectObjectToLaunch() {
        int index = 0;
        pointerC.PointTo(nearbyObjects[index]);
        pointerGO.SetActive(true);

        while (!Input.GetKeyDown(KeyCode.F)) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                if (index == 0) index = nearbyObjects.Count - 1;
                else index--;
                pointerC.PointTo(nearbyObjects[index]);
            }
            else if (Input.GetKeyDown(KeyCode.E)) {
                if (index == nearbyObjects.Count - 1) index = 0;
                else index++;
                pointerC.PointTo(nearbyObjects[index]);
            }
            yield return null;
        }

        EventBroker.CallSetCanMove(false);
        pointerGO.SetActive(false);
        // launching = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        objOriginalPos = nearbyObjects[index].position;
        nearbyObjects[index].position = transform.position;

        launchedObject = nearbyObjects[index].gameObject;
        launchedObject.GetComponent<ILaunchable>().BeingLaunched = true;

        StartCoroutine("SelectDirectionToLaunch");
    }

    IEnumerator SelectDirectionToLaunch() {
        while (!Input.GetKeyDown(KeyCode.Z)) {
            if (orientation == SpringLeafDirections.LeftRight) {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && !clickedDirection)
                    StartCoroutine(LaunchingObject(leftTarget));
                else if (Input.GetKeyDown(KeyCode.RightArrow) && !clickedDirection)
                    StartCoroutine(LaunchingObject(rightTarget));

            } else if (orientation == SpringLeafDirections.UpDown) {
                if (Input.GetKeyDown(KeyCode.UpArrow) && !clickedDirection) {
                    StartCoroutine(LaunchingObject(upTarget));
                } else if (Input.GetKeyDown(KeyCode.DownArrow) && !clickedDirection) {
                    
                    StartCoroutine(LaunchingObject(downTarget));
                }
            }
            yield return null;
        }
        
        ResetSpringLeaf();
        launchedObject.GetComponent<ILaunchable>().ResetObject();
        StartCoroutine("SelectObjectToLaunch");
    }

    private IEnumerator LaunchingObject(Transform direction) {
        clickedDirection = true;
        StopCoroutine("SelectDirectionToLaunch");
        EventBroker.CallCameraTarget(launchedObject.transform);

        if (launchedObject.GetComponent<RasBehavior>() != null || launchedObject.GetComponent<StrawbertBehavior>() != null)
            ras.withStrawbert = false;

        while (Vector2.Distance(launchedObject.transform.position, direction.position) > 0.05f && !launchedObject.GetComponent<ILaunchable>().HittingSomething) {
            launchedObject.transform.position = Vector2.Lerp(launchedObject.transform.position, direction.position, smoothing * Time.deltaTime);
            yield return null;
        }
        
        if (launchedObject.GetComponent<ILaunchable>().InRiver) {
            launchedObject.transform.position = objOriginalPos;
            launchedObject.GetComponent<ILaunchable>().InRiver = false;
        }

        ResetSpringLeaf();
        launchedObject.GetComponent<ILaunchable>().ResetObject();
        if (!ras.withStrawbert) StartCoroutine(ras.TeleportToStrawbert());
        EventBroker.CallSetCanMove(true);
    }

    private void ResetSpringLeaf() {
        GetComponent<CapsuleCollider2D>().enabled = true;
        EventBroker.CallPlayerCamera();
        clickedDirection = false;
    }
}
