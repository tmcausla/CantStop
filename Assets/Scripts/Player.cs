using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pointMarkerPrefab;
    public int points = 0;
    public int[] scores;
    public GameObject[] scoreMarkers;
    private GameManager gm;
    public Color playerColor;

    private void Awake()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    // updates player point markers on active tracks
    private void UpdateMarker(int activeTrack) // assumes active track was subtracted by 2 when UpdateScore is called
    {
        if (!scoreMarkers[activeTrack]) // creates a point marker if none already exist
        {
            scoreMarkers[activeTrack] = Instantiate(pointMarkerPrefab, gm.pointTracks[activeTrack].trackMarkers[scores[activeTrack] - 1].position, gm.pointTracks[activeTrack].trackMarkers[scores[activeTrack] - 1].rotation);
        }

        // moves point marker based on player score of active track
        if (scores[activeTrack] <= gm.pointTracks[activeTrack].pointMax)
        {
            scoreMarkers[activeTrack].transform.position = gm.pointTracks[activeTrack].trackMarkers[scores[activeTrack] - 1].position;
        }
    }

    // player scores for each active track based on temp score and updates player point markers on active tracks
    public void UpdateScore(int activeTrack) // assumes active track was subtracted by 2 when this fxn is called
    {
        scores[activeTrack] = gm.tempScores[activeTrack] > gm.pointTracks[activeTrack].pointMax ? gm.pointTracks[activeTrack].pointMax : gm.tempScores[activeTrack];
        UpdateMarker(activeTrack);
        if (scores[activeTrack] == gm.pointTracks[activeTrack].pointMax)
        {
            points++;
            gm.pointTracks[activeTrack].CloseTrack(this);
        }

        if (points >= 3)
        {
            gm.gameOver = true;
            Debug.Log($"{gameObject.name} wins!");
        }
    }
}
