﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeCoolDown : MonoBehaviour
{
    Image CDMeter;
    float maxCooldown;
    float currentCooldown;
    GameObject go;
    KnifeController gs;
    public Sprite offCD;
    public Sprite onCD;
    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("Knife");
        gs = go.GetComponent<KnifeController>();
        CDMeter = GetComponent<Image>();
        maxCooldown = gs.cooldown;
        CDMeter.sprite = offCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (CDMeter.fillAmount == 1)
        {
            CDMeter.sprite = offCD;
        }
        else
        {
            CDMeter.sprite = onCD;
        }
        currentCooldown = gs._cooldownTimer;
        CDMeter.fillAmount = (maxCooldown - currentCooldown) / maxCooldown;
    }
}
