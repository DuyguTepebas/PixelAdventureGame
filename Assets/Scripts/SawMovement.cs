using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float xLeft;
    [SerializeField] private float xRight;
    private Coroutine _moveRoutine;
    private bool toRight;
    private void Start()
    {
        _moveRoutine = StartCoroutine(nameof(MoveRoutine));
    }

    IEnumerator MoveRoutine()
    {
        toRight = Random.Range(0, 2) == 0;
        while (true)
        {
            if (toRight)
            {
                transform.position = Vector2.Lerp(transform.position,(Vector2)transform.position + Vector2.right, speed * Time.deltaTime);
                if (transform.position.x >= xRight)
                {
                    toRight = false;
                }            
            }
            else
            {
                transform.position = Vector2.Lerp(transform.position,(Vector2)transform.position + Vector2.left, speed * Time.deltaTime);
                if (transform.position.x <= xLeft)
                {
                    toRight = true;
                }  
            }
            yield return null;
            
        }
        
    }

}// class
