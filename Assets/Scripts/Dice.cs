using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] diceSides;
    public Dice[] options;
    public SpriteRenderer rend;
    public int rollValue;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public IEnumerator RollDie()
    {
        int randomSide = 0;

        for (int i = 0; i < 20; i++)
        {
            randomSide = Random.Range(1, 7);
            rend.sprite = diceSides[randomSide - 1];
            yield return new WaitForSeconds(0.1f);
        }

        rollValue = randomSide;
        //Debug.Log(rollValue);
        UpdateOptions(rollValue);
    }

    private void UpdateOptions(int value)
    {
        foreach (Dice die in options)
        {
            die.gameObject.SetActive(true);
            die.rend.sprite = die.diceSides[value - 1];
        }
    }
}
