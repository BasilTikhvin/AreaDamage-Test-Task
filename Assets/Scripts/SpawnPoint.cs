using UnityEngine;

namespace TestTask
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private float _radius;

        private void Start()
        {
            SpawnNewEnemy();
        }

        public void SpawnNewEnemy()
        {
            Instantiate(_enemies[Random.Range(0, _enemies.Length)], new Vector3(GetRandomPointInsideCircle().x, transform.position.y, GetRandomPointInsideCircle().z), Quaternion.identity);
        }

        public Vector3 GetRandomPointInsideCircle()
        {
            return transform.position + Random.insideUnitSphere * _radius;
        }

#if UNITY_EDITOR
        private static Color gizmoColor = new(0, 1, 0, 0.15f);

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, _radius);
        }
#endif

    }
}