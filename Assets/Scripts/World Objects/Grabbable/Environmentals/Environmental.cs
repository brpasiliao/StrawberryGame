using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Environmental : Grabbable {
    public Transform parentOG;

    protected override void Start() {
        base.Start();
        parentOG = transform.parent;
    }

    public override IEnumerator GrabAction() {
        yield return StartCoroutine(base.GrabAction());

        while (!Input.anyKey) { 
            yield return null; 
        }

        if (Input.GetKeyDown("space")) Primary();
        else if (Input.GetKeyDown(KeyCode.F)) Secondary();
        else Cancel();
        // else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) != 0 || Input.GetAxisRaw(PlayerInput.VERTICAL) != 0)
        //     Cancel();
    }

    protected virtual void Primary() {}

    protected virtual void Secondary() {}

    protected virtual void Cancel() {}
}
