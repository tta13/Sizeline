using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }
    #endregion

    [SerializeField] private GameObject cardTemplate;
    [SerializeField] private Transform content;
    [SerializeField] private int initialCardAmount = 1;

    private List<Card> _cards;

    private Card _currentCard;

    private void Start()
    {
        _cards = new List<Card>();

        for(int i = 0; i < initialCardAmount; i++)
        {
            _cards.Add(CardManager.Instance.GetRandomCard());
            var card = Instantiate(cardTemplate, content);
            card.GetComponent<CardTemplate>().SetCard(_cards[i]);
            card.GetComponent<Button>().onClick.AddListener(
                () => CreateTemporaryCard(card.transform.GetSiblingIndex()));
        }
    }

    public void SetCurrentCard(Card card)
    {
        _currentCard = card;
    }

    private void CreateTemporaryCard(int cardPressedIndex)
    {
        if (_currentCard == null) return;

        var tempCard = Instantiate(cardTemplate, content);
        tempCard.transform.SetSiblingIndex(cardPressedIndex + 1);
        var images = tempCard.GetComponentsInChildren<Image>();
        foreach(var image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, .4f);
        }
        tempCard.GetComponent<CardTemplate>().SetCard(_currentCard);
    }
}
