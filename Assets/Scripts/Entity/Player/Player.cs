using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{

    #region COMPONENTS
        [Header ("General")]
            [SerializeField] protected PlayerInput playerInput;
            [SerializeField] protected Rigidbody2D rgBody;

        [Header ("Colliders")]
            [SerializeField] protected CapsuleCollider2D mainCollider;
            [SerializeField] protected BoxCollider2D floorColliderDetector;

        [Header ("Render")]
            [SerializeField] protected SpriteRenderer render;
            [SerializeField] protected Animator animator;

        [Header ("Layers")]
            [SerializeField] protected LayerMask groundLayer;
    #endregion
            [SerializeField] protected PlayerController playerController;
            //[SerializeField] protected PlayerRenderer playerRenderer;
            [SerializeField] protected bool isGrounded;

    #region ATTRIBUTES




    #endregion

    private void Awake() {

        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.movementSpeed = 7f;
        playerController.AssignElements(ref rgBody, ref groundLayer, ref floorColliderDetector, movementSpeed);
        //playerRenderer.AssignElements(ref isGrounded);
    }

    // Update is called once per frame 
    void Update()
    {
        isGrounded = IsGrounded();
        playerController.Execute(true, isGrounded);
    }

    #region COLLISION_DETECTION
    private bool IsGrounded() => floorColliderDetector.IsTouchingLayers(groundLayer);
    #endregion
}
