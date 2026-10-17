using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
   [SerializeField] protected private PlayerInput playerInput;
   [SerializeField]protected float f_speed = 5f;
   [SerializeField]protected private Animator animator;
   private SpriteRenderer spriteRenderer;
   private bool b_moving;

    //Physics 
    public float f_gravityScale = -25f;
    public float f_fallMultiplier = 2.5f;
    public float f_jumpForce = 12f;
    public bool b_isGrounded = false;
    //private CharacterController controller;
    LayerMask layerMask;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        layerMask = LayerMask.GetMask("Floor","Player");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))

        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10000, Color.red);
            Debug.Log("Did not Hit");
        }
    }





    // Update is called once per frame
    void Update()
    {
      


        #region Player Movement
        float moveInput = playerInput.actions["Move"].ReadValue<float>();
        transform.position += Vector3.right * moveInput * f_speed * Time.deltaTime;







        #endregion

        #region Animation
        animator.SetBool("Moving", b_moving);
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (moveInput != 0)
        {
            b_moving = true;
        }
        else
        {
            b_moving = false;
        }
        #endregion
    }
}
