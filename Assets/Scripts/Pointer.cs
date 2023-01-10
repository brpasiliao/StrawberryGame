using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public float offset = 0;
    void Start() {}

    void Update() {}

    public void PointTo(Transform target) {
        transform.SetParent(target);
        transform.position = new Vector2(target.position.x, target.position.y);
        transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + offset);
    }
}
