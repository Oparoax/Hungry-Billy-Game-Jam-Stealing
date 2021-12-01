using UnityEngine;
using UnityEngine.Events;

public class Char_Controller : MonoBehaviour
{
    [Header("Movement Modifiers")]
    //Float for the speed of player
    [SerializeField] private float speed = 10f;
    //Integer for the amount of times the player may be caught before the game ends
    [SerializeField] private int captureAmount = 3;

    //Float for the interaction range of player
    public float interactionRange = 1f;
    //Integer for the amount of money the player removes from enemies on each interaction
    public int stealAmount = 30;
    //Integer for how much money the player has stored on them
    public int playerWallet = 0;
    //Integer for how much money the player has banked
    public int playerBank = 0;

    //Unity event for when players money amount has changed
    public UnityEvent OnMoneyChanged;
    //Unity event for when player has been caught stealing
    public UnityEvent OnCaught;

    [Header("Player Sprites")] 
    //Sprite array to store the different orientations of the player model
    [SerializeField] Sprite[] playerSprite;
    //Player controls variable for the Input controller
    private Player_Controls playerController;
    //Vector2 to move/stop the player
    private Vector2 move;
    //Animator to animate the character rotating
    private Animator animator;

    void Awake()
    {
        //Defines animator variable
        animator = GetComponent<Animator>();
        //Defines playerController variable
        playerController = new Player_Controls();

        //Reads players movement and stores as a vector 2
        playerController.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        //Reads when player stops moving and sets variable to 0
        playerController.Player.Move.canceled += ctx => move = Vector2.zero;

        //Reads when player interacts and trigger Interact method
        playerController.Player.Steal.performed += ctx => Interact();
    }

    void Update()
    {
        //Fires move method every frame
        Move();
    }


    /// <summary>
    /// Handles player movement
    /// </summary>
    public void Move()
    {
        //Moves player to desired location
        transform.Translate(move * speed * Time.deltaTime);

        //Set float pararmeter within animator to rotate player
        animator.SetFloat("MoveX", move.x);
        animator.SetFloat("MoveY", move.y);
    }


    /// <summary>
    /// Interact script for when interaction occurs
    /// </summary>
    private void Interact()
    {
        //Collider array for all collider within the interaction range of the player
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);

        //Loops through each collider within array
        foreach (Collider2D collider in colliders)
        {
            //Defines collision variable to an IInteration 
            var collision = collider.GetComponent<IInteraction>();
            //Checks if no collider exists
            if (collision != null)
            {
                //Checks if collider is an enemy
                if (collider.CompareTag("Enemy"))
                {
                    //Defines a variabel to test if the collider are interacting with is a CicleCollider2D
                    CircleCollider2D circleCollider = collider as CircleCollider2D;
                    //Checks if collider was a CircleCollider2D
                    if (circleCollider != null)
                    {
                        //Sets player wallet to 0
                        playerWallet = 0;
                        //Triggers OnMoneyChanged Event
                        OnMoneyChanged?.Invoke();
                        //Triggers OnCaught Event
                        OnCaught?.Invoke();

                        Debug.Log("You have been caught");
                        //returns statement
                        return;
                    }
                }
                //Initiates interaction to steal/store money
                collision.Interact();
            }
        }
    }


    /// <summary>
    /// Draws gizmos in scene view to display interaction range for testing
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        //Draws circle around player to show interaction range
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, interactionRange);
    }


    /// <summary>
    /// Enable method for new input system
    /// </summary>
    private void OnEnable()
    {
        playerController.Enable();
    }
    /// <summary>
    /// Disable method for new input system
    /// </summary>
    private void OnDisable()
    {
        playerController.Disable();
    }
}
