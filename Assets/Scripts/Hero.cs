using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TestTask
{
    public class Hero : Destructible
    {
        [Header("Stats")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackRate;
        [SerializeField] private float _areaOfDamage;
        [SerializeField] private int _maxEnemiesDamaged;
        [SerializeField] private LayerMask _enemyLayer;

        [Space]
        [Header("PowerUp Chance")]
        [SerializeField] private Vector2 _percentage;
        [SerializeField] private Vector2 _moveSpeedChance;
        [SerializeField] private Vector2 _attackDamageChance;
        [SerializeField] private Vector2 _attackRadiusChance;

        [Space]
        [Header("PowerUp Step")]
        [SerializeField] private float _moveSpeedStep;
        [SerializeField] private float _damageStep;
        [SerializeField] private float _areaOfDamageStep;

        public Action<int> OnEnemyKilled;
        public Action<float> OnMoveSpeedPowerUp;
        public Action<float> OnDamagePowerUp;
        public Action<float> OnAoEPowerUp;

        public float InputX { get; set; }
        public float InputZ { get; set; }

        private float _attackTime;
        private int _randomValue;

        protected override void Start()
        {
            base.Start();

            OnMoveSpeedPowerUp.Invoke(_moveSpeed);
            OnDamagePowerUp.Invoke(_damage);
            OnAoEPowerUp.Invoke(_areaOfDamage);
        }

        private void Update()
        {
            Move();

            Attack();
        }

        private void Move()
        {
            transform.position += _moveSpeed * Time.deltaTime * new Vector3(InputX, 0, InputZ);
        }

        private void Attack()
        {
            if (Time.time > _attackTime)
            {
                Collider[] hits = Physics.OverlapSphere(transform.position, _areaOfDamage, _enemyLayer);

                _attackTime = Time.time + _attackRate;

                int enemiesDamaged = 0;

                SortHitsArrayByDistance(hits);

                for (int i = 0; i < hits.Length; i++)
                {
                    if (enemiesDamaged >= _maxEnemiesDamaged) return;

                    if (hits[i].TryGetComponent(out Destructible target))
                    {
                        enemiesDamaged++;
                        target.TakeDamage((int)_damage);
                    }
                }
            }
        }

        private void SortHitsArrayByDistance(Collider[] array)
        {
            for (int j = 0; j < array.Length; j++)
            {
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (Vector3.Distance(transform.position, array[i].transform.position) > Vector3.Distance(transform.position, array[i + 1].transform.position))
                    {
                        var temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                    }
                }
            }
        }

        public void PowerUP()
        {
            _randomValue = UnityEngine.Random.Range((int)_percentage.x, (int)_percentage.y);

            if (_randomValue >= _moveSpeedChance.x && _randomValue <= _moveSpeedChance.y)
            {
                _moveSpeed += _moveSpeedStep;
                OnMoveSpeedPowerUp.Invoke(_moveSpeed);
            }
            else if (_randomValue >= _attackDamageChance.x && _randomValue <= _attackDamageChance.y)
            {
                _damage += _damageStep;
                OnDamagePowerUp.Invoke(_damage);
            }
            else if (_randomValue >= _attackRadiusChance.x && _randomValue <= _attackRadiusChance.y)
            {
                _areaOfDamage += _areaOfDamageStep;
                OnAoEPowerUp.Invoke(_areaOfDamage);
            }
        }
    }
}