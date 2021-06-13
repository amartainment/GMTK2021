using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public float damagePerHit = 5;
    public Slider healthSlider;
    public GameObject loseScreen;

    // Start is called before the first frame update
    private void OnEnable()
    {
        MyEventSystem.damagedWall += DoDamage;
        MyEventSystem.laserCreated += fake;
        MyEventSystem.fusion += fake;
    }

    private void OnDisable()
    {
        MyEventSystem.damagedWall -= DoDamage;
        MyEventSystem.laserCreated -= fake;
        MyEventSystem.fusion -= fake;
    }
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        Death();
    }

    void Death()
    {
        if(health <= 0)
        {
            loseScreen.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    void UpdateHealth()
    {
        healthSlider.value = health / maxHealth;

        if (health <0)
        {
            Debug.Log("Gameover");
        }
    }
    void DoDamage(int i)
    {
        health -= damagePerHit;
    }
    
    void fake(int i)
    {

    }


}
