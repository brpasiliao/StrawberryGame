using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : Environmental, IThrowable {
    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

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

    public void ThrownCollisionEnter(Collision2D collision)
    {
        throw new System.NotImplementedException();
    }

    public void ThrownCollisionExit(Collision2D collision)
    {
        throw new System.NotImplementedException();
    }

    public void ThrownTriggerEnter(Collider2D collision)
    {
        throw new System.NotImplementedException();
    }

    public void ThrowingObject()
    {
        throw new System.NotImplementedException();
    }

    public void ResetObject()
    {
        throw new System.NotImplementedException();
    }
}
