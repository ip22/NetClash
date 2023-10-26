using Colyseus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    private const string RoomName = "state_handler";
    private const string GetReadyName = "GetReady";
    private const string StartGameName = "Start";
    private const string CancelStartGameName = "CancelStart";

    private ColyseusRoom<State> _room;

    public event Action GetReady;
    public event Action StartGame;
    public event Action CancelStartGame;

    protected override void Awake() {
        base.Awake();

        Instance.InitializeClient();
        DontDestroyOnLoad(gameObject);
    }

    // если надо дождаться и вернуть комнату
    //public async Task<ColyseusRoom<State>> Connect() {
    //    _room = await Instance.client.JoinOrCreate<State>(RoomName);
    //    return _room;
    //}

    public async Task Connect() {
        _room = await Instance.client.JoinOrCreate<State>(RoomName);
        _room.OnMessage<object>(GetReadyName, (empty) => GetReady?.Invoke());
        _room.OnMessage<object>(StartGameName, (empty) => StartGame?.Invoke());
        _room.OnMessage<object>(CancelStartGameName, (empty) => CancelStartGame?.Invoke());
    }

    public void Leave() {
        _room?.Leave();
        _room = null;
    }
}
