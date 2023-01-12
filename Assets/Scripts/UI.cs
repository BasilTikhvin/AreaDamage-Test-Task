using Newtonsoft.Json.Linq;
using System;
using System.Data;
using TestTask;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Hero _player;
    [Space]
    [SerializeField] private Text _enemiesKilledText;
    [SerializeField] private Text _moveSpeedText;
    [SerializeField] private Text _damageText;
    [SerializeField] private Text _damageAreaText;

    public  int _enemiesKilledAmount;

    private void Awake()
    {
        Enemy.OnEnemyDeath += UpdateEnemiesKilledAmount;

        _player.OnMoveSpeedPowerUp += UpdateMoveSpeedText;
        _player.OnDamagePowerUp += UpdateDamageText;
        _player.OnAoEPowerUp += UpdateDamageAreaText;

        _enemiesKilledText.text = $"ENEMIES KILLED: {_enemiesKilledAmount}";
    }

    private void UpdateEnemiesKilledAmount()
    {
        _enemiesKilledAmount++;
        _enemiesKilledText.text = $"ENEMIES KILLED: {_enemiesKilledAmount}";
    }

    private void UpdateMoveSpeedText(float value)
    {
        _moveSpeedText.text = $"Move Speed: {value}";
    }

    private void UpdateDamageText(float value)
    {
        _damageText.text = $"Damage: {value}";
    }

    private void UpdateDamageAreaText(float value)
    {
        _damageAreaText.text = $"Area of Damage: {value}";
    }
}
