using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{
    #region SETABLES
        [Header ("External Components")]
            protected Rigidbody2D rgBody;
    #endregion

    #region VARIABLES
            [Header ("Movement")]
                protected float movementSpeed;
                private bool isRunning;

            [Header ("Jump")]
                protected float jumpForce = 15f;
                protected LayerMask groundLayer;
                protected BoxCollider2D floorCollider;
                private bool isGrounded;

            [Header ("Input")]
                protected Vector2 moveInput;
                protected bool jumpingInput; 

            [Header ("Last Jumped")]
                protected float waitJumpChanged;
                protected float jumpChangedCounter;

            [Header ("Coyote Time")]
                protected float waitCoyoteTime;
                protected float  coyoteTimeCounter;
        
            [Header ("JumpingBuffer")]
                protected float waitJumpBufferTime;
                protected float jumpBufferTimeCounter;

            [Header("Animator")]
                [SerializeField]
                protected Animator animator;
                [SerializeField]
                protected GameObject player;

    #endregion

    #region STATICS
    [Header ("Gravity")]
            protected static float gravityScale;
            protected static float fallMultipier = 1.8f;
            protected static float waitTime = 0.12f;
    #endregion

    public void AssignElements(ref Rigidbody2D rgBody, ref LayerMask groundLayer, ref BoxCollider2D floorCollider, float movementSpeed)
    {
        this.rgBody = rgBody;
        this.groundLayer = groundLayer;
        this.floorCollider = floorCollider;
        this.movementSpeed = movementSpeed;

        gravityScale = rgBody.gravityScale;

        waitJumpChanged = waitTime;
        waitCoyoteTime = waitTime;
        waitJumpBufferTime = waitTime;
    }

    public void Execute(bool isAlive, bool isGrounded, bool isRunning)
    {
        if(!isAlive)
            return;

        this.isGrounded = isGrounded;
        this.isRunning = isRunning;

        Run();
        Gravity();
        ResetJumps();
        Jump();
    }

    #region Run

    #region KEY_DETECTION
        // Get the movement input value, this method runs in his thread
        public void OnMove(InputAction.CallbackContext value) => moveInput = value.ReadValue<Vector2>();

        // Get the boolean if the button is pressed
        public void OnJump(InputAction.CallbackContext value) => jumpingInput = value.ReadValueAsButton();
    #endregion

    private void Run()
    {
        // Use the values getted in the function OnMove from the Input System
        Vector2 playerVelocity = new Vector2(moveInput.x * this.movementSpeed, rgBody.velocity.y);
        rgBody.velocity = playerVelocity;

        // Activate the animation changin the boolean
        animator.SetBool("isRunning", isRunning);
    }
    #endregion


    #region GRAVITY
    private void Gravity()
        {
            // Increase the gravity to jump less if the player isn't pressing the jump button
            if (rgBody.velocity.y > 0 && !jumpingInput)
                rgBody.gravityScale = gravityScale * (fallMultipier);
            // Set the gravity as normal
            else
                rgBody.gravityScale = gravityScale;
        }
    #endregion

    #region JUMP
        private void ResetJumps()
        {
            // Condition to avoid continuos jump and reset the timer
            jumpChangedCounter = !jumpingInput ?  waitJumpChanged : jumpChangedCounter - Time.deltaTime;
            // Reset CoyoteTime
            coyoteTimeCounter = isGrounded ? waitCoyoteTime : coyoteTimeCounter - Time.deltaTime;
            // Reset JumpBuffer
            jumpBufferTimeCounter = jumpingInput && jumpChangedCounter > 0 ? waitJumpBufferTime : jumpBufferTimeCounter - Time.deltaTime;
        }
        
        private void Jump()
        {
            if(coyoteTimeCounter > 0 && jumpBufferTimeCounter > 0)
            {
                coyoteTimeCounter = 0f;
                jumpBufferTimeCounter = 0f;
                jumpChangedCounter = 0f;

                // Stop the current velocity and assign the new velocity using forces
                rgBody.velocity = Vector2.up * 0;
                rgBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    #endregion


}
