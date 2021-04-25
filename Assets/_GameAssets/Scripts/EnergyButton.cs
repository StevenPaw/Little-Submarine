using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyButton : MonoBehaviour
{
    [SerializeField] private bool buttonActivated = false;
    [SerializeField] private LayerMask WhatActivatesButton;
    [SerializeField] private String FishTag = "Fish";
    [SerializeField] private String neededFish = "YellowFish";
    [SerializeField] private Animator anim;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioSource audioSource;
    private GameObject savedObject;
    private MovingBlock savedMovingBlock;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = soundManager.GetVolume();
        
        if (Physics2D.OverlapCircle(transform.position, .2f, WhatActivatesButton))
        {
            GameObject otherObject = Physics2D.OverlapCircle(transform.position, .2f, WhatActivatesButton).gameObject;
            if (savedObject != otherObject)
            {
                savedObject = otherObject;
                savedMovingBlock = savedObject.GetComponent<MovingBlock>();
            }
            if (savedObject.tag == FishTag)
            {
                if(savedMovingBlock.getBlockType() == neededFish){
                    if (!buttonActivated)
                    {
                        buttonActivated = true;
                        anim.SetBool("isActivated", true);
                        audioSource.Play();
                    }
                }
            }
        }
        else
        {
            if (buttonActivated)
            {
                buttonActivated = false;
                anim.SetBool("isActivated", false);
                audioSource.Play();
            }
        }
    }

    public bool getActive()
    {
        return buttonActivated;
    }
}
