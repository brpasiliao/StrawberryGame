using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpringLeafDirections {UpDown, LeftRight}

public class SpringLeaf : Environmental {
    RasBehavior ras;
    public GameObject pointerGO;
    public Pointer pointerC;
    private SpringLeafDirections orientation;

    public int distance = 4;
    public float smoothing = 1;
    public bool pickedUp;

    private List<Transform> nearbyObjects;
    public Transform upTarget, downTarget, leftTarget, rightTarget;
    private GameObject launchedObject;
    private GameObject pit;

    // private Transform selected;
    private Vector2 target;
    private Vector3 objOriginalPos;
    private bool clickedDirection;

    private void OnEnable() {
        EventBroker.onFlowerReach += ToggleCircleCollider;
    }

    private void OnDisable() {
        EventBroker.onFlowerReach -= ToggleCircleCollider;
    }

    protected override void Start() {
        base.Start();
        ras = GameObject.FindWithTag(Tags.RAS).GetComponent<RasBehavior>();
        nearbyObjects = new List<Transform>();
    }

    void Update() {}

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<StrawbertBehavior>() != null && !pickedUp) {
            if (nearbyObjects.Count == 0) nearbyObjects.Add(collider.transform);
            else nearbyObjects.Insert(0, collider.transform);

            if (ras.withStrawbert) {
                if (nearbyObjects.Count == 1) nearbyObjects.Add(ras.transform);
                else nearbyObjects.Insert(1, ras.transform);
            }

            StartCoroutine("SelectObjectToLaunch");

        } else if (collider.gameObject.GetComponent<Light>() != null && !pickedUp) {
            nearbyObjects.Add(collider.transform);
        } else if (collider.gameObject.GetComponent<Pit>() != null && pickedUp) {
            nearbyObjects.Add(collider.transform);
            StartCoroutine(PlaceOnPit());
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
        } else if(collider.gameObject.GetComponent<Pit>() != null) {
            nearbyObjects.Remove(collider.transform);
            StopCoroutine("PlaceOnPit");
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

            } else if (Input.GetKeyDown(KeyCode.E)) {
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
        nearbyObjects[index].position = gameObject.transform.position;

        launchedObject = nearbyObjects[index].gameObject;
        launchedObject.GetComponent<ILaunchable>().BeingLaunched = true;
        launchedObject.GetComponent<ILaunchable>().PosOG = launchedObject.transform.position;
        EventBroker.CallCameraTarget(launchedObject.transform);

        StartCoroutine("SelectDirectionToLaunch");
    }

    IEnumerator PlaceOnPit() {
        int index = 0;
        pointerC.PointTo(nearbyObjects[index]);
        pointerGO.SetActive(true);

        while (!Input.GetKeyDown(KeyCode.F)) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                Debug.Log("log");
                orientation = SpringLeafDirections.UpDown;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (index == 0) index = nearbyObjects.Count - 1;
                    else index--;
                pointerC.PointTo(nearbyObjects[index]);
            } else if (Input.GetKeyDown(KeyCode.E)) {
                Debug.Log("lag");
                orientation = SpringLeafDirections.LeftRight;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                if (index == nearbyObjects.Count - 1) index = 0;
                    else index++;
                pointerC.PointTo(nearbyObjects[index]);
            }
            yield return null;
        }

        pit = nearbyObjects[index].gameObject;
        transform.position = pit.transform.position;
        transform.SetParent(pit.transform);
        ChangingLocation();
        if (nearbyObjects.Count > 1) {
            nearbyObjects.Clear();
        }
        StopCoroutine("PlaceOnPit");
        //launchedObject.GetComponent<CapsuleCollider2D>().enabled = false;
        

    }

    IEnumerator SelectDirectionToLaunch() {
        while (!Input.GetKeyDown(KeyCode.Z)) {
            if (orientation == SpringLeafDirections.LeftRight) {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && !clickedDirection)
                    StartCoroutine(LaunchingObject(upTarget));
                else if (Input.GetKeyDown(KeyCode.RightArrow) && !clickedDirection)
                    StartCoroutine(LaunchingObject(downTarget));

            } else if (orientation == SpringLeafDirections.UpDown) {
                if (Input.GetKeyDown(KeyCode.UpArrow) && !clickedDirection)
                    StartCoroutine(LaunchingObject(upTarget));
                else if (Input.GetKeyDown(KeyCode.DownArrow) && !clickedDirection)
                    StartCoroutine(LaunchingObject(downTarget));
            }
            yield return null;
        }
        
        ResetSpringLeaf();
        launchedObject.GetComponent<ILaunchable>().ResetObject();
        StartCoroutine("SelectObjectToLaunch");
        EventBroker.CallSetCanMove(true);
    }

    private IEnumerator LaunchingObject(Transform direction) {
        clickedDirection = true;
        StopCoroutine("SelectDirectionToLaunch");

        if (launchedObject.GetComponent<RasBehavior>() != null || launchedObject.GetComponent<StrawbertBehavior>() != null)
            ras.withStrawbert = false;

        while (Vector2.Distance(launchedObject.transform.position, direction.position) > 0.05f && !launchedObject.GetComponent<ILaunchable>().HittingSomething) {
            launchedObject.transform.position = Vector2.Lerp(launchedObject.transform.position, direction.position, smoothing * Time.deltaTime);
            yield return null;
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

    protected override void Primary() {
        StartCoroutine(flower.Retract());
        ChangingLocation();
    }

    protected override void Secondary() {
        transform.SetParent(flower.transform);
        flower.StartCoroutine("Reach");
    }

    protected override void Cancel() {
        StartCoroutine(flower.Retract());
    }

    private void ToggleCircleCollider() {
        if (GetComponent<CircleCollider2D>().enabled) 
            GetComponent<CircleCollider2D>().enabled = false;
        else
            GetComponent<CircleCollider2D>().enabled = true;
    }

    private void ChangingLocation() {
        if (!pickedUp) {
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            if(pit != null)
            pit.GetComponent<CapsuleCollider2D>().enabled = true;
            transform.SetParent(ras.transform);
            transform.position = ras.transform.position;
            pickedUp = true;
        } else {
            GetComponent<CapsuleCollider2D>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = true;
            if(pit != null)
            pit.GetComponent<CapsuleCollider2D>().enabled = false;
            pickedUp = false;
        }
    }

    private void Planted() {
        GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        if (pit != null)
            pit.GetComponent<CapsuleCollider2D>().enabled = true;
        pickedUp = false;
    }
}
