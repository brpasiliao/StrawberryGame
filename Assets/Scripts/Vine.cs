using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour {
    public GameObject head;     // tip of vine
    private Transform ht;
    public float posOG;         // original position of head
    public float posStep;       // how long each movement is
    public float posSpeed;      // how much time in between each step
    public float posMax;        // the farthest length it can go
    public float posBack;       // multiplier to retract faster
    public bool stretching;

    void Start() {
        ht = head.transform;
    }

    void Update() {
        if (!stretching) {
            if (Input.GetAxisRaw("Vertical") > 0) transform.rotation = Quaternion.Euler(0, 0, 90);
            if (Input.GetAxisRaw("Vertical") < 0) transform.rotation = Quaternion.Euler(0, 0, -90);
            if (Input.GetAxisRaw("Horizontal") > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
            if (Input.GetAxisRaw("Horizontal") < 0) transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKeyDown("space")) StartCoroutine("StretchVine");
    }

    IEnumerator StretchVine() {     
        stretching = true;

        while (ht.localPosition.x < posMax) {
            ht.Translate(posStep, 0, 0);
            yield return new WaitForSeconds(posSpeed);
        }

        while (ht.localPosition.x > posOG) {
            ht.Translate(-posStep*posBack, 0, 0);
            yield return new WaitForSeconds(posSpeed);
        }

        ht.localPosition = new Vector3 (posOG, 0, -2);
        stretching = false;
    }
}
