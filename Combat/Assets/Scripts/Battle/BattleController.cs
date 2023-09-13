using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

[RequireComponent(typeof(CommandBattleSpawner))]
public class BattleController : MonoBehaviour
{
    public BattleUnit CurrentUnit { get; private set; }
    public bool IsPresenceFighters
    {
        get { return _hero.Health.IsAlive  && _enemy.Health.IsAlive; }
    }

    public Action<BattleUnit> OnChangeUnit;

    [SerializeField] private BattleUnit _heroTemplates;
    [SerializeField] private BattleUnit _enemiTemplates;

    private BattleUnit _hero;
    private BattleUnit _enemy;

    private List<BattleUnit> _initiativeUnits = new List<BattleUnit>();

    private CommandBattleSpawner _unitSpawner;

    private void Awake()
    {
        _unitSpawner = GetComponent<CommandBattleSpawner>();
    }

    public void InitUnits(Action OnKilledUnit = null)
    {
        _enemy = _unitSpawner.SpawnUnitToRight(_enemiTemplates, OnKilledUnit);
        _hero = _unitSpawner.SpawnUnitToLeft(_heroTemplates, OnKilledUnit);

        _enemy.SetTarget(_hero);
        _hero.SetTarget(_enemy);
    }

    public IEnumerator Round()
    {
        foreach (var battleUnit in _initiativeUnits)
        {
            OnChangeUnit?.Invoke(battleUnit);
            CurrentUnit = battleUnit;

            yield return battleUnit.StepAttack();

            CurrentUnit = null;
        }
    }
}
