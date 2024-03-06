using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PointTrack[] pointTracks;

    //holds references for the 4 game dice
    public Dice[] diceSet;

    //allows GameManager to reference UI for options dice
    public OptionsDice optionsDice;

    // reference for "&" text on options UI
    [SerializeField] private GameObject andUI;

    // reference for buttons that confirms roll selection
    [SerializeField] private ButtonManager combo1Buttons;
    [SerializeField] private ButtonManager combo2Buttons;
    [SerializeField] private ButtonManager combo3Buttons;

    // reference for roll dice button
    [SerializeField] private GameObject rollDiceButton;
    public Player activePlayer;

    // tracks and allows player to roll for up to 3 active lanes during their turn
    public List<int> activeLanes;

    // UI elements are hidden when dice are rolled
    public void RollAllDice()
    {
        HideUI();
        foreach (Dice die in diceSet)
        {
            StartCoroutine(die.RollDie());
        }
        StartCoroutine(ShowUI());
    }

    // hides UI elements
    public void HideUI()
    {
        optionsDice.HideDice();
        andUI.SetActive(false);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
        rollDiceButton.SetActive(false);
    }

    // shows UI elements
    public IEnumerator ShowUI()
    {
        for (int i = 0; i < 19; i++)
        {
            yield return new WaitForSeconds(0.11f);
        }
        andUI.SetActive(true);
        combo1Buttons.DisplayButtonUI(diceSet[0].rollValue + diceSet[1].rollValue, diceSet[2].rollValue + diceSet[3].rollValue);
        combo2Buttons.DisplayButtonUI(diceSet[0].rollValue + diceSet[2].rollValue, diceSet[3].rollValue + diceSet[1].rollValue);
        combo3Buttons.DisplayButtonUI(diceSet[0].rollValue + diceSet[3].rollValue, diceSet[2].rollValue + diceSet[1].rollValue);
    }


    // updates player score using dice combo 1
    public void ConfirmOption1()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[1].rollValue))
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[1].rollValue);
        }
        if (!activeLanes.Contains(diceSet[2].rollValue + diceSet[3].rollValue))
        {
            activeLanes.Add(diceSet[2].rollValue + diceSet[3].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[1].rollValue - 2);
        activePlayer.UpdateScore(diceSet[2].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    public void ConfirmOption1A()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[1].rollValue)) 
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[1].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[1].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    public void ConfirmOption1B()
    {
        if (!activeLanes.Contains(diceSet[2].rollValue + diceSet[3].rollValue)) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[2].rollValue + diceSet[3].rollValue);
        }
        activePlayer.UpdateScore(diceSet[2].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    // updates player score using dice combo 2
    public void ConfirmOption2()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[2].rollValue))
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[2].rollValue);
        }
        if (!activeLanes.Contains(diceSet[1].rollValue + diceSet[3].rollValue))
        {
            activeLanes.Add(diceSet[1].rollValue + diceSet[3].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[2].rollValue - 2);
        activePlayer.UpdateScore(diceSet[1].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    public void ConfirmOption2A()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[2].rollValue)) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[2].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[2].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    public void ConfirmOption2B()
    {
        if (!activeLanes.Contains(diceSet[1].rollValue + diceSet[3].rollValue)) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[1].rollValue + diceSet[3].rollValue);
        }
        activePlayer.UpdateScore(diceSet[1].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    // updates player score using dice combo 3
    public void ConfirmOption3()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[3].rollValue))
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[3].rollValue);
        }
        if (!activeLanes.Contains(diceSet[2].rollValue + diceSet[1].rollValue))
        {
            activeLanes.Add(diceSet[2].rollValue + diceSet[1].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[3].rollValue - 2);
        activePlayer.UpdateScore(diceSet[2].rollValue + diceSet[1].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    public void ConfirmOption3A()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[3].rollValue)) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[3].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    public void ConfirmOption3B()
    {
        if (!activeLanes.Contains(diceSet[2].rollValue + diceSet[1].rollValue)) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[2].rollValue + diceSet[1].rollValue);
        }
        activePlayer.UpdateScore(diceSet[2].rollValue + diceSet[1].rollValue - 2);

        rollDiceButton.SetActive(true);
        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }
}
