using UnityEngine;
using UnityEngine.Serialization;

public class Control : MonoBehaviour
{
    private readonly float   _acceleration = 1.0f;
    private readonly float   _maxVelocity  = 10.0f;
    private          Vector2 _velocity;

    [FormerlySerializedAs("playerID")] public string playerId = "Food1";

    // Start is called before the first frame update
    private void Start()
    {
        _velocity.x = 0.0f;
        _velocity.y = 0.0f;
    }

    // Update is called once per frame
    private void Update()
    {
        float yTranslation;
        float xTranslation;

        if (Input.GetAxis(playerId + "Y") != 0 && _velocity.y < _maxVelocity)
        {
            _velocity.y += Input.GetAxis(playerId + "Y") * _acceleration;

            if (_velocity.y > _maxVelocity) _velocity.y = _maxVelocity;
        }

        if (Input.GetAxis(playerId + "X") != 0 && _velocity.x < _maxVelocity)
        {
            _velocity.x += Input.GetAxis(playerId + "X") * _acceleration;

            if (_velocity.x > _maxVelocity) _velocity.x = _maxVelocity;
        }


        yTranslation = Input.GetAxis(playerId + "Y") * _velocity.y;
        xTranslation = Input.GetAxis(playerId + "X") * _velocity.x;

        xTranslation *= Time.deltaTime;
        yTranslation *= Time.deltaTime;

        transform.Translate(xTranslation, 0,            0);
        transform.Translate(0,            yTranslation, 0);
    }
}