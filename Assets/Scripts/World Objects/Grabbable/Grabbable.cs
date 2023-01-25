using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grabbable : MonoBehaviour {
    protected static Flower flower;

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<Flower>() != null) {
            flower = collider.gameObject.GetComponent<Flower>();
            flower.StopCoroutine("Reach");
            StartCoroutine(GrabAction());
        }
    }

    protected virtual IEnumerator GrabAction() {
        Debug.Log("grabaction");
        flower.grabbing = true;
        // flower.transform.position = transform.position;

        yield return null;
    }
}
