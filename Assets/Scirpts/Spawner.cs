using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [Header("Normal Coins")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform player;
    [SerializeField] float interval = 1.5f;
    [SerializeField] int maxCoins = 5;
    [SerializeField] float area = 5f;

    [Header("Red & Blue Coins")]
    [SerializeField] GameObject redCoinPrefab;
    [SerializeField] GameObject blueCoinPrefab;

    void Start() => InvokeRepeating(nameof(SpawnCoin), 1f, interval);

    void SpawnCoin()
    {
        if (transform.childCount >= maxCoins) return;

        float x = Random.Range(-area, area);
        float z = Random.Range(-area, area);
        Vector3 pos = new Vector3(x, 0.5f, z);
        if (Vector3.Distance(pos, player.position) < 2f) return;

        Instantiate(coinPrefab, pos, coinPrefab.transform.rotation, transform);
    }

    public void SpawnRedCoin()
    {
        if (redCoinPrefab == null)
        {
            Debug.LogError("Red Coin Prefab is not assigned in the Spawner Inspector!");
            return;
        }

        Vector3 pos = new Vector3(0f, 0.5f, 0f);

        Instantiate(redCoinPrefab, pos, redCoinPrefab.transform.rotation, transform);
        Debug.Log("Red Coin Spawned!");
    }

    public void SpawnBlueCoinsCircle(Transform targetPlayer)
    {
        int totalBlueCoins = 50;
        float radius = 3f;

        for (int i = 0; i < totalBlueCoins; i++)
        {
            float angle = i * (360f / totalBlueCoins);
            float rad = angle * Mathf.Deg2Rad;

            float x = targetPlayer.position.x + Mathf.Cos(rad) * radius;
            float z = targetPlayer.position.z + Mathf.Sin(rad) * radius;
            Vector3 spawnPos = new Vector3(x, 0.5f, z);

            Instantiate(blueCoinPrefab, spawnPos, Quaternion.identity);
        }
    }
}
