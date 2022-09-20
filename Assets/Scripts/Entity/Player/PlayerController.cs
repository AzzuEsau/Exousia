using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController {
    #region SETABLES
        [Header("Movement")]
            [SerializeField] private float movementSpeed;
        
        [Header ("Layers")]
            [SerializeField] private LayerMask groundLayer;

        [Header ("External Componetns")]
            private Rigidbody2D rigidbody2D;
            private BoxCollider2D floorCollider;
    #endregion

    #region VARIABLES
        #region SERIALIZABLES
            [Header ("Jump")]
                [SerializeField] private float jumpForce;
                [SerializeField] private GameObject collidersGameObject;
        #endregion

        #region PRIVATED
            [Header ("Input")]
                private Vector2 moveInput;
                private bool jumpingInput; 


            [Header ("Last Jumped")]
                private float waitJumpChanged;
                private float jumpChangedCounter;

            [Header ("Coyote Time")]
                private float waitCoyoteTime;
                private float  coyoteTimeCounter;
        
            [Header ("JumpingBuffer")]
                private float waitJumpBufferTime;
                private float jumpBufferTimeCounter;


        #endregion
    #endregion
    
    #region STATICS
        private static float _waitTime = 0.12f;

        [Header ("Gravity")]
            private static float gravityScale;
            private static float fallMultipier = 1.5f;
    #endregion

    PlayerController(float movementSpeed, Rigidbody2D rg)
    {
        this.rigidbody2D = rg;
        this.movementSpeed = movementSpeed;



        gravityScale = rigidbody2D.gravityScale;
    }

    #region MAIN
        public void Update(bool isAlive) 
        {
            if(!isAlive)
                return;
            Execute();
        }

        private void Execute()
        {
            Run();
            Gravity();
            Jump();
        }
    #endregion




    #region KEY_DETECTION
        // Get the movement input value, this method runs in his thread
        public void OnMove(InputAction.CallbackContext value) => moveInput = value.ReadValue<Vector2>();

        // Get the boolean if the button is pressed
        public void OnJump (InputAction.CallbackContext value) => jumpingInput = value.ReadValueAsButton();
    #endregion

    #region COLLISION_DETECTION
        private bool IsGrounded () => floorCollider.IsTouchingLayers(groundLayer);
    #endregion

    #region GRAVITY
        private void Gravity()
        {
            // Increase the gravity to jump less if the player isn't pressing the jump button
            if (rigidbody2D.velocity.y > 0 && !jumpingInput)
                rigidbody2D.gravityScale = gravityScale * (fallMultipier);
            // Set the gravity as normal
            else
                rigidbody2D.gravityScale = gravityScale;
        }
    #endregion

    #region RUN
        //make the movement positive and compare it with 0 Epsilon
        private bool IsRunning() => Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;

        // private void FlipSprite()
        // {
        //     if(IsRunning())
        //         //Rotate de player using sign wich retur if the value is positive or negative
        //         transform.localScale = new Vector2 (Mathf.Sign(_rg.velocity.x), 1f);
        // }

        private void Run()
        {
            // Use the values getted in the function OnMove from the Input System
            Vector2 _playerVelocity = new Vector2(moveInput.x * movementSpeed, rigidbody2D.velocity.y);
            rigidbody2D.velocity = _playerVelocity;

            // // Activate the animation changin the boolean
            // _animator.SetBool("isRunning", IsRunning());
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
                rigidbody2D.velocity = Vector2.up * 0;
                rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    #endregion

}
