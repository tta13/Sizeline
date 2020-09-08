using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private TMP_Text feedback;
    [SerializeField] private int initialCardAmount = 1;

    private List<Card> _cards;
    private Card _currentCard;
    private GameObject _tempCard;
    private Player _player;

    public GameObject tempCard => _tempCard;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

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

        if (_tempCard == null) return;
        {
            var cardRef = _tempCard.GetComponent<CardTemplate>();
            cardRef.SetCard(_currentCard);
            cardRef.Refresh();
        }            
    }

    private void CreateTemporaryCard(int cardPressedIndex)
    {
        if (_currentCard == null) return;

        if (_tempCard == null)
        {
            _tempCard = Instantiate(cardTemplate, content);
            SetImageAlpha(.4f);
            _tempCard.GetComponent<CardTemplate>().SetCard(_currentCard);
        }
        _tempCard.transform.SetSiblingIndex(cardPressedIndex + 1);
    }

    private void SetImageAlpha(float alpha)
    {
        var images = _tempCard.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }

    public void ChangeIndex(int amount)
    {
        var newIndex = _tempCard.transform.GetSiblingIndex() + amount;

        if (newIndex > content.childCount - 1)
            newIndex = content.childCount - 1;
        else if (newIndex < 0)
            newIndex = 0;

        _tempCard.transform.SetSiblingIndex(newIndex);
    } 

    public void Place()
    {
        _player.UseCard();

        var isRight = CheckAnswer();
        SetFeedback(isRight);

        if (!isRight)
        {
            Debug.Log("Wrong answer");
            Destroy(_tempCard);
            _currentCard = null;
            _player.DrawCard();
            return;
        }

        var siblingIndex = _tempCard.transform.GetSiblingIndex();
        _cards.Insert(siblingIndex, _tempCard.GetComponent<CardTemplate>().GetCard());
        SetImageAlpha(1f);
        _tempCard.GetComponent<Button>().onClick.AddListener(
            () => CreateTemporaryCard(siblingIndex));
        _tempCard = null;
    }

    private void SetFeedback(bool isRight)
    {
        feedback.gameObject.SetActive(true);
        feedback.text = isRight ? "Boa!" : "Resposta errada!";
    }

    private bool CheckAnswer()
    {
        var card = _tempCard.GetComponent<CardTemplate>().GetCard();
        var siblingIndex = _tempCard.transform.GetSiblingIndex();

        Card leftCard = null, rightCard = null;

        if(siblingIndex - 1 >= 0)
            leftCard = content.GetChild(siblingIndex - 1).GetComponent<CardTemplate>().GetCard();
        if(siblingIndex + 1 < content.childCount)
            rightCard = content.GetChild(siblingIndex + 1).GetComponent<CardTemplate>().GetCard();

        var answer = false;

        if ((leftCard == null || leftCard.size <= card.size) &&
            (rightCard == null || rightCard.size >= card.size))
            answer = true;

        return answer;
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
