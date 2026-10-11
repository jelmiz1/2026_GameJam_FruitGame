using UnityEngine;

public class PlayerAnimationState : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Velocity", controller.velocity.y);

        if (controller.velocity.x == 0) {
            animator.SetBool("Moving", false);
        }
        else
            animator.SetBool("Moving",true);
        if((int)controller.velocity.y < -2)
         animator.SetBool("Falling", true);
        else
            animator.SetBool("Falling", false);
        animator.SetBool("Grounded", controller.isGrounded);

    }
}
