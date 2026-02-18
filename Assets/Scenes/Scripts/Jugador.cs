using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    private float movimientoX;
    public float velocidad = 2;
    private Rigidbody2D rb2d;
    private Animator animator;
    public Transform puntoDeInicio;

    public float fuerzaSalto = 2;

    private bool estaEnSuelo;
    public LayerMask layerSuelo;
    private float radioEsferaTocaSuelo = 0.2f;
    public Transform compruebaSuelo;
    private GameManager gameManager;

    public AudioSource audioSource;
    public AudioClip getRecolectar;
    public AudioClip sonidoMuerto;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No se encontro un GameManager en la escena.");
        }
    }

    private void Update()
    {
        rb2d.linearVelocity = new Vector2(movimientoX * velocidad, rb2d.linearVelocity.y);
        if (Mathf.Abs(movimientoX) > 0.01f)
        {
            animator.SetBool("estaCorriendo", true);
        }
        else
        {
            animator.SetBool("estaCorriendo", false);
            rb2d.linearVelocity = new Vector2(0, rb2d.linearVelocity.y);
        }
    }

    private void FixedUpdate()
    {
        bool estabaEnSueloAntes = estaEnSuelo;
        estaEnSuelo = Physics2D.OverlapCircle(compruebaSuelo.position, radioEsferaTocaSuelo, layerSuelo);

        if (!estabaEnSueloAntes && estaEnSuelo)
        {
            animator.SetBool("estaSaltando", false);
        }
    }

    private void OnMove(InputValue inputMovimiento)
    {
        movimientoX = inputMovimiento.Get<Vector2>().x;

        if (movimientoX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movimientoX) * 1f, 1f, 1f);
        }
    }

    private void OnJump(InputValue inputJump)
    {
        if (estaEnSuelo)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, fuerzaSalto);

            animator.SetBool("estaSaltando", true);
        }
    }

    public void ReiniciarPosicion()
    {
        transform.position = puntoDeInicio.position;
        rb2d.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Recolectar"))
        {
            audioSource.PlayOneShot(getRecolectar);
            if (gameManager != null) gameManager.SumarPuntos();
            Destroy(collision.gameObject);
        }

        if (collision.transform.CompareTag("Enemigo") || collision.transform.CompareTag("Muerte"))
        {
            audioSource.PlayOneShot(sonidoMuerto);
            if (gameManager != null)
            {
                gameManager.PerderVida();
            }
        }

        if (collision.transform.CompareTag("Casa"))
        {
            if (gameManager != null)
            {
                gameManager.CargarEscenaVictoria();
            }
        }
    }

}
