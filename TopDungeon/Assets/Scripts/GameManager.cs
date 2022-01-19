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
    public FloatingTextManager floatingTextManager;

    //logic
    public int gold;
    public int exp;    

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
        save += "0" + "|";
        save += gold.ToString() + "|";
        save += exp.ToString() + "|";
        save += "0";
        PlayerPrefs.SetString("Save", save);
        Debug.Log("Save");
    }
    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("Save"))
            return;
        string[] data = PlayerPrefs.GetString("Save").Split('|');

        //Change player skin
        gold = int.Parse(data[1]);
        exp = int.Parse(data[2]);
        //Change the weapon level

        Debug.Log("Load");
    }


    //Floating text;
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
}
