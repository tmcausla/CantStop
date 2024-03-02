using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PointTrack[] pointTracks;
    public int activeTrack;
    public Dice[] diceSet;
    public OptionsDice optionsDice;
    [SerializeField] private TextMeshProUGUI and1;
    [SerializeField] private TextMeshProUGUI and2;
    [SerializeField] private TextMeshProUGUI and3;

    public void RollAllDice()
    {
        optionsDice.HideDice();
        foreach (Dice die in diceSet)
        {
            StartCoroutine(die.RollDie());
        }
    }

    public void HideUI()
    {

    }
}
