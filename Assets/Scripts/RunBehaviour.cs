using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBehaviour : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Animator anim;

    public bool IsRun { get; set; } = false;

    private void FixedUpdate()
    {
        if (!GameManager.isStart)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        Mover(h);
        PlayerAnimation(h);
        PlayerTurn(h);
    }
    
    #region Character Movements

    void Mover(float h)
    {
        rb.velocity = new Vector2(h * speed, rb.velocity.y);
    }
    
    #endregion

    #region Character Turn

    void PlayerTurn (float h)
    {
        if (h > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (h < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    #endregion

    #region Character Run Animation

    void PlayerAnimation(float h)
    {
        if (h != 0)
        {
            IsRun = true;
        }
        else
        {
            IsRun = false;
        }
        anim.SetBool("isRun", IsRun);
    }

    #endregion
}
