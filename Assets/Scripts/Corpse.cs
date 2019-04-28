using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour {

    [SerializeField]
    private GameObject _harvestFX = null;

    [SerializeField]
    private float _harvestValue = 30f;

    private bool _isHarvested = false;
    public bool IsHarvested() { return _isHarvested; }
	
	// Update is called once per frame
	void Update () {
		
	}

    public float Harvest()
    {
        _isHarvested = true;

        Instantiate(_harvestFX, this.transform);

        return _harvestValue;
    }
}
