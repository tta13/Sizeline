using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int initialCardAmount = 4;
    [SerializeField] GameObject cardTemplate;
    [SerializeField] private Transform content;

    private List<Card> _cards;

    private void Start()
    {
        _cards = new List<Card>();

        for(int i = 0; i < initialCardAmount; i++)
        {
            var randomCard = CardManager.Instance.GetRandomCard();
            _cards.Add(randomCard);
            var card = Instantiate(cardTemplate, content);
            card.GetComponent<CardTemplate>().SetCard(randomCard);
            card.GetComponent<Button>().onClick.AddListener(() => SetCard(randomCard));
        }
    }

    private void SetCard(Card c)
    {
        GameManager.Instance.SetCurrentCard(c);
    }

    public void UseCard()
    {

    }

    public void DrawCard()
    {
        var randomCard = CardManager.Instance.GetRandomCard();
        _cards.Add(randomCard);
        var card = Instantiate(cardTemplate, content);
        card.GetComponent<CardTemplate>().SetCard(randomCard);
        card.GetComponent<Button>().onClick.AddListener(() => SetCard(randomCard));
    }
}
