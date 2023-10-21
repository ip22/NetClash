using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject _lockScreenCanvas;
    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new List<Card>();
    [SerializeField] private List<Card> _selectedCards = new List<Card>();

    // инкапсул€ци€ списков
    public IReadOnlyList<Card> AvailableCards { get { return _availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return _selectedCards; } }

    public event Action<IReadOnlyList<Card>, IReadOnlyList<Card>> UpdateAvailable;
    public event Action<IReadOnlyList<Card>> UpdateSelected;
    //

    #region Editor
#if UNITY_EDITOR
    [SerializeField] private AvailableDeckUI _availableDeckUI;

    // срабатывает, когда мен€етс€ кодc
    private void OnValidate() {
        _availableDeckUI.SetAllCardsCount(_cards);
    }
#endif
    #endregion

    public void Init(List<int> availableCardsIndexes, int[] selectedCardsIndexes) {
        for (int i = 0; i < availableCardsIndexes.Count; i++) {
            _availableCards.Add(_cards[availableCardsIndexes[i]]);
        }

        for (int i = 0; i < selectedCardsIndexes.Length; i++) {
            _selectedCards.Add(_cards[selectedCardsIndexes[i]]);
        }

        UpdateAvailable?.Invoke(AvailableCards, SelectedCards);
        UpdateSelected?.Invoke(SelectedCards);

        _lockScreenCanvas.SetActive(false);
        //UpdateAvailable += Test;
    }

    /*private void Test(IReadOnlyList<Card> obj) {

        throw new NotImplementedException();
    }*/

    public void ChangeDeck(IReadOnlyList<Card> selectedCards, Action success) {
        _lockScreenCanvas.SetActive(true);
        int[] IDs = new int[selectedCards.Count];
        for (int i = 0; i < selectedCards.Count; i++) {
            IDs[i] = selectedCards[i].id;
        }

        string json = JsonUtility.ToJson(new Wrapper(IDs));
        string uri = URLLibrary.MAIN + URLLibrary.SETSELECTDECK;
        Dictionary<string, string> data = new Dictionary<string, string> { { "userID", UserInfo.Instance.ID.ToString() }, { "json", json } };

        success += () => {
            for (int i = 0; i < _selectedCards.Count; i++) {
                _selectedCards[i] = selectedCards[i];
            }
            UpdateSelected?.Invoke(SelectedCards);
        };

        Network.Instance.Post(uri, data, (s) => SendSuccess(s, success), Error);
    }

    private void SendSuccess(string message, Action success) {
        if (message != "ok") {
            Error(message);
            return;
        }
        success?.Invoke();
        _lockScreenCanvas.SetActive(false);
    }

    private void Error(string message) {
        Debug.LogError("Sending new selected deck error: " + message);
        _lockScreenCanvas.SetActive(false);
    }

    [System.Serializable]
    private class Wrapper
    {
        public int[] IDs;
        public Wrapper(int[] iDs) {
            this.IDs = iDs;
        }
    }
}

[System.Serializable] // чтобы класс отображалс€ в инспекторе
public class Card
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
}