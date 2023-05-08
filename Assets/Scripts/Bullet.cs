using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float destroyDuration = 2f;
    private Vector2 _direction;
    private Player _player;

    
    private void OnEnable()
    {
        _player = (Player)FindObjectOfType(typeof(Player));
        _direction = new Vector2(transform.position.x - _player.transform.position.x, 0);
        StartCoroutine(nameof(MoveRoutine));
        Invoke(nameof(DestroySelf),destroyDuration);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.gameObject.name);
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            DestroySelf();
        }
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position,(Vector2)transform.position + _direction, speed * Time.deltaTime);
            yield return null;
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
    
    
    
}//class
