using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Environmental : Grabbable {
    public Transform parentOG;

    protected void Start() {
        parentOG = transform.parent;
    }

    protected override IEnumerator GrabAction() {
        yield return StartCoroutine(base.GrabAction());

        while (!Input.anyKey) { yield return null; }

        if (Input.GetKeyDown("space")) Primary();
        else if (Input.GetKeyDown(KeyCode.F)) Secondary();
        else StartCoroutine(flower.Retract());
    }

    protected virtual void Primary() {}

    protected virtual void Secondary() {}
}
