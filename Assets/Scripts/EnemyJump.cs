using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField]
    private float difficulty = 2;
    [SerializeField]
    float goodThrow = 30;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Vector3 newTarget = target.position;
            newTarget = new Vector3 (Random.Range(-2f, 2f) + target.position.x, target.position.y, Random.Range(0f, 2f) + target.position.z);
            float distancediff = newTarget.z - transform.position.z;
            float _random = Random.Range(goodThrow - distancediff * 0.2f, goodThrow + distancediff * 0.2f);
            yield return new WaitForSeconds(Random.Range(0f, 2f) + difficulty);
            GameObject prefab = Instantiate(enemies[Random.Range(0,5)], transform.position, transform.rotation);
            prefab.GetComponent<Rigidbody>().AddForce((newTarget - prefab.transform.position) * _random);
        }
    }
}
