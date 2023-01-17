using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Animation States
public class Stem : MonoBehaviour {
    public static string stemDir;

    void Start() {}

    void Update() {
        if (!Flower.reaching && !SpringLeaf.launching){
            ChangeDirection();
        }
        else if (Flower.reaching && !SpringLeaf.launching)
            ReachAnimation();
        
    }

    private void ChangeDirection(){
        if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45); // northeast
            stemDir = Directions.NORTHEAST;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 135); // northwest
            stemDir = Directions.NORTHWEST;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45); //southeast
            stemDir = Directions.SOUTHEAST;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -135); //southwest
            stemDir = Directions.SOUTHWEST;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90); // north
            stemDir = Directions.NORTH;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90); // south
            stemDir = Directions.SOUTH;
        }
        else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // east
            stemDir = Directions.EAST;
        }
        else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180); // west
            stemDir = Directions.WEST;
        }
    }

    private void ReachAnimation(){

    }
}
