using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI winnerText;
    public TextMeshProUGUI[] playerScores;

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
}
