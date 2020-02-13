using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaControler : MonoBehaviour
{
    Rigidbody rb = null;
    [SerializeField] Transform checkGround = default;
    [SerializeField] Transform checkCeilling = default;
    [SerializeField] LayerMask groundLayers = default;
    bool isGrounded = false;

    bool isFacingRight = true;
    [SerializeField] ForceMode forceMode = ForceMode.Force;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpForce = 1;
    [SerializeField] float gravityScale = 1;

    Animator anim = null;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(Input.GetAxis("Horizontal"));
    }
    
    void Jump()
    {
    }

    void Move(float x)
    {
        Vector3 movement = new Vector3(x * speed * Time.deltaTime * 1000, 0, 0);
        Debug.Log(Physics.OverlapSphere(checkGround.position, 0.2f, groundLayers).Length);
        if (Input.GetButtonDown("Jump") && Physics.OverlapSphere(checkGround.position, 0.2f, groundLayers).Length != 0)
        {
            rb.AddForce(Vector3.up * jumpForce);
            anim.SetTrigger("jumpTrigger");
        }
        else if (Physics.OverlapSphere(checkGround.position, 0.5f, groundLayers).Length != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        }
        //movement.y = movement.y + (Physics.gravity.y * gravityScale);

        rb.velocity = new Vector3(movement.x, rb.velocity.y + (Physics.gravity.y * gravityScale * Time.deltaTime), 0);

        if (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f) anim.SetBool("isWalking", true);
        else anim.SetBool("isWalking", false);
        if (rb.velocity.x < 0 && isFacingRight || rb.velocity.x > 0 && !isFacingRight) Flip();
    }
    
    void Flip()
    {
        if (isFacingRight)
            isFacingRight = false;
        else
            isFacingRight = true;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

}