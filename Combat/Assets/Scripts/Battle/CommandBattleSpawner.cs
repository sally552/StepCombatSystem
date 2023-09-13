using System;
using Units;
using UnityEngine;

public class CommandBattleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnLeftPoint;
    [SerializeField]
    private Transform _spawnRightPoint;

    public BattleUnit SpawnUnitToLeft(BattleUnit unit, Action OnDead = null) => SpawnUnits(unit, _spawnLeftPoint, OnDead);
    public BattleUnit SpawnUnitToRight(BattleUnit unit, Action OnDead = null) => SpawnUnits(unit, _spawnRightPoint, OnDead);

    private BattleUnit SpawnUnits(BattleUnit unit, Transform spawnPoint, Action OnDead = null)
    {
        BattleUnit newFighter = Instantiate(unit, spawnPoint);
        newFighter.Initialize();
        newFighter.Health.Died += OnDead;

        return newFighter;
    }
}