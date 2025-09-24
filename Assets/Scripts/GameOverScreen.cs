using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private GameManager gm;
    [SerializeField] private TextMeshProUGUI winnerText;
    public TextMeshProUGUI[] playerScores;

    private void Awake()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    public void DisplayWinner(Player player, Player[] players)
    {
        winnerText.text = $"{player.gameObject.name} wins!";
        winnerText.color = player.playerColor;

        for (int i = 0; i < players.Length; i++)
        {
            playerScores[i].text = $"{players[i].gameObject.name}\n{players[i].points}";
            playerScores[i].color = players[i].playerColor;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowGameState()
    {
        gameObject.SetActive(false);
        gm.pointsMap.SetActive(true);
        gm.trackLabels.SetActive(true);
        gm.backButton.SetActive(true);
;
        foreach (Player player in gm.players)
        {
            foreach (GameObject marker in player.scoreMarkers)
            {
                if (marker) marker.SetActive(true);
            }
        }
        foreach (PointTrack track in gm.pointTracks)
        {
            track.trackBlocker.SetActive(false);
        }
    }
}
