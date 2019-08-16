using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class Enemies : MonoBehaviour
    {
        public Dictionary<int, Enemy> EnemyDictionary;
        private int _id;
        public int MaxEnemyCount;

        public void Init()
        {
            InitEnemies();
            _id = 1;
            MaxEnemyCount = 7;
        }

        public void InitEnemies()
        {
            if (EnemyDictionary != null)
            {
                foreach (var enemy in EnemyDictionary.Values)
                {
                    enemy.Die();
                }
            }
            EnemyDictionary= new Dictionary<int, Enemy>();
        }

        public void SpawnEnemy()
        {
            var enemy = Resources.LoadAsync("Enemy");
            enemy.asset.name = "Enemy" + _id;
            var instance = (GameObject)Instantiate(enemy.asset,
                new Vector3(0.0f, 0.0f, 0.0f),
                Quaternion.identity);
            EnemyDictionary.Add(_id,instance.GetComponent<Enemy>());
            instance.GetComponent<Enemy>().Spawn(_id);
            instance.GetComponent<Enemy>().Id = _id;
            instance.GetComponent<Enemy>().Hp = 5;
            instance.GetComponent<Enemy>().AttackPower = 5;
            instance.GetComponent<Enemy>().DefensePower = 5;
            instance.GetComponent<Enemy>().Name = enemy.asset.name;
            _id++;
        }

        public void StartTurn()
        {
            Debug.Log("敵のターン");
            if (EnemyDictionary.Count == 0)
            {
                StartCoroutine(EndTurn(0.5f));
            }
//            if(Random.Range(0,100) < 5 && EnemyDictionary.Values.Count<MaxEnemyCount) SpawnEnemy();
            var count = 1;
            foreach (var enemy in EnemyDictionary.Values)
            {
                StartCoroutine(enemy.Action(() =>
                {
                    if (count == EnemyDictionary.Count)
                    {
                        StartCoroutine(EndTurn(0));
                    }
                    count++;
                }));
            }
        }

        public IEnumerator EndTurn(float lateTime)
        {
            yield return new WaitForSeconds(lateTime);
            GameController.Instance.player.StartTurn();
        }
    }
}