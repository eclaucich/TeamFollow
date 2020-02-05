using UnityEngine;
using UnityEngine.UI;

public class HerramientaFlechaRecta : Herramienta
{
    [SerializeField] private GameObject flechaRectaPrefab = null;

    private GameObject flechaActual = null;

    private int lineMode = 0;

    [SerializeField] private GameObject imagenBoton = null;
    [SerializeField] private Sprite imagenFlechaLlena = null;
    [SerializeField] private Sprite imagenFlechaPunteada = null;

    private void Start()
    {
        nombre = "Flecha";
        imagenBoton.GetComponent<Image>().sprite = imagenFlechaLlena;
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
            Debug.Log("USANDO");
        }
        
    }

    public override void DejarDeUsar()
    {
        flechaActual.GetComponent<FlechaRecta>().CrearPunta(lineMode);
        flechaActual = null;
        Debug.Log("DEJAR DE USAR");
    }

    public void SeleccionarFlechaLlena()
    {
        //GetComponentInChildren<Text>().text = "Llena";
        imagenBoton.GetComponent<Image>().sprite = imagenFlechaLlena;
        lineMode = 0;
        SetHerramientaActual();
    }

    public void SeleccionarFlechaPunteada()
    {
        //GetComponentInChildren<Text>().text = "Punteada";
        imagenBoton.GetComponent<Image>().sprite = imagenFlechaPunteada;
        lineMode = 2;
        SetHerramientaActual();
    }

    public void SeleccionarFlechaRecta()
    {
        //GetComponentInChildren<Text>().text = "Recta";
        imagenBoton.GetComponent<Image>().sprite = imagenFlechaLlena;
        lineMode = 1;
        SetHerramientaActual();
    }
}
