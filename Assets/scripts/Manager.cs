using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Tiled2Unity;

delegate IEnumerator State();
public class Manager : MonoBehaviour
{

    //Timers
    float levelTimer;
    float SpawnTimer;

    //Game Variables 
    State current;
    public static int Wastage;
    static bool ControlsDisabled;
    int level = 0;
    // float levelTime;
    int WastageThreshold;

    //GameObjects for Gameplay
    GameObject player = null;
    public GameObject PlayerPrefab;
    public GameObject[] levels;
    GameObject Currentlevel;
    electronic[] electronics;

    //Gameobjects for ui 
    public Animator Fader;
    public Text PassOrFail, Countdown;
    public Text Timer;
    public GameObject GameOverUi;
    public GameObject InGameUI;
    public Image WastageBar;

    IEnumerator Start()
    {
        Fader = GameObject.Find("Fader").GetComponent<Animator>();
        Fader.SetBool("fading", true);
        PassOrFail = GameObject.Find("PassOrFail").GetComponent<Text>();
        GameOverUi = GameObject.Find("GameOverUi");
        WastageBar = GameObject.Find("Wastage Bar").GetComponent<Image>();
        Countdown = GameObject.Find("Countdown").GetComponent<Text>();
        Timer = GameObject.Find("Time").GetComponent<Text>();
        InGameUI.SetActive(false);
        GameOverUi.SetActive(false);
        current = LoadLevel;

        while (true)
        {
            yield return StartCoroutine(current());
        }
    }

    void InitMap()
    {
        //get level and load curent level 
        Currentlevel = Instantiate(levels[level], Vector3.zero, Quaternion.identity) as GameObject;
        TiledMap map = Currentlevel.GetComponent<TiledMap>();
        Currentlevel.transform.position = new Vector3(-((map.NumTilesWide / 2) * map.TileWidth * map.ExportScale), (map.NumTilesHigh / 2) * map.TileHeight * map.ExportScale, 0);
        electronics = Currentlevel.GetComponentsInChildren<electronic>();
    }

    IEnumerator LoadLevel()
    {
        InitMap();
        Debug.Log("loading level");
        if (player == null)
            player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        else
            player.transform.position = Vector3.zero;
        WastageThreshold = 500;
        Fader.SetBool("fading", false);
        yield return new WaitForSeconds(1.0f);
        Countdown.text = "3";
        Countdown.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Countdown.text = "2";
        yield return new WaitForSeconds(0.5f);
        Countdown.text = "1";
        yield return new WaitForSeconds(0.5f);
        Countdown.enabled = false;
        current = GameRunning;
    }

    IEnumerator GameRunning()
    {
        InGameUI.SetActive(true);
        Debug.Log("game running");
        levelTimer = 60.0f;
        SpawnTimer = 0.0f;
        while (current == GameRunning)
        {
            levelTimer -= Time.deltaTime;
            SpawnTimer += Time.deltaTime;
            if (SpawnTimer >= 2.0f)
            {
                SpawnSingle();
                SpawnTimer = 0.0f;
            }
            if (levelTimer <= 0.0f || Wastage > WastageThreshold) 
            {
                current = GameOver;
                break;
            }
            WastageBar.rectTransform.sizeDelta = new Vector2((135 * Wastage / WastageThreshold), 20);
            Timer.text = "Time :   " + ((int)levelTimer);
            yield return null;
        }
        InGameUI.SetActive(false);
    }

    IEnumerator GameOver()
    {
        if (Wastage > WastageThreshold)
            PassOrFail.text = "You FAIL";
        else
            PassOrFail.text = "Level Passed !";
        GameOverUi.SetActive(true);
        PassOrFail.enabled = true;
        while(current == GameOver)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                current = LoadLevel;
                Wastage = 0 ;
                break;
            }
            yield return null ;
        }
        GameOverUi.SetActive(false);
    }

    void SpawnSingle()
    {
        int rand = Random.Range(0, electronics.Length);
        electronics[rand].ToggleOn(true);
    }

}
