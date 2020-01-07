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

    private void Start()
    {
        nombre = "Ficha";
        actualFicha = fichaRedonda;
        GetComponentInChildren<Text>().text = "Redonda";
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

        RawImage imagen = go.GetComponent<RawImage>();
        Color colorActual = GetComponentInParent<PanelHerramientas>().GetPanelCrearJugadas().GetColorActual();

        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);

        herramientaSeleccion.SetHerramientaActual();
    }


    public void SeleccionarFichaRedonda()
    {
        GetComponentInChildren<Text>().text = "Redonda";
        actualFicha = fichaRedonda;
        SetHerramientaActual();
    }

    public void SeleccionarFichaCuadrada()
    {
        GetComponentInChildren<Text>().text = "Cuadrada";
        actualFicha = fichaCuadrada;
        SetHerramientaActual();
    }

    public void SeleccionarFichaTriangular()
    {
        GetComponentInChildren<Text>().text = "Triangular";
        actualFicha = fichaTriangular;
        SetHerramientaActual();
    }

    public void SeleccionarFichaCruz()
    {
        GetComponentInChildren<Text>().text = "Cruz";
        actualFicha = fichaCruz;
        SetHerramientaActual();
    }
}
