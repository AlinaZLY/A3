using UnityEngine;
using System.Collections;

public class GoldCoin : MonoBehaviour
{
    public int value = 10;
    public float flySpeed = 5f;
    private Transform player;

    void Start()
    {
        player = GameObject.Find("MainTower").transform;
        StartCoroutine(FlyToPlayer());
    }

    IEnumerator FlyToPlayer()
    {
        yield return new WaitForSeconds(2f);
        while (Vector3.Distance(transform.position, player.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.position,
                flySpeed * Time.deltaTime
            );
            yield return null;
        }

        if (GoldSystem.Instance != null)
        {
            GoldSystem.Instance.AddGold(value);
        }
        else
        {
            Debug.LogError("GoldSystem µ¿˝Œ¥’“µΩ£°");
        }
        Destroy(gameObject);
    }
    public ParticleSystem collectEffect;

    void OnDestroy()
    {
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }
    }
}