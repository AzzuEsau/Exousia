using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour{
    #region SETABLES
        [Header ("External Components")]
            [SerializeField] protected Rigidbody2D rgBody;
    #endregion

    #region VARIABLES
            [Header ("Movement")]
                [SerializeField] protected float movementSpeed;

            [Header ("Jump")]
                [SerializeField] protected float jumpForce = 15f;
                [SerializeField] protected LayerMask groundLayer;
                [SerializeField] protected BoxCollider2D floorCollider;

            [Header ("Input")]
                [SerializeField] protected Vector2 moveInput;
                [SerializeField] protected bool jumpingInput; 

            [Header ("Last Jumped")]
                [SerializeField] protected float waitJumpChanged;
                [SerializeField] protected float jumpChangedCounter;

            [Header ("Coyote Time")]
                [SerializeField] protected float waitCoyoteTime;
                [SerializeField] protected float  coyoteTimeCounter;
        
            [Header ("JumpingBuffer")]
                [SerializeField] protected float waitJumpBufferTime;
                [SerializeField] protected float jumpBufferTimeCounter;
    #endregion
    
    #region STATICS
        [Header ("Gravity")]
            [SerializeField] protected static float gravityScale;
            [SerializeField] protected static float fallMultipier = 1.8f;
            [SerializeField] protected static float waitTime = 0.12f;
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

    public void Execute(bool isAlive)
    {
        if(!isAlive)
            return;

        Run();
        Gravity();
        ResetJumps();
        Jump();
    }

    #region KEY_DETECTION
        // Get the movement input value, this method runs in his thread
        public void OnMove(InputAction.CallbackContext value) => moveInput = value.ReadValue<Vector2>();

        // Get the boolean if the button is pressed
        public void OnJump (InputAction.CallbackContext value) => jumpingInput = value.ReadValueAsButton();
    #endregion


    #region RUN
        //make the movement positive and compare it with 0 Epsilon
        public bool IsRunning() => Mathf.Abs(rgBody.velocity.x) > Mathf.Epsilon;

        // private void FlipSprite()
        // {
        //     if(IsRunning())
        //         //Rotate de player using sign wich retur if the value is positive or negative
        //         transform.localScale = new Vector2 (Mathf.Sign(_rg.velocity.x), 1f);
        // }

        private void Run()
        {
            // Use the values getted in the function OnMove from the Input System
            Vector2 playerVelocity = new Vector2(moveInput.x * this.movementSpeed, rgBody.velocity.y);
            rgBody.velocity = playerVelocity;

            // // Activate the animation changin the boolean
            // _animator.SetBool("isRunning", IsRunning());
        }
    #endregion

    #region COLLISION_DETECTION
        private bool IsGrounded () => floorCollider.IsTouchingLayers(groundLayer);
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
            coyoteTimeCounter = IsGrounded() ? waitCoyoteTime : coyoteTimeCounter - Time.deltaTime;
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
