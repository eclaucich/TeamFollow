using UnityEngine;
using UnityEngine.UI;

public class HerramientaFichaAliado : Herramienta {

    [SerializeField] private FichaJugada fichaRedonda = null;
    [SerializeField] private FichaJugada fichaCuadrada = null;
    [SerializeField] private FichaJugada fichaTriangular = null;
    [SerializeField] private FichaJugada fichaCruz = null;

    [SerializeField] private HerramientaSeleccion herramientaSeleccion = null;

    private FichaJugada actualFichaPrefab;
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

        actualFichaPrefab = fichaRedonda;
        SetBotonImagen();
    }

    public override void Usar()
    {
        Vector3 mPos = Input.mousePosition;
        Vector3 goPos = Camera.main.ScreenToWorldPoint(mPos);
        goPos.z = 0f;

        GameObject go = Instantiate(actualFichaPrefab.gameObject, goPos, Quaternion.identity, panelEdicionTransform);

        go.GetComponent<FichaJugada>().SetColorActual(panelCrearJugadas.GetColorActual());

        herramientaSeleccion.SetHerramientaActual();

        Debug.Log("DICHA USADA");
    }


    public void SeleccionarFichaRedonda()
    {
        actualFichaPrefab = fichaRedonda;

        SetBotonImagen();
        SetHerramientaActual();
    }

    public void SeleccionarFichaCuadrada()
    {
        actualFichaPrefab = fichaCuadrada;

        SetBotonImagen();
        SetHerramientaActual();
    }

    public void SeleccionarFichaTriangular()
    {
        actualFichaPrefab = fichaTriangular;

        SetBotonImagen();
        SetHerramientaActual();
    }

    public void SeleccionarFichaCruz()
    {
        actualFichaPrefab = fichaCruz;

        SetBotonImagen();
        SetHerramientaActual();
    }

    private void SetBotonImagen()
    {
        imagen.sprite = actualFichaPrefab.GetDisplayImage();
        Color colorActual = panelCrearJugadas.GetColorActual();
        imagen.color = new Color(colorActual.r, colorActual.g, colorActual.b, 255f);
    }
}
