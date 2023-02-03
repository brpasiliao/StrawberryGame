using System.Collections;
using UnityEngine;

public class Flower : MonoBehaviour {
    public StrawbertBehavior strawbertB;

    public float posOG;         // original position of flower
    public float posStep;       // how long each movement is
    public float posMax;        // the farthest length it can go
    public float posBack;       // multiplier to retract faster
    public float lerpSmoothing;
    public float lerpCutoff;
    public bool canReach = true;

    public bool reaching = false;   // animation state

    void Update() {
        if (canReach && Input.GetKeyDown("space")) {
            StartCoroutine("Reach");
            EventBroker.CallFlowerReach();
        }
    }

    public void SetCanReach(bool can) {
        if (can) canReach = true;
        else canReach = false;
    }

    public IEnumerator Reach() {
        GetComponent<BoxCollider2D>().enabled = true;
        strawbertB.SetCanFunction(false);
        reaching = true; 

        Vector3 end = new Vector3(posMax, 0, 0);
        while (transform.localPosition.x < posMax-lerpCutoff) {
            transform.localPosition = Vector2.Lerp(transform.localPosition, end, lerpSmoothing * Time.deltaTime);            // transform.Translate(posStep, 0, 0);
            yield return null;
        }

        GetComponent<BoxCollider2D>().enabled = false;
        Release();
        StartCoroutine(Retract()); 
        AstarPath.active.Scan();
    }

    public IEnumerator Retract() {
        Debug.Log("retract");
        Vector3 end = new Vector3(posOG, 0, 0);
        while (transform.localPosition.x > posOG+lerpCutoff) {
            transform.localPosition = Vector2.Lerp(transform.localPosition, end, lerpSmoothing * Time.deltaTime);
            yield return null;
        }

        if (transform.childCount > 0) {
            if (transform.GetChild(0).GetComponent<Environmental>() != null)
                Release();
            else if (transform.GetChild(0).GetComponent<Collectible>() != null)
                transform.GetChild(0).GetComponent<Collectible>().Collect();
        }
        
        strawbertB.SetCanFunction(true);
        reaching = false; 
        transform.localPosition = new Vector2 (posOG, transform.localPosition.y);
        GetComponent<BoxCollider2D>().enabled = false;
        EventBroker.CallFlowerReach();
        AstarPath.active.Scan();
    }

    public IEnumerator Grapple() {
        Debug.Log("grapple");
        transform.SetParent(null);
        strawbertB.transform.SetParent(transform);

        Vector3 end = new Vector3(posOG, 0, 0);
        while (strawbertB.transform.localPosition.x < posOG-lerpCutoff && !strawbertB.HittingSomething) {
            strawbertB.transform.localPosition = Vector2.Lerp(strawbertB.transform.localPosition, end, lerpSmoothing * Time.deltaTime);
            yield return null;
        }

        strawbertB.transform.SetParent(null);
        transform.SetParent(strawbertB.stem.transform);
        transform.localPosition = new Vector2 (posOG, transform.localPosition.y);
        reaching = false; 
        strawbertB.SetCanFunction(true);
    }

    private void Release() {
        if (transform.childCount > 0) {
            Transform child = transform.GetChild(0);
            transform.GetChild(0).SetParent(child.GetComponent<Environmental>().parentOG);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.GetComponent<Grabbable>() != null) {
            StopCoroutine("Reach");
            StartCoroutine(collider.GetComponent<Grabbable>().GrabAction());
        } else if (collider.GetComponent<LogBreak>() != null || collider.tag == Tags.WALLCOLLISION) {
            StopCoroutine("Reach");
            StartCoroutine(Retract());
        }
    }
}