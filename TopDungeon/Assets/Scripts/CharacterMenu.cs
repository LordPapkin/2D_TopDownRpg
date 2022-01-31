using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CharacterMenu : MonoBehaviour
{
    //text fields
    public TextMeshProUGUI levelText, hitpointText, goldText, upgradeCostText, xpText;

    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;


    //exp
    int currentLevel;
    int currentLevelXp;


    private Vector3 xpBarScale = Vector3.one;
    //Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--;
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count-1;

            OnSelectionChange();
        }
    }
    private void OnSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.ChangeSprite(currentCharacterSelection);
    }
    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }
    // Update the character Information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if(GameManager.instance.weapon.weaponLevel < 7)
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        else
        {
            upgradeCostText.text = "Maxed!";
        }

        //Meta
        hitpointText.text = GameManager.instance.player.currentHealth.ToString() + "/" + GameManager.instance.player.maxHealth.ToString();
        goldText.text = GameManager.instance.gold.ToString();
        levelText.text = GameManager.instance.level.ToString();

        //xp
        currentLevel = GameManager.instance.level;
        if(currentLevel >= GameManager.instance.xpTable.Count)
        {
            xpText.text = "MAX LEVEL!";
            xpBarScale.x = 1f;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetExpToLevel(currentLevel - 1);
            int nextLevelXp = GameManager.instance.GetExpToLevel(currentLevel);

            int diff = nextLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.exp - prevLevelXp;

            float ratio = (float)currXpIntoLevel / (float)diff;
            xpBarScale.x = ratio;
            xpText.text = currXpIntoLevel.ToString() + "/" + diff.ToString();
        }
        
        xpBar.localScale = xpBarScale;
    }
}
