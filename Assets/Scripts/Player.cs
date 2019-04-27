using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    static private Player _player = null;
    static public Player Get() { return _player; }

    [SerializeField]
    private float _healthStart = 100f;
    private float _health = 100f;

    private void Awake()
    {
        _player = this;

        _health = _healthStart;
    }
}
