using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipesGreen;
    public GameObject pipesOrange;
    public float spawnRate = 4;
    public float heightOffset = 2;
    public int numberOfGreenBeforeOrange = 4;

    private float timer = 0;
    private int numberOfPipesSpawned = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver == false && GameControl.instance.gamePaused == false)
        {
            if (timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                SpawnPipe();
                timer = 0;
            }
        }

    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;


        if (numberOfPipesSpawned == numberOfGreenBeforeOrange)
        {
            Instantiate(pipesOrange, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
            numberOfPipesSpawned = 0;
        }
        else
        {
            Instantiate(pipesGreen, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);
        }

        numberOfPipesSpawned += 1;
    }
}
