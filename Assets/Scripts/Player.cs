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

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // moves appropriate point marker based on selected dice combo
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

    // increments player score and updates point marker based on selected dice combo
    public void UpdateScore(int activeTrack) // assumes active track was subtracted by 2 when this fxn is called
    {
        scores[activeTrack] = gm.tempScores[activeTrack];
        UpdateMarker(activeTrack);
        if (scores[activeTrack] == gm.pointTracks[activeTrack].pointMax)
        {
            points++;
            gm.pointTracks[activeTrack].isFinished = true;
        }

        if (points == 3)
        {
            Debug.Log($"{gameObject.name} wins!");
        }
    }
}
