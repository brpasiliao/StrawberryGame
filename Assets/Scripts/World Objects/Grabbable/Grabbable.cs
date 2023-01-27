using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Grabbable : MonoBehaviour {
    protected static Flower flower;

    protected virtual void Start() {
        flower = GameObject.FindWithTag(Tags.PLAYER).GetComponent<StrawbertBehavior>().flower;
    }

    public virtual IEnumerator GrabAction() {
        yield return null;
    }
}
