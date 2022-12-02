using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControllerScript : MonoBehaviour
{
    public  float dashCooldown           = 1f;
    public float  dashStun = 0.5f;
    public  float revSpeed               = 1.0f;
    public  float speed                  = 1;
    public  float stunDuration           = 0.25f;
    private float _dashCooldownRemaining = 0;
    private float _stunDurationRemaining = 0;

    public bool busy = false;
    public bool invulnerable = false;
    public  string           playerID = "Food";
    public  Vector3          prevMove;
    public Rigidbody2D      _rb2D;
    private ContactPoint2D[] _cp;

    //// Start is called before the first frame update.
    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _hitmarker = GetComponent<Hitmarker>();
    }
    // Update is called once per frame.

    private Hitmarker _hitmarker;

    private void Update()
    {
        GameObject set = GameObject.Find("Settings");
        Settings settings = set.GetComponent<Settings>();

        if (playerID == "Food0")
        {
            if (!(settings.getActive(0)))
            {
                gameObject.SetActive(false);

                GameObject meter = GameObject.Find("P0Meter");
                meter.SetActive(false);

                GameObject score = GameObject.Find("P0Score");
                score.SetActive(false);

                //GameObject scores = GameObject.Find("Scores");
                //scores.GetComponent<AllScores>()._scores[0] = Int32.MinValue;
            }
        }
        else if (playerID == "Food1")
        {
            if (!(settings.getActive(1)))
            {
                gameObject.SetActive(false);

                GameObject meter = GameObject.Find("P1Meter");
                meter.SetActive(false);

                GameObject score = GameObject.Find("P1Score");
                score.SetActive(false);

               // GameObject scores = GameObject.Find("Scores");
               // scores.GetComponent<AllScores>()._scores[1] = Int32.MinValue;
            }
        }
        else if (playerID == "Food2")
        {
            if (!(settings.getActive(2)))
            {
                gameObject.SetActive(false);

                GameObject meter = GameObject.Find("P2Meter");
                meter.SetActive(false);

                GameObject score = GameObject.Find("P2Score");
                score.SetActive(false);

                //GameObject scores = GameObject.Find("Scores");
               // scores.GetComponent<AllScores>()._scores[2] = Int32.MinValue;
            }
        }
        else if (playerID == "Food3")
        {
            if (!(settings.getActive(3)))
            {
                gameObject.SetActive(false);

                GameObject meter = GameObject.Find("P3Meter");
                meter.SetActive(false);

                GameObject score = GameObject.Find("P3Score");
                score.SetActive(false);

               // GameObject scores = GameObject.Find("Scores");
               // scores.GetComponent<AllScores>()._scores[3] = Int32.MinValue;

                

            }
        }

        _hitmarker.knockbackEnabled = false;
        //Cooldown and setting hitbox for attack
        if (_dashCooldownRemaining > 0)
        {

            _dashCooldownRemaining -= Time.deltaTime;
            if (_dashCooldownRemaining > 0)
                _hitmarker.knockbackEnabled = true;
        }

        if (_stunDurationRemaining > 0)
            _stunDurationRemaining -= Time.deltaTime;

        if (busy || _dashCooldownRemaining > dashStun || _stunDurationRemaining > 0)
        { // No input during hitstun or attack cooldown
            
            return;

        } 

        if (Input.GetButton(playerID + "X") || Input.GetButton(playerID + "Y") || Input.GetAxis(playerID + "X") != 0 || Input.GetAxis(playerID + "Y") != 0)
        {
            var moveVector = new Vector3(
                Input.GetAxis(playerID + "X"),
                Input.GetAxis(playerID + "Y"),
                0
            );
            var angle = (float) Math.Atan2(moveVector.y, moveVector.x);
            angle = (float) (angle * (180 / Math.PI));


            //Rotate the character to always face the direction of travel
            _rb2D.MoveRotation(angle * revSpeed * Time.fixedDeltaTime);

            _rb2D.velocity = moveVector * speed;
            prevMove       = moveVector; //Save move vector for when attacking while standing still
        }

        if (Input.GetButtonDown(playerID + "Dash"))
        {
            _dashCooldownRemaining = dashCooldown;
            _rb2D.velocity = prevMove.normalized * speed * 3.0f;
            _hitmarker.knockbackEnabled = true;
        }
    }

    public void stun()
    {
        _stunDurationRemaining = stunDuration;
    }

    public void kill()
    {
        if (!invulnerable)
            gameObject.GetComponent<RespawnScript>().SetDead(true);
    }
}