using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private GameObject shootingPanel;
    private bool _canFire = false;
    private int _fireCheck;

    private void Awake()
    {
        _fireCheck = PlayerPrefs.GetInt("FireCheck", 0);
        _canFire = _fireCheck == 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Shoot();
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("ShootingElement"))
        {
            StartCoroutine(ShowPanel());
            _canFire = true;
            _fireCheck = 1;
            PlayerPrefs.SetInt("FireCheck", _fireCheck);
            Debug.Log("shooting active");
            Destroy(col.gameObject);
        }

        
    }

    void Shoot()
    {
        if(!_canFire) return;
        Debug.Log("Shoot");
        Bullet bulletScript = Instantiate(bullet, transform.position, Quaternion.identity);
    }

    IEnumerator ShowPanel()
    {
        shootingPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        shootingPanel.SetActive(false);
    }
    
    
}//class
