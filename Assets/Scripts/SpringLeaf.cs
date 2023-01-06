using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringLeaf : MonoBehaviour {
    void Start() {}

    void Update() {}

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<StrawbertBehavior>() != null) {
            // strawbert = collider.gameObject.GetComponent<StrawbertMovement>();
            collider.transform.position = transform.position;
        }
    }
}
