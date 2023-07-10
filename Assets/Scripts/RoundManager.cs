using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private GameObject Zombie;
    [SerializeField] private GameObject BuffedZombie;
    [SerializeField] private GameObject ArcherZombie;
    [SerializeField] private float spawnRadius = 18f;

    private int currentRound = 1;

    private void Update()
    {
        if(currentRound == 1)
        {
            StartCoroutine(SpawnRound1());
            currentRound++;
        }
    }
    public IEnumerator SpawnRound1()
    {
        yield return new WaitForSeconds(3f);
        Vector2 spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(0.7f);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(1.2f);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(BuffedZombie, spawnPos, Quaternion.identity);
        spawnPos = FindObjectOfType<TopDownController>().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(Zombie, spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(0f);
    }
}
