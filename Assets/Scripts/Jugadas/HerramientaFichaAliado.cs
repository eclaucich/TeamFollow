using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerramientaFichaAliado : Herramienta {

    [SerializeField] private float offsetX = 0f;
    [SerializeField] private float offsety = 0f;

    [SerializeField] private GameObject fichaRedonda = null;
    [SerializeField] private GameObject fichaCuadrada = null;
    [SerializeField] private GameObject fichaTriangular = null;
    [SerializeField] private GameObject fichaCruz = null;

    [SerializeField] private HerramientaSeleccion herramientaSeleccion = null;

    private GameObject actualFicha;
    [SerializeField] private GameObject imagenBoton = null;

    private Transform panelEdicionTransform;
    private PanelCrearJugadas panelCrearJugadas;
    private Image imagen;

    private void Start()
    {
        nombre = "Ficha";
       
        imagen = imagenBoton.GetComponent<Image>();

        panelCrearJugadas = GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas();
        panelEdicionTransform = GameObject.Find("PanelEdicion").transform;

        actualFicha = fichaRedonda;
        imagen.sprite = fichaRedonda.GetComponent<Image>().sprite;
        Color colorActual = panelCrearJugadas.GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);
    }

    public override void Usar()
    {
        Vector3 mPos = Input.mousePosition;
        mPos.z = 2f;
        Vector3 goPos = Camera.main.ScreenToWorldPoint(mPos);

        GameObject go = Instantiate(actualFicha, goPos, Quaternion.identity, panelEdicionTransform);

        Image imagen = go.GetComponent<Image>();
        Color colorActual = panelCrearJugadas.GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        herramientaSeleccion.SetHerramientaActual();
    }


    public void SeleccionarFichaRedonda()
    {
        actualFicha = fichaRedonda;

        imagen.sprite = fichaRedonda.GetComponent<Image>().sprite;

        Color colorActual = panelCrearJugadas.GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        SetHerramientaActual();
    }

    public void SeleccionarFichaCuadrada()
    {
        actualFicha = fichaCuadrada;

        imagen.sprite = fichaCuadrada.GetComponent<Image>().sprite;

        Color colorActual = panelCrearJugadas.GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        SetHerramientaActual();
    }

    public void SeleccionarFichaTriangular()
    {
        actualFicha = fichaTriangular;

        imagen.sprite = fichaTriangular.GetComponent<Image>().sprite;
        Color colorActual = panelCrearJugadas.GetColorActual();

        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        SetHerramientaActual();
    }

    public void SeleccionarFichaCruz()
    {
        actualFicha = fichaCruz;

        imagen.sprite = fichaCruz.GetComponent<Image>().sprite;

        Color colorActual = panelCrearJugadas.GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        SetHerramientaActual();
    }
}
