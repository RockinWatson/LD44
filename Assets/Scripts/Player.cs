using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    static private Player _player = null;
    static public Player Get() { return _player; }

    [SerializeField]
    private GameObject[] _summons = null;

    [SerializeField]
    private float _healthStart = 2000f;
    [SerializeField]
    private float _healthMax = 9000f;
    private float _health = 9000f;

    [SerializeField]
    private float _harvestRadius = 3f;

    private RaycastHit2D[] _hits = new RaycastHit2D[10];

    private void Awake()
    {
        _player = this;

        _health = _healthStart;
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            HarvestCorpses();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Summon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Summon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Summon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Summon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Summon(4);
        }
    }

    private void HarvestCorpses()
    {
        //@TODO: Do a circle cast, find Corpses.
        //ContactFilter2D filter = new ContactFilter2D();
        //filter.layerMask 
        int count = Physics2D.CircleCast(this.transform.position, _harvestRadius, Vector2.zero, new ContactFilter2D(), _hits);
        if(count > 0)
        {
            foreach(RaycastHit2D hit in _hits)
            {
                if (hit && hit.transform)
                {
                    Corpse corpse = hit.transform.GetComponent<Corpse>();
                    if (corpse && !corpse.IsHarvested())
                    {
                        float value = corpse.Harvest();
                        _health = Mathf.Min(_health + value, _healthMax);
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        Rect rect = new Rect(Vector2.zero, new Vector2(200f, 50f));
        GUI.TextArea(rect, _health + " / " + _healthMax);
    }

    private void Summon(int index)
    {
        Instantiate(_summons[index], this.transform.position, Quaternion.identity);
    }
}
