using UnityEngine;

public class MenuSubscriber : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private SelectedDeckUI _selectedDeckUI;
    [SerializeField] private SelectedDeckUI _selectedDeckUI_2;
    [SerializeField] private AvailableDeckUI _availableDeckUI;


    private void Start() {
        _deckManager.UpdateSelected += _selectedDeckUI.UpdateCardsList;
        _deckManager.UpdateSelected += _selectedDeckUI_2.UpdateCardsList;
        _deckManager.UpdateAvailable += _availableDeckUI.UpdateCardsList;
    }

    private void OnDestroy() {
        _deckManager.UpdateSelected -= _selectedDeckUI.UpdateCardsList;
        _deckManager.UpdateSelected -= _selectedDeckUI_2.UpdateCardsList;
        _deckManager.UpdateAvailable -= _availableDeckUI.UpdateCardsList;
    }
}
