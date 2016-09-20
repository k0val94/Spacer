using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerConroller : NetworkBehaviour
{

    public float maxSpeed = 4;
    public float jumpForce = 550;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    [HideInInspector]
    public bool lookingRight = true;

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool isGrounded = false;
    private bool jump = false;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
        }
        
    }

    void FixedUpdate()
    {

        
        float hor = Input.GetAxis("Horizontal");
        if ((hor > 0 && !lookingRight) || (hor < 0 && lookingRight))
        {
            Flip();
        }
        anim.SetFloat("Speed", Mathf.Abs(hor)); //an den Animator wir der Wert Speed übergeben (Absolut) weil Flip()
        if (!isLocalPlayer)
        {
            return;
        }
        rb2d.velocity = new Vector2(hor * maxSpeed, rb2d.velocity.y); //bewegung

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15F, whatIsGround); //eigener Ground Check
        anim.SetBool("isGrounded", isGrounded); //übergabe an Animator
       
        if (jump)
        {
            rb2d.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }
    }

    public void Flip()
    {
        lookingRight = !lookingRight;
        Vector3 myScale = transform.localScale; // x von meinem transform invertieren
        myScale.x *= -1;
        transform.localScale = myScale;
    }
}
