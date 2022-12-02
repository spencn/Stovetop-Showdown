using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P3CoolDown : MonoBehaviour
{
    Image CDMeter;
    float maxCooldown;
    float currentCooldown;
    GameObject go;
    CarrotSpecial gs;
    public Sprite offCD;
    public Sprite onCD;
    void Start()
    {
        go = GameObject.Find("Player3");
        gs = go.GetComponent<CarrotSpecial>();
        CDMeter = GetComponent<Image> ();
        maxCooldown = gs.cooldown;
        CDMeter.sprite = offCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (CDMeter.fillAmount == 1)
        {
            CDMeter.sprite = offCD;
        } else
        {
            CDMeter.sprite = onCD;
        }
        currentCooldown = gs._cdTimer;  
        CDMeter.fillAmount = (maxCooldown - currentCooldown)/maxCooldown;
    }
}
