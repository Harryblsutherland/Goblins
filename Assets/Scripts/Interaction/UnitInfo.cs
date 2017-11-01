using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfo : Interaction
{

    public string Name;
    public float maxHealth, currentHealth;
    bool show = false;
    public Sprite portraitImage;
    public string unitType;
    public Image healthBar;
    public PlayerSetupDefinition player;

    private void awake()
    {
        player = GetComponent<Player>().Info;
    }

    private void Start()
    {
        healthBar = transform.Find("Canvas/HealthBackGround/Health").GetComponent<Image>();
        healthBar.color = Utilities.ThreeColourLerp(Color.red, Color.yellow, Color.green, currentHealth / maxHealth);
    }
    public override void Select()
    {
        show = true;

    }
    public void RecieveDamage(DamageObject prAttack)
    {
        currentHealth -= prAttack.damage;
        healthBar.fillAmount = (currentHealth / maxHealth);
        healthBar.color = Utilities.ThreeColourLerp(Color.red, Color.yellow, Color.green, currentHealth / maxHealth);
    }
    void Update()
    {
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
                                    maxHealth + " / " + currentHealth,
                                    GetComponent<Player>().Info.Name
                                    );
    }
    public override void Deselect()
    {
        show = false;
        InfoManager.Current.Clearbox();
    }

}
