using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllScores : MonoBehaviour
{
    public int[] _scores;
    public  int   numPlayers;
    GameObject set;
    Settings settings;
    GameObject go;
    AllScores gs;

    private void Start()
    {
        go = GameObject.Find("Score");
        gs = go.GetComponent<AllScores>();
        _scores = new int[numPlayers];
        GameObject scores = GameObject.Find("Scores");
        set = GameObject.Find("Settings");
         settings = set.GetComponent<Settings>();

    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            Destroy(gameObject);
        }
        if(settings.getActive(0) == false)
        {
            _scores[0] = Int32.MinValue;
        }
        if (settings.getActive(1) == false)
        {
            _scores[1] = Int32.MinValue;
        }
        if (settings.getActive(2) == false)
        {
            _scores[2] = Int32.MinValue;
        }
        if (settings.getActive(3) == false)
        {
            _scores[3] = Int32.MinValue;
        }
        if (settings.getActive(4) == false)
        {
            _scores[4] = Int32.MinValue;
        }

    }

    public void AwardKnockOut(Hitmarker script)
    {
        if (script == null || script.owner == -1) // invalid
            return;
        if (script.lastTouched == -1 || script.owner == script.lastTouched) // suicide 
        {
            _scores[script.owner] -= 1;
            script.lastTouched = -1;
            UpdateScores();
            return;
        }

        _scores[script.lastTouched] = _scores[script.lastTouched] + 1;
        script.lastTouched = -1;
        UpdateScores();
    }

    /**
     * 0-indexed
     */
    public int GetWinner()
    {
        int max = Int32.MinValue;
        for (int i = 0; i < gs.numPlayers; i++)
        {
            if (!(settings.getActive(i)))
            {
                continue;
            }
            if (_scores[i] > max)
            {
                max = _scores[i];
            }

        }
            int count = 0;
            //var max = _scores.Max(x => x);
            for (int i = 0; i < gs.numPlayers; i++)
        {
            if (!(settings.getActive(i)))
            {
                continue;
            }
            if (_scores[i] == max)
            {
                count++;
            }
            if (_scores[i] == max && count == 2)
            {
                return -1;
            }
        }
        //if (_scores.Count(x => x == max) > 2)
          //  return -1;
        return Array.IndexOf(_scores, max);
    }

    public int getScore(int index)
    {
        return _scores[index];
    }

    private void UpdateScores()
    {
        for (var i = 0; i < 5; i++)
        {
            if (!(settings.getActive(i)))
            {
                continue;
            }
            GameObject.Find("P" + i + "Score").GetComponent<Text>().text = _scores[i].ToString();
        }
           
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);


        
    }
}