using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavy : Environmental {
    protected override void Primary() {
        StartCoroutine(flower.Grapple());
    }

    protected override void Secondary() {
        Debug.Log("heavy secondary");
        transform.SetParent(flower.transform);
        StartCoroutine(PushOrPull());
    }

    protected override void Cancel() {
        flower.strawbertB.transform.SetParent(null);
        flower.transform.SetParent(flower.strawbertB.stem.transform);
        StartCoroutine(flower.Retract());
    }

    // dont judge this i promise i will fix
    // private IEnumerator PushOrPull() {
    //     while (!Input.GetKeyDown(KeyCode.UpArrow) ||
    //         !Input.GetKeyDown(KeyCode.DownArrow) ||
    //         !Input.GetKeyDown(KeyCode.LeftArrow) ||
    //         !Input.GetKeyDown(KeyCode.RightArrow))
    //             { yield return null; }

    //     Debug.Log("chose direction");

    //     if (flower.strawbertB.stem.direction == Directions.NORTH) {
    //         if (Input.GetKeyDown(KeyCode.UpArrow)) {
    //             transform.SetParent(flower.transform);
    //             flower.StartCoroutine("Reach");
    //         } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
    //             transform.SetParent(flower.transform);
    //             StartCoroutine(flower.Retract());
    //         }
    //     } else if (flower.strawbertB.stem.direction == Directions.SOUTH) {
    //         if (Input.GetKeyDown(KeyCode.UpArrow)) {
    //             transform.SetParent(flower.transform);
    //             StartCoroutine(flower.Retract());
    //         } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
    //             transform.SetParent(flower.transform);
    //             flower.StartCoroutine("Reach");
    //         }
    //     } else if (flower.strawbertB.stem.direction == Directions.WEST) {
    //         if (Input.GetKeyDown(KeyCode.LeftArrow)) {
    //             transform.SetParent(flower.transform);
    //             StartCoroutine(flower.Retract());
    //         } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
    //             transform.SetParent(flower.transform);
    //             flower.StartCoroutine("Reach");
    //         }
    //     } else if (flower.strawbertB.stem.direction == Directions.EAST) {
    //         if (Input.GetKeyDown(KeyCode.LeftArrow)) {
    //             transform.SetParent(flower.transform);
    //             flower.StartCoroutine("Reach");
    //         } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
    //             transform.SetParent(flower.transform);
    //             StartCoroutine(flower.Retract());
    //         }
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<RockScratch>() != null) {
            flower.StopCoroutine("Reach");
            StartCoroutine(flower.Retract());
        }
    }
}
