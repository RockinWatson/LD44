using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _corpse = null;

    [SerializeField]
    private float speed = 1.0f;

    //TODO: Enemy Reset

    private void FixedUpdate()
    {
        Player player = Player.Get();
        var range = Vector2.Distance(transform.position, player.transform.position);
        transform.Translate(Vector2.MoveTowards(transform.position, player.transform.position, range) * speed * Time.deltaTime);
    }

    private void Reset()
    {
        this.gameObject.SetActive(false);
    }

    public void Kill()
    {
        Instantiate(_corpse, this.transform.position, Quaternion.identity);

        Reset();
    }
}
