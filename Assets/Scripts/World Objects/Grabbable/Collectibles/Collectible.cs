using System;
using UnityEngine;
using System.Collections;

public abstract class Collectible : Grabbable {
    protected Action CollectEvent;

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        base.OnTriggerEnter2D(collider);

        if (collider.GetComponent<StrawbertBehavior>())
            Collect();
    }

    public void Collect() {
        CollectEvent();
        Destroy(gameObject);
    }

    protected override IEnumerator GrabAction() {
        yield return StartCoroutine(base.GrabAction());
        transform.SetParent(flower.transform);
        StartCoroutine(flower.Retract());
    }
}
