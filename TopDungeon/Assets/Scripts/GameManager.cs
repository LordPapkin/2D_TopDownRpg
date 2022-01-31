using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int sceneIndex;

    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(this.gameObject);
    }
    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    //logic
    public int gold;
    public int exp;
    public int level = 1;


    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;        
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void NextLevel()
    {        
        SaveState();
        if (SceneManager.sceneCountInBuildSettings > sceneIndex + 1)
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    public void SaveState()
    {
        string save = "";
        //save += player.spirte_number.ToString() + "|";
        save += 0 + "|";
        save += gold.ToString() + "|";
        save += exp.ToString() + "|";
        save += level.ToString() + "|";
        save += player.currentHealth.ToString() + "|";
        save += player.maxHealth.ToString() + "|";
        save += weapon.weaponLevel.ToString(); 
        PlayerPrefs.SetString("Save", save);
        Debug.Log("Save");
    }
    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("Save"))
            return;
        string[] data = PlayerPrefs.GetString("Save").Split('|');

        //player.ChangeSprite(int.Parse(data[0]));
        gold = int.Parse(data[1]);
        exp = int.Parse(data[2]);
        level = int.Parse(data[3]);
        player.currentHealth = int.Parse(data[4]);
        player.maxHealth = int.Parse(data[5]);
        weapon.weaponLevel = int.Parse(data[6]);
        Debug.Log("Load");
    }

    //Floating text;
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        //is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;
        if(gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //exp system
    public int GetCurrentLevel()
    {
        int level = 0;
        int add = 0;

        while(exp >= add)
        {
            add += xpTable[level];
            level++;

            if (level == xpTable.Count) //max level
                return level;
        }

        return level;
    }
    public int GetExpToLevel(int whatLevel)
    {
        int level = 0;
        int xp = 0;

        while(level < whatLevel)
        {            
            xp += xpTable[level];
            level++;
        }

        return xp;
    }

    public void GrantXP(int xpGain)
    {        
        exp += xpGain;
        if (level < GetCurrentLevel())
        {
            player.LevelUp();
            level++;
        }
           
    }
    
}
