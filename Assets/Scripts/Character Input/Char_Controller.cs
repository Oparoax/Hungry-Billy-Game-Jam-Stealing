using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Char_Controller : MonoBehaviour
{
    [Header("Movement Modifiers")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float weight = 1f;
    [Range(1,5)][SerializeField] private float weightModifier;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private int captureAmount = 3;
    public float interactionRange = 1f;
    public int stealAmount = 30;
    public int playerWallet = 0;
    public int playerBank = 0;
    public UnityEvent OnMoneyChanged;
    public UnityEvent OnCaught;

    [Header("Components")]
    private Collider2D playerColl;
    private Player_Controls playerController;
    private GameObject playerObject;
    private SpriteRenderer sprRen;

    float horizontalAxis, verticalAxis;

    private Vector2 move;

    private bool faceRight = true, faceUp = true;

    void Awake()
    {
        sprRen = GetComponent<SpriteRenderer>();
        playerColl = GetComponent<CircleCollider2D>();
        playerController = new Player_Controls();

        playerObject = playerColl.gameObject;

        playerController.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        playerController.Player.Move.canceled += ctx => move = Vector2.zero;

        playerController.Player.Steal.performed += ctx => Interact();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        horizontalAxis = move.x;
        verticalAxis = move.y;

        var desiredMoveDirection = new Vector2(horizontalAxis,verticalAxis);

        transform.Translate(desiredMoveDirection * speed * Time.deltaTime);

        //if (horizontalAxis > 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 90);
        //}
        //else if (horizontalAxis < 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, -90);
        //}

        //if (verticalAxis > 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else if (verticalAxis < 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 180);
        //}

        //float angle = Mathf.Atan2(verticalAxis, horizontalAxis) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);
        foreach (Collider2D collider in colliders)
        {
            var collision = collider.GetComponent<IInteraction>();
            if (collision != null)
            {
                if (collider.CompareTag("Enemy"))
                {
                    CircleCollider2D circleCollider = collider as CircleCollider2D;
                    if (circleCollider != null)
                    {
                        playerWallet = 0;
                        OnMoneyChanged?.Invoke();
                        OnCaught?.Invoke();
                        Debug.Log("You have been caught");
                        return;
                    }
                }
                collision.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, interactionRange);
    }


    private void OnEnable()
    {
        playerController.Enable();
    }
    private void OnDisable()
    {
        playerController.Disable();
    }
}
