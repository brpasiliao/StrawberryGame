using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawbertAnimation : MonoBehaviour {
    public StrawbertBehavior strawbertB;

    private Animator animator;
    private string currentState;

    public bool canAnimate = true;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (strawbertB.flower.reaching) ReachAnimation();
        else if (canAnimate) MoveAnimation();
    }

    public void SetCanAnimate(bool can) {
        if (can) canAnimate = true;
        else canAnimate = false;
    }

    void MoveAnimation() {
        if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0)
            ChangeAnimationState(PlayerAnimations.NORTHEAST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0)
            ChangeAnimationState(PlayerAnimations.NORTHWEST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0)
            ChangeAnimationState(PlayerAnimations.SOUTHEAST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0)
            ChangeAnimationState(PlayerAnimations.SOUTHWEST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) > 0)
            ChangeAnimationState(PlayerAnimations.NORTH);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) < 0)
            ChangeAnimationState(PlayerAnimations.SOUTH);
        else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) > 0)
            ChangeAnimationState(PlayerAnimations.EAST);
        else if (Input.GetAxisRaw(PlayerInput.HORIZONTAL) < 0)
            ChangeAnimationState(PlayerAnimations.WEST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) == 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) == 0 && currentState == PlayerAnimations.SOUTH)
            ChangeAnimationState(PlayerAnimations.IDLE);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) == 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) == 0 && currentState == PlayerAnimations.NORTH)
            ChangeAnimationState(PlayerAnimations.IDLENORTH);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) == 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) == 0 && currentState == PlayerAnimations.EAST)
            ChangeAnimationState(PlayerAnimations.IDLEEAST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) == 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) == 0 && currentState == PlayerAnimations.WEST)
            ChangeAnimationState(PlayerAnimations.IDLEWEST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) == 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) == 0 && currentState == PlayerAnimations.SOUTHEAST)
            ChangeAnimationState(PlayerAnimations.IDLESOUTHEAST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) == 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) == 0 && currentState == PlayerAnimations.SOUTHWEST)
            ChangeAnimationState(PlayerAnimations.IDLESOUTHWEST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) == 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) == 0 && currentState == PlayerAnimations.NORTHEAST)
            ChangeAnimationState(PlayerAnimations.IDLENORTHEAST);
        else if (Input.GetAxisRaw(PlayerInput.VERTICAL) == 0 && Input.GetAxisRaw(PlayerInput.HORIZONTAL) == 0 && currentState == PlayerAnimations.NORTHWEST)
            ChangeAnimationState(PlayerAnimations.IDLENORTHWEST);
    }

    void ReachAnimation() {
        if (strawbertB.stem.direction == Directions.NORTH)
            ChangeAnimationState(FlowerAnimations.NORTH);
        else if (strawbertB.stem.direction == Directions.EAST)
            ChangeAnimationState(FlowerAnimations.EAST);
        else if (strawbertB.stem.direction == Directions.WEST)
            ChangeAnimationState(FlowerAnimations.WEST);
        else if (strawbertB.stem.direction == Directions.SOUTH)
            ChangeAnimationState(FlowerAnimations.SOUTH);
        else if (strawbertB.stem.direction == Directions.NORTHEAST)
            ChangeAnimationState(FlowerAnimations.NORTHEAST);
        else if (strawbertB.stem.direction == Directions.NORTHWEST)
            ChangeAnimationState(FlowerAnimations.NORTHWEST);
        else if (strawbertB.stem.direction == Directions.SOUTHEAST)
            ChangeAnimationState(FlowerAnimations.SOUTHEAST);
        else if (strawbertB.stem.direction == Directions.SOUTHWEST)
            ChangeAnimationState(FlowerAnimations.SOUTHWEST);
    }

    void ChangeAnimationState(string newState) {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        //play the animation
        animator.Play(newState);

        //reassign the current state
        currentState = newState;
    }
}
