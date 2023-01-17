using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertBehavior : MonoBehaviour {
    public float speed;
    private Vector3 destination;

    private void OnEnable()
    {
        EventBroker.onSpringleafActivation += SpringLeafToss;
    }

    private void OnDisable()
    {
        EventBroker.onSpringleafActivation -= SpringLeafToss;
    }

    void Start() {}

    void Update() {
        if (!Flower.reaching && !SpringLeaf.launching) 
            Walk();
    }

    void Walk() {
        destination = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        destination.Normalize();
        transform.Translate(destination.x*speed, destination.y*speed, 0);
    }

    void SpringLeafToss(SpringLeafDirections direction, SpringLeafObject thrownObject,int distance) {
        if (thrownObject.ToString() == gameObject.tag) {
            if (direction == SpringLeafDirections.LeftRight) {
                //newPos = new Vector3(transform.position.x + s)
                //while(Vector3.Distance(transform.position, )))
                transform.Translate(new Vector3(distance, 0, 0));
            }
            else if (direction == SpringLeafDirections.UpDown) {
                transform.Translate(new Vector3(0, -distance, 0));
            }
        }
    }

}
