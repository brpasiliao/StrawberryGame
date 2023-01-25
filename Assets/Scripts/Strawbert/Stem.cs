using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Animation States
public class Stem : MonoBehaviour {
    public StrawbertBehavior strawbertB;

    public bool canRotate = true;
    public string direction;

    void Update() {
        if (canRotate) ChangeDirection();
    }

    public void SetCanRotate(bool can) {
        if (can) canRotate = true;
        else canRotate = false;
    }

    private void ChangeDirection() {
        if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 45); // northeast
            direction = Directions.NORTHEAST;
        } else if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0) {
            transform.rotation = Quaternion.Euler(0, 0, 135); // northwest
            direction = Directions.NORTHWEST;
        } else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0) {
            transform.rotation = Quaternion.Euler(0, 0, -45); //southeast
            direction = Directions.SOUTHEAST;
        } else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0) {
            transform.rotation = Quaternion.Euler(0, 0, -135); //southwest
            direction = Directions.SOUTHWEST;
        } else if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 90); // north
            direction = Directions.NORTH;
        } else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0) {
            transform.rotation = Quaternion.Euler(0, 0, -90); // south
            direction = Directions.SOUTH;
        } else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0); // east
            direction = Directions.EAST;
        } else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0) {
            transform.rotation = Quaternion.Euler(0, 0, 180); // west
            direction = Directions.WEST;
        }
    }

    void ReachAnimation() {}
}
