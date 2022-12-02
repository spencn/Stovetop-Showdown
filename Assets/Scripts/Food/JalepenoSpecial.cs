using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JalepenoSpecial : MonoBehaviour
{
    private string _playerID;
    public  float  cooldown;
    public float  _cooldownTimer = 0;

    public GameObject fire;

    public  float                  endlag;
    private float                  _endlagTimer;
    private PlayerControllerScript _pcs;
    private Collider2D             _col2D;

    GameObject go;
    AudioScript gs;

    // Start is called before the first frame update
    private void Start()
    {
        _col2D    = gameObject.GetComponent<Collider2D>();
        _pcs      = gameObject.GetComponent<PlayerControllerScript>();
        _playerID = GetComponent<PlayerControllerScript>().playerID;

        go = GameObject.Find("SFX");
        gs = go.GetComponent<AudioScript>();
    }

    private void OnDisable()
    {
        _cooldownTimer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_cooldownTimer <= 0 && Input.GetButtonDown(_playerID + "Special"))
        {
            gs.fireSound();
            _cooldownTimer = cooldown;
            _endlagTimer   = endlag;
            _col2D.enabled = false;
            _pcs.busy      = true;
            Instantiate(
                fire,
                gameObject.transform.position,
                new Quaternion(0, 0, 0, 0),
                gameObject.transform.parent
            );
        }

        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }

        if (_endlagTimer > 0)
        {
            _endlagTimer -= Time.deltaTime;
            if (_endlagTimer <= 0)
            {
                _pcs.busy      = false;
                _col2D.enabled = true;
            }
        }
    }
}