using Unity.VisualScripting;
using UnityEngine;

public class ArrowProyectile : MonoBehaviour{
    [SerializeField] private float arrowSpeed = 10f;
    [SerializeField] float arrowLifeTime = 3f;

    PlayerMovement player;
    Rigidbody2D rb;

    float xSpeed;
    float direction = 1f;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Start(){
        player = FindAnyObjectByType<PlayerMovement>();

        // The arrow's speed is based on the player facing
        xSpeed = player.transform.localScale.x * arrowSpeed;
        rb.linearVelocity = new Vector2(xSpeed, 0f);
    }

    public void SetDirection(float dir){
        direction = dir;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;

        rb.linearVelocity = new Vector2(xSpeed, 0f);
        Destroy(gameObject, arrowLifeTime);
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")){
            // Destroy enemy and arrow
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")){
            rb.linearVelocity = Vector2.zero;    
        }    
    }
}
