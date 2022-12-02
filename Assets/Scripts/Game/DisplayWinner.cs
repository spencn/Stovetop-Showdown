using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWinner : MonoBehaviour
{
    Image winner;
    public Sprite[] characters;

    void Start()
    {
        winner = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject go = GameObject.Find("Score");
        AllScores gs = go.GetComponent<AllScores>();
        if (gs.GetWinner() == 0)
        {
            winner.sprite = characters[0];
        }
        else if (gs.GetWinner() == 1)
        {
            winner.sprite = characters[1];
        }
        else if (gs.GetWinner() == 2)
        {
            winner.sprite = characters[2];
        }
        else if (gs.GetWinner() == 3)
        {
            winner.sprite = characters[3];
        }
        else if (gs.GetWinner() == 4)
        {
            winner.sprite = characters[4];
        }
    }
}
