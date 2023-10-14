using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Card[] _cards;
    [SerializeField] private List<Card> _availableCards = new List<Card>();
    [SerializeField] private List<Card> _selectedCards = new List<Card>();

    // инкапсул€ци€ списков
    public IReadOnlyList<Card> AvailableCards { get { return _availableCards; } }
    public IReadOnlyList<Card> SelectedCards { get { return _selectedCards; } }

    public event Action<IReadOnlyList<Card>> UpdateAvailable;
    public event Action<IReadOnlyList<Card>> UpdateSelected;


    public void Init(List<int> availableCardsIndexes, int[] selectedCardsIndexes) {
        for (int i = 0; i < availableCardsIndexes.Count; i++) {
            _availableCards.Add(_cards[availableCardsIndexes[i]]);
        }

        for (int i = 0; i < selectedCardsIndexes.Length; i++) {
            _selectedCards.Add(_cards[selectedCardsIndexes[i]]);
        }

        UpdateAvailable?.Invoke(AvailableCards);
        UpdateSelected?.Invoke(SelectedCards);

        UpdateAvailable += Test;
    }

    private void Test(IReadOnlyList<Card> obj) {
        
        throw new NotImplementedException();
    }
}

[System.Serializable] // чтобы класс отображалс€ в инспекторе
public class Card
{
    [field: SerializeField] public string name { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
}