using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTargeting : MonoBehaviour
{
    public PlayerHealth playerhealth;

    private Transform playerTarget;
    private NavMeshAgent agent;
    private bool searchingForPlayer = false;

    System.Random rnd = new System.Random();

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (playerTarget == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchForPlayer());
            }
        }
    }

    void Update()
    {
        agent.SetDestination(playerTarget.position);
    }

    IEnumerator SearchForPlayer()
    {
        GameObject searchResult = GameObject.FindGameObjectWithTag("Player");
        if (searchResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            playerTarget = searchResult.transform;
            playerhealth = searchResult.GetComponent<PlayerHealth>();
            searchingForPlayer = false;
            yield return false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerhealth.TakeDamage(rnd.Next(5, 15));
        }
    }
}