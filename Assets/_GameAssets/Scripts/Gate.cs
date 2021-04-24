using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private EnergyButton activatingButton;
    [SerializeField] private BoxCollider2D colliderComponent;
    [SerializeField] private Animator anim;
    
    private void Update()
    {
        if (activatingButton.getActive())
        {
            anim.SetBool("isOpen", true);
            colliderComponent.enabled = false;
        }
        else
        {
            anim.SetBool("isOpen", false);
            colliderComponent.enabled = true;
        }
    }
}
