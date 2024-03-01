using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PointTrack[] pointTracks;
    public int activeTrack;
    public Dice[] diceSet;
    public GameObject optionsDice;

    public void RollAllDice()
    {
        optionsDice.SetActive(false);
        foreach (Dice die in diceSet)
        {
            StartCoroutine(die.RollDie());
        }

        optionsDice.SetActive(true);
        StartCoroutine(optionsDice.GetComponent<OptionsDice>().UpdateOptions(diceSet[0].rollValue, diceSet[1].rollValue, diceSet[2].rollValue, diceSet[3].rollValue));
    }
}
