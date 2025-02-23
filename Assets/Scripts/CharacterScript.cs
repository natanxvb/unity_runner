using TMPro;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float shift;
    [SerializeField] private float jumpForce;
    [SerializeField] private Animator anim;
    [SerializeField] private TMP_Text score;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject itemVFX, shieldVFX, obstacleVFX;
    [SerializeField] private AudioClip itemSFX, shieldSFX, obstacleSFX, destroySFX;
    [SerializeField] private AudioSource sound, music;

    private float roundScore;
    private bool isShield;
    
    private static readonly int Jump = Animator.StringToHash("jump");
    private static readonly int Death = Animator.StringToHash("death");

    private void Update()
    {
        UpdateScore();
        HandleMovement();
        HandleJump();
    }

    private void FixedUpdate()
    {
        MoveForward();
    }

    private void UpdateScore()
    {
        roundScore += Time.deltaTime;
        score.text = $"Score: {roundScore:f1}";
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) && transform.position.x != -shift)
        {
            transform.Translate(-shift, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D) && transform.position.x != shift)
        {
            transform.Translate(shift, 0, 0);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        anim.SetBool(Jump, rb.velocity.y > 0);
    }

    private void MoveForward()
    {
        rb.MovePosition(transform.position + transform.forward * (speed * Time.deltaTime));
    }

    private void DeactivateShield()
    {
        isShield = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (isShield)
            {
                Destroy(other.gameObject);
                PlaySound(destroySFX);
            }
            else
            {
                OnGameOver();
                PlaySound(obstacleSFX);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            CollectMoney(other);
        }
        else if (other.CompareTag("Shield"))
        {
            ActivateShield(other);
        }
    }

    private void CollectMoney(Collider other)
    {
        roundScore += 5;
        score.text = $"Score: {roundScore:f1}";

        InstantiateAndDestroyVFX(itemVFX, other.transform.position, other.transform.rotation);
        PlaySound(itemSFX);
        Destroy(other.gameObject);
    }

    private void ActivateShield(Collider other)
    {
        isShield = true;
        Invoke(nameof(DeactivateShield), 5f);

        var vfx = Instantiate(shieldVFX, transform.position + Vector3.up, other.transform.rotation);
        vfx.transform.SetParent(transform);
        Destroy(vfx, 5f);

        PlaySound(shieldSFX);
        Destroy(other.gameObject);
    }

    private void OnGameOver()
    {
        menu.SetActive(true);
        anim.SetBool(Death, true); // TODO there is no death parameter in animator!!!

        InstantiateAndDestroyVFX(obstacleVFX, transform.position, transform.rotation);
        music.Stop();
        
        // disable the script to prevent further movement
        enabled = false;
    }

    private void PlaySound(AudioClip clip)
    {
        sound.clip = clip;
        sound.Play();
    }

    private void InstantiateAndDestroyVFX(GameObject vfxPrefab, Vector3 position, Quaternion rotation)
    {
        var vfx = Instantiate(vfxPrefab, position, rotation);
        Destroy(vfx, 3f);
    }
}