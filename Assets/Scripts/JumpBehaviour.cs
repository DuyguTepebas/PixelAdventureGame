using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] bool isGround = true;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private Animator animator;
    public float jumpSpeed = 15f;
    
    private void Update()
    {
        /*if (!GameManager.isStart)
        {
            return;
        }
        RaycastHit2D isHit = Physics2D.Raycast(transform.position,Vector2.down,0.24f,layer);
        
        if (isHit.collider != null)
        {
            isGround = true;
            animator.SetBool("isJump", true);
        }
        else
        {
            isGround = false;
            animator.SetBool("isJump",false);
        }

        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpSpeed);
        }*/
        
        //--------------------
        
        if (!GameManager.isStart)
        {
            return;
        }

        RaycastHit2D isHit = Physics2D.Raycast(transform.position,Vector2.down,0.24f,layer);

        if (isHit.collider != null)
        {
            isGround = true;
            animator.SetBool("isJump", false);
        }
        else
        {
            isGround = false;
            animator.SetBool("isJump", true);
        }

        if (isGround==true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpSpeed);
        }
    }
}// Class
