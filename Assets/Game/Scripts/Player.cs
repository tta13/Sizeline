using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private int initialCardAmount = 4;
    [SerializeField] GameObject cardTemplate;
    [SerializeField] private Transform content;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject endGame;

    private List<Card> _cards;
    private GameObject _lastCard;

    private void Start()
    {
        _cards = new List<Card>();

        for(int i = 0; i < initialCardAmount; i++)
        {
            var randomCard = CardManager.Instance.GetRandomCard();
            _cards.Add(randomCard);
            var card = Instantiate(cardTemplate, content);
            card.GetComponent<CardTemplate>().SetCard(randomCard);
            card.GetComponent<Button>().onClick.AddListener(() => SetCard(card, randomCard));
        }
    }

    private void Update()
    {
        if (_cards.Count > 0) return;
        gamePanel.SetActive(false);
        endGame.SetActive(true);
    }

    private void SetCard(GameObject g, Card c)
    {
        _lastCard = g;
        GameManager.Instance.SetCurrentCard(c);
    }

    public void UseCard()
    {
        var index = _lastCard.transform.GetSiblingIndex();
        Destroy(_lastCard);
        _cards.RemoveAt(index);
    }

    public void DrawCard()
    {
        var randomCard = CardManager.Instance.GetRandomCard();
        _cards.Add(randomCard);
        var card = Instantiate(cardTemplate, content);
        card.GetComponent<CardTemplate>().SetCard(randomCard);
        card.GetComponent<Button>().onClick.AddListener(() => SetCard(card, randomCard));
    }
}
