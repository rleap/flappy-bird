using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{

    public float destroyPipeZone = -6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver == false && GameControl.instance.gamePaused == false)
        {
            transform.position = transform.position + (Vector3.left * GameControl.instance.scrollSpeed) * Time.deltaTime;

            if (transform.position.x < destroyPipeZone)
            {
                Debug.Log("Pipe Deleted");
                Destroy(gameObject);
            }
        }

    }
}
