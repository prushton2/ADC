using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json.Linq;

public class HealthPool : MonoBehaviour
{
    public int maxHealth = 200;
    public int minHealth = 0;
    public int health = 200;
    public bool isInvincible = false;
    public bool isDead = false;

    public GameObject healthBar;

    public float HealthBarMaxWidth;

    public GameObject youdiedtext;
    public Player player;


    public void updateIsDead() {
        isDead = health <= minHealth;

        healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(getPercentage() * HealthBarMaxWidth, healthBar.GetComponent<RectTransform>().sizeDelta.y);

        if(isDead) {
            player.movementLocked = true;
            youdiedtext.SetActive(true);
        }
    }

    public void dealDamage(int damage) {
        if(isInvincible) {
            return;
        }

        Debug.Log(damage);
        
        health = Math.Clamp(health - damage, minHealth, maxHealth);
        updateIsDead();
    }

    public void heal(int healing) {
        health = Math.Clamp(health + healing, minHealth, maxHealth);
        updateIsDead();
    }

    public float getPercentage() {
        return (float)health / (float)maxHealth;
    }

    public void kill() {
        Debug.Log("kill");
        health = minHealth;
        updateIsDead();
    }
}
