using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpringLeafDirections {UpDown, LeftRight}
public enum SpringLeafObject { Player, Ras, ThrownObject}

public class SpringLeaf : MonoBehaviour {
    public RasBehavior Ras;
    public GameObject pointerGO;
    public Pointer pointerC;

    public static bool launching;
    public SpringLeafDirections orientation;
    public SpringLeafObject objectToThrow;

    public int distance = 4;
    public float smoothing = 1;

    private List<Transform> nearbyObjects;
    public Transform upTarget, downTarget, leftTarget, rightTarget;
    private GameObject thrownObject;

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

            if (Ras.withStrawbert) {
                if (nearbyObjects.Count == 1) nearbyObjects.Add(Ras.transform);
                else nearbyObjects.Insert(1, Ras.transform);
            }

            StartCoroutine("SelectOption");

        } else if (collider.gameObject.GetComponent<RasBehavior>() == null) {
            nearbyObjects.Add(collider.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<StrawbertBehavior>() != null) {
            nearbyObjects.Remove(collider.transform);
            nearbyObjects.Remove(Ras.transform);
            StopCoroutine("SelectOption");
            pointerGO.SetActive(false);

        } else if (collider.gameObject.GetComponent<RasBehavior>() == null) {
            nearbyObjects.Remove(collider.transform);
        }
    }

    IEnumerator SelectOption() {
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

        launching = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        objOriginalPos = nearbyObjects[index].position;
        nearbyObjects[index].position = transform.position;
        ThrowingObject(index);

        while (!Input.GetKeyDown(KeyCode.Z)) {
            thrownObject = nearbyObjects[index].gameObject;
            if (orientation == SpringLeafDirections.LeftRight) {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && !clickedDirection) {
                    clickedDirection = true;
                    StartCoroutine(ThrowingObject(leftTarget));
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow) && !clickedDirection) {
                    clickedDirection = true;
                    StartCoroutine(ThrowingObject(rightTarget));
                }
            } else if (orientation == SpringLeafDirections.UpDown) {
                if (Input.GetKeyDown(KeyCode.UpArrow) && !clickedDirection) {
                    clickedDirection = true;
                    StartCoroutine(ThrowingObject(upTarget));
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow) && !clickedDirection) {
                    clickedDirection = true;
                    StartCoroutine(ThrowingObject(downTarget));
                }
            }

            yield return null;
        }
        ResetSpringLeaf();
        ResetThrownObject();
    }

    private IEnumerator ThrowingObject(Transform direction) {
        while (Vector2.Distance(thrownObject.transform.position, direction.position) > 0.05f && thrownObject.GetComponent<StrawbertBehavior>().hittingSomething != true)
        {
            thrownObject.transform.position = Vector2.Lerp(thrownObject.transform.position, direction.position, smoothing * Time.deltaTime);
            yield return null;
        }
        
        if(thrownObject.GetComponent<StrawbertBehavior>().inRiver == true)
        {
            thrownObject.transform.position = objOriginalPos;
            thrownObject.GetComponent<StrawbertBehavior>().inRiver = false;
        }

        ResetSpringLeaf();
        ResetThrownObject();
    }

    private void ResetSpringLeaf() {
        GetComponent<CapsuleCollider2D>().enabled = true;
        launching = false;
        clickedDirection = false;
    }

    private void ResetThrownObject() {
        if (thrownObject.GetComponent<StrawbertBehavior>() != null)
        {
            thrownObject.GetComponent<StrawbertBehavior>().enabled = true;
            thrownObject.GetComponent<CapsuleCollider2D>().enabled = true;
            thrownObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
        else if (thrownObject.GetComponent<RasBehavior>() != null)
        {
            thrownObject.GetComponent<RasBehavior>().enabled = true;
            thrownObject.GetComponent<CapsuleCollider2D>().enabled = true;
            thrownObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
        else if(thrownObject.GetComponent<Environmental>() != null)
        {
            thrownObject.GetComponent<CapsuleCollider2D>().enabled = true;
            thrownObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        }
    }

    private void ThrowingObject(int index) {
        if (nearbyObjects[index].GetComponent<StrawbertBehavior>() != null) {
            nearbyObjects[index].GetComponent<StrawbertBehavior>().enabled = false;
        }
        else if (thrownObject.GetComponent<RasBehavior>() != null)
        {
            thrownObject.GetComponent<RasBehavior>().enabled = false;
        }
        else if (thrownObject.GetComponent<Environmental>() != null)
        {

        }
    }
}
