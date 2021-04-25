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
    private GameObject savedObject;
    private MovingBlock savedMovingBlock;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
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
                    buttonActivated = true;
                    anim.SetBool("isActivated", true);
                }
            }
        }
        else
        {
            buttonActivated = false;
            anim.SetBool("isActivated", false);
        }
    }

    public bool getActive()
    {
        return buttonActivated;
    }
}
