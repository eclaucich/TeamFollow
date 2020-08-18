using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JugadorEntradaDato : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Text numeroCamiseta = null;

    [SerializeField] private Transform movingTransform = null;
    [SerializeField] private Transform canchaTransform = null;
    [SerializeField] private Transform bancaTransform = null;
    [SerializeField] private RectTransform estadisticasTransform = null;

    [SerializeField] private SeccionEstadisticas seccionEstadisticas = null;
    [SerializeField] private SeccionBanca seccionBanca = null;
    [SerializeField] private SeccionCancha seccionCancha= null;

    [SerializeField] private RectTransform delimiterBanca = null;
    [SerializeField] private RectTransform delimiterEstadisticas = null;

    private Jugador jugadorFocus;

    bool escalado = false;
    float movingScaleFactor = 1.3f;

    float sectionDelimiterY;
    float sectionDelimiterX;

    private Estadisticas estadisticasJugador;

    private void Start()
    {
        estadisticasJugador = new Estadisticas(AppController.instance.equipoActual.GetDeporte());

        sectionDelimiterY = bancaTransform.GetComponent<RectTransform>().rect.height - (transform.GetComponent<RectTransform>().rect.height/2f);
        sectionDelimiterX = canchaTransform.GetComponent<RectTransform>().rect.width - 1.5f*estadisticasTransform.rect.width;

        Debug.Log("DELIMITER: " + sectionDelimiterX);
    }


    public void SetJugadorFocus(Jugador _jugador)
    {
        jugadorFocus = _jugador;
        Debug.Log("NOMBRE JUGADOR: " + jugadorFocus.GetNombre());
        nombreJugadorText.text = jugadorFocus.GetNombre();
        numeroCamiseta.text = jugadorFocus.GetNumeroCamiseta();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ///JugadorFocus seleccionado (cambiar nombre del jugador seleccionado y los valors de los botones de estadisticas)
        seccionEstadisticas.SetJugadorEntradaDatoFocus(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.SetParent(movingTransform, false);
        transform.SetAsLastSibling();
        if (!escalado)
        {
            transform.localScale *= movingScaleFactor;
            escalado = true;
        }
        Vector3 mPos = Input.mousePosition;
        Vector3 goPos = Camera.main.ScreenToWorldPoint(mPos);
        goPos.z = 0f;
        
        if (goPos.x > delimiterEstadisticas.position.x)
            goPos.x = delimiterEstadisticas.position.x;
        transform.position = goPos;

        if (transform.position.x < delimiterBanca.position.x)
        {
            seccionBanca.SetActiveContorno(true);
            seccionCancha.SetActiveContorno(false);
        }
        else
        {
            seccionBanca.SetActiveContorno(false);
            seccionCancha.SetActiveContorno(true);
        }
    }

    public void OnMouseUp()
    {
        transform.localScale = new Vector3(1f,1f,1f);
        escalado = false;

        if(transform.position.x < delimiterBanca.position.x)
        {
            Debug.Log("SOLTADO SOBRE BANCA");
            transform.SetParent(bancaTransform, false);
        }
        else
        {
            Debug.Log("SOLTADO SOBRE CANCHA");
            transform.SetParent(canchaTransform, true);
        }

        seccionBanca.SetActiveContorno(false);
        seccionCancha.SetActiveContorno(false);
    }

    public void AgregarEstadistica(string _categoria, int _valor)
    {
        Debug.Log("ESTADISTICAS JUGADOR: " + nombreJugadorText.text);
        Debug.Log("CATEGORIA: " + _categoria);
        Debug.Log("VALOR: " + _valor);
        estadisticasJugador.AgregarEstadisticas(_categoria, _valor);
    }

    public string GetNombreJugador()
    {
        return nombreJugadorText.text;
    }

    public int GetValorEstadistica(string _categoria)
    {
        return estadisticasJugador.GetValorEstadistica(_categoria);
    }

    public void SetFechaEntradaDato(DateTime _fecha)
    {
        estadisticasJugador.SetFecha(_fecha);
    }

    public void GuardarEntradaDato(string _nombrePartido, string _tipoEntradaDato, DateTime _fecha, ResultadoEntradaDatos _res, List<Evento> _eventos, Partido.TipoResultadoPartido _tipoResultado)
    {
        jugadorFocus.GuardarEntradaDato(_tipoEntradaDato, estadisticasJugador, _nombrePartido, _fecha, _res, _eventos, _tipoResultado);
    }

    public void AgregarEstadisticasEquipo(Estadisticas _estEquipo)
    {
        Debug.Log("CANT: " + estadisticasJugador.GetCantidadCategorias());
        _estEquipo.AgregarEstadisticas(estadisticasJugador);
    }

    public Jugador GetJugador()
    {
        return jugadorFocus;
    }
}
