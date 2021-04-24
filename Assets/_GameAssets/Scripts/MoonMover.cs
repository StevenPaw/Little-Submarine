using System;
using UnityEngine;

public class MoonMover : MonoBehaviour
{
    [SerializeField] private Transform anchorPos;
    [SerializeField] private float moveFactor = 1f;
    [SerializeField] private float offsetX = 1f;

    private void Update()
    {
        transform.position = new Vector3(anchorPos.position.x * moveFactor + offsetX, transform.position.y, transform.position.z);
    }
}
