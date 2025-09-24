using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pointsMap;
    public GameObject trackLabels;
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
    [SerializeField] private bool hardMode;
    public GameObject backButton;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI turnIndicator;
    public Player activePlayer;
    public Player[] players;
    private int playerIdx = 0;

    // tracks and allows player to roll for up to 3 active lanes during their turn
    public List<int> activeLanes;
    public bool gameOver = false;
    private static GameManager instance;
    //I LOVE KELLY SO MUCH

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {
        activePlayer = players[0]; // THIS WILL BE REPLACED BY LOADING SELECTIONS FROM PREVIOUS MENU SCREEN INTO GAME MANAGER
        rollDiceButton.GetComponent<Image>().color = activePlayer.playerColor;

        turnIndicator.color = activePlayer.playerColor;
        turnIndicator.text = $"{activePlayer.gameObject.name}'s\nturn"; 
    }

    // rolls the 4 game dice on a player's turn
    public void RollAllDice()
    {
        HideUI(); // UI elements are hidden when dice are rolled
        foreach (Dice die in diceSet)
        {
            die.rollValue = Random.Range(1, 7);
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
            // pushes appearance of buttons until after dice rolling animations
            yield return new WaitForSeconds(0.11f);
        }
        andUI.SetActive(true);
        combo1Buttons.DisplayButtonUI(diceSet[0].rollValue + diceSet[1].rollValue, diceSet[2].rollValue + diceSet[3].rollValue);
        combo2Buttons.DisplayButtonUI(diceSet[0].rollValue + diceSet[2].rollValue, diceSet[3].rollValue + diceSet[1].rollValue);
        combo3Buttons.DisplayButtonUI(diceSet[0].rollValue + diceSet[3].rollValue, diceSet[2].rollValue + diceSet[1].rollValue);

        if (Busted())
        {
            Debug.Log("You bust!");
            turnIndicator.text = "YA\nBUST!";
            nextTurnButton.SetActive(true);
            nextTurnButton.GetComponent<Image>().color = playerIdx + 1 == players.Length ? players[0].playerColor : players[playerIdx + 1].playerColor;
        }
    }

    public void RollOrStop()
    {
        rollDiceButton.SetActive(true);
        if (activeLanes.Count > 0 && !Busted())
        {
            if (hardMode)
            {
                stopButton.SetActive(HardModeCheck());
                if (!HardModeCheck())
                {
                    Debug.Log("HARD MODE BIATCH");
                }
            }
            else
            {
                stopButton.SetActive(true);
            }
        }

        combo1Buttons.HideButtons();
        combo2Buttons.HideButtons();
        combo3Buttons.HideButtons();
    }


    // updates temp score using dice combo 1
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

        RollOrStop();
    }

    public void ConfirmOption1A()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[1].rollValue))
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[1].rollValue);
        }
        UpdateTempScore(diceSet[0].rollValue + diceSet[1].rollValue - 2);

        RollOrStop();
    }

    public void ConfirmOption1B()
    {
        if (!activeLanes.Contains(diceSet[2].rollValue + diceSet[3].rollValue)) 
        {
            activeLanes.Add(diceSet[2].rollValue + diceSet[3].rollValue);
        }
        UpdateTempScore(diceSet[2].rollValue + diceSet[3].rollValue - 2);

        RollOrStop();
    }

    // updates temp score using dice combo 2
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

        RollOrStop();
    }

    public void ConfirmOption2A()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[2].rollValue)) 
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[2].rollValue);
        }
        UpdateTempScore(diceSet[0].rollValue + diceSet[2].rollValue - 2);

        RollOrStop();
    }

    public void ConfirmOption2B()
    {
        if (!activeLanes.Contains(diceSet[1].rollValue + diceSet[3].rollValue)) 
        {
            activeLanes.Add(diceSet[1].rollValue + diceSet[3].rollValue);
        }
        UpdateTempScore(diceSet[1].rollValue + diceSet[3].rollValue - 2);

        RollOrStop();
    }

    // updates temp score using dice combo 3
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

        RollOrStop();
    }

    public void ConfirmOption3A()
    {
        if (!activeLanes.Contains(diceSet[0].rollValue + diceSet[3].rollValue)) 
        {
            activeLanes.Add(diceSet[0].rollValue + diceSet[3].rollValue);
        }
        UpdateTempScore(diceSet[0].rollValue + diceSet[3].rollValue - 2);

        RollOrStop();
    }

    public void ConfirmOption3B()
    {
        if (!activeLanes.Contains(diceSet[2].rollValue + diceSet[1].rollValue)) 
        {
            activeLanes.Add(diceSet[2].rollValue + diceSet[1].rollValue);
        }
        UpdateTempScore(diceSet[2].rollValue + diceSet[1].rollValue - 2);

        RollOrStop();
    }

    // moves appropriate temp point marker based on selected dice combo
    private void UpdateTempMarker(int activeTrack) // assumes active track was subtracted by 2 when UpdateTempScore is called
    {
        if (!tempScoreMarkers[activeTrack]) // creates a point marker if none already exists
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

    // increments temp score and updates temp point marker based on selected dice combo
    private void UpdateTempScore(int activeTrack) // assumes active track was subtracted by 2 when fxn is called
    {
        tempScores[activeTrack]++;
        UpdateTempMarker(activeTrack);
    }

    public void StopAndScore()
    {
        foreach (int lane in activeLanes)
        {
            activePlayer.UpdateScore(lane - 2);
            tempScoreMarkers[lane - 2].SetActive(false);
        }
        activeLanes.Clear();

        rollDiceButton.SetActive(false);
        stopButton.SetActive(false);

        if (!gameOver)
        {
            nextTurnButton.SetActive(true);
            nextTurnButton.GetComponent<Image>().color = playerIdx + 1 == players.Length ? players[0].playerColor : players[playerIdx + 1].playerColor;
        }
        else
        {
            GameOver();
        }
    }

    private bool Busted() 
    {
        return combo1Buttons.bust && combo2Buttons.bust && combo3Buttons.bust;
    }

    private bool HardModeCheck() //returns false if a temp score marker is overlapping another player's marker,  prevents stop and score
    {
        foreach (Player player in players)
        {
            if (player == activePlayer)
            {
                continue;
            }

            foreach (int lane in activeLanes)
            {
                if (player.scores[lane - 2] == tempScores[lane - 2])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void StartNextTurn()
    {
        if (activeLanes.Count > 0)
        {
            foreach (int lane in activeLanes)
            {
                tempScoreMarkers[lane - 2].SetActive(false);
            }
            activeLanes.Clear();
        }

        StartCoroutine(FadeAndChangeText());       
        
        nextTurnButton.SetActive(false);
    }

    private void GameOver()
    {
        pointsMap.SetActive(false);
        trackLabels.SetActive(false);
        turnIndicator.text = "";
        foreach (Dice die in diceSet)
        {
            die.gameObject.SetActive(false);
        }
        HideUI();
        foreach (Player player in players)
        {
            foreach (GameObject marker in player.scoreMarkers)
            {
                if (marker) marker.SetActive(false);
            }
        }
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<GameOverScreen>().DisplayWinner(activePlayer, players);
    }

    public void BackToEndScreen()
    {
        gameOverScreen.SetActive(true);
        pointsMap.SetActive(false);
        trackLabels.SetActive(false);
        backButton.SetActive(false);

        foreach (Player player in players)
        {
            foreach (GameObject marker in player.scoreMarkers)
            {
                if (marker) marker.SetActive(false);
            }
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color textColor = turnIndicator.color;

        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / 0.5f);
            textColor.a = alpha;
            turnIndicator.color = textColor;
            yield return null;
        }

        textColor.a = endAlpha;
        turnIndicator.color = textColor;
    }

    private IEnumerator FadeAndChangeText()
    {
        // Fade Out
        yield return StartCoroutine(Fade(1f, 0f));

        // Wait for 1 second
        yield return new WaitForSeconds(0.5f);
        
        playerIdx = playerIdx + 1 == players.Length ? 0 : playerIdx + 1;
        activePlayer = players[playerIdx];

        for (int i = 0; i < tempScores.Length; i++)
        {
            tempScores[i] = activePlayer.scores[i];
        }

        turnIndicator.color = activePlayer.playerColor;
        turnIndicator.text = $"{activePlayer.gameObject.name}'s\nturn";
        rollDiceButton.SetActive(true);
        rollDiceButton.GetComponent<Image>().color = activePlayer.playerColor;

        combo1Buttons.ChangeButtonColor(activePlayer);
        combo2Buttons.ChangeButtonColor(activePlayer);
        combo3Buttons.ChangeButtonColor(activePlayer);

        // Fade In
        yield return StartCoroutine(Fade(0f, 1f));
    }
}
