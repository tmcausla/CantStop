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
    [SerializeField] private GameObject buttonsUI;

    // reference for roll dice button
    [SerializeField] private GameObject rollDiceButton;

    public TextMeshProUGUI option1ButtonText;
    public TextMeshProUGUI option1aButtonText;
    public TextMeshProUGUI option1bButtonText;
    public TextMeshProUGUI option2ButtonText;
    public TextMeshProUGUI option2aButtonText;
    public TextMeshProUGUI option2bButtonText;
    public TextMeshProUGUI option3ButtonText;
    public TextMeshProUGUI option3aButtonText;
    public TextMeshProUGUI option3bButtonText;
    public Player activePlayer; // needs to hold a reference for each player and cycle through each to feed activePlayer into confirmOptions buttons

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
    }

    // hides UI elements
    public void HideUI()
    {
        optionsDice.HideDice();
        andUI.SetActive(false);
        buttonsUI.SetActive(false);
        rollDiceButton.SetActive(false);
    }

    // shows UI elements
    public void ShowUI()
    {
        andUI.SetActive(true);
        buttonsUI.SetActive(true); // CHECK FOR VALID OTIONS
        UpdateButtonText();
    }

    private void DisplayButtonUI() // PUT A CHECK FOR ACTIVE LANE WHEN SUM IS ADDED
    {
        switch(activeLanes.Count)
        {
            case 0:
            case 1:
                buttonsUI.SetActive(true);
                return;
            case 2:
                // check if at least one combo is in active lanes list
                    //if yes:
                        // offer both combos normally
                        // GM: offer big button for UI
                    //if no:
                        // small button for both combos, only one gets scored
                        // GM: offer 2 small buttons for UI
                return;
            case 3:
                // check if one or both combos are in active lanes list
                    //if yes:
                        // single button if both are active
                        // GM: offer big button for UI
                        // small button for single active combo
                        // GM: offer 1 small button for UI
                    //if no:
                        // player has busted, no progress on any tracks
                        // end turn, start next player's turn
                return;
            default:
                return;
        }
    }

    // updates player score using dice combo 1
    public void ConfirmOption1()
    {
        if (activeLanes.Contains(diceSet[0].rollValue + diceSet[1].rollValue) && activeLanes.Contains(diceSet[2].rollValue + diceSet[3].rollValue))
        {
            Debug.Log("Both values are already in list");
        }

        activeLanes.Add(diceSet[0].rollValue + diceSet[1].rollValue);
        activeLanes.Add(diceSet[2].rollValue + diceSet[3].rollValue);
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[1].rollValue - 2);
        activePlayer.UpdateScore(diceSet[2].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);

        
    }

    public void ConfirmOption1A()
    {
        if (activeLanes.Count < 3) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[1].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[1].rollValue - 2);

        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);
    }

    public void ConfirmOption1B()
    {
        if (activeLanes.Count < 3) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[2].rollValue + diceSet[3].rollValue);
        }
        activePlayer.UpdateScore(diceSet[2].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);
    }

    // updates player score using dice combo 2
    public void ConfirmOption2()
    {
        if (activeLanes.Contains(diceSet[0].rollValue + diceSet[2].rollValue) && activeLanes.Contains(diceSet[1].rollValue + diceSet[3].rollValue))
        {
            Debug.Log("Both values are already in list");
        }

        activeLanes.Add(diceSet[0].rollValue + diceSet[2].rollValue);
        activeLanes.Add(diceSet[1].rollValue + diceSet[3].rollValue);
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[2].rollValue - 2);
        activePlayer.UpdateScore(diceSet[1].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);

        
    }

    public void ConfirmOption2A()
    {
        if (activeLanes.Count < 3) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[2].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[2].rollValue - 2);

        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);
    }

    public void ConfirmOption2B()
    {
        if (activeLanes.Count < 3) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[1].rollValue + diceSet[3].rollValue);
        }
        activePlayer.UpdateScore(diceSet[1].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);
    }

    // updates player score using dice combo 3
    public void ConfirmOption3()
    {

        if (activeLanes.Contains(diceSet[0].rollValue + diceSet[3].rollValue) && activeLanes.Contains(diceSet[2].rollValue + diceSet[1].rollValue))
        {
            Debug.Log("Both values are already in list");
        }
        activeLanes.Add(diceSet[0].rollValue + diceSet[3].rollValue);
        activeLanes.Add(diceSet[2].rollValue + diceSet[1].rollValue);
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[3].rollValue - 2);
        activePlayer.UpdateScore(diceSet[2].rollValue + diceSet[1].rollValue - 2);
        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);

        
    }

    public void ConfirmOption3A()
    {
        if (activeLanes.Count < 3) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[3].rollValue);
        }
        activePlayer.UpdateScore(diceSet[0].rollValue + diceSet[3].rollValue - 2);

        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);
    }

    public void ConfirmOption3B()
    {
        if (activeLanes.Count < 3) // change to whether die combo is in active lanes
        {
            activeLanes.Add(diceSet[2].rollValue + diceSet[1].rollValue);
        }
        activePlayer.UpdateScore(diceSet[2].rollValue + diceSet[1].rollValue - 2);

        rollDiceButton.SetActive(true);
        buttonsUI.SetActive(false);
    }



    // updates selection buttons to match what was rolled on the game dice
    private void UpdateButtonText()
    {
        option1ButtonText.text = $"Move up on {diceSet[0].rollValue + diceSet[1].rollValue} and {diceSet[2].rollValue + diceSet[3].rollValue}";
        option1aButtonText.text = $"Move up on {diceSet[0].rollValue + diceSet[1].rollValue}";
        option1bButtonText.text = $"Move up on {diceSet[2].rollValue + diceSet[3].rollValue}";

        option2ButtonText.text = $"Move up on {diceSet[0].rollValue + diceSet[2].rollValue} and {diceSet[1].rollValue + diceSet[3].rollValue}";
        option2aButtonText.text = $"Move up on {diceSet[0].rollValue + diceSet[2].rollValue}";
        option2bButtonText.text = $"Move up on {diceSet[1].rollValue + diceSet[3].rollValue}";

        option3ButtonText.text = $"Move up on {diceSet[0].rollValue + diceSet[3].rollValue} and {diceSet[2].rollValue + diceSet[1].rollValue}";
        option3aButtonText.text = $"Move up on {diceSet[0].rollValue + diceSet[3].rollValue}";
        option3aButtonText.text = $"Move up on {diceSet[2].rollValue + diceSet[1].rollValue}";
    }

    
}
