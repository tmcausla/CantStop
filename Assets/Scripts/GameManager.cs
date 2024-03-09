using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PointTrack[] pointTracks;
    public int[] tempScores;
    public GameObject[] tempScoreMarkers;
    public GameObject tempScoreMarkerPrefab;

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
    [SerializeField] private GameObject stopButton;
    [SerializeField] private GameObject nextTurnButton;
    public Player activePlayer;
    public Player[] players;
    private int playerIdx = 0;

    // tracks and allows player to roll for up to 3 active lanes during their turn
    public List<int> activeLanes;

    private void Start()
    {
        activePlayer = players[0];
    }

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
        stopButton.SetActive(false);
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

        if (Busted())
        {
            nextTurnButton.SetActive(true);
        }
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
        UpdateTempScore(diceSet[0].rollValue + diceSet[1].rollValue - 2);
        UpdateTempScore(diceSet[2].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

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
        UpdateTempScore(diceSet[0].rollValue + diceSet[1].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

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
        UpdateTempScore(diceSet[2].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

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
        UpdateTempScore(diceSet[0].rollValue + diceSet[2].rollValue - 2);
        UpdateTempScore(diceSet[1].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

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
        UpdateTempScore(diceSet[0].rollValue + diceSet[2].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

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
        UpdateTempScore(diceSet[1].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

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
        UpdateTempScore(diceSet[0].rollValue + diceSet[3].rollValue - 2);
        UpdateTempScore(diceSet[2].rollValue + diceSet[1].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

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
        UpdateTempScore(diceSet[0].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

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
        UpdateTempScore(diceSet[2].rollValue + diceSet[1].rollValue - 2);

        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            stopButton.SetActive(true);
        }

        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }

    // moves appropriate point marker based on selected dice combo
    private void UpdateTempMarker(int activeTrack) // assumes active track was subtracted by 2 when UpdateTempScore is called
    {
        if (!tempScoreMarkers[activeTrack]) // creates a point marker if none already exist
        {
            tempScoreMarkers[activeTrack] = Instantiate(tempScoreMarkerPrefab, pointTracks[activeTrack].trackMarkers[tempScores[activeTrack] - 1].position, pointTracks[activeTrack].trackMarkers[tempScores[activeTrack] - 1].rotation);
        }
        tempScoreMarkers[activeTrack].SetActive(true);
        // moves point marker based on player score of active track
        if (tempScores[activeTrack] <= pointTracks[activeTrack].pointMax)
        {
            tempScoreMarkers[activeTrack].transform.position = pointTracks[activeTrack].trackMarkers[tempScores[activeTrack] - 1].position;
        }
    }

    // increments player score and updates point marker based on selected dice combo
    private void UpdateTempScore(int activeTrack) // assumes active track was subtracted by 2 when fxn is called
    {
        tempScores[activeTrack]++;
        UpdateTempMarker(activeTrack);
    }

    public void StopAndScore()
    {
        foreach(int lane in activeLanes)
        {
            activePlayer.UpdateScore(lane - 2);
            tempScoreMarkers[lane - 2].SetActive(false);
        }
        activeLanes.Clear();

        rollDiceButton.SetActive(false);
        nextTurnButton.SetActive(true);
        stopButton.SetActive(false);
    }

    private bool Busted()
    {
        return combo1Buttons.bust && combo2Buttons.bust && combo3Buttons.bust;
    }

    public void StartNextTurn()
    {
        if (activeLanes.Count > 0)
        {
            foreach(int lane in activeLanes)
            {
                tempScoreMarkers[lane - 2].SetActive(false);
            }
            activeLanes.Clear();
        }

        playerIdx = playerIdx + 1 == players.Length ? 0 : playerIdx + 1;
        activePlayer = players[playerIdx];

        for (int i = 0; i < tempScores.Length; i++)
        {
            tempScores[i] = activePlayer.scores[i];
        }
        rollDiceButton.SetActive(true);
        nextTurnButton.SetActive(false);
    }
}
