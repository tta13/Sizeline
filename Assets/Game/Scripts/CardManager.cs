using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    #region Singleton
    private static CardManager _instance;

    public static CardManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<CardManager>();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    #endregion

    [SerializeField] private List<Card> cards;

    public List<Card> GetCards() => cards;

    public Card GetRandomCard()
    {
        var index = Random.Range(0, cards.Count);
        var card = cards[index];
        cards.RemoveAt(index);
        return card;
    }
}
