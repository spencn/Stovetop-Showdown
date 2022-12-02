using UnityEngine;

public class OliveSpecial : MonoBehaviour
{
    private PlayerControllerScript _pcs;
    public float cooldown;
    public float boostSpeed;
    public float _cooldownTimer;
    private Hitmarker _hm;

    public GameObject oilSplash;
    private bool oilActive = false;
    public float oilTime;
    private float oilRemaining;

    private GameObject oil;

    public float startLag;
    private float lagCounter;

    public float iframes;
    private float iCount;
    
    private Vector3 originalOilPos;

    public GameObject oilCharge;
    private GameObject charge;

    private SpriteRenderer chargeSprite;
    public float transparencySpeed;

    GameObject go;
    AudioScript gs;

    private void Start()
    {
        _pcs = gameObject.GetComponent<PlayerControllerScript>();
        _hm = gameObject.GetComponent<Hitmarker>();

        go = GameObject.Find("SFX");
        gs = go.GetComponent<AudioScript>();

    }
    private void OnDisable()
    {
        _cooldownTimer = 0;
    }


    // Update is called once per frame.
    private void Update()
    {
        if (Input.GetButtonDown(_pcs.playerID + "Special") && !_pcs.busy && _cooldownTimer <= 0)
        {
            gs.squirtSound();
            _cooldownTimer = cooldown;
            _pcs.busy = true;

            oilActive = true;
            lagCounter = startLag;
            oilRemaining = oilTime;

            charge = Instantiate(oilCharge, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);

            chargeSprite = charge.GetComponent<SpriteRenderer>();
        }
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;         
        }
        if (oilActive && lagCounter > 0)
        {
            lagCounter -= Time.deltaTime;

            charge.transform.position = gameObject.transform.position;

            var tempColor = chargeSprite.color;

            tempColor.a += transparencySpeed;
            chargeSprite.color = tempColor;

            if (lagCounter < 0)
            {
                oil = Instantiate(oilSplash, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
                oil.name = "Oil";
                originalOilPos = oil.transform.position;
                Destroy(oil, oilTime);

                _hm.knockbackEnabled = true;
                _hm.knockback *= 3;
                gameObject.GetComponent<Rigidbody2D>().velocity = _pcs.prevMove.normalized * _pcs.speed * boostSpeed;
                gameObject.GetComponent<Collider2D>().enabled = false;

                Destroy(charge);

                iCount = iframes;
            }
        }
        else if (oilActive && oilRemaining > 0)
        {
            oilRemaining -= Time.deltaTime;

            if (iCount < 0)
            {
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                iCount -= Time.deltaTime;
            }

            if (oilRemaining > 0)
            {
                oil.transform.position = originalOilPos;
            }
        }
        else if (oilActive)
        {
            _pcs.busy = false;
            _hm.knockbackEnabled = false;
            _hm.knockback /= 3;
        }
    }
}