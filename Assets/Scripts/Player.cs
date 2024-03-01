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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdateScore(gm.activeTrack - 2);
        }
    }

    private void UpdateMarker(int activeTrack)
    {
        if (!scoreMarkers[activeTrack])
        {
            scoreMarkers[activeTrack] = Instantiate(pointMarkerPrefab, gm.pointTracks[activeTrack].trackMarkers[scores[activeTrack] - 1].position, gm.pointTracks[activeTrack].trackMarkers[scores[activeTrack] - 1].rotation);
        }

        if (scores[activeTrack] <= gm.pointTracks[activeTrack].pointMax)
        {
            scoreMarkers[activeTrack].transform.position = gm.pointTracks[activeTrack].trackMarkers[scores[activeTrack] - 1].position;
        }
        
    }

    public void UpdateScore(int activeTrack)
    {
        scores[activeTrack]++;
        UpdateMarker(activeTrack);
    }
}
