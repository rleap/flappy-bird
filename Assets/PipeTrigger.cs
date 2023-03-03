using UnityEngine;

public class PipeTrigger : MonoBehaviour
{
    public int pipePoints = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            GameControl.instance.AddScore(pipePoints);
        }
    }
}
