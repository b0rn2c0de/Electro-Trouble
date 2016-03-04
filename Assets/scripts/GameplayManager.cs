/*using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameplayManager {

    public static GameplayManager instance;

    public Manager parent;
    public GameObject player = null;
    public electronic[] electronics;
    public static int Wastage;
    int WastageThreshold;

    float levelTimer;
    float SpawnTimer;

    GameObject Currentlevel;
    public GameObject InGameUI;
    public Text Timer;
    public Image WastageBar;

    private GameplayManager() { }

    public static GameplayManager GetInstance(Manager parent)
    {
        instance = new GameplayManager();
        instance.parent = parent;
        instance.player=parent.player;
        instance.WastageThreshold = parent.was;
        instance.Currentlevel = currentlev;
        instance.electronics = instance.Currentlevel.GetComponentsInChildren<electronic>();
        instance.InGameUI = GameObject.Find("In game Ui");
        instance.WastageBar = GameObject.Find("Wastage Bar").GetComponent<Image>();
        instance.Timer = GameObject.Find("Time").GetComponent<Text>();
        Wastage = 0;
        return instance;
    }

    public IEnumerator run()
    {
        //InGameUI.SetActive(true);
        Debug.Log("game running");
        levelTimer = 60.0f;
        SpawnTimer = 0.0f;
        while (true)
        {
            levelTimer -= Time.deltaTime;
            SpawnTimer += Time.deltaTime;
            if (SpawnTimer >= 2.0f)
            {
                SpawnSingle();
                SpawnTimer = 0.0f;
            }
            if (levelTimer <= 0.0f)
            {
                //switch to level end 
                break;
            }
            WastageBar.rectTransform.sizeDelta = new Vector2((135 * Wastage / WastageThreshold), 20);
            Timer.text = "Time :   " + ((int)levelTimer);
            yield return null;
        }
    }
    void SpawnSingle()
    {
        int rand = Random.Range(0, electronics.Length);
        electronics[rand].ToggleOn(true);
    }
}
*/