using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public GameObject[] Objects;
        public int ObjectPoolSize;
        public float WaitForNextMax;
        public float WaitForNextMin;
        public float CountDown;

        public Sprite[] enemySprites;
        public RuntimeAnimatorController[] enemyAnimations;

        private List<GameObject> _objectsPool;
        private Transform _spawnerTrans;

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
        void Update()
        {
            CountDown -= Time.deltaTime;
            if (CountDown <= 0)
            {
                SpawnRandObj(_objectsPool, _spawnerTrans);
                CountDown = Random.Range(WaitForNextMin, WaitForNextMax);
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
