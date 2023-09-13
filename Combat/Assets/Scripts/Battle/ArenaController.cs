using System;
using System.Collections;
using Units;
using UnityEngine;

[RequireComponent(typeof(BattleController))]
public class ArenaController : MonoBehaviour
{
    private int _round = 1;
    public Action OnBattleFinished;
    public Action<int> OnRoundFinished;

    private BattleController _battleController;
    private Coroutine _battle;
    private void Awake()
    {
        _battleController = GetComponent<BattleController>();
    }

    private void Start()
    {
        _battleController.InitUnits(OnKilledUnit);
        StartBattle();
    }

    private void StartBattle()
    {
        _battle = StartCoroutine(Battle());
    }

    private IEnumerator Battle()
    {

        //ToDo вынести в слой ниже
        while (_battleController.IsPresenceFighters)
        {
            yield return _battleController.Round();
            _round++;
            OnRoundFinished?.Invoke(_round);
        }        

        OnBattleFinished?.Invoke();
    }


    private void OnKilledUnit()
    {
        RestartBattle();
    }

    public void RestartBattle()
    {
        StopCoroutine(_battle);
        StartBattle();
    }
}
