using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HerramientaFichaAliado : Herramienta {

    [SerializeField] private GameObject fichaRedonda = null;

    [SerializeField] private GameObject fichaCuadrada = null;

    [SerializeField] private GameObject fichaTriangular = null;

    [SerializeField] private GameObject fichaCruz = null;

    [SerializeField] private HerramientaSeleccion herramientaSeleccion = null;

    private GameObject actualFicha;
    [SerializeField] private GameObject imagenBoton = null;


    private void Start()
    {
        nombre = "Ficha";
        actualFicha = fichaRedonda;
        //GetComponentInChildren<Text>().text = "Redonda";
        Image imagen = imagenBoton.GetComponent<Image>();
        imagen.sprite = fichaRedonda.GetComponent<Image>().sprite;
        Color colorActual = GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);
    }

    public override void Usar()
    {
        Transform parent = GameObject.Find("PanelEdicion").transform;
        GameObject go = Instantiate(actualFicha, parent, false);
        go.transform.SetPositionAndRotation(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity);

        /*for (int i = 0; i < go.transform.childCount; i++) ///ESTO ES PARA CUANDO ES UN OBJETO 3D
        {
            go.transform.GetChild(i).GetComponentInChildren<MeshRenderer>().material = ElegirMaterial(GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().GetColorActual());
        }*/

        Image imagen = go.GetComponent<Image>();
        Color colorActual = GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().GetColorActual();

        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        herramientaSeleccion.SetHerramientaActual();
    }


    public void SeleccionarFichaRedonda()
    {
        //GetComponentInChildren<Text>().text = "Redonda";
        Image imagen = imagenBoton.GetComponent<Image>();
        imagen.sprite = fichaRedonda.GetComponent<Image>().sprite;
        Color colorActual = GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        actualFicha = fichaRedonda;
        SetHerramientaActual();
    }

    public void SeleccionarFichaCuadrada()
    {
        //GetComponentInChildren<Text>().text = "Cuadrada";
        Image imagen = imagenBoton.GetComponent<Image>();
        imagen.sprite = fichaCuadrada.GetComponent<Image>().sprite;
        Color colorActual = GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        actualFicha = fichaCuadrada;
        SetHerramientaActual();
    }

    public void SeleccionarFichaTriangular()
    {
        //GetComponentInChildren<Text>().text = "Triangular";
        Image imagen = imagenBoton.GetComponent<Image>();
        imagen.sprite = fichaTriangular.GetComponent<Image>().sprite;
        Color colorActual = GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        actualFicha = fichaTriangular;
        SetHerramientaActual();
    }

    public void SeleccionarFichaCruz()
    {
        //GetComponentInChildren<Text>().text = "Cruz";
        Image imagen = imagenBoton.GetComponent<Image>();
        imagen.sprite = fichaCruz.GetComponent<Image>().sprite;
        Color colorActual = GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        actualFicha = fichaCruz;
        SetHerramientaActual();
    }
}
