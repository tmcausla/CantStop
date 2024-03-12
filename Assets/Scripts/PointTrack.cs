using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTrack : MonoBehaviour
{
    // highest point value player can achieve on a track
    public int pointMax;

    // holds positions on the board for each step on a track
    public Transform[] trackMarkers;
    public bool isFinished = false;

    public GameObject trackBlocker;

    public void CloseTrack(Player player)
    {
        isFinished = true;
        trackBlocker.SetActive(true);
        trackBlocker.GetComponent<SpriteRenderer>().color = player.playerColor;
    }
}
