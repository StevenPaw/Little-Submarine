using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyButton : MonoBehaviour
{
    [SerializeField] private bool buttonActivated = false;
    [SerializeField] private LayerMask WhatActivatesButton;
    [SerializeField] private String neededFish = "EnergyFish";
    private void Update()
    {
        if (Physics2D.OverlapCircle(transform.position, .2f, WhatActivatesButton))
        {
            GameObject otherObject = Physics2D.OverlapCircle(transform.position, .2f, WhatActivatesButton).gameObject;
            if (otherObject.tag == neededFish)
            {
                buttonActivated = true;
            }
        }
        else
        {
            buttonActivated = false;
        }
    }

    public bool getActive()
    {
        return buttonActivated;
    }
}
