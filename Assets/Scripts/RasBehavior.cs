using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasBehavior : MonoBehaviour, IThrowable {
    public Transform strawbert;

    public bool canMove = true;
    public bool withStrawbert = true;
    public float waitTeleport;

    public float speed;
    public float maxDistanceX;      // max x distance from strawbert
    public float maxDistanceY;      // max y distance from strawbert
    private float posDiffX = 0;
    private float posDiffY = 0;

    public bool HittingSomething { get; set; }
    public bool InRiver { get; set; }

    void Start() {}

    void Update(){
        // if (!SpringLeaf.launching)
        //     FollowStrawbert();
    }

    /*void SetCanMove(bool can) {
        if (can) GetComponent<Pathfinding>().canMove = true;
        else GetComponent<Pathfinding>().canMove = false;
    }*/

    // private void FollowStrawbert() {
    //     posDiffX = Math.Abs(strawbert.position.x - transform.position.x);
    //     posDiffY = Math.Abs(strawbert.position.y - transform.position.y);
    //     if (posDiffX > maxDistanceX || posDiffY > maxDistanceX)
    //         transform.position = Vector2.MoveTowards(transform.position, strawbert.position, speed);
    // }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            HittingSomething = true;
        }
        else if (collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching) {
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            InRiver = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == Tags.WALLCOLLISION || collision.gameObject.tag == Tags.OBJECT) {
            HittingSomething = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == Tags.RIVERCOLLISION && SpringLeaf.launching) {
            InRiver = false;
        }
    }

    public void ThrowingObject() {
        enabled = false;
    }

    public void ResetObject() {
        enabled = true;
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }

    IEnumerator TeleportToStrawbert() {
        yield return new WaitForSeconds(waitTeleport);
        transform.position = new Vector2(strawbert.position.x-1, strawbert.position.y);
        withStrawbert = true;
    }
}
