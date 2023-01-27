using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBreak : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.GetComponent<Light>() && collision.gameObject.GetComponent<ILaunchable>().BeingLaunched)
            Destroy(gameObject);
    }
}
