using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JugadorEntradaDato : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField] private Image icono = null;
    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Text numeroCamiseta = null;
    [SerializeField] private Text posicionText = null;

    [SerializeField] private Transform movingTransform = null;
    [SerializeField] private Transform canchaTransform = null;
    [SerializeField] private Transform bancaTransform = null;
    [SerializeField] private RectTransform estadisticasTransform = null;

    [SerializeField] private SeccionEstadisticas seccionEstadisticas = null;
    [SerializeField] private SeccionBanca seccionBanca = null;
    [SerializeField] private SeccionCancha seccionCancha= null;

    [SerializeField] private RectTransform delimiterBanca = null;
    [SerializeField] private RectTransform delimiterEstadisticas = null;

    [SerializeField] private Color colorBanca;
    [SerializeField] private Color colorCancha;
    [SerializeField] private Color colorSeleccionado;

    private Jugador jugadorFocus;

    bool escalado = false;
    float movingScaleFactor = 1.3f;

    float sectionDelimiterY;
    float sectionDelimiterX;

    private Estadisticas estadisticasJugador;
    private Transform lastParent;
    private bool firstIn;

    private void Start()
    {
        estadisticasJugador = new Estadisticas(AppController.instance.equipoActual.GetDeporte());

        sectionDelimiterY = bancaTransform.GetComponent<RectTransform>().rect.height - (transform.GetComponent<RectTransform>().rect.height/2f);
        sectionDelimiterX = canchaTransform.GetComponent<RectTransform>().rect.width - 1.5f*estadisticasTransform.rect.width;

        lastParent = bancaTransform;
        firstIn = true;

        icono.color = colorBanca;
    }

    public void InitiateJugador(List<string> _categorias)
    {
        foreach (var cat in _categorias)
        {
            Debug.Log("CAT:" + cat);
            AgregarEstadistica(cat, 1);
            AgregarEstadistica(cat, -1);
        }
    }

    public void SetJugadorFocus(Jugador _jugador)
    {
        jugadorFocus = _jugador;
        Debug.Log("NOMBRE JUGADOR: " + jugadorFocus.GetNombre());
        nombreJugadorText.text = jugadorFocus.GetNombre();
        numeroCamiseta.text = jugadorFocus.GetNumeroCamiseta();
        posicionText.text = jugadorFocus.GetPosicionActual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        seccionEstadisticas.SetJugadorEntradaDatoFocus(this);
        lastParent = transform.parent;
        icono.color = colorSeleccionado;
    }

    public void RemoverSeleccionado()
    {
        if (lastParent == bancaTransform)
            icono.color = colorBanca;
        else if (lastParent == canchaTransform)
            icono.color = colorCancha;
        else
            Debug.Log("PADRE DISTINTO");
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
            if (lastParent == canchaTransform)
                seccionEstadisticas.AgregarEventoCambioJugador(jugadorFocus, false);
            transform.SetParent(bancaTransform, false);
            lastParent = bancaTransform;
            icono.color = colorBanca;
        }
        else
        {
            Debug.Log("SOLTADO SOBRE CANCHA");
            if (lastParent == bancaTransform)
                seccionEstadisticas.AgregarEventoCambioJugador(jugadorFocus, true);
            transform.SetParent(canchaTransform, true);
            lastParent = canchaTransform;
            icono.color = colorCancha;
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

    public void GuardarEntradaDato(List<string> _categorias, string _nombrePartido, string _tipoEntradaDato, DateTime _fecha, ResultadoEntradaDatos _res, List<Evento> _eventos, Partido.TipoResultadoPartido _tipoResultado, int _cantPeriodos)
    {
        foreach (var cat in _categorias)
        {
            if (estadisticasJugador.Find(cat)[0] == 0)
                estadisticasJugador.AgregarEstadisticas(cat, 0);
        }
        jugadorFocus.GuardarEntradaDato(_tipoEntradaDato, estadisticasJugador, _nombrePartido, _fecha, _res, _eventos, _tipoResultado, _cantPeriodos);
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

    public void ToggleNombre(bool _aux)
    {
        nombreJugadorText.gameObject.SetActive(_aux);
    }

    public void ToggleNumero(bool _aux)
    {
        numeroCamiseta.gameObject.SetActive(_aux);
    }

    public void TogglePosicion(bool _aux)
    {
        posicionText.gameObject.SetActive(_aux);
    }
}
