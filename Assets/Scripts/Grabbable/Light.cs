using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Environmental {
    new void Start() { base.Start(); }

    void Update() {}

    protected override void Primary() {
        transform.SetParent(flower.transform);
        flower.StartCoroutine("Retract");
    }

    protected override void Secondary() {
        transform.SetParent(flower.transform);
        flower.StartCoroutine("Reach");
    }
}
