using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpecial : MonoBehaviour
{
    private PlayerControllerScript _pcs;
    float                          angle      = 360.0f;
    float                          time       = 0.1f;
    public  float                  specialKnock = 45000;
    public  float                  attackTime = 5.0f;
    public  float                  attackTimeTimer;
    public  float                  cooldown = 5.0f;
    public  float                  _cdTimer;
    private bool                   specialOnCD = false;
    private bool                   _specialing = false;
    private Hitmarker              _hitmarker;
    private BoxCollider2D          _coll;
    Vector3                        axis = Vector3.forward;
    private PlayerControllerScript _cps;
    GameObject go;
    AudioScript gs;

    void Start()
    {
        _pcs       = gameObject.GetComponent<PlayerControllerScript>();
        _hitmarker = gameObject.GetComponent<Hitmarker>();
        _coll      = gameObject.GetComponent<BoxCollider2D>();
        _cps       = gameObject.GetComponent<PlayerControllerScript>();

        go = GameObject.Find("SFX");
        gs = go.GetComponent<AudioScript>();
    }
    private void OnDisable()
    {
        _cdTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _pcs._rb2D.mass = 18;
        if (Input.GetButtonDown(_pcs.playerID + "Special"))
        {
            if (specialOnCD == false)
            {
                gs.swingSound();
               
                specialOnCD       = true;
                _specialing       = true;
                _cps.invulnerable = true;
                _coll.enabled     = true;
                attackTimeTimer   = attackTime;
                _cdTimer          = cooldown;
            }
        }

        if (_specialing)
        {
            _pcs._rb2D.mass = 9999999999999; //Add mass to block attack
            _hitmarker.knockback = (attackTime - attackTimeTimer) * specialKnock;
            _hitmarker.knockbackEnabled = true;
            attackTimeTimer      -= Time.deltaTime;
            transform.RotateAround(transform.position, axis, angle * Time.deltaTime / time);
            if (attackTimeTimer <= 0)
            {
                _hitmarker.knockback = 16000;
                _hitmarker.knockbackEnabled = false;
                _coll.enabled        =  false;
                _specialing          =  false;
                _cps.invulnerable    =  false;
            }
        }

        if (specialOnCD)
        {
            _cdTimer -= Time.deltaTime;
            if (_cdTimer <= 0)
            {
                specialOnCD = false;
            }
        }
    }
}