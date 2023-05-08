using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController : MonoBehaviour
{
    // ========= MOVEMENT =================
    public float speed = 4;

    // ======== HEALTH ==========
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public Transform respawnPosition;
    public ParticleSystem hitParticle;

    // ======== PROJECTILE ==========
    public GameObject projectilePrefab;

    // ======== AUDIO ==========
    public AudioClip hitSound;
    public AudioClip shootingSound;

    // ======== HEALTH ==========
    public int health => currentHealth;

    // =========== MOVEMENT ==============
    private Rigidbody2D rigidbody2d;
    private Vector2 currentInput;

    // ======== HEALTH ==========
    private int currentHealth;
    private float invincibleTimer;
    private bool isInvincible;

    // ==== ANIMATION =====
    private Animator animator;
    private Vector2 lookDirection = new(1, 0);

    // ================= SOUNDS =======================
    private AudioSource audioSource;


    private void Start()
    {
        // =========== MOVEMENT ==============
        rigidbody2d = GetComponent<Rigidbody2D>();

        // ======== HEALTH ==========
        invincibleTimer = -1.0f;
        currentHealth = maxHealth;

        // ==== ANIMATION =====
        animator = GetComponent<Animator>();

        // ==== AUDIO =====
        audioSource = GetComponent<AudioSource>();

        // Get the Gamepad
    }

    private void Update()
    {
        // ================= HEALTH ====================
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        // ============== MOVEMENT ======================
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        currentInput = move;


        // ============== ANIMATION =======================

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // ============== PROJECTILE ======================

        var fireKey = Input.GetKeyDown(KeyCode.C) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1") ||
                      Input.GetButton("Fire1");
        if (fireKey)
        {
            LaunchProjectile();
            MakeXboxControllerVibrateForSeconds(0.1f);
        }

        // ======== DIALOGUE ==========
        if (Input.GetKeyDown(KeyCode.X))
        {
            var hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f,
                1 << LayerMask.NameToLayer("NPC"));
            if (hit.collider != null)
            {
                var character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null) character.DisplayDialog();
            }
        }
    }

    private void MakeXboxControllerVibrateForSeconds(float seconds)
    {
        StartCoroutine(XboxControllerVibration(seconds));
    }

    private static IEnumerator XboxControllerVibration(float seconds)
    {
        Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);
        yield return new WaitForSeconds(seconds);
        Gamepad.current.SetMotorSpeeds(0, 0);
    }

    private void FixedUpdate()
    {
        var position = rigidbody2d.position;

        position = position + currentInput * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    // ===================== HEALTH ==================
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
            audioSource.PlayOneShot(hitSound);

            Instantiate(hitParticle, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (currentHealth == 0)
            Respawn();

        UIHealthBar.Instance.SetValue(currentHealth / (float)maxHealth);
    }

    private void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition.position;
    }

    // =============== PROJECTICLE ========================
    private void LaunchProjectile()
    {
        var projectileObject =
            Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        var projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        audioSource.PlayOneShot(shootingSound);
    }

    // =============== SOUND ==========================

    //Allow to play a sound on the player sound source. used by Collectible
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    #region COLLISION

    private void OnCollisionEnter2D(Collision2D other)
    {
        // If the "other" object is the worldLimiter, which is a Polygon Collider 2D, prevent the player from going through it
        if (other.gameObject.layer == LayerMask.NameToLayer("WorldLimiter"))
        {
            var contact = other.GetContact(0);
            var normal = contact.normal;
            var dot = Vector2.Dot(normal, Vector2.up);
            if (dot > 0.5f)
                return;

            var position = rigidbody2d.position;
            position += contact.normal * contact.separation;
            rigidbody2d.MovePosition(position);
        }
    }

    #endregion
}