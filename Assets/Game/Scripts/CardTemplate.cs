using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardTemplate : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameTxt;

    private Card _myCard;

    public void SetCard(Card card)
    {
        _myCard = card;
    }

    public Card GetCard() => _myCard;

    bool _firstUpdate = true;

    private void Update()
    {
        if (!_firstUpdate && !refresh) return;

        image.sprite = _myCard.sprite;
        nameTxt.text = _myCard.name;

        _firstUpdate = false;
    }

    bool refresh = false;

    public void Refresh()
    {
        refresh = true;
    }
}
