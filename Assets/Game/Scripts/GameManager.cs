using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject cardTemplate;
    [SerializeField] private Transform content;
    [SerializeField] private int initialCardAmount = 1;

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
