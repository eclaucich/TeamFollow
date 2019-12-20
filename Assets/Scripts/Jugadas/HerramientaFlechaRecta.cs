using UnityEngine;
using UnityEngine.UI;

public class HerramientaFlechaRecta : Herramienta
{
    [SerializeField] private GameObject flechaRectaPrefab = null;

    private GameObject flechaActual = null;

    private int lineMode = 0;

    private void Start()
    {
        nombre = "Flecha";
    }

    public override void Usar()
    {
        /*if (flechaActual == null)
        {
            GameObject go = Instantiate(flechaRectaPrefab);
            go.transform.SetParent(GameObject.Find("PanelEdicion").transform);
            go.transform.SetPositionAndRotation(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity);
            flechaActual = go;
            go.GetComponent<FlechaRecta>().SetInitialPosition(Input.mousePosition);
        }
        else
        {
            flechaActual.GetComponent<FlechaRecta>().SetFinalPosition(Input.mousePosition);
            flechaActual = null;
        }*/

        if(flechaActual == null)
        {
            GameObject go = Instantiate(flechaRectaPrefab);
            go.transform.SetParent(GameObject.Find("PanelEdicion").transform);
            go.transform.SetPositionAndRotation(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity);
            flechaActual = go;
        }
        else
        {
            flechaActual.GetComponent<FlechaRecta>().CreateLineRenderer(lineMode);
        }
        
    }

    public override void DejarDeUsar()
    {
        flechaActual.GetComponent<FlechaRecta>().CrearPunta(lineMode);
        flechaActual = null;
    }

    public void SeleccionarFlechaLlena()
    {
        GetComponentInChildren<Text>().text = "Llena";
        lineMode = 0;
        SetHerramientaActual();
    }

    public void SeleccionarFlechaPunteada()
    {
        GetComponentInChildren<Text>().text = "Punteada";
        lineMode = 2;
        SetHerramientaActual();
    }

    public void SeleccionarFlechaRecta()
    {
        GetComponentInChildren<Text>().text = "Recta";
        lineMode = 1;
        SetHerramientaActual();
    }
}
