using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLeaf : MonoBehaviour {
    public RasBehavior Ras;
    public GameObject pointerGO;
    public Pointer pointerC;

    public static bool launching;

    private List<Transform> nearbyObjects;
    // private Transform selected;

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
                if (index == 0) index = nearbyObjects.Count-1;
                else index--;
                pointerC.PointTo(nearbyObjects[index]);
            } else if (Input.GetKeyDown(KeyCode.E)) {
                if (index == nearbyObjects.Count-1) index = 0;
                else index++;
                pointerC.PointTo(nearbyObjects[index]);
            }

            yield return null;
        }

        launching = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        nearbyObjects[index].position = transform.position;

        while (!Input.GetKeyDown(KeyCode.Z)) yield return null;

        GetComponent<CapsuleCollider2D>().enabled = true;
        launching = false;
    }
}
