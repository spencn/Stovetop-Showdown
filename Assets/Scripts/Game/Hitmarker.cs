using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    public int owner = -1;
    public int lastTouched = -1;
    public float knockback = 0;
    public bool knockbackEnabled = false;
    GameObject go;
    AudioScript gs;

    private void Start()
    {
        go = GameObject.Find("SFX");
        gs = go.GetComponent<AudioScript>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Hitmarker theirs = other.gameObject.GetComponent<Hitmarker>();
        PlayerControllerScript theirCPS = other.gameObject.GetComponent<PlayerControllerScript>();
        if (theirs == null)
            return;
        theirs.lastTouched = owner;
        lastTouched = theirs.owner;
        
        if (knockbackEnabled && theirCPS != null && !theirCPS.invulnerable) {
            
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                
                rb.AddForce(-other.GetContact(0).normal * knockback);   
            }

            var controller = other.gameObject.GetComponent<PlayerControllerScript>();
            if (controller != null)
            {
                gs.hitSound();
                controller.stun();
            }
        }
    }
}
