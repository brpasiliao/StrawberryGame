using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : Environmental {
    new void Start() { base.Start(); }

    void Update() {}

    protected override void Primary() {
        flower.grappling = true;
        StartCoroutine(flower.Retract());
    }

    protected override void Secondary() {
        transform.SetParent(flower.transform);
        flower.StartCoroutine("Reach");
    }
}
