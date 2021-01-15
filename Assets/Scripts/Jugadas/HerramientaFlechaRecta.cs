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
    [SerializeField] private Herramienta herramientaSeleccion = null;

    private PanelEdicion panelEdicion;
    private bool panelHerramientasCerrado = false;

    private void Start()
    {
        nombre = "Flecha";
        imagenBoton.GetComponent<Image>().sprite = imagenFlechaLlena;
        panelEdicion = GameObject.Find("PanelEdicion").GetComponent<PanelEdicion>();
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

        panelEdicion.SetSwipe(false);

        if(flechaActual == null)
        {
            //GameObject go = Instantiate(flechaRectaPrefab);
            //go.transform.SetParent(GameObject.Find("PanelEdicion").transform);
            //go.transform.SetPositionAndRotation(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity);
            //go.transform.localPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);

            Vector3 mPos = Input.mousePosition;
            mPos.z = 2f;
            Vector3 goPos = Camera.main.ScreenToWorldPoint(mPos);

            GameObject go = Instantiate(flechaRectaPrefab, goPos, Quaternion.identity, GameObject.Find("PanelEdicion").transform);
            flechaActual = go;
        }
        else
        {
            if (panelHerramientasCerrado == false)
            {
                panelEdicion.CerrarPanelHerramientas();
                panelHerramientasCerrado = true;
            }

            flechaActual.GetComponent<FlechaRecta>().CreateLineRenderer(lineMode);
            Debug.Log("USANDO");
        }
        
    }

    public override void DejarDeUsar()
    {
        panelEdicion.SetSwipe(true);
        flechaActual.GetComponent<FlechaRecta>().CrearPunta(lineMode);
        flechaActual = null;
        Debug.Log("DEJAR DE USAR");
        panelEdicion.TogglePanelHerramientas();
        panelHerramientasCerrado = false;
        herramientaSeleccion.SetHerramientaActual();
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
