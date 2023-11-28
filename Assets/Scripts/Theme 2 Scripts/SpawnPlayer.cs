using System.Collections;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] Transform player1Prefab;
    [SerializeField] Transform player2Prefab;
    [SerializeField] Transform player1Spawn;
    [SerializeField] Transform player2Spawn;

    [SerializeField] private int spawnDelay = 2;
    public static SpawnPlayer sp;

    private void Start()
    {
        if (sp == null)
        {
            sp = GameObject.FindGameObjectWithTag("SP").GetComponent<SpawnPlayer>();
        }
        StartCoroutine(PlayerSpawn());
        return;
    }

    IEnumerator PlayerSpawn()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(player1Prefab, player1Spawn.position, player1Spawn.rotation);
        Instantiate(player2Prefab, player2Spawn.position, player2Spawn.rotation);
    }

    public IEnumerator RespawnPlayer1()
    {
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(player1Prefab, player1Spawn.position, player1Spawn.rotation);
    }

    public IEnumerator RespawnPlayer2()
    {
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(player2Prefab, player2Spawn.position, player2Spawn.rotation);
    }

    public static void DestroyPlayer1(PlayerController player)
    {
        Destroy(player.gameObject);
        sp.StartCoroutine(sp.RespawnPlayer1());
    }

    public static void DestroyPlayer2(PlayerController2 player2)
    {
        Destroy(player2.gameObject);
        sp.StartCoroutine(sp.RespawnPlayer2());
    }
}