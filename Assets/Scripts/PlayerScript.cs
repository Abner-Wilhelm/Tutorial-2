using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public GameObject Player;

    public float speed;

    public Text score;

    public Text lives;

    public GameObject lose;

    public GameObject win;

    private int scoreValue = 0;

    private int livesValue = 3;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;

    private bool facingRight = true;

    private float hozMovement;

    private float vertMovement;

    Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        lose.SetActive(false);
        win.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hozMovement = Input.GetAxis("Horizontal");
        vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if(rd2d.velocity.y != 0){
             anim.SetInteger("State", 2);
        }
        else if(rd2d.velocity.x != 0){
             anim.SetInteger("State", 1);
        }
        else {
            anim.SetInteger("State", 0);
        }
    }

    void Update(){

        if (facingRight == false && hozMovement > 0)
    {
        Flip();
    }
        else if (facingRight == true && hozMovement < 0)
    {
        Flip();
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            Dead();
        }

    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Coin"))
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(other.gameObject);
            Teleport();
        }



    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
    private void Teleport()
    {
        if(scoreValue==4){
        gameObject.transform.position = new Vector2(80f, 2f);
        livesValue = 3;
         lives.text = "Lives: " + livesValue.ToString();}

         else if(scoreValue==8){
        gameObject.transform.position = new Vector2(35f, 70f);
        musicSource.clip = musicClipTwo;
        musicSource.Play();
        win.SetActive(true);
            
         }


}
    private void Dead()
    {
        if(livesValue==0){
        Destroy(Player);
        lose.SetActive(true);
        }
}
}