using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    float damagePerHit = 5;
    public Slider healthSlider;

    // Start is called before the first frame update
    private void OnEnable()
    {
        MyEventSystem.damagedWall += DoDamage;
    }

    private void OnDisable()
    {
        MyEventSystem.damagedWall -= DoDamage;
    }
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        healthSlider.value = health / maxHealth;
    }
    void DoDamage(int i)
    {
        health -= damagePerHit;
    }


}
