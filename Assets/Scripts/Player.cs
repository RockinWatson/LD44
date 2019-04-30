using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    static private Player _player = null;
    static public Player Get() { return _player; }

    [SerializeField]
    private GameObject[] _summons = null;
    [SerializeField]
    private float[] _summonCosts = null;

    [SerializeField]
    private float _healthStart = 9000f;
    [SerializeField]
    private float _healthMax = 9000f;
    private float _health = 9000f;
    public float GetHealth() {
        return _health;
    }
    public void TakeDmg() {
        _health = Mathf.Max(0f, _health - 500f);
        PlayDogSFX();

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
    private TextMesh _scoreTextMesh;

    [SerializeField]
    private float _harvestRadius = 3f;

    private RaycastHit2D[] _hits = new RaycastHit2D[10];

    private int _sfxPick;

    [SerializeField]
    private GameObject _deathFX = null;
    private bool _isDead = false;
    public bool IsDead() { return _isDead; }

    private void Awake()
    {
        _player = this;

        _health = _healthStart;

        _score = 0f;
    }

    private void Update()
    {
        if(_isDead) return;
        CheckDeath();
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

    private void PlayDogSFX()
    {
        if (!AudioController.dog1.isPlaying || !AudioController.dog2.isPlaying)
        {
            _sfxPick = Random.Range(1, 3);
            if (_sfxPick == 1)
            {
                AudioController.dog1.Play();
            }
            else { AudioController.dog2.Play(); }
        }
    }

    private void Summon(int index)
    {
        float cost = _summonCosts[index];
        if (cost < _health)
        {
            Instantiate(_summons[index], this.transform.position, Quaternion.identity);

            _health -= cost;
            _score += cost;
            _scoreTextMesh.text = "" + _score;
        } else
        {
            //@TODO: Potential SFX for can't summon.
        }
    }

    private void CheckDeath()
    {
        if(!_isDead && _health <= 0f)
        {
            _isDead = true;

            this.GetComponent<SpriteRenderer>().color = Color.clear;

            Instantiate(_deathFX, this.transform.position, Quaternion.identity);

            PlayerPrefs.SetInt("HighScore", (int)Player.Get().GetScore());

            AudioController.catdie.Play();

            Invoke("LoadGameOver", 5f);
        }
    }

    private void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
