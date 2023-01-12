using UnityEngine;

namespace TestTask
{
    public class PlayerController : MonoBehaviour
    {
        private Hero _player;

        private void Start()
        {
            _player = transform.root.GetComponent<Hero>();
        }

        private void Update()
        {
            _player.InputX = Input.GetAxis("Horizontal");
            _player.InputZ = Input.GetAxis("Vertical");
        }
    }
}