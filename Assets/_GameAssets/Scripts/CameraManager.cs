using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform playerPos;

    private void Update()
    {
        Vector3 targetPosition = playerPos.position + new Vector3(0f, 0f, -10f);
        
        float moveSpeed = Vector3.Distance(transform.position, targetPosition) *
                          Vector3.Distance(transform.position, targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
