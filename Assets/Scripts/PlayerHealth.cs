using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 3;

    [Header("Blink Settings")]
    [SerializeField] private Color blinkColor = Color.red;
    [SerializeField] private float blinkDuration = 0.1f;

    private bool isVulnerable = true;
    private int currentHealth;
    private SpriteRenderer spriteRenderer;

    void Start(){
        currentHealth = maxHealth;

        spriteRenderer = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(){
        if (isVulnerable){
            currentHealth--;

            if (currentHealth <= 0){
                Debug.Log("Died");
            }else{
                StartCoroutine(Blink(4));
            }
        }
    }

    private IEnumerator Blink(int numberBlinks){
        isVulnerable = false; // Invulnerable while is blinking

        do{
            spriteRenderer.color = blinkColor;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(blinkDuration);
            numberBlinks--;
        }while (numberBlinks > 0);

        isVulnerable = true;
    }
}
