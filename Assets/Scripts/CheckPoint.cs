using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float positionOnX;
    [SerializeField] private float speed;
    [SerializeField] private bool toRight;
    [SerializeField] private GameObject checkPoint;

    private void Awake()
    {
        mainCamera = (Camera)FindObjectOfType(typeof(Camera));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(ChangeDirection),3);
            MoveCamera();
        }
    }

    private void ChangeDirection()
    {
        checkPoint.SetActive(true);
        gameObject.SetActive(false);
    }

    public void MoveCamera()
    {
        StartCoroutine(nameof(MoveRoutine));
    }
    
    IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (toRight)
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,mainCamera.transform.position + Vector3.right, speed * Time.deltaTime);
                if (mainCamera.transform.position.x >= positionOnX)
                {
                    yield break;
                }  
            }
            else
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,mainCamera.transform.position + Vector3.left, speed * Time.deltaTime);
                if (mainCamera.transform.position.x <= positionOnX)
                {
                    yield break;
                }  
            }
            yield return null;
            
        }
        
    }
}//class
