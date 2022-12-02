using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public float time;

    public bool[] charActivated = new bool[5];

    private void Start()
    {
        time = 120.0f;
        var input = GameObject.Find("InputField").GetComponent<InputField>();
        var se    = new InputField.SubmitEvent();
        se.AddListener(SubmitName);
        input.onEndEdit = se;
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Victory"))
        {
            Destroy(gameObject);
        }
    }
        private void SubmitName(string arg0)
    {
        time = float.Parse(arg0);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void setP0(bool active)
    {
        charActivated[0] = !charActivated[0];
    }

    public void setP1(bool active)
    {
        charActivated[1] = !charActivated[1];
    }

    public void setP2(bool active)
    {
        charActivated[2] = !charActivated[2];
    }

    public void setP3(bool active)
    {
        charActivated[3] = !charActivated[3];
    }

    public void setP4(bool active)
    {
        charActivated[4] = !charActivated[4];
    }

    public bool getActive(int i)
    {
        return charActivated[i];
    }
}