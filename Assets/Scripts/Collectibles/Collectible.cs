using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<StrawbertBehavior>()) {
            Destroy(gameObject);
        } 
    }
}
