using System.Collections;
using UnityEngine;

public class PlayerDamageFeedback : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    
    [Header("Feedback")]
    public float blinkTime = 0.15f;
    public int blinkCount = 3;
    public float knockbackForce = 6f;

    public void OnDamaged(Vector2 hitDirection)
    {
        StopAllCoroutines();
        StartCoroutine(DamageRoutine(hitDirection));
    }

    IEnumerator DamageRoutine(Vector2 hitDirection)
    {
        // Bloqueia movimento por um tempinho
        PlayerController controller = GetComponent<PlayerController>();
        if (controller != null)
            controller.canMove = false;
        
        // Knockback direto
        rb.linearVelocity = hitDirection.normalized * knockbackForce;

        // Blink
        Color originalColor = spriteRenderer.color;

        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.color = new Color(1f, 0.6f, 0.6f, 1f); // levemente avermelhado
            yield return new WaitForSeconds(blinkTime);

            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkTime);
        }
        
        // Libera movimento
        if (controller != null)
            controller.canMove = true;
    }
}