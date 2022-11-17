using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
    public Stem stem;

    public float posOG;         // original position of head
    public float posStep;       // how long each movement is
    public float posSpeed;      // how much time in between each step
    public float posMax;        // the farthest length it can go
    public float posBack;       // multiplier to retract faster

    public bool reaching = false;
    public bool grabbing = false;

    void Start() {}

    void Update() {
        if (!reaching && Input.GetKeyDown("space"))
            StartCoroutine("Reach");
    }

    IEnumerator Reach() {
        Debug.Log("reach");

        GetComponent<BoxCollider2D>().enabled = true; // can grab multiple?
        reaching = true;

        while (transform.localPosition.x < posMax) {
            transform.Translate(posStep, 0, 0);
            yield return new WaitForSeconds(posSpeed);
        }

        transform.DetachChildren();
        StartCoroutine("Retract"); 
    }

    public IEnumerator Retract() {
        Debug.Log("retract");
        grabbing = false;

        while (transform.localPosition.x > posOG) {
            transform.Translate(-posStep*posBack, 0, 0);
            yield return new WaitForSeconds(posSpeed);
        }

        transform.DetachChildren();
        transform.localPosition = new Vector3 (posOG, 0, -2);
        reaching = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // private void OnTriggerEnter2D(Collider2D collider) {
    //     if (collider.gameObject.GetComponent<Grabbable>() != null) {
    //         StopCoroutine("Reach");
    //         StartCoroutine("GrabAction", collider.gameObject);
    //     }
    // }

    // IEnumerator Reach() {     
    //     Debug.Log("reach");

    //     GetComponent<BoxCollider2D>().enabled = true; // can grab multiple?
    //     reaching = true;

    //     while (transform.localPosition.x < posMax) {
    //         transform.Translate(posStep, 0, 0);
    //         yield return new WaitForSeconds(posSpeed);
    //     }

    //     transform.DetachChildren();
    //     StartCoroutine("Retract"); 
    // }
    
    // IEnumerator Retract() {
    //     Debug.Log("retract");

    //     while (transform.localPosition.x > posOG) {
    //         transform.Translate(-posStep*posBack, 0, 0);
    //         yield return new WaitForSeconds(posSpeed);
    //     }

    //     transform.DetachChildren();
    //     transform.localPosition = new Vector3 (posOG, 0, -2);
    //     reaching = false;
    //     GetComponent<BoxCollider2D>().enabled = false;
    // }

    // IEnumerator GrabAction(GameObject item) {
    //     Debug.Log("grabaction");
    //     transform.position = item.transform.position;

    //     while (!Input.anyKey) {
    //         yield return null;
    //     }

    //     if (Input.GetKeyDown("space")) {
    //         item.transform.SetParent(transform);
    //         StartCoroutine("Retract");
    //     } else if (Input.GetKeyDown(KeyCode.F)) {
    //         item.transform.SetParent(transform);
    //         StartCoroutine("Reach");
    //     } else 
    //         StartCoroutine("Retract");
    // }
}