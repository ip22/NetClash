using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CardSelecter : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private AvailableDeckUI _availableDeckUI;
    [SerializeField] private SelectedDeckUI _selectedDeckUI;

    private List<Card> _availableCards = new List<Card>();
    private List<Card> _selectedCards = new List<Card>();
    // индекс карты, которая выбрана в верхнем списке
    public IReadOnlyList<Card> AvailableCards { get { return _availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return _selectedCards; } }

    private int _selectToggleIndex = 0;

    private void OnEnable() {
        Init();
    }

    private void Init() {
        _availableCards.Clear();
        for (int i = 0; i < _deckManager.AvailableCards.Count; i++) {
            _availableCards.Add(_deckManager.AvailableCards[i]);
        }

        _selectedCards.Clear();
        for (int i = 0; i < _deckManager.SelectedCards.Count; i++) {
            _selectedCards.Add(_deckManager.SelectedCards[i]);
        }
    }

    public void SetSelectToggleIndex(int index) {
        _selectToggleIndex = index;

    }

    public void SelectCard(int cardID) {
        _selectedCards[_selectToggleIndex] = _availableCards[cardID - 1];
        _selectedDeckUI.UpdateCardsList(SelectedCards);
        _availableDeckUI.UpdateCardsList(AvailableCards, SelectedCards);
    }


    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _selectMenu;

    public void CancelButton() {
        _mainMenu.gameObject.SetActive(true);
        _selectMenu.gameObject.SetActive(false);
        _availableCards.Clear();
        _selectedCards.Clear();
        _deckManager.BackToMainMenu();
    }

    public void SaveButton() {
        SaveDeck();
    }

    private int[] _newSelectedCardsIDs;
    private void SaveDeck() {
        _newSelectedCardsIDs = new int[SelectedCards.Count];
        for (int i = 0; i < SelectedCards.Count; i++) {
            _newSelectedCardsIDs[i] = SelectedCards[i].id;
        }



        Network.Instance.Post(URLLibrary.MAIN + URLLibrary.SETSELECTEDCARDS,
        new Dictionary<string, string> {
            { "userID", /*UserInfo.Instance.ID.ToString()*/"5" },
            { "newSelectedIDs", JsonUtility.ToJson(_newSelectedCardsIDs) }
        },
        SuccessSave, ErrorSave);
    }

    private void ErrorSave(string error) {
        Debug.LogError(error);
        CancelButton();
    }

    private void SuccessSave(string data) {
        Debug.Log(JsonUtility.ToJson(data));

        _mainMenu.gameObject.SetActive(true);
        _selectMenu.gameObject.SetActive(false);



        var availableCardsIDs = new List<int>();
        for (int i = 0; i < AvailableCards.Count; i++) {
            availableCardsIDs.Add(AvailableCards[i].id);
        }

        _deckManager.Init(availableCardsIDs, _newSelectedCardsIDs);
    }
}
