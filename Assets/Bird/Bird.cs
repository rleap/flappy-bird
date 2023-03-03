using UnityEngine;

public class Bird : MonoBehaviour
{
    public float flapForce = 200f;

    private Rigidbody2D rb2d;
    private bool isDead;
    private Animator wingFlapAnimation;
    public AudioSource hitSound;
    public AudioSource swooshSound;
    public AudioSource dieSound;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        isDead = false;
        wingFlapAnimation = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.gameOver == false && GameControl.instance.gamePaused == false)
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

            if (isDead == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, flapForce));
                    float angle = Vector2.Angle(Vector2.up, rb2d.velocity);

                    if (rb2d.velocity.y < 0)
                    {
                        angle = -angle;
                    }

                    transform.rotation = new Quaternion(0, 0, angle, (float)Space.Self);
                    swooshSound.Play();
                }
            }
            wingFlapAnimation.enabled = true;
        }
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
            wingFlapAnimation.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isDead = true;
        GameControl.instance.BirdDied();
        wingFlapAnimation.enabled = false;
        hitSound.Play();
    }
}
