using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _corpse = null;

    [SerializeField]
    private float speed = 1.0f;

    //TODO: Enemy Reset

    private void Update()
    {
        Player player = Player.Get();
        transform.position += (player.transform.position - transform.position).normalized * speed * Time.deltaTime;
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
