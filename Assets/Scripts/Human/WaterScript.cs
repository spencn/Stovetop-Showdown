using System;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    private Camera _camera;

    public float             _cooldownTimer;
    private PolygonCollider2D _polygonCollider2D;

    private SpriteRenderer _sprite;
    public  float          cooldown = 2;

    GameObject go;
    AudioScript gs;


    private void Start()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
        _camera            = Camera.main;
        _sprite            = GetComponent<SpriteRenderer>();

        go = GameObject.Find("SFX");
        gs = go.GetComponent<AudioScript>();
    }

    private void Update()
    {
        GameObject set = GameObject.Find("Settings");
        Settings settings = set.GetComponent<Settings>();

        if (!(settings.getActive(4)))
        {
            gameObject.SetActive(false);
            GameObject meter = GameObject.Find("DropMeter");
            meter.SetActive(false);
        }

        _cooldownTimer -= Time.deltaTime;
        if (Input.GetMouseButton(1) && _cooldownTimer <= 0)
        {
            gs.waterSound();
            _cooldownTimer  = cooldown;
            _sprite.enabled = true;

            var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z                         = -1;
            gameObject.transform.position = pos;
        }

        _sprite.enabled = _cooldownTimer > 0;

        _polygonCollider2D.enabled = _cooldownTimer < cooldown / 2 && _cooldownTimer > 0;
        if (!_sprite.enabled) return;


        var tmp = _sprite.color;
        tmp.a         = _cooldownTimer > cooldown / 2 ? 1f : 0.5f;
        _sprite.color = tmp;
        _sprite.transform.localScale = new Vector3(1, 1, 0) * Math.Max(1,
                                           1 + (_cooldownTimer < cooldown / 2 ? cooldown / 2 : _cooldownTimer) /
                                           cooldown * 2);
    }
}