using UnityEngine;

public class CharacterController : MonoBehaviour
{

    private Rigidbody2D p_Rigidbody2D;
    public bool facingRight = true;
    private Vector3 p_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float p_MovementSmoothing = .05f;
    [SerializeField] private float p_JumpForce = 400f;
    [SerializeField] private float p_runSpeed = 400f;
    [SerializeField] private float p_rotationSpeed = 400f;
    private float p_horizontalMove = 0f;
    private float p_RotationMove = 0f;
    private bool p_jump = false;
    public bool playerOnTheTree = false;
    public Transform treeTransform;
    private int p_TreeLayerNumber = 8;

    private void Start()
    {
        p_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            p_horizontalMove = p_runSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            p_horizontalMove = -p_runSpeed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            p_RotationMove = p_rotationSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            p_RotationMove = -p_rotationSpeed;
        }
        else
        {
            p_horizontalMove = 0f;
            p_RotationMove = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            p_jump = true;
        }
    }

    private void FixedUpdate()
    {
        Move(p_horizontalMove * Time.fixedDeltaTime, p_jump, p_RotationMove * Time.fixedDeltaTime);
        p_jump = false;
    }

    private void Move(float move, bool jump, float rotation)
    {
        Vector3 targetVelocity = new Vector2(move, p_Rigidbody2D.velocity.y);
        p_Rigidbody2D.velocity = Vector3.SmoothDamp(p_Rigidbody2D.velocity, targetVelocity, ref p_Velocity, p_MovementSmoothing);
        p_Rigidbody2D.rotation = p_Rigidbody2D.rotation + rotation;
        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            p_Rigidbody2D.AddForce(new Vector2(0f, p_JumpForce));
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

        p_Rigidbody2D.rotation = p_Rigidbody2D.rotation * (-1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == p_TreeLayerNumber)
        {
            playerOnTheTree = true;
            treeTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == p_TreeLayerNumber)
        {
            playerOnTheTree = false;
            treeTransform = null;
        }
    }
}
