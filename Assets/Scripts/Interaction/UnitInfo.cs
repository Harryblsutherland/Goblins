using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : Interaction
{

    public string Name;
    public float maxHealth;
    public float currentHealth;
    public float armour;
    public float healthRegen;
    public List<string> upgradeKeys = new List<string>();
    bool show = false;
    public Sprite portraitImage;
    public string unitType;
    public Image healthBar;
    public PlayerSetupDefinition player;

    private float currentHealthRegen;

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }

        set
        {
            if (maxHealth < value)
            {
                currentHealth += value - maxHealth;
                
            }
            maxHealth = value;
            updateHealthBar();
        }
    }

    private void awake()
    {
        player = GetComponent<Player>().Info;
    }

    private void Start()
    {
        healthBar = transform.Find("Canvas/HealthBackGround/Health").GetComponent<Image>();
        updateHealthBar();
    }
    public override void Select()
    {
        show = true;

    }
    public void RecieveDamage(DamageObject prAttack)
    {
        currentHealth -= prAttack.damage - armour;
        updateHealthBar();

    }
    void Update()
    {
        RegenHealth();
        if (!show)
        {
            return;
        }
        if (GetComponent<ProductionManager>() != null)
        {
            GetComponent<ProductionManager>().UpdateProductionQueue();
        }
        InfoManager.Current.SetImage(portraitImage);
        InfoManager.Current.SetLines(
                                    Name,
                                    MaxHealth + " / " + Mathf.RoundToInt(currentHealth),
                                    GetComponent<Player>().Info.Name
                                    );
    }

    private void RegenHealth()
    {
        if(currentHealth == MaxHealth && healthRegen > 0)
        {
            return;
        }
        currentHealthRegen += (healthRegen * Time.deltaTime);
        if (currentHealthRegen > 1 || currentHealthRegen < -1)
        {
            currentHealth += currentHealthRegen;
            currentHealthRegen = 0;
            updateHealthBar();
            if (currentHealth > MaxHealth)
            {
                currentHealth = MaxHealth;
            }
        }
    }

    public override void Deselect()
    {
        show = false;
        InfoManager.Current.Clearbox();
    }
    private void updateHealthBar()
    {
        healthBar.fillAmount = (currentHealth / MaxHealth);
        healthBar.color = Utilities.ThreeColourLerp(Color.red, Color.yellow, Color.green, currentHealth / MaxHealth);
    }
}
