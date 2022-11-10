using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environmental : Grabbable {
    void Start() {}

    void Update() {}

    // private void OnTriggerEnter2D(Collider2D collider) {
    //     if (collider.gameObject.GetComponent<Flower>() != null)
    //         Flower flower = collider.gameObject.GetComponent<Flower>();
    //         StopCoroutine("flower.Reach");
    //         StartCoroutine("GrabAction", collider.gameObject);
    //     }
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

    // void Primary() {}

    // void Secondary() {}
}
