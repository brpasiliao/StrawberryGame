using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertAnimation : MonoBehaviour {
    private Animator animator;
    private string currentState;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (!Flower.reaching && !SpringLeaf.launching)
            moveAnimation();
        else if (Flower.reaching && !SpringLeaf.launching)
            ReachAnimation();
    }

    void moveAnimation(){
        if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0){
            ChangeAnimationState(PlayerAnimations.NORTHEAST); //northEast;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0){
            ChangeAnimationState(PlayerAnimations.NORTHWEST);//northWest;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0){
            ChangeAnimationState(PlayerAnimations.SOUTHEAST);//southEast;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0){
            ChangeAnimationState(PlayerAnimations.SOUTHWEST);//southWest;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0){
            ChangeAnimationState(PlayerAnimations.NORTH);//north;
        }
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0){
            ChangeAnimationState(PlayerAnimations.SOUTH);//south;
        }
        else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0){
            ChangeAnimationState(PlayerAnimations.EAST);//east;
        }
        else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0){
            ChangeAnimationState(PlayerAnimations.WEST);//west;
        }
        else{
            ChangeAnimationState(PlayerAnimations.IDLE);
        }
    }

    void ReachAnimation()
    {
        if(Stem.stemDir == Directions.NORTH){
            ChangeAnimationState(FlowerAnimations.NORTH);
        }
        else if(Stem.stemDir == Directions.EAST){
            ChangeAnimationState(FlowerAnimations.EAST);
        }
        else if (Stem.stemDir == Directions.WEST){
            ChangeAnimationState(FlowerAnimations.WEST);
        }
        else if (Stem.stemDir == Directions.SOUTH){
            ChangeAnimationState(FlowerAnimations.SOUTH);
        }
        else if (Stem.stemDir == Directions.NORTHEAST){
            ChangeAnimationState(FlowerAnimations.NORTHEAST);
        }
        else if (Stem.stemDir == Directions.NORTHWEST){
            ChangeAnimationState(FlowerAnimations.NORTHWEST);
        }
        else if (Stem.stemDir == Directions.SOUTHEAST){
            ChangeAnimationState(FlowerAnimations.SOUTHEAST);
        }
        else if (Stem.stemDir == Directions.SOUTHWEST){
            ChangeAnimationState(FlowerAnimations.SOUTHWEST);
        }
    }

    void ChangeAnimationState(string newState){
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        //play the animation
        animator.Play(newState);

        //reassign the current state
        currentState = newState;
    }
}
