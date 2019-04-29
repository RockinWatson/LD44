using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFX : MonoBehaviour {

    [SerializeField]
    private GameObject _corpse = null;

    public void Corpse()
    {
        SpawnCorpse();

        Destroy(this.gameObject);
    }

    private void SpawnCorpse()
    {
        if (_corpse)
        {
            Instantiate(_corpse, this.transform.position, Quaternion.identity);
        }
    }
}
