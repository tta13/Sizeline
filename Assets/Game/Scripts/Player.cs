using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _cards.Add(CardManager.Instance.GetRandomCard());
            var card = Instantiate(cardTemplate, content);
        }
    }
}
