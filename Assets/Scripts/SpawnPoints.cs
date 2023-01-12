using UnityEngine;

namespace TestTask
{
    public class SpawnPoints : MonoBehaviour
    {
        [SerializeField] private SpawnPoint[] _spawnPoints;

        public SpawnPoint GetRandomSpawnPoint()
        {
            return _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        }
    }
}