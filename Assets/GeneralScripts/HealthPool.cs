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

    public void updateIsDead() {
        isDead = health <= minHealth;
    }

    public void dealDamage(int damage) {
        if(isInvincible) {
            return;
        }
        
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

    public JObject getData() {
        return JObject.FromObject(new {
            maxHealth = (int)this.maxHealth,
            minHealth = (int)this.minHealth,
            health = (int)this.health,
            isDead = (bool)this.isDead
        });
    }

    public void setData(JObject health) {
        this.maxHealth = (int)health["maxHealth"];
        this.minHealth = (int)health["minHealth"];
        this.health = (int)health["health"];
        this.isDead = (bool)health["isDead"];
    }
}
