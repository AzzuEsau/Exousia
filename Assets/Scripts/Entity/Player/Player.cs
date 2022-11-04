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
            [SerializeField] protected int maxLife = 6;

    [Header ("Colliders")]
            [SerializeField] protected CapsuleCollider2D mainCollider;
            [SerializeField] protected BoxCollider2D floorColliderDetector;
            [SerializeField] protected BoxCollider2D attackCollider;

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
            [SerializeField] protected bool interactInput;
            [SerializeField] protected bool attackInput, isAttacking;
            [SerializeField] protected LifeBar lifeBar;
            [SerializeField] private float damage;

    #region ATTRIBUTES



    #endregion

    private void Awake() {

        
    }

    // Start is called before the first frame update
    void Start()
    {
        this.movementSpeed = 7f;
        playerController.AssignElements(ref rgBody, ref groundLayer, ref floorColliderDetector,  ref attackCollider, movementSpeed);
        //playerRenderer.AssignElements(ref isGrounded);
    }

    // Update is called once per frame 
    void Update()
    {
        UpdateLife();
        FlipSprite();
        isGrounded = IsGrounded();
        isRunning = IsRunning();
        isAttacking = attackInput;
        playerController.Execute(life > 0, isGrounded, isRunning, isAttacking);
    }

    
    public void OnInteract(InputAction.CallbackContext value) => interactInput = value.ReadValueAsButton();
    public void OnAttack(InputAction.CallbackContext value) => attackInput = value.ReadValueAsButton();

    //make the movement positive and compare it with 0 Epsilon
    private bool IsRunning() => Mathf.Abs(rgBody.velocity.x) > Mathf.Epsilon;
    

    private void FlipSprite()
    {
        if (IsRunning() && playerController.moveInputX() != 0)
        {
            //Rotate de player using sign wich retur if the value is positive or negative
            transform.localScale = new Vector2(Mathf.Sign(rgBody.velocity.x), 1f);
            //transform.localScale = new Vector2(Mathf.Sign(rgBody.velocity.x), 1f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC") && interactInput)
        {
            DialogueInteractable npcInteraction = collision.GetComponent<DialogueInteractable>();

            if(npcInteraction != null)
            {
                npcInteraction.Interact();
            }
        }
    }

    public override bool OnHurt(float damage, GameObject source)
    {
        if(life > 0 )
        {
            DecreaseLife(damage);
            StartCoroutine(KnockBack(.5f, source));
            if(life == 0)
            {
                UpdateLife();
                // Aqui matar al personaje y poner la pantalla principal
            }
        
            return true;
        }
        return false;
    }

    #region INTERACTION
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Interactable"))
        {
            ImportantObjectsInteractable ImportantObjsInt = collision.GetComponent<ImportantObjectsInteractable>();
          
            if(ImportantObjsInt != null)
            {
                ImportantObjsInt.Interact();
            }
            
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

    public bool IsAttacking() => isAttacking;

    public void increaselife(int increase) => this.life = this.life + increase > maxLife ? maxLife : this.life + increase;
}
