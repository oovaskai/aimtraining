using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100;
    float currentHealth;
    public float headShotMultiplier = 100;
    public GameObject healthBarPrefab;

    HealthBar healthBar;

    public float respawnTime;
    float respawnTimer;

    public bool dead;
    bool respawning;

    public GameObject hitEffect;
    MoveTarget rail;
    HingeJoint hinge;
    Quaternion targetRotation;

    SoundManager sound;

    void Start()
    {
        currentHealth = health;
        dead = false;
        rail = transform.parent.GetComponent<MoveTarget>();
        hinge = GetComponent<HingeJoint>();
        targetRotation = transform.localRotation;
        sound = GetComponent<SoundManager>();

        healthBar = Instantiate(healthBarPrefab, FindObjectOfType<Canvas>().transform).GetComponent<HealthBar>();
        healthBar.target = transform; 
    }

    void Update()
    {
        if (dead || respawning)
        {
            if (respawnTime > 0 && dead)
            {
                respawnTimer -= Time.deltaTime;

                if (respawnTimer <= 0)
                {
                    Respawn();
                }
            }

            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * 7);

            if (respawning)
            {
                float margin = 0.1f;
                if ((transform.localRotation.x > targetRotation.x - margin && transform.localRotation.x < targetRotation.x + margin) &&
                    (transform.localRotation.y > targetRotation.y - margin && transform.localRotation.y < targetRotation.y + margin) &&
                    (transform.localRotation.z > targetRotation.z - margin && transform.localRotation.z < targetRotation.z + margin))
                {
                    respawning = false;
                    foreach (MeshCollider collider in GetComponentsInChildren<MeshCollider>())
                    {
                        collider.isTrigger = false;
                    }
                    if (rail != null)
                    {
                        rail.moving = true;
                    }
                    hinge.useSpring = true;
                }
            }
        } 
    }

    public void Hit(float damage, bool headshot)
    {
        if (!dead)
        {
            float totalDamage = damage;
            if (headshot)
                totalDamage *= headShotMultiplier;

            currentHealth -= totalDamage;
            healthBar.ShowHealth(currentHealth/health);

            if (currentHealth <= 0)
            {
                Die();
                PlayerStats.kills++;

                if (headshot)
                {
                    Color red = new Color(255, 136, 121, 255);
                    healthBar.KillAnimation("Headshot +" + PlayerStats.AddPoints(2), new Color(0.93f, 0.62f, 0.58f));
                    PlayerStats.headshots++;
                }
                    
                else
                    healthBar.KillAnimation("Kill +" + PlayerStats.AddPoints(1), new Color(0.62f, 0.93f, 0.58f));
            }

            PlayerStats.hits++;
        }
    }

    void Die()
    {
        if (rail != null)
        {
            rail.moving = false;
        }
        dead = true;
        respawnTimer = respawnTime;

        foreach (MeshCollider collider in GetComponentsInChildren<MeshCollider>())
        {
            collider.isTrigger = true;
        }

        hinge.useSpring = false;
        targetRotation *= Quaternion.Euler(90, 0f, 0f);
    }

    public void Respawn()
    {
        respawning = true;
        dead = false;
        currentHealth = health;
        targetRotation *= Quaternion.Euler(-90, 0f, 0f);
        sound.PlaySound(0);
    }
}
