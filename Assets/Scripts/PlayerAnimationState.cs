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
        animator.SetFloat("Velocity", controller.velocity.x);

        if (controller.velocity.x == 0) {
            animator.SetBool("Moving", false);
        }
        else
            animator.SetBool("Moving",true);

        animator.SetBool("Grounded", controller.isGrounded);
    }
}
