using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Entity {
    #region SETABLES
        
        [Header ("Layers")]
            [SerializeField] private LayerMask groundLayer;

        [Header ("External Componetns")]
            private Rigidbody2D rb2D;
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
        // private static float waitTime = 0.12f;

        [Header ("Gravity")]
            private static float gravityScale;
            private static float fallMultipier = 1.5f;
    #endregion

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        


        movementSpeed = 10;
        jumpForce = 10;
        gravityScale = rb2D.gravityScale;
    }

    void Update()
    {
        Main(true);
        IsRunning();
    }

    #region MAIN
        public void Main(bool isAlive) 
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
        public void OnMove(InputAction.CallbackContext value) { 
            moveInput = value.ReadValue<Vector2>();
            Debug.Log(value.ReadValue<Vector2>());
        }

        // Get the boolean if the button is pressed
        public void OnJump(InputAction.CallbackContext value) {
            jumpingInput = value.ReadValueAsButton();
            Debug.Log(value.ReadValueAsButton());
        }
    #endregion

    #region COLLISION_DETECTION
        private bool IsGrounded () => floorCollider.IsTouchingLayers(groundLayer);
    #endregion

    #region GRAVITY
        private void Gravity()
        {
            // Increase the gravity to jump less if the player isn't pressing the jump button
            if (rb2D.velocity.y > 0 && !jumpingInput)
                rb2D.gravityScale = gravityScale * (fallMultipier);
            // Set the gravity as normal
            else
                rb2D.gravityScale = gravityScale;
        }
    #endregion

    #region RUN
        //make the movement positive and compare it with 0 Epsilon
        public bool IsRunning() => Mathf.Abs(rb2D.velocity.x) > Mathf.Epsilon;

        // private void FlipSprite()
        // {
        //     if(IsRunning())
        //         //Rotate de player using sign wich retur if the value is positive or negative
        //         transform.localScale = new Vector2 (Mathf.Sign(_rg.velocity.x), 1f);
        // }

        private void Run()
        {
            // Use the values getted in the function OnMove from the Input System
            Vector2 _playerVelocity = new Vector2(moveInput.x * movementSpeed, rb2D.velocity.y);
            rb2D.velocity = _playerVelocity;

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
                rb2D.velocity = Vector2.up * 0;
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    #endregion

}
