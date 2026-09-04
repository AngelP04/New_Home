using Cultive;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }
    //Informacion de escena
    public SceneInfo SceneInfo;
    //Player
    private PlayerMovement player;
    private HealthManager playerHealth;
    public int playerHealthMax, playerHealthCurrent;
    public bool inInventory;
    //Tiempo
    public float decimalMinutes;
    public int minutos, horas;
    //Audio
    public int actualTrack;
    //Cultivos
    private Semilla[] plants;
    public bool timeAdded;
    private float timeCounter;
    //Pausa
    public GameObject menuPause;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        playerHealth = player.GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Player
        playerHealthMax = playerHealth.maxHealth;
        playerHealthCurrent = playerHealth.currentHealth;
        //Audio
        /*if(AudioManager.instance.audioPlay)
        {
            if (!AudioManager.instance.audioTracks[AudioManager.instance.currentTrack].isPlaying)
            {
                AudioManager.instance.audioTracks[AudioManager.instance.currentTrack].Play();
            }
        }*/
        //Dialgos
        player.talking = DialogManager.instance.dialogActive;
        //Tiempo
        UpdateTime();
        //Tiempo cultivos
        UpdatePlants();
        //Menu de Pausa
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menuPause.SetActive(!menuPause.activeInHierarchy);
        }

    }

    public void newTrack(int newTrack)
    {
        actualTrack = newTrack;
    }

    private void UpdateTime()
    {
        decimalMinutes += 1 * Time.deltaTime;
        minutos = Mathf.RoundToInt(decimalMinutes);
        if (minutos >= 60)
        {
            horas += 1;
            decimalMinutes = 0;
        }
        if (horas > 24)
        {
            horas = 0;
        }
    }

    private void UpdatePlants()
    {
        if (SceneInfo.isNextScene)
        {
            SceneInfo.timePassed += Time.deltaTime;
            timeCounter = SceneInfo.timePassed;
        }
        else
        {
            if (FindObjectsByType<Semilla>(FindObjectsSortMode.None).Length != 0)
            {
                plants = FindObjectsByType<Semilla>(FindObjectsSortMode.None);
            }
            if (plants == null)
            {
                return;
            }
            if (!timeAdded)
            {
                foreach (Semilla plant in plants)
                {
                    plant.actualTime += timeCounter;
                    plant.timeNoWater += timeCounter;
                }
                timeAdded = true;
                SceneInfo.timePassed = 0;
            }
        }
    }
}
