using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using System;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    private bool isDeath = false;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private Text healtText;
    private Dictionary<string, int> enemyMap;
    private DateTime lastTakenDamage = new();
    private void Start()
    {
        healtText.text = "Healt: " + StaticClass.Healt.ToString();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        enemyMap = new Dictionary<string, int>();
        enemyMap.Add("Trap1", 10);
        enemyMap.Add("Trap2", 15);
        enemyMap.Add("Enemy1", 15);
        enemyMap.Add("Enemy2", 25);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("Trap") 
            || collision.gameObject.tag.StartsWith("Enemy"))
        {
            if (StaticClass.Healt > 0 
                && DateTime.Now - lastTakenDamage >= TimeSpan.FromSeconds(5))
                TakeDamage(enemyMap[collision.gameObject.tag]);
        }
    }

    private void TakeDamage(int damageTaken)
    {
        lastTakenDamage = DateTime.Now;
        StaticClass.Healt -= damageTaken;
        healtText.text = "Healt: " + StaticClass.Healt.ToString();
        if (StaticClass.Healt <= 0)
        {
            if (!isDeath)
                DeathLogic();
            isDeath = true;
        }
    }

    private void DeathLogic()
    {
        // Run animation death
        animator.SetTrigger("IsDeath");
        // Run sound death
        deathSound.Play();
        // Logic death
        rb.bodyType = RigidbodyType2D.Static;
        StaticClass.Healt = StaticClass.DefaultHealt;
        ResetScene();
    }

    private async void ResetScene()
    {
        //SceneManager.LoadScene("SampleScene");
        await Task.Delay(1000);
        StaticClass.Score = StaticClass.PreviousScore;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
