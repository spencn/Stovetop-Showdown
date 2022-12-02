using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    private Vector3                  _respawnPosition;
    public float respawnInvuln = 2;
    private float _respawnInvulnTimer = 0;
    private bool _respawnedRecently = false;
    public  float                    respawnTime = 4f;
    private float                    _timeDeadRemaining;
    private bool isDead = true;
    private Dictionary<Object, bool> _intialValues = new Dictionary<Object, bool>();
    private PlayerControllerScript _pcs; 
    private void Start()
    {
        _pcs = gameObject.GetComponent<PlayerControllerScript>();
        _respawnPosition = gameObject.transform.position;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (!_respawnedRecently)
                return;
            _respawnInvulnTimer -= Time.deltaTime;
            if (_respawnInvulnTimer <= 0)
            {
                _respawnedRecently = false;
                _pcs.invulnerable = false;
            }
            return;
        }

        _timeDeadRemaining -= Time.deltaTime;
        if (_timeDeadRemaining <= 0)
            SetDead(false);
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
    }

    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public void SetDead(bool isDead)
    {
        if (this.isDead == isDead)
            return;
        this.isDead = isDead;
        _timeDeadRemaining = respawnTime;
        _respawnInvulnTimer = respawnInvuln;
        if (isDead)
        {
            foreach (var c in gameObject.GetComponents<Behaviour>())
            {
                if (c == this) continue;
                _intialValues[c] = c.enabled;
                c.enabled        = false;
            }
            foreach (var c in gameObject.GetComponents<Collider>())
            {
                _intialValues[c] = c.enabled;
                c.enabled        = false;
            }
            foreach (var c in gameObject.GetComponents<Renderer>())
            {
                _intialValues[c] = c.enabled;
                c.enabled        = false;
            }
            
        }
        else
        {
            gameObject.transform.SetPositionAndRotation(_respawnPosition, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
            _pcs.busy = false;
            _pcs.invulnerable = true;
            _respawnedRecently = true;
            foreach (var entry in _intialValues)
            {
                if (entry.Key is Behaviour)
                    ((Behaviour) entry.Key).enabled = entry.Value;
                if (entry.Key is Collider)
                    ((Collider) entry.Key).enabled = entry.Value;
                if (entry.Key is Renderer)
                    ((Renderer) entry.Key).enabled = entry.Value;
            }
        }
    }

}