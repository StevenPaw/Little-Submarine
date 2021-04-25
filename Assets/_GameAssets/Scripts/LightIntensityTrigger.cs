using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityTrigger : MonoBehaviour
{
    [SerializeField] private PostProcessEffect postProcessEffect;
    [SerializeField] private LayerMask WhatActivatesTrigger;

    [SerializeField] private float changeIntensityTo;
    [SerializeField] private bool activateAutomaticIntensity;
    
    private void Awake()
    {
        postProcessEffect = GameObject.Find("Global Post Processing").GetComponent<PostProcessEffect>();
    }

    private void Update()
    {
        /*if (triggerCollider.bounds.Intersects(playerCollider.bounds))
        {
            Debug.Log("IS TOUCHING!");
            if (activateAutomaticIntensity)
            {
                postProcessEffect.ChangeAutomaticIntensity(activateAutomaticIntensity);
            }
            else
            {
                postProcessEffect.ChangeAutomaticIntensity(false);
                postProcessEffect.ChangeVignetteIntensity(changeIntensityTo);
            }
        }*/
        
        if (Physics2D.OverlapCircle(transform.position, .2f, WhatActivatesTrigger))
        {
            Debug.Log("IS TOUCHING!");

            if (postProcessEffect.GetIntensity() != changeIntensityTo)
            {
                postProcessEffect.ChangeVignetteIntensity(changeIntensityTo);
            }

            if (postProcessEffect.GetIsAutomatic() != activateAutomaticIntensity)
            {
                postProcessEffect.ChangeAutomaticIntensity(activateAutomaticIntensity);
            }
        }
    }
}
