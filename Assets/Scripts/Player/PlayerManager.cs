using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    //Movement
   [SerializeField] protected private PlayerInput playerInput;
   [SerializeField]protected float f_speed = 5f;
    //Animations
   [SerializeField]protected private Animator animator;
   private SpriteRenderer spriteRenderer;
   private bool b_moving;

    //Physics 
    [SerializeField]float f_jumpHeight = 5f;
    [SerializeField] float f_gravityScale = 5f;
    float velocity;
    [SerializeField] float floorHeight = 0.5f;
    [SerializeField] Transform feet;
    [SerializeField] ContactFilter2D contactFilter;
    [SerializeField] bool isGrounded;
    [SerializeField] Collider2D[] results = new Collider2D[1];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }
  

    // Update is called once per frame
    void Update()
    {
        #region Jumping and Gravity
        velocity += Physics2D.gravity.y * f_gravityScale * Time.deltaTime;

        if(Physics2D.OverlapBox(feet.position, feet.localScale, 0, contactFilter, results) > 0 && velocity<0)
        {
            velocity = 0;
            Vector2 surface = Physics2D.ClosestPoint(transform.position, results[0])+Vector2.up*floorHeight;
            transform.position = new Vector3(transform.position.x, surface.y, transform.position.z);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }   

        if(playerInput.actions["Jump"].WasPressedThisFrame() && isGrounded)
        {
            velocity += Mathf.Sqrt(f_jumpHeight * -2f * (Physics2D.gravity.y*f_gravityScale));
        }
        transform.Translate(new Vector3(0, velocity,0) * Time.deltaTime);
        #endregion
        #region Player Movement
        float moveInput = playerInput.actions["Move"].ReadValue<float>();
        transform.position += Vector3.right * moveInput * f_speed * Time.deltaTime;







        #endregion
        #region Animation
        /*
      
        
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
       
        */
        #endregion
    }
}
