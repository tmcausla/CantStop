using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{
    private GameManager gm;
    public TextMeshProUGUI bothComboButtonText;
    public TextMeshProUGUI combo1ButtonText;
    public TextMeshProUGUI combo2ButtonText;
    public bool bust = false;
    [SerializeField] private GameObject[] buttons;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private bool IsValid(int activeTrack)
    {
        return !gm.pointTracks[activeTrack - 2].isFinished && gm.tempScores[activeTrack - 2] < gm.pointTracks[activeTrack - 2].pointMax;
    }

    public void DisplayButtonUI(int sum1, int sum2)
    {
        bust = false;
        if (gm.activeLanes.Count < 2)
        {
            if (IsValid(sum1) && IsValid(sum2))
            {
                buttons[0].SetActive(true);
            }
            else
            {
                buttons[1].SetActive(IsValid(sum1));
                buttons[2].SetActive(IsValid(sum2));
            }
            if (!IsValid(sum1) && !IsValid(sum2))
            {
                Debug.Log("No valid dice combo");
                bust = true;
            }
            
            UpdateButtonText(sum1, sum2);
            return;
        }
        else if (gm.activeLanes.Count == 2)
        {
            // check if at least one combo is in active lanes list
            if (gm.activeLanes.Contains(sum1) || gm.activeLanes.Contains(sum2))
            {
                if (IsValid(sum1) && IsValid(sum2))
                {
                    buttons[0].SetActive(true);
                }
                else
                {
                    buttons[1].SetActive(IsValid(sum1));
                    buttons[2].SetActive(IsValid(sum2));
                }
                if (!IsValid(sum1) && !IsValid(sum2))
                {
                    Debug.Log("No valid dice combo");
                    bust = true;
                }
            }
            else if (sum1 == sum2)
            {
                buttons[0].SetActive(IsValid(sum1));
                if (!IsValid(sum1))
                {
                    Debug.Log("No valid dice combo");
                    bust = true;
                }
            }
            else 
            {
                buttons[1].SetActive(IsValid(sum1));
                buttons[2].SetActive(IsValid(sum2));
            }

            if (!IsValid(sum1) && !IsValid(sum2))
            {
                Debug.Log("No valid dice combo");
                bust = true;
            }

            UpdateButtonText(sum1, sum2);
            return;
        }
        else if (gm.activeLanes.Count == 3)
        {
            // check if both combos are in active lanes list
            if (gm.activeLanes.Contains(sum1) && gm.activeLanes.Contains(sum2))
            {
                if (IsValid(sum1) && IsValid(sum2))
                {
                    buttons[0].SetActive(true);
                }
                else
                {
                    buttons[1].SetActive(IsValid(sum1));
                    buttons[2].SetActive(IsValid(sum2));
                }
                if (!IsValid(sum1) && !IsValid(sum2))
                {
                    Debug.Log("No valid dice combo");
                    bust = true;
                }
            }
            else if (gm.activeLanes.Contains(sum1))
            {
                buttons[1].SetActive(IsValid(sum1));
                if (!IsValid(sum1))
                {
                    Debug.Log("No valid dice combo");
                    bust = true;
                }
            }
            else if (gm.activeLanes.Contains(sum2))
            {
                buttons[2].SetActive(IsValid(sum2));
                if (!IsValid(sum2))
                {
                    Debug.Log("No valid dice combo");
                    bust = true;
                }
            }
            else
            {
                Debug.Log("No valid dice combo");
                bust = true;
            }

            UpdateButtonText(sum1, sum2);
            return;
        }
    }

    private void UpdateButtonText(int sum1, int sum2)
    {
        bothComboButtonText.text = $"Move up on {sum1} and {sum2}";
        combo1ButtonText.text = $"Move up on {sum1}";
        combo2ButtonText.text = $"Move up on {sum2}";
    }

    public void HideButtons()
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }
}
