using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pointMarkerPrefab;
    public int[] scores;
    public GameObject[] scoreMarkers;
    private GameManager gm;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // moves appropriate point marker based on option button selection
    private void UpdateMarker(int activeTrack)
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

    // increments player score and updates point marker based on option button selection
    public void UpdateScore(int activeTrack)
    {
        scores[activeTrack]++;
        UpdateMarker(activeTrack);
    }
}
