using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text textoNumeroPuntos;
    public TMP_Text textoVidas;

    private int puntos = ;
    private int vidas = 2;

    void Start()
    {
        ActualizarInterfaz();
    }

    public void SumarPuntos()
    {
        puntos++;
        ActualizarInterfaz();
    }

    public void PerderVida()
    {
        vidas--;
        ActualizarInterfaz();
        if (vidas <= 0)
        {
            SceneManager.LoadScene("Derrota");
        }
        else
        {
            GameObject jugador = GameObject.FindWithTag("Player");
            if (jugador != null)
            {
                jugador.GetComponent<Jugador>().ReiniciarPosicion();
            }
        }

    }

    void ActualizarInterfaz()
    {
        textoNumeroPuntos.text = puntos.ToString();
        textoVidas.text = vidas.ToString();
    }

    public void CargarEscenaVictoria()
    {
        SceneManager.LoadScene("Victoria");
    }

    }