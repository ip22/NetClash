using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchMakingManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _matchMakingCanvas;
    [SerializeField] private GameObject _cancelButton;

    public void Subscribe() {
        MultiplayerManager.Instance.GetReady += GetReady;
        MultiplayerManager.Instance.StartGame += StartGame;
        MultiplayerManager.Instance.CancelStartGame += CancelStartGame;
    }

    public void Unsubscribe() {
        MultiplayerManager.Instance.GetReady -= GetReady;
        MultiplayerManager.Instance.StartGame -= StartGame;
        MultiplayerManager.Instance.CancelStartGame -= CancelStartGame;
    }

    private void GetReady() {
        _cancelButton.SetActive(false);
    }

    private void CancelStartGame() {
        _cancelButton.SetActive(true);
    }

    private void StartGame() {
        
    }


    public async void FindOpponent() { // EnterArena #snake
        _cancelButton.SetActive(false);
        _mainMenuCanvas.SetActive(false);
        _matchMakingCanvas.SetActive(true);

        // комната если нужна
        //var room = MultiplayerManager.Instance.Connect();

        await MultiplayerManager.Instance.Connect();
        _cancelButton.SetActive(true);
    }

    public void CancelFind() { 
        _mainMenuCanvas?.SetActive(false);
        _matchMakingCanvas?.SetActive(true);

        MultiplayerManager.Instance.Leave();
    }
}
