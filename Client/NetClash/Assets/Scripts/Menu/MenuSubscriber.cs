using UnityEngine;

public class MenuSubscriber : MonoBehaviour
{
    [SerializeField] private DeckManager _deckManager;
    [SerializeField] private SelectedDeckUI _selectedDeckUI;
    [SerializeField] private SelectedDeckUI _selectedDeckUI_2;
    [SerializeField] private SelectedDeckUI _selectedDeckUI_MM;
    [SerializeField] private AvailableDeckUI _availableDeckUI;
    [SerializeField] private MatchMakingManager _matchMakingManager;

    private void Start() {
        _deckManager.UpdateSelected += _selectedDeckUI.UpdateCardsList;
        _deckManager.UpdateSelected += _selectedDeckUI_2.UpdateCardsList;
        _deckManager.UpdateSelected += _selectedDeckUI_MM.UpdateCardsList;
        _deckManager.UpdateAvailable += _availableDeckUI.UpdateCardsList;

        _matchMakingManager.Subscribe();
    }

    private void OnDestroy() {
        _deckManager.UpdateSelected -= _selectedDeckUI.UpdateCardsList;
        _deckManager.UpdateSelected -= _selectedDeckUI_2.UpdateCardsList;
        _deckManager.UpdateSelected -= _selectedDeckUI_MM.UpdateCardsList;
        _deckManager.UpdateAvailable -= _availableDeckUI.UpdateCardsList;

        _matchMakingManager.Unsubscribe();
    }
}
