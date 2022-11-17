using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Environmental {
    void Start() {
    }

    void Update() {}

    protected override void Primary() {
        transform.SetParent(flower.transform);
        flower.StartCoroutine("Retract");
        // transform.SetParent(null);
    }

    protected override void Secondary() {
        transform.SetParent(flower.transform);
        flower.StartCoroutine("Reach");
    }
}
