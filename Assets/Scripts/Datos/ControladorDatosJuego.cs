using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cultive;
using System.Linq;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using TimeSystem;
using PlayerBuildingSystem;

[System.Serializable]
public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject player;

    public string archivoGuardado;

    public string archivoPrueba;

    public List<ObjectData> objsData = new();

    public List<TileInfo> tileInfos = new();

    public DatosJuego datosJuego = new DatosJuego();

    private DatosJuego datosZona = new();

    [SerializeField] private SceneInfo sceneInfo, sceneInfoBuilds;

    private CultiveLayer cultiveLayer;

    private ConstructionLayer constructionLayer;

    public static ControladorDatosJuego instance { get; private set; }

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
        archivoGuardado = Application.dataPath + "/datosJuego.json";

        archivoPrueba = Application.dataPath + "/datosPreuba.json";

    }

    private void CargarInventario()
    {
        InventoryManager.instance.itemDatas = datosJuego.itemDatas;
        InventoryManager.instance.LoadItemsData();
    }

    public void CargarDatos()
    {
        LoadPlayer();
        CargarEscena();
    }

    public void GetDatos(bool changeZone = true)
    {
        if (!changeZone)
        {
            if (File.Exists(archivoGuardado))
            {
                string contenido = File.ReadAllText(archivoGuardado);
                datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);
            }
        }
        else
        {
            if (sceneInfo.datosEscenas.ContainsKey(SceneManager.GetActiveScene().buildIndex))
            {
                datosZona = sceneInfoBuilds.datosEscenas[SceneManager.GetActiveScene().buildIndex];
                datosJuego = datosZona;
                string cadenaJSON = JsonUtility.ToJson(datosZona);

                Debug.Log(cadenaJSON);
            }
        }
    }

    private void LoadPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = datosJuego.position;
        player.GetComponent<HealthManager>().currentHealth = datosJuego.vida;
        CargarInventario();
    }

    private void LoadUI()
    {
        GameManager.instance.horas += datosJuego.hora;
        GameManager.instance.decimalMinutes += datosJuego.minutos;
        timeManager.instance.LoadDay(datosJuego.dateTime);
    }

    public void CargarEscena()
    {
        
        LoadUI();
        LoadObjs();

    }

    private void LoadObjs()
    {
        cultiveLayer = FindObjectOfType<CultiveLayer>();
        constructionLayer = FindObjectOfType<ConstructionLayer>();
        objsData = datosJuego.objsDataSaved;
        tileInfos = datosJuego.tileInfoSaved;
        ObjsToLoad objsToLoad = new();
        objsToLoad.models = datosJuego.modelsSaved;
        objsToLoad.seeds = datosJuego.seedsSaved;
        foreach (ObjectData obj in objsData)
        {
            GameObject newObj = Instantiate(Resources.Load("Prefabs/Objetos/" + obj.id, typeof(GameObject))) as GameObject;
            newObj.GetComponent<PersistenceObject>().FromData(obj);
            objsToLoad.objects.Add(newObj);
        }
        if (cultiveLayer != null)
        {
            cultiveLayer.Load(sceneInfo.tiles, objsToLoad.seeds, objsToLoad.objects);
        }
        if(constructionLayer != null)
        {
            constructionLayer.Load(objsToLoad.models, objsToLoad.objects);
        }
    }

    private void GuardarDatos(bool changeZone)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        JsonableListWrapper datos = new();
        foreach(GameObject planta in GameObject.FindGameObjectsWithTag("Planta"))
        {
            ObjectData data = planta.GetComponent<PersistenceObject>().ToData();
            datos.list.Add(data);
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Object"))
        {
            ObjectData data = obj.GetComponent<PersistenceObject>().ToData();
            datos.list.Add(data);
        }

        tileInfos = sceneInfo.tiles;
        objsData = datos.list;

        DatosJuego nuevosDatos = returnDataToSave(changeZone);

        if(!changeZone)
        {
            string cadenaJSON = JsonUtility.ToJson(nuevosDatos);

            File.WriteAllText(archivoGuardado, cadenaJSON);

            Debug.Log("Archivo Guardado");
        }
        else
        {
            GuardarEscena(nuevosDatos);
        }
    }

    private DatosJuego returnDataToSave(bool changeZone)
    {
        SerializableDateTime actualDate = timeManager.instance.SaveDateTime();
        DatosJuego nuevosDatos = new DatosJuego()
        {
            position = player.transform.position,
            vida = player.GetComponent<HealthManager>().currentHealth,
            objsDataSaved = objsData,
            tileInfoSaved = tileInfos,
            hora = GameManager.instance.horas,
            minutos = GameManager.instance.minutos,
            modelsSaved = sceneInfoBuilds.seeds.Values.ToList(),
            escena = SceneManager.GetActiveScene().buildIndex,
            dateTime = actualDate,
        };
        if(!changeZone)
        {
            InventoryManager.instance.SaveItemsData();
            nuevosDatos.itemDatas = InventoryManager.instance.itemDatas;
        }
        return nuevosDatos;
    }

    public void crearNuevosDatos(bool changeScene = false)
    {
        GuardarDatos(changeScene);
    }

    private void GuardarEscena(DatosJuego nuevosDatosEscena)
    {
        int actualScene = SceneManager.GetActiveScene().buildIndex;
        if(sceneInfo.datosEscenas.ContainsKey(actualScene))
        {
            sceneInfo.datosEscenas[actualScene] = nuevosDatosEscena;
            sceneInfoBuilds.datosEscenas[actualScene] = nuevosDatosEscena;
        }
        else
        {
            sceneInfo.datosEscenas.Add(actualScene, nuevosDatosEscena);
            sceneInfoBuilds.datosEscenas.Add(actualScene, nuevosDatosEscena);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            crearNuevosDatos();
        }
    }
}

[Serializable]
public class JsonableListWrapper
{
    public List<ObjectData> list = new();
}
[Serializable]
public class TileMapInfo
{
    public List<TileInfo> tiles = new();
}

[Serializable]
public class ObjsToLoad
{
    public List<Model> seeds = new();
    public List<Model> models = new();
    public List<GameObject> objects = new();
}
