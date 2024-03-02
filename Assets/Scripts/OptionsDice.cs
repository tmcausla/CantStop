using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsDice : MonoBehaviour
{
    public Dice[] options;

    public void HideDice()
    {
        foreach (Dice die in options)
        {
            die.gameObject.SetActive(false);
        }
    }
}
