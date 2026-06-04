using UnityEngine;
using UnityEngine.InputSystem;

public class Moving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck; // Buat objek kosong di kaki player
    public Vector2 boxSize = new Vector2(0.5f, 0.2f); // Ukuran kotak deteksi
    public LayerMask groundLayer; // Pilih layer "Ground" di Inspector

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        // ✅ CEK GROUND (Ganti sistem lama)
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, groundLayer);

        float moveInput = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            moveInput = -1f;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            moveInput = 1f;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // ✅ Animasi (Tetap sama)
        UpdateAnimations(moveInput);

        // Flip karakter
        if (moveInput > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        // Loncat (Sekarang lebih stabil)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void UpdateAnimations(float moveInput)
    {
        if (!isGrounded && rb.linearVelocity.y > 0)
            anim.Play("lompat");
        else if (!isGrounded && rb.linearVelocity.y < 0)
            anim.Play("jatuh");
        else if (moveInput != 0)
            anim.Play("pinkanim");
        else
            anim.Play("idlepink");
    }

    // Tetap gunakan OnCollision untuk musuh
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
            GameManager.instance.LoseLife();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            GameManager.instance.AddScore(10);
            Destroy(col.gameObject);
        }
    }

    // Untuk melihat kotak deteksi di Scene View (biar gampang settingnya)
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(groundCheck.position, boxSize);
        }
    }
}