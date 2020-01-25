using UnityEngine;

/// <summary>
/// 
/// Es el panel principal de las nuevas entradas de datos
/// Tiene como hijos a el panel principal para todas las entradas que muestra la lista de estadisticas,
/// y un hijo que será un prefab de una entrada de datos específica del deporte actual
/// 
/// </summary>

public class PanelEntradaDatos : MonoBehaviour {

    //[SerializeField] private GameObject panel_nuevaEntradaDatos = null;                            //Panel para ingresar los datos
    [SerializeField] private GameObject panel_seleccionEstadisticas = null;
    //[SerializeField] private GameObject panel_confirmacion_borrado = null;

    [SerializeField] private GameObject entradaDatosTenis = null;
    [SerializeField] private GameObject entradaDatosNormal = null;

    private EntradaDatos entradaDatosActual;

    /*public void MostrarPanelPrincipal()                                                     //Muestra el panel principal
    {
        panel_principal.SetActive(true);
        //panel_nuevaEntradaDatos.SetActive(false);
        //panel_seleccionEstadisticas.SetActive(false);

        CanvasController.instance.escenas.Add(5);

        Screen.orientation = ScreenOrientation.Portrait;
    }*/
        
    /// 
    /// Esta función activa el primer panel que se verá en cualquier entrada de datos,
    /// muestra la lista de estadísticas del deporte actual, y dá la posibilidad de elegir entre Partido y Practica
    /// 
    public void MostrarPanelSeleccionEstadisticas()
    {
        //panel_nuevaEntradaDatos.SetActive(false);
        panel_seleccionEstadisticas.SetActive(true);

        panel_seleccionEstadisticas.GetComponent<PanelSeleccionEstadisticas>().SetearPanelEstadisticas();

        //CanvasController.instance.escenas.Add(9);
        CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.DetalleEquipoPrincipal);
    }

    /*
    public void MostrarPanelNuevaEntradaDatosPartido()                                      //Muestra el panel de entrada de datos relacionado con Partidos
    {
        panel_principal.SetActive(false);
        panel_nuevaEntradaDatos.SetActive(true);
        panel_seleccionEstadisticas.SetActive(false);

        if (AppController.instance.equipoActual.GetDeporte() == "Tenis")
        {
            GameObject go = Instantiate(entradaDatosTenis, this.transform, false);
            go.transform.name = "EntradaDatosTenisPartido";
        }
        else
        {
            panel_nuevaEntradaDatos.GetComponent<PanelNuevaEntradaDatos>().SetEntradaDatosPartido();
        }

        CanvasController.instance.escenas.Add(6);
    }

    public void MostrarPanelNuevaEntradaDatosPractica()                                     //Muestra el panel de entrada de datos relacionado con Practicas
    {
        panel_principal.SetActive(false);
        panel_nuevaEntradaDatos.SetActive(true);
        panel_seleccionEstadisticas.SetActive(false);

        panel_nuevaEntradaDatos.GetComponent<PanelNuevaEntradaDatos>().SetEntradaDatosPractica();

        CanvasController.instance.escenas.Add(7);
    }*/
    /*
    public void MostrarPanelConfirmacionBorrado()
    {
        panel_confirmacion_borrado.GetComponent<ConfirmacionBorradoEntradaDatos>().Desplegar();
    }*/

    /// 
    /// Se llama desde el botón presente en el panel de lista de estadisticas,
    /// crea y muestra el prefab de la entrada de datos del deporte adecuado
    /// 
    public void MostrarPanelNuevaEntradaDatos(bool isPartido)
    {
        entradaDatosActual = GetEntradaDatosActual();

        if(AppController.instance.equipoActual.GetJugadores().Count == 0)
        {
            Destroy(entradaDatosActual.gameObject);
            PanelSeleccionEstadisticas.instance.MostrarMensajeError();
            return;
        }

        if (entradaDatosActual != null)
        {
            panel_seleccionEstadisticas.SetActive(false);
            entradaDatosActual.Display(isPartido);  
        }

        CanvasController.instance.retrocesoPausado = true;
    }

    /// 
    /// Según el deporte enfocado, se crea el prefab adecuado
    /// 
    private EntradaDatos GetEntradaDatosActual()
    {
        if(AppController.instance.equipoActual.GetDeporte() == Deportes.Deporte.Tenis || AppController.instance.equipoActual.GetDeporte() == Deportes.Deporte.Padel)
        {
            GameObject go = Instantiate(entradaDatosTenis, this.transform, false);
            go.transform.name = "EntradaDatosTenisPartido";

            entradaDatosActual = go.GetComponent<EntradaDatosTenis>();
        }
        else
        {
            GameObject go = Instantiate(entradaDatosNormal, this.transform, false);
            go.transform.name = "EntradaDatosNormal";

            entradaDatosActual = go.GetComponent<PanelNuevaEntradaDatos>();
        }

        return entradaDatosActual;
    }
}
