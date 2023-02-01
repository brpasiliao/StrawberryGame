using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {
    public bool isMush = false;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (isMush) GetMushed();
    }

    public void GetMushed() {
        Destroy(gameObject);
    }
}
