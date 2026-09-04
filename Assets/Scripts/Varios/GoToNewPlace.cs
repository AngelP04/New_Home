using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = "New Scene name here";

    public string goToPlaceName;

    public bool isNextScene;

    public SceneInfo sceneInfo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals(("Player")))
        {
            ControladorDatosJuego.instance.crearNuevosDatos(true);
            FindObjectOfType<PlayerMovement>().nextPlaceName = goToPlaceName;
            sceneInfo.isNextScene = isNextScene;
            sceneInfo.hora = GameManager.instance.horas;
            sceneInfo.minuto = GameManager.instance.minutos;
            SceneManager.LoadScene(newPlaceName);
        }

    }
}
