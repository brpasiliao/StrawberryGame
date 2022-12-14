using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasBehavior : MonoBehaviour {
    public Transform strawbert;

    public bool withStrawbert = true;

    public float speed;
    public float maxDistanceX;      // max x distance from strawbert
    public float maxDistanceY;      // max y distance from strawbert
    private float posDiffX = 0;
    private float posDiffY = 0;

    void Start() {}

    void Update() {
        posDiffX = Math.Abs(strawbert.position.x - transform.position.x);
        posDiffY = Math.Abs(strawbert.position.y - transform.position.y);
        if (posDiffX > maxDistanceX || posDiffY > maxDistanceX)
            transform.position = Vector2.MoveTowards(transform.position, strawbert.position, speed);
    }
}
