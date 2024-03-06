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
    [SerializeField] private GameObject[] buttons;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void DisplayButtonUI(int sum1, int sum2)
    {
        Debug.Log($"There are {gm.activeLanes.Count} active lanes");
        if (gm.activeLanes.Count < 2)
        {
            buttons[0].SetActive(true);
            UpdateButtonText(sum1, sum2);
            return;
        }
        else if (gm.activeLanes.Count == 2)
        {
            // check if at least one combo is in active lanes list
            if (gm.activeLanes.Contains(sum1) || gm.activeLanes.Contains(sum2))
            {
                Debug.Log("Activating one big button");
                buttons[0].SetActive(true);
            }
            else 
            {
                Debug.Log("Activating two small buttons");
                buttons[1].SetActive(true);
                buttons[2].SetActive(true);
            }
            UpdateButtonText(sum1, sum2);
            return;
        }
        else if (gm.activeLanes.Count == 3)
        {
            // check if both combos are in active lanes list
            if (gm.activeLanes.Contains(sum1) && gm.activeLanes.Contains(sum2))
            {
                Debug.Log("Activating one big button");
                buttons[0].SetActive(true);
            }
            else if (gm.activeLanes.Contains(sum1))
            {
                Debug.Log("Activating small left button");
                buttons[1].SetActive(true);
            }
            else if (gm.activeLanes.Contains(sum2))
            {
                Debug.Log("Activating small right button");
                buttons[2].SetActive(true);
            }
            else
            {
                Debug.Log("You bust! Next player's turn");
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
