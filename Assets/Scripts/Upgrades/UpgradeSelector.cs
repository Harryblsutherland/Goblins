using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelector : MonoBehaviour
{

    public static UpgradeSelector current;
    public List<Button> buttons = new List<Button>();
    public Vector3 hiddenPosition;
    public Vector3 visiblePosition;
    public float speed;
    private PlayerSetupDefinition player;
    private List<GameObject> upgradeObject = new List<GameObject>();
    private string upgradeKey;
    private bool active;

    public void Start()
    {
        current = this;
        hiddenPosition = transform.position;
        visiblePosition = transform.position;
        visiblePosition.y += 200; 
    }
    private void Update()
    {
        MoveUpgradeSelector();
    }

    public void DefineButtons(List<GameObject> prUpgrades, string prUpgradeKey, PlayerSetupDefinition prPlayer)
    {
        active = true;
        upgradeKey = prUpgradeKey;
        player = prPlayer;
        for (int i = 0; i < prUpgrades.Count; i++)
        {
            upgradeObject = prUpgrades;
            buttons[i].image.sprite = upgradeObject[i].GetComponent<Upgrade>().upgradeIcon;
            buttons[i].gameObject.SetActive(true);
        }
    }
    public void MoveUpgradeSelector()
    {
        if (!active && transform.position != hiddenPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, hiddenPosition, speed * Time.deltaTime);
        }
        else if (active && transform.position != visiblePosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, visiblePosition, speed * Time.deltaTime);

        }
    }
    public void SelectUpgrade(int prUpgradeNumber)
    {
        active = false;
        player.raceManager.GetUpgradeHandler(upgradeKey).AddUpgradeToUnitType(upgradeObject[prUpgradeNumber]);
        foreach(var button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

}
