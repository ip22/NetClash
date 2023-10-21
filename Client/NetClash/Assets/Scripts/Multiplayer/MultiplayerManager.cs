using Colyseus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : ColyseusManager<MultiplayerManager>
{
    private const string RoomName = "state_handler";
    private ColyseusRoom<State> _room;

    protected override void Awake() {
        base.Awake();

        Instance.InitializeClient();
        Connect();
    }

    private async void Connect() {
        _room = await Instance.client.JoinOrCreate<State>(RoomName);
    }
}
