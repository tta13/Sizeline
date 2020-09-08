using System.Collections.Generic;
using TMPro;
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
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject endGame;
    [SerializeField] TMP_Text feedback;

    public List<Card> GetCards() => cards;

    public Card GetRandomCard()
    {
        if(cards.Count <= 0)
        {
            EndGame();
            return null;
        }
        var index = Random.Range(0, cards.Count);
        var card = cards[index];
        cards.RemoveAt(index);
        return card;
    }

    private void EndGame()
    {
        gamePanel.SetActive(false);
        endGame.SetActive(true);
        feedback.text = "Acabaram as cartas ...";
    }
}
