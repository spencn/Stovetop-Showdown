using System;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    private Camera            _camera;
    public float             _cooldownTimer;
    private bool              _dragging;
    private Vector3           _endPos;
    private LineRenderer      _line;
    private PolygonCollider2D _polygonCollider2D;
    private SpriteRenderer    _spriteRenderer;
    private Vector3           _startPos;
    public  float             cooldown = 4;

    GameObject go;
    AudioScript gs;

    private void Start()
    {
        _polygonCollider2D  = GetComponent<PolygonCollider2D>();
        _spriteRenderer     = GetComponent<SpriteRenderer>();
        _line               = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _camera             = Camera.main;

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
            GameObject meter = GameObject.Find("KnifeMeter");
            meter.SetActive(false);

            GameObject score = GameObject.Find("P4Score");
            score.SetActive(false);
        }

            _endPos   = _camera.ScreenToWorldPoint(Input.mousePosition);
        _endPos.z = 0;

        if (Input.GetMouseButton(0) && !_dragging && _cooldownTimer <= 0)
        {
            
            _startPos   = _camera.ScreenToWorldPoint(Input.mousePosition);
            _startPos.z = 0;
            _dragging   = true;
        }
        else if (!Input.GetMouseButton(0) && _dragging && (_endPos - _startPos).magnitude > 1)
        {
            gs.knifeSound();
            _dragging                  = false;
            _cooldownTimer             = cooldown;
            _spriteRenderer.enabled    = true;
            _polygonCollider2D.enabled = true;
            Draw();
        }

        if (_dragging)
        {
            _line.SetPosition(0, _startPos);
            _line.SetPosition(1, _endPos);
            _line.enabled = true;
        }
        else
        {
            _cooldownTimer   -= Time.deltaTime;
            _line.startColor =  new Color(1 - (cooldown - _cooldownTimer) / cooldown, 0, 0);
            _line.endColor   =  new Color(1 - (cooldown - _cooldownTimer) / cooldown, 0, 0);

            if (_cooldownTimer < cooldown / 2)
            {
                _spriteRenderer.enabled    = false;
                _polygonCollider2D.enabled = false;
                if (_cooldownTimer <= 0) _line.enabled = false;
            }
        }
    }

    private void Draw()
    {
        var delta = _endPos - _startPos;
        if (Math.Abs(delta.magnitude) < 0.1)
            return;
        var pos   = _startPos + delta / 2;
        var angle = (float) (180 * Math.Atan(delta.y / delta.x) / Math.PI - 45);
        gameObject.transform.SetPositionAndRotation(pos + new Vector3(-delta.y, delta.x, 0).normalized * 1.7f,
            Quaternion.Euler(new Vector3(0, 0,
                angle + (delta.x < 0 ? 180 : delta.y < 0 ? 360 : 0))));
    }
}