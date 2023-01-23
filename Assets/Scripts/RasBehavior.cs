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

    void Update()
    {
        if (!SpringLeaf.launching)
            FollowStrawbert();
    }

    private void FollowStrawbert()
    {
        posDiffX = Math.Abs(strawbert.position.x - transform.position.x);
        posDiffY = Math.Abs(strawbert.position.y - transform.position.y);
        if (posDiffX > maxDistanceX || posDiffY > maxDistanceX)
            transform.position = Vector2.MoveTowards(transform.position, strawbert.position, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ThrownCollisionEnter(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ThrownCollisionExit(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ThrownTriggerEnter(collision);
    }



    public void ThrownCollisionEnter(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT)
        {
            HittingSomething = true;
        }
        else if (collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching)
        {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            InRiver = true;
        }
    }

    public void ThrownCollisionExit(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT)
        {
            HittingSomething = false;
        }
        else if (collision.gameObject.tag == Tags.RIVERCOLLISION)
        {
            //inRiver = false;
        }
    }

    public void ThrownTriggerEnter(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching)
        {
            InRiver = false;
        }
    }

    public void ThrowingObject()
    {
        enabled = false;
    }

    public void ResetObject()
    {
        enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
