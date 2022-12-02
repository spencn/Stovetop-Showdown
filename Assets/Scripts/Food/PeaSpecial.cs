using UnityEngine;

public class PeaSpecial : MonoBehaviour
{
    // Start is called before the first frame update
    private string     playerID;
    public  float      cooldown;
    public  float      _cooldownTimer = 0;
    public  float      spread         = 45f;
    public  GameObject pea;

    GameObject  go;
    AudioScript gs;

    private void Start()
    {
        playerID = GetComponent<PlayerControllerScript>().playerID;

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
        if (_cooldownTimer <= 0)
        {
            if (!Input.GetButtonDown(playerID + "Special"))
                return;
            gs.peaSound();
            _cooldownTimer = cooldown;
            var vel = gameObject.GetComponent<PlayerControllerScript>().prevMove.normalized;
            if (vel.Equals(Vector3.zero))
            {
                vel = Vector3.right;
            }

            for (var i = -1; i < 2; i++)
            {
                var velRot = (Quaternion.AngleAxis(spread * i, Vector3.forward) * vel).normalized;
                var inst = Instantiate(pea,
                    gameObject.transform.position + velRot * 1.5f,
                    gameObject.transform.rotation,
                    gameObject.transform.parent);
                inst.name                                 = "Pea";
                inst.GetComponent<Rigidbody2D>().velocity = velRot * 12;
                Destroy(inst, 10);
            }
        }
        else
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }
}