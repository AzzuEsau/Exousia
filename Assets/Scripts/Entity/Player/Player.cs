using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{

    #region COMPONENTS
        [Header ("General")]
            [SerializeField] protected PlayerInput playerInput;
            [SerializeField] protected Transform trans;
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
            [SerializeField] protected bool isRunning;
            [SerializeField] protected LifeBar lifeBar;

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
        UpdateLife();
        FlipSprite();
        isGrounded = IsGrounded();
        isRunning = IsRunning();
        playerController.Execute(true, isGrounded, isRunning);
    }

    //make the movement positive and compare it with 0 Epsilon
    private bool IsRunning() => Mathf.Abs(rgBody.velocity.x) > Mathf.Epsilon;

    private void FlipSprite()
    {
        if (IsRunning())
        {
            //Rotate de player using sign wich retur if the value is positive or negative
            transform.localScale = new Vector2(Mathf.Sign(rgBody.velocity.x), 1f);
            //transform.localScale = new Vector2(Mathf.Sign(rgBody.velocity.x), 1f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC") && Input.GetKeyUp(KeyCode.E))
        {
            DialogueInteractable npcInteraction = collision.GetComponent<DialogueInteractable>();

            if(npcInteraction != null)
            {
                npcInteraction.Interact();
            }
        }
    }

    #region INTERACTION
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Interactable"))
        {
            MoneyInteractable moneyInteraction = collision.GetComponent<MoneyInteractable>();
          
            if(moneyInteraction != null)
            {
                moneyInteraction.Interact();
            }
        }
}
    #endregion

    #region COLLISION_DETECTION
    private bool IsGrounded() => floorColliderDetector.IsTouchingLayers(groundLayer);
    #endregion

    protected void UpdateLife()
    {
        if (life < lifeBar.GetLife())
            lifeBar.DecreaseLife((int)(lifeBar.GetLife() - life));
        else if (life > lifeBar.GetLife())
            lifeBar.IncreaseLife((int)(life - lifeBar.GetLife()));
    }

    public Transform GetTransform()
    {
        return this.trans;
    }


}
