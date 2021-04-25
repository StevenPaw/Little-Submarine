using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessEffect : MonoBehaviour
{
    [SerializeField] private Vignette vignette = null;

    [SerializeField] private float vignetteIntensity;
    [Range(0.0f,1.0f)] [SerializeField] private float vignettePower;
    [SerializeField] private Transform playerPos;

    [SerializeField] private bool isAutomatic = true;
    [Range(0.0f,1.0f)] [SerializeField] private float targetIntensity;
    [Range(0.0f,1.0f)] [SerializeField] private float targetSpeed;
    
    private void Awake()
    {
        PostProcessVolume activeVolume = GetComponent<PostProcessVolume>();;
        if (activeVolume != null)
        {
            activeVolume.profile.TryGetSettings(out vignette);
        }

        if (vignette != null)
        {
            vignette.intensity.value = vignetteIntensity;
        }

        playerPos = GameObject.FindWithTag("Player").gameObject.transform;
    }

    private void Update()
    {
        if (isAutomatic)
        {
            targetIntensity = ((-playerPos.position.y * 0.01f) + 0.05f) * vignettePower;
        }
        
        if (vignetteIntensity - targetIntensity > targetSpeed)
        {
            vignetteIntensity -= targetSpeed;
        }
        else if(vignetteIntensity - targetIntensity < -targetSpeed)
        {
            vignetteIntensity += targetSpeed;
        }
        else
        {
            vignetteIntensity = targetIntensity;
        }

        vignette.intensity.value = vignetteIntensity;
        vignette.smoothness.value = vignetteIntensity;
    }

    public void ChangeVignetteIntensity(float intensityIn)
    {
        targetIntensity = intensityIn;
    }

    public void ChangeAutomaticIntensity(bool isAutomaticIn)
    {
        isAutomatic = isAutomaticIn;
    }

    public float GetIntensity()
    {
        return targetIntensity;
    }
    
    public bool GetIsAutomatic()
    {
        return isAutomatic;
    }
}
