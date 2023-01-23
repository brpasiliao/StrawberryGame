using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environmental : Grabbable {
    public Transform parentOG;

    protected void Start() {
        parentOG = transform.parent;
    }

    void Update() {}

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<Flower>() != null) {
            flower = collider.gameObject.GetComponent<Flower>();
            flower.StopCoroutine("Reach");
            StartCoroutine("GrabAction");
        }
    }

    IEnumerator GrabAction() {
        Debug.Log("grabaction");
        flower.grabbing = true;
        // flower.transform.position = transform.position;

        while (!Input.anyKey) { yield return null; }

        if (Input.GetKeyDown("space")) Primary();
        else if (Input.GetKeyDown(KeyCode.F)) Secondary();
        else flower.StartCoroutine("Retract");
    }

    protected virtual void Primary() {}

    protected virtual void Secondary() {}
}
