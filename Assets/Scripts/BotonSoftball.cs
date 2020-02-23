using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonSoftball : MonoBehaviour
{
    private Jugador jugador;

    [SerializeField] private GameObject listaOpciones = null;

    private Button boton;

    private void Start()
    {
        boton = GetComponent<Button>();
        OcultarOpciones();
    }

    public void SetJugador(Jugador _jugador)
    {
        jugador = _jugador;
    }

    public void MostrarOpciones()
    {
        listaOpciones.SetActive(true);
    }

    virtual public void OcultarOpciones()
    {
        listaOpciones.SetActive(false);
    }

    public void DesactivarBoton()
    {
        boton.enabled = false;
    }

    public void ActivarBoton()
    {
        boton.enabled = true;
    }


}
