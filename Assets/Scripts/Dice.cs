using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    // dice sprites
    public Sprite[] diceSides;

    // holds the option UI dice that match up with the game dice rolled by player
    public Dice[] options;
    public SpriteRenderer rend;
    public int rollValue;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    // shuffles dice number sprites and assigns random number 1-6 to die then updates sprites for the possible combos
    public IEnumerator RollDie()
    {
        int randomSide = 0;

        for (int i = 0; i < 20; i++) // cycles through 20 sprites to visualize die roll
        {
            randomSide = Random.Range(1, 7);
            rend.sprite = diceSides[randomSide - 1];
            yield return new WaitForSeconds(0.1f);
        }

        rollValue = randomSide;
        UpdateOptions(rollValue);
    }

    // assigns sprite to options UI based on rolled value
    private void UpdateOptions(int value)
    {
        foreach (Dice die in options)
        {
            die.gameObject.SetActive(true);
            die.rend.sprite = die.diceSides[value - 1];
        }
    }
}
