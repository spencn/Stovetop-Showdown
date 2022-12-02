using UnityEngine;
using UnityEngine.UI;

public class ScoreToText : MonoBehaviour
{
    //public AllScores scores;
    GameObject set;
    Settings settings;

    private void Start()
    {
        set = GameObject.Find("Settings");
        settings = set.GetComponent<Settings>();
    }

    // Start is called before the first frame update
    private void Update()
    {
        GameObject go = GameObject.Find("Score");
        AllScores gs = go.GetComponent<AllScores>();
        for (var i = 0; i < gs.numPlayers; i++)
            if (settings.getActive(i) == false)
            {
                continue;
            } else
            {
                if(i == 0)
                {
                    GameObject.Find("P" + i + "Score").GetComponent<Text>().text = ("Elia the Olive's score: " + gs.getScore(i));
                } else if (i == 1)
                {
                    GameObject.Find("P" + i + "Score").GetComponent<Text>().text = ("Pepe the Jalapeno's score: " + gs.getScore(i));
                }
                else if (i == 2)
                {
                    GameObject.Find("P" + i + "Score").GetComponent<Text>().text = ("Osprio the String Bean's score: " + gs.getScore(i));
                }
                else if (i == 3)
                {
                    GameObject.Find("P" + i + "Score").GetComponent<Text>().text = ("Aurancio the Carrot's score: " + gs.getScore(i));
                }
                else if (i == 4)
                {
                    GameObject.Find("P" + i + "Score").GetComponent<Text>().text = ("Preston Gravy's score: " + gs.getScore(i));
                }

            }
                


        if (gs.GetWinner() == 0)
        {
            GameObject.Find("Winner").GetComponent<Text>().text = "Elia the Olive wins";
        } else if (gs.GetWinner() == 1)
        {
            GameObject.Find("Winner").GetComponent<Text>().text = "Pepe the Jalapeno wins";
        }
        else if (gs.GetWinner() == 2)
        {
            GameObject.Find("Winner").GetComponent<Text>().text = "Osprio the String Bean wins";
        }
        else if (gs.GetWinner() == 3)
        {
            GameObject.Find("Winner").GetComponent<Text>().text = "Aurancio the Carrot wins";
        }
        else if (gs.GetWinner() == 4)
        {
            GameObject.Find("Winner").GetComponent<Text>().text = "Preston Gravy wins";
        }

        gameObject.SetActive(false);
    }
}