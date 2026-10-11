using UnityEngine;

public class PlayerAnimationState : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private FruitSpawner fruitSpawner;
    private Animator animator;
    [SerializeField] private float transitionDelay = 0.25f; //delay for stopping run animation
    [SerializeField] private float delayCounter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        delayCounter = transitionDelay;
    }

    private void Update()
    {
        animator.SetFloat("Velocity", controller.velocity.y);

        // I NEED A DELAY FOR THIS BRUHH
        if ((int)controller.velocity.x == 0) // we're casting to int bc these floats can be assholes
            delayCounter = delayCounter - Time.deltaTime; // counting down if not moving
        else
            delayCounter = transitionDelay; // reset it if moving
        if (delayCounter <= 0) { // once stopped for long enough we get to stop moving yay
            animator.SetBool("Moving", false);
        }
        else
            animator.SetBool("Moving",true);


        if((int)controller.velocity.y < 0)
         animator.SetBool("Falling", true);
        else
            animator.SetBool("Falling", false);
        animator.SetBool("Grounded", controller.isGrounded);
        animator.SetBool("Winding", fruitSpawner.isCharging);

    }
}
