using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private float _timeRemaining;

    public AllScores scores;

    public Text  timer;
    public float totalTime;

    private void Start()
    {
        GameObject go = GameObject.Find("Settings");
        Settings gs = go.GetComponent<Settings>();
        totalTime = gs.time;
        _timeRemaining = totalTime;
    }

    private void Update()
    {
        if (_timeRemaining <= 0.0f)
        {
            if (scores.GetWinner() == -1)
                timer.text = "Sudden Death";
            else
                SceneManager.LoadScene("Victory");
        }
        else
        {
            _timeRemaining -= Time.deltaTime;
            timer.text = (int) (_timeRemaining / 60) + ":" +
                         ((int) _timeRemaining % 60).ToString().PadLeft(2, '0');
        }
    }
}