using Cinemachine;
using Cultive;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TimeSystem;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{

    private PlayerMovement player;
    public Vector2 facingDirection = Vector2.zero;
    public SceneInfo sceneInfo;
    private AnimalesConseguidos animalesConseguidos;
    private AnimalLayer _animalLayer;

    public string PlaceName;
    [SerializeField] private PolygonCollider2D cameraLimits;
    private CinemachineConfiner2D confiner2D;

    private void Awake()
    {
        ControladorDatosJuego.instance.GetDatos();
    }
    // Start is called before the first frame update
    void Start()
    {
        confiner2D = FindFirstObjectByType<CinemachineConfiner2D>();
        player = FindObjectOfType<PlayerMovement>();
        animalesConseguidos = FindObjectOfType<AnimalesConseguidos>();
        _animalLayer = FindObjectOfType<AnimalLayer>();

        if(!player.nextPlaceName.Equals(PlaceName))
        {
            return;
        }
        player.transform.position = transform.position;

        player.lastMovement = facingDirection;

        confiner2D.m_BoundingShape2D = cameraLimits;

        if(sceneInfo.buttonPressed && sceneInfo.isNextScene)
        {
            _animalLayer.SpawnAnimal(animalesConseguidos.animal);
        }
        if (!sceneInfo.isNextScene)
        {
            GameManager.instance.timeAdded = false;
        }
        GameManager.instance.horas = sceneInfo.hora;
        GameManager.instance.minutos = sceneInfo.minuto;
        ControladorDatosJuego.instance.CargarEscena();

    }
}
