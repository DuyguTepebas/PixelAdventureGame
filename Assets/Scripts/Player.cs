using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDieable
{
    #region Serializable Variables
    [Header("--- Player Script References ---")]
    [SerializeField] private RunBehaviour playerMovementRef;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    [Space(3)][Header("--- Player Components ---")]
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Transform feet, weapon;
    [SerializeField] private Transform deathPoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private AudioClip coinSound, deathSound;
    [SerializeField] private int health, maxHealth;
    private float _fillAmount;
    [SerializeField] private Image healthBar;

    public int Health
    {
        get => health;
    }

    #endregion

    private void Awake()
    {
        int healthCheck = PlayerPrefs.GetInt("Health", maxHealth);
        health = healthCheck > 0 ? healthCheck : maxHealth;
        SetHealthBar();
    }

    private void Start()
    {
        StartCoroutine(nameof(CheckIfPlayerDied));
    }

    IEnumerator CheckIfPlayerDied()
    {
        while (true)
        {
            if (transform.position.y < deathPoint.position.y)
            {
                Die();
                yield break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Debug.Log(col.gameObject.name);
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (feet.position.y >= (col.transform.position.y + .5f))
            {
                EnemyMovement enemyMovement = col.gameObject.GetComponent<EnemyMovement>();
                enemyMovement.Die();
            }
            else
            {
                Damage();
                SetHealthBar();
            }
        }
    }

    private void SetHealthBar()
    {
        healthBar.fillAmount = ReMap();
    }

    float ReMap()
    {
        return Mathf.Lerp(0, 1f, Mathf.InverseLerp(0, 6, health));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Fruit"))
        {
            Destroy(col.gameObject);
            audioSource.PlayOneShot(coinSound);
            gameManager.ScoreIncrease();
        }

        if (col.gameObject.CompareTag("Box"))
        {
            gameManager.NextLevel();
        }

        if (col.gameObject.CompareTag("EndPoint"))
        {
            uiManager.OpenWinPanel();
        }
        
    }

    void Damage()
    {
        health--;
        health = Mathf.Max(health, 0);
        PlayerPrefs.SetInt("Health",health);
        Die();
    }
    
    public void Die()
    {
        audioSource.PlayOneShot(deathSound);
        playerMovementRef.enabled = false;
        rb2d.gravityScale = 0;
        rb2d.velocity = Vector2.zero;
        playerCollider.enabled = false;
        animator.SetTrigger("deathTrigger");
        uiManager.OpenRestartPanel();
        gameManager.ResetScore();
    }
}// class
