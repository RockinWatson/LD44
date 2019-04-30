using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    static private Player _player = null;
    static public Player Get() { return _player; }

    [SerializeField]
    private GameObject[] _summons = null;

    [SerializeField]
    private float _healthStart = 9000f;
    [SerializeField]
    private float _healthMax = 9000f;
    private float _health = 9000f;
    public float GetHealth() {
        return _health;
    }
    public void TakeDmg() {
        _health -= 1500f;
    }
    public void AddHealth(float value)
    {
        _health = Mathf.Min(_health + value, _healthMax);
    }
    private float _score = 0f;
    public float GetScore() {
        return _score;
    }

    [SerializeField]
    private TextMesh _healthText;

    [SerializeField]
    private float _elementZeroCost;
    [SerializeField]
    private float _elementOneCost;
    [SerializeField]
    private float _elementTwoCost;
    [SerializeField]
    private float _elementThreeCost;
    [SerializeField]
    private float _elementFourCost;

    [SerializeField]
    private TextMesh _scoreTextMesh;

    [SerializeField]
    private float _harvestRadius = 3f;

    private RaycastHit2D[] _hits = new RaycastHit2D[10];

    private void Awake()
    {
        _player = this;

        _health = _healthStart;

        _score = 0f;
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
        for(int i = 0; i < 5; ++i)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                Summon(i);
            }
        }

        _healthText.text = _health.ToString();
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
                        corpse.Harvest();
                    }
                }
            }
        }
    }

    //private void OnGUI()
    //{
    //    Rect rect = new Rect(Vector2.zero, new Vector2(200f, 50f));
    //    GUI.TextArea(rect, _health + " / " + _healthMax);
    //}

    private void Summon(int index)
    {
        Instantiate(_summons[index], this.transform.position, Quaternion.identity);
        CalculateLifeForSummons(index);
    }

    private void CalculateLifeForSummons(int index) {
        switch (index)
        {
            case 0:
                _health -= _elementZeroCost;
                _score += _elementZeroCost;
                break;
            case 1:
                _health -= _elementOneCost;
                _score += _elementOneCost;
                break;
            case 2:
                _health -= _elementTwoCost;
                _score += _elementTwoCost;
                break;
            case 3:
                _health -= _elementThreeCost;
                _score += _elementThreeCost;
                break;
            case 4:
                _health -= _elementFourCost;
                _score += _elementFourCost;
                break;
            default:
                throw new System.Exception("Player Summon Index out of Range!!!!!!");
        }
        _scoreTextMesh.text = "" + _score;
    }
}
