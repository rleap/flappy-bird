using UnityEngine;

public class Scroll : MonoBehaviour
{

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver == false && GameControl.instance.gamePaused == false)
        {
            transform.position = transform.position + (Vector3.left * GameControl.instance.scrollSpeed) * Time.deltaTime;
        }
    }

}
