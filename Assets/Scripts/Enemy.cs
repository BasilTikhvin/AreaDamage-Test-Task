using System;
using UnityEngine;

namespace TestTask
{
    public class Enemy : Destructible
    {
        private SpawnPoints _spawnPoints;
        public static Action OnEnemyDeath;

        protected override void Start()
        {
            base.Start();

            _spawnPoints = FindObjectOfType<SpawnPoints>();
        }

        protected override void Die()
        {
            base.Die();

            OnEnemyDeath.Invoke();
            _spawnPoints.GetRandomSpawnPoint().SpawnNewEnemy();
        }
    }
}