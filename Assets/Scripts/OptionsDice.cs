using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsDice : MonoBehaviour
{
    public Dice[] options;

    public IEnumerator UpdateOptions(int die1, int die2, int die3, int die4)
    {
        Debug.Log($"Working with {die1}, {die2}, {die3}, {die4}");
        for (int i = 0; i < options.Length; i++)
        {
            switch(i)
            {
                case 0:
                case 1:
                case 2:
                    options[i].rend.sprite = options[i].diceSides[die1 - 1];
                    break;
                case 3:
                case 4:
                case 5:
                    options[i].rend.sprite = options[i].diceSides[die2 - 1];
                    break;
                case 6:
                case 7:
                case 8:
                    options[i].rend.sprite = options[i].diceSides[die3 - 1];
                    break;
                case 9:
                case 10:
                case 11:
                    options[i].rend.sprite = options[i].diceSides[die4 - 1];
                    break;
                
            }
        }
        yield return new WaitForSeconds(1f);
    }
}
