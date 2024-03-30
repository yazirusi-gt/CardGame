using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerManager : MonoBehaviour
{ 
    public List<int> Deck = new List<int>();

    public int heroHp;
    public int manaCost;
    public int defaultManaCost;

    public void Init(List<int> cardDeck)
    {
        Deck = cardDeck;
        heroHp = 10;
        manaCost = 10;
        defaultManaCost = 10;
    }

    public void InCreaseManaCost()
    {
        defaultManaCost++;
        manaCost = defaultManaCost;
    }
}
