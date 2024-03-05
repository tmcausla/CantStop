using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsDice : MonoBehaviour
{
    // array reference for option UI dice
    public Dice[] options;

    // hides dice sprites
    public void HideDice()
    {
        foreach (Dice die in options)
        {
            die.gameObject.SetActive(false);
        }
    }
}
