using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject pointMarkerPrefab;
    public int score2 = 0;
    public GameObject score2Marker;
    public int score3 = 0;
    public GameObject score3Marker;
    public int score4 = 0;
    public GameObject score4Marker;
    public int score5 = 0;
    public GameObject score5Marker;
    public int score6 = 0;
    public GameObject score6Marker;
    public int score7 = 0;
    public GameObject score7Marker;
    public Transform[] track7;
    public int score8 = 0;
    public GameObject score8Marker;
    public int score9 = 0;
    public GameObject score9Marker;
    public int score10 = 0;
    public GameObject score10Marker;
    public int score11 = 0;
    public GameObject score11Marker;
    public int score12 = 0;
    public GameObject score12Marker;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            score7++;
            UpdateMarker();
        }
    }

    private void UpdateMarker()
    {
        if (!score7Marker)
        {
            score7Marker = Instantiate(pointMarkerPrefab, track7[score7 - 1].position, track7[score7 - 1].rotation);
        }
        score7Marker.transform.position = track7[score7 - 1].position;
    }
}
