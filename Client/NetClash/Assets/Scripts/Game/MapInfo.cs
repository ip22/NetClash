using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    #region SingletoneOneScene
    public static MapInfo Instance { get; private set; }

    private void Awake() {
        if (Instance) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnDestroy() {
        if (Instance == null) Instance = null;
    }
    #endregion

    // towers
    [SerializeField] private List<Tower> _enemyTowers = new List<Tower>();
    [SerializeField] private List<Tower> _playerTowers = new List<Tower>();

    // units
    // ground
    [SerializeField] private List<Unit> _enemyGroundUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerGroundUnits = new List<Unit>();
    // air
    [SerializeField] private List<Unit> _enemyAirUnits = new List<Unit>();
    [SerializeField] private List<Unit> _playerAirUnits = new List<Unit>();

    private void Start() {
        SubscribeDestroy(_enemyTowers);
        SubscribeDestroy(_playerTowers);

        SubscribeDestroy(_enemyGroundUnits);
        SubscribeDestroy(_playerGroundUnits);

        SubscribeDestroy(_enemyAirUnits);
        SubscribeDestroy(_playerAirUnits);
    }

    public void AddUnit(Unit unit) {
        List<Unit> list;
        if (unit.isEnemy) list = unit.parameters.isAir ? _enemyAirUnits : _enemyGroundUnits;
        else list = unit.parameters.isAir ? _playerAirUnits : _playerGroundUnits;

       AddObjectToList(list, unit);
    }

    public bool TryGetNearestAnyUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance) {
        TryGetNearestGroundUnit(currentPosition, enemy, out Unit groundUnit, out float groundDistance);
        TryGetNearestAirUnit(currentPosition, enemy, out Unit airUnit, out float airDistance);

        if (airDistance < groundDistance) {
            unit = airUnit;
            distance = airDistance;
        } else {
            unit = groundUnit;
            distance = groundDistance;
        }

        return unit;
    }

    public bool TryGetNearestGroundUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance) {
        List<Unit> units = enemy ? _enemyGroundUnits : _playerGroundUnits;
        unit = GetNearest(currentPosition, units, out distance);
        return unit;
    }

    public bool TryGetNearestAirUnit(in Vector3 currentPosition, bool enemy, out Unit unit, out float distance) {
        List<Unit> units = enemy ? _enemyAirUnits : _playerAirUnits;
        unit = GetNearest(currentPosition, units, out distance);
        return unit;
    }

    public Tower GetNearestTower(in Vector3 currentPosition, bool enemy) {
        List<Tower> towers = enemy ? _enemyTowers : _playerTowers;

        return GetNearest(currentPosition, towers, out float distance);
    }

    private T GetNearest<T>(in Vector3 currentPosition, List<T> objects, out float distance) where T : MonoBehaviour {
        distance = float.MaxValue;
        if (objects.Count <= 0) return null;

        distance = Vector3.Distance(currentPosition, objects[0].transform.position);
        T nearest = objects[0];

        for (int i = 0; i < objects.Count; i++) {
            float tempDistance = Vector3.Distance(currentPosition, objects[i].transform.position);

            if (tempDistance > distance) continue;

            nearest = objects[i];
            distance = tempDistance;
        }

        return nearest;
    }

    private void SubscribeDestroy<T>(List<T> objects) where T : IDestroyed {
        for (int i = 0; i < objects.Count; i++) {
            T obj = objects[i];

            void RemoveAndUnsubscribe() {
                RemoveObjectFromList(objects, obj);
                obj.Destroyed -= RemoveAndUnsubscribe;
            }

            objects[i].Destroyed += RemoveAndUnsubscribe;
        }
    }

    private void AddObjectToList<T>(List<T> list, T obj) where T : IDestroyed {
        list.Add(obj);

        void RemoveAndUnsubscribe() {
            RemoveObjectFromList(list, obj);
            obj.Destroyed -= RemoveAndUnsubscribe;
        }

        obj.Destroyed += RemoveAndUnsubscribe;
    }

    private void RemoveObjectFromList<T>(List<T> list, T obj) {
        if (list.Contains(obj)) list.Remove(obj);
    }
}