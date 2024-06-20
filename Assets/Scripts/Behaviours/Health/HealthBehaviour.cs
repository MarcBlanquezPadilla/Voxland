using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class HealthBehaviour : MonoBehaviour {

    [Header("Properties")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool inmortal = false;

    [Header("Events")]
    [SerializeField] private UnityEvent<float> onHurt;
    [SerializeField] private UnityEvent onDie;
    [SerializeField] private UnityEvent<float> onChangeHp;

    [Header("Fx | Audio")]
    [SerializeField] private GameObject fx;
    [SerializeField] private string audioName;

    public void Awake() {

        health = maxHealth;
        onChangeHp.Invoke(ReturnHealthPercent());
    }

    public void Hurt(float damage) {

        if (!inmortal) {

            health -= damage;
            onHurt.Invoke(health);

            if (health <= 0) {
                health = 0;
                onDie.Invoke();
            }

            PlayFX();
            onChangeHp.Invoke(ReturnHealthPercent());
        }
    }

    public void AddHealt(int addHealth) {

        health += addHealth;

        if (health > maxHealth) 
            health = maxHealth;

        onChangeHp.Invoke(ReturnHealthPercent());
    }

    public float ReturnHealth() {

        return health;
    }

    public float ReturnMaxHealth() {

        return maxHealth;
    }

    public float ReturnHealthPercent() {

        return health / maxHealth;
    }

    public bool SetInmortal() { 

        return inmortal = true;
    }

    public bool SetMortal() {

        return inmortal = false;
    }

    public void SetMaxHealth(float newHealth) {

        maxHealth = newHealth;
    }

    private void PlayFX() {

        if (fx != null)
            Instantiate(fx, transform.position, Quaternion.identity);

        if (audioName != "")
            AudioManager.Instance.playAudio(audioName);
    }
}
