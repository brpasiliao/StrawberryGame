using System;
using UnityEngine;
using System.Collections;

public abstract class Collectible : Grabbable {
    protected Action CollectEvent;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<StrawbertBehavior>())
            Collect();
    }

    public void Collect() {
        CollectEvent();
        Destroy(gameObject);
    }

    public override IEnumerator GrabAction() {
        yield return StartCoroutine(base.GrabAction());
        transform.SetParent(flower.transform);
        StartCoroutine(flower.Retract());
    }
}
