using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasBehavior : MonoBehaviour, IThrowable {
    public Transform strawbert;

    public bool withStrawbert = true;

    public float speed;
    public float maxDistanceX;      // max x distance from strawbert
    public float maxDistanceY;      // max y distance from strawbert
    private float posDiffX = 0;
    private float posDiffY = 0;

    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

    void Start() {}

    void Update() {
        posDiffX = Math.Abs(strawbert.position.x - transform.position.x);
        posDiffY = Math.Abs(strawbert.position.y - transform.position.y);
        if (posDiffX > maxDistanceX || posDiffY > maxDistanceX)
            transform.position = Vector2.MoveTowards(transform.position, strawbert.position, speed);
    }

    public void ResetObject()
    {
        throw new NotImplementedException();
    }

    public void ThrowingObject()
    {
        throw new NotImplementedException();
    }

    public void ThrownCollisionEnter(Collision2D collision)
    {
        throw new NotImplementedException();
    }

    public void ThrownCollisionExit(Collision2D collision)
    {
        throw new NotImplementedException();
    }

    public void ThrownTriggerEnter(Collider2D collision)
    {
        throw new NotImplementedException();
    }
}
