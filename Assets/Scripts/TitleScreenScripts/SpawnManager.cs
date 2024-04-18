using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    public float cardMovementSpeed;
    public float cardRotationSpeed;

    int lastSpawner = 0;
    int secondLastSpawner = 0;
    private float timeBetweenSpawns;
    float currentTime = 0;

    public Transform standbyTransfrom;

    public List<GameObject> spawnableCards = new List<GameObject>();

    [SerializeField] public List<GameObject> spawners = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawns = 2;
    }

    // Update is called once per frame
    void Update()
{
        currentTime += Time.deltaTime;

        if (currentTime >= timeBetweenSpawns)
        {
            
            currentTime = 0;
            SpawnCard();
        }
    }


    private void SpawnCard()
    {
        int random;
        do
        {
            random = Random.Range(0, spawners.Count);
        }
        while (random == lastSpawner || random == secondLastSpawner);

        secondLastSpawner = lastSpawner;
        lastSpawner = random;

        GameObject spawner = spawners[random];

        random = Random.Range(0, spawnableCards.Count);

        GameObject go = spawnableCards[random];
        spawnableCards.Remove(go);
        go.transform.position = spawner.transform.position;
    }
}
