using UnityEngine;

public enum CoinType { Normal, Red, Blue }
public class Coin : MonoBehaviour
{
    [SerializeField] CoinType coinType = CoinType.Normal;
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;

    void Update() => transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (coinType == CoinType.Red)
            {
                Spawner spawner = FindObjectOfType<Spawner>();
                if (spawner != null)
                {
                    spawner.SpawnBlueCoinsCircle(other.transform);
                }
                GameManager.Instance.ActivateBonusMode();
                Destroy(gameObject);
            }
            else if (coinType == CoinType.Blue)
            {
                GameManager.Instance.AddBonusScore(value);
                Destroy(gameObject);
            }
            else
            {
                GameManager.Instance.AddScore(value);
                Destroy(gameObject);
            }
        }
    }
}
