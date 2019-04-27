using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public float speed;

        public GameObject player;

        //TODO: Enemy Reset

        private void FixedUpdate()
        {
            var range = Vector2.Distance(transform.position, player.transform.position);
            transform.Translate(Vector2.MoveTowards(transform.position, player.transform.position, range) * speed * Time.deltaTime);
        }
    }
}
