using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject[] Objects;
        public int ObjectPoolSize;

        [SerializeField]
        private float _waitForNextMin = 1f;
        [SerializeField]
        private float _waitForNextMax = 10f;
        [SerializeField]
        private float _waitForNextRange = 3f;

        [SerializeField]
        private float _initialCountDown = 5f;
        private float _countDown;

        [SerializeField]
        private float _intendedGameTimeSeconds = 120f;
        private float _gameTime;

        public Sprite[] enemySprites;
        public RuntimeAnimatorController[] enemyAnimations;

        private List<GameObject> _objectsPool;
        private Transform _spawnerTrans;

        private void Awake()
        {
            _countDown = _initialCountDown;
            _gameTime = _intendedGameTimeSeconds;
        }

        // Use this for initialization
        void Start()
        {
            _spawnerTrans = GetComponent<Transform>();
            _objectsPool = new List<GameObject>();
            foreach (var obj in Objects)
            {
                for (int i = 0; i < ObjectPoolSize; i++)
                {
                    GameObject o = Instantiate(obj);
                    o.SetActive(false);
                    _objectsPool.Add(o);
                }
            }
        }

        // Update is called once per frame
        private void Update()
        {
            _gameTime = Mathf.Max(0f, _gameTime - Time.deltaTime);
            _countDown -= Time.deltaTime;
            if (_countDown <= 0f)
            {
                SpawnRandObj(_objectsPool, _spawnerTrans);

                //@TODO: Update range based on time...
                float nextSpawnTime = _waitForNextMin + (_waitForNextMax - _waitForNextMin) * _gameTime / _intendedGameTimeSeconds;

                //Debug.Log("Range: " + nextSpawnTime + " - " + nextSpawnTime + _waitForNextRange);
                _countDown = Random.Range(nextSpawnTime, nextSpawnTime + _waitForNextRange);
            }
        }

        private void SpawnRandObj(List<GameObject> objPool, Transform transform)
        {
            if (objPool != null)
            {
                GameObject gameObj = objPool[Random.Range(0, objPool.Count)];
                if (!gameObj.activeInHierarchy)
                {
                    InstantiateObject(gameObj, transform);
                }
            }
        }

        private void InstantiateObject(GameObject gameObj, Transform transform)
        {
            Vector2 pos;
            pos = new Vector2(transform.position.x, transform.position.y);
            gameObj.transform.position = pos;

            AssignRandomSpriteAnimation(gameObj);

            //TODO: Reset Enemy

            gameObj.SetActive(true);
        }

        private void AssignRandomSpriteAnimation(GameObject gameObj) {
            SpriteRenderer sprRend = gameObj.GetComponent<SpriteRenderer>();
            Animator animator = gameObj.GetComponent<Animator>();

            int getInt = Random.Range(0, enemySprites.Length);

            Sprite randSprite = enemySprites[getInt];
            sprRend.sprite = randSprite;
            RuntimeAnimatorController animationController = enemyAnimations[getInt];
            animator.runtimeAnimatorController = animationController;
        }
    }
}
