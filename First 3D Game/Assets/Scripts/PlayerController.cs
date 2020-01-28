using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody rig;
    private AudioSource audioSource;

    void Awake()
    {
        //get rigidbody compnent
        rig = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 1.0f;

    }

    void Update ()
    {
        if (GameManager.instance.paused)
            return;
        Move();
    }

    void Move()
    {
        
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed;
        dir.y = rig.velocity.y;
        /*
        float jump = Input.GetAxis("Jump");

        
        
        if ((jump > 0) && (rig.position.y <= .5))
        {
            dir.y = 40;
        }
        else if (rig.position.y > .4)
        {
            dir.y = -2;
        }
        else
        {
            dir.y = 0;
        }dww
        */
        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
        rig.velocity = dir;

        Vector3 facingDir = new Vector3(xInput, 0, zInput);
        if (facingDir.magnitude > 0)
        {
            transform.forward = facingDir;
        }
        
    }

    void TryJump ()
    {
        Ray ray1 = new Ray(transform.position + new Vector3(-.5f, 0, .5f), Vector3.down);
        Ray ray2 = new Ray(transform.position + new Vector3(-.5f, 0, -.5f), Vector3.down);
        Ray ray3 = new Ray(transform.position + new Vector3(.5f, 0, .5f), Vector3.down);
        Ray ray4 = new Ray(transform.position + new Vector3(.5f, 0, -.5f), Vector3.down);

        if ((Physics.Raycast(ray1, .7f)) || (Physics.Raycast(ray2, .7f)) || (Physics.Raycast(ray3, .7f)) || (Physics.Raycast(ray4, .7f)))
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy") )
        {
            GameManager.instance.score = GameManager.instance.startScore;
            GameManager.instance.lives--;
            
            if (GameManager.instance.lives < 0)
                GameManager.instance.GameOver();
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(1);
            Destroy(other.gameObject);
            audioSource.Play();            
        }
        else if (other.CompareTag("CoinBlue"))
        {
            GameManager.instance.AddScore(4);
            Destroy(other.gameObject);
            audioSource.Play();
        }
        else if (other.CompareTag("Lava"))
        {
            GameManager.instance.score = GameManager.instance.startScore;
            GameManager.instance.lives--;
            if (GameManager.instance.lives < 0)
                GameManager.instance.GameOver();
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (other.CompareTag("Goal"))
        {
            GameManager.instance.LevelEnd();
        }
         
    }
}
