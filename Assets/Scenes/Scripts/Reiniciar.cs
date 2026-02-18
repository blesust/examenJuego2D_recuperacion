using UnityEngine;
using UnityEngine.SceneManagement;

public class Reiniciar : MonoBehaviour
{
    public void IniciarJuego()
    {
        // Carga la escena principal del juego 
        SceneManager.LoadScene("Nivel1");
    }

    public void SalirDelJuego()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
