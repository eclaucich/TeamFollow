using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntradaDatosTenis : EntradaDatos
{
    [SerializeField] private MensajeError mensajeErrorGuardado = null;

    [SerializeField] private Text nombrePartidoText = null;

    [SerializeField] private GameObject textoPartidoFinalizado = null;

    [SerializeField] private MensajeDesplegable panelConfirmacionGuardado = null;
    private bool desplegarConfirmacionGuardado = false;

    //Paneles principal de la entrada de datos
    [SerializeField] private GameObject seccionListaJugadores = null;        //Se muestran los jugadores para poder seleccionar los que se quieran
    [SerializeField] private GameObject seccionEntradaDatos = null;          //La entrada de datos en si
    //[SerializeField] private GameObject seccionOpcionesAdicionales = null;   //Panel que muestra algunas opciones adicionales (ventaja y cantidad de sets)
    ///////////

    //Secciones de la entrada de datos
    [SerializeField] private GameObject seccionListaEstadisticas = null;     //Seccion dentro del panel de entrada de datos, muestra la lista de estadisticas preseleccionadas
    [SerializeField] private GameObject seccionPuntosGame = null;            //Posee los dos botones que permiten sumar puntos del game actual
    [SerializeField] private GameObject seccionPuntosTiebreak = null;        //Posee los dos botones que permiten sumar puntos del tiebreak actual

    [SerializeField] private Text puntosGameJ1Text = null;                   //Textos donde se muestran los puntos que tiene el J1 y el J2 en el game actual
    [SerializeField] private Text puntosGameJ2Text = null;                   

    [SerializeField] private Text tiebreakText = null;                       //Detalles sobre el tiebreak actual, cartel indicando que se esta jugando tiebreak,
    [SerializeField] private Text puntosTBJ1Text = null;                         //y los textos para mostrar el puntaje actual
    [SerializeField] private Text puntosTBJ2Text = null;

    [SerializeField] private GameObject setPrefab = null;                    //Prefab de un set, muestra la cantidad de games de cada jugador, y los puntos del tiebreak (si necesario)
    [SerializeField] private Transform setParentTransform = null;            //Transform para crear el prefab

    [SerializeField] private Text nombreJ1Text = null;                       //Nombre de jugadores
    [SerializeField] private Text nombreJ2Text = null;

    [SerializeField] private Toggle ventajaToggle = null;                    //Toggle que permite activar/desactivar la ventaja, en las opciones adicionales
    [SerializeField] private Text cantidadSetsText = null;
    ////////////

    private bool isVentaja = true;          //si se juega con ventaja o no
    private int actualSets = 0;             //cantidad de sets jugadas hasta el momento
    private int maxCantSets = 1;            //cantidad de sets maximos (al alcanzarla termina la entrada de datos)

    private SetTenis setActual;                     //Set actual que será modificado   
    private List<string> listaEstadisticas;         //Lista con el nombre de las estadisticas
    private Estadisticas estadisticas;              //Objeto que irá almacenando las estadisticas ingresadas, para luego poder guardarlas
    private bool eligiendoEstadistica = false;      //Variable auxiliar que no permite sumar puntos mientras que no se haya elegido una estadistica
    private bool agregandoJ1 = true;                //Variable auxiliar para saber si los puntos se le suman al J1 o al J2

    private bool isPartido = true;                  //Se juega partido o práctica
    private bool partidoFinalizado = false;

    private Jugador jugadorFocus;                   //Jugador focus en la entrada de datos
        
    private void Start()
    {
        listaEstadisticas = PanelSeleccionEstadisticas.instance.GetListaNombreEstadisticas();
        SetearSeccionListaEstadisticas();
        
        estadisticas = new Estadisticas(Deportes.DeporteEnum.Tenis);  
    }

    /// 
    /// Controla el cartel para guardar los datos,
    /// que cuando se aprete atrás, se active o desactive
    /// 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || desplegarConfirmacionGuardado)
        {
            if (seccionListaJugadores.activeSelf)
            {
                CanvasController.instance.retrocesoPausado = false;
                CanvasController.instance.AgregarPanelAnterior(CanvasController.Paneles.SeleccionEstadisticas);
                CanvasController.instance.MostrarPanelAnterior();
                Destroy(gameObject);
                return;
            }
            if (!partidoFinalizado)
            {
                panelConfirmacionGuardado.ToggleDesplegar();
                if (panelConfirmacionGuardado.isDesplegado() && desplegarConfirmacionGuardado) desplegarConfirmacionGuardado = false;
            }
        }
    }

    /// <summary>
    /// Se setea el estado principal de la entrada de datos,
    /// se llama desde el PanelEntradaDatos, una vez que se hayan elegido las estadisticas
    /// </summary>
    public override void Display(bool _isPartido)
    {
        isPartido = _isPartido;     

        //gameObject.GetComponent<RawImage>().texture = AppController.instance.GetTextureActual();

        gameObject.SetActive(true);  //asegurarse de que esté activo el panel

        //Primero se muestra el panel para elegir al jugador
        //Se desactivan todos los demas paneles
        seccionListaJugadores.SetActive(true);

        CanvasController.instance.overlayPanel.SetNombrePanel("SELECCION JUGADORES", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("PLAYERS SELECTION", AppController.Idiomas.Ingles);

        //seccionOpcionesAdicionales.SetActive(false);
        seccionEntradaDatos.SetActive(false);

        seccionListaEstadisticas.SetActive(false);
        //panelConfirmacionGuardado.SetActive(false);

        textoPartidoFinalizado.SetActive(false);

        //Se setea el panel que muestra la lista de jugadores
        seccionListaJugadores.GetComponent<SeleccionListaJugadores>().SetearListaJugadores();
    }

    /// 
    /// Descativa el panel de seleccion de jugadores para activar el panel que le siga
    /// 
    public override void TerminarSeleccionJugadores(List<Jugador> listaJugadores, int cantSeleccionados)
    {
        CanvasController.instance.overlayPanel.SetNombrePanel("", AppController.Idiomas.Español);
        CanvasController.instance.overlayPanel.SetNombrePanel("", AppController.Idiomas.Ingles);

        seccionListaJugadores.SetActive(false);
        //seccionOpcionesAdicionales.SetActive(true);
        seccionEntradaDatos.SetActive(false);

        jugadorFocus = listaJugadores[0];

        //AppController.instance.UpdateTexture();
        //gameObject.GetComponent<RawImage>().texture = AppController.instance.GetTextureActual();

        CanvasController.instance.botonDespliegueMenu.SetActive(false);

        ComenzarEntradaDatos();
    }

    /// 
    /// Setea el estado inicial de la entrada de datos,
    /// si se juga con ventaja, se crea un set, se setean los nombres de los jugadores,
    /// Se ejecuta desde el boton "siguiente" del panel de opciones adicionales
    /// 
    public void ComenzarEntradaDatos()
    {
        isVentaja = ventajaToggle.isOn;

        switch (cantidadSetsText.text)
        {
            case "1": maxCantSets = 1; break;
            case "3": maxCantSets = 3; break;
            case "5": maxCantSets = 5; break;
        }

        CrearNuevoSet();

        nombreJ1Text.text = jugadorFocus.GetNombre();
        nombreJ2Text.text = "Jugador 2";

        tiebreakText.gameObject.SetActive(false);

        seccionEntradaDatos.SetActive(true);
        //seccionOpcionesAdicionales.SetActive(false);
    }

    /// 
    /// Función que crea un nuevo set al comienzo de la entrada de datos, o cuando finaliza un set previo
    ///
    public void CrearNuevoSet()
    {
        if(actualSets >= maxCantSets)
        {
            textoPartidoFinalizado.SetActive(true);
            tiebreakText.gameObject.SetActive(false);
            desplegarConfirmacionGuardado = true;
            partidoFinalizado = true;
            panelConfirmacionGuardado.ToggleDesplegar();
            panelConfirmacionGuardado.DeshabilitarCloseZone();
            return;
        }
        GameObject setGO = Instantiate(setPrefab, setParentTransform, false);
        setActual = setGO.GetComponent<SetTenis>();
        setActual.SetVentaja(isVentaja);

        seccionPuntosGame.SetActive(true);
        seccionPuntosTiebreak.SetActive(false);

        puntosGameJ1Text.text = "0";
        puntosGameJ2Text.text = "0";

        puntosTBJ1Text.text = "0";
        puntosTBJ2Text.text = "0";

        actualSets++;
    }


    
    /// 
    /// Función que se llama al comenzar un nuevo tiebreak
    /// 
    public void ActivarSeccionTiebreak()
    {
        tiebreakText.gameObject.SetActive(true);

        seccionPuntosTiebreak.SetActive(true);
        seccionPuntosGame.SetActive(false);

        puntosTBJ1Text.text = "0";
        puntosTBJ2Text.text = "0";
    }

    /// 
    /// Se crean botones con los nombres de las estadisticas que se tendran en cuenta
    /// 
    private void SetearSeccionListaEstadisticas()
    {
        Transform button = seccionListaEstadisticas.transform.GetChild(0);
        foreach (var est in listaEstadisticas)
        {
            GameObject go = Instantiate(button.gameObject, seccionListaEstadisticas.transform, false);
            go.GetComponentInChildren<Text>().text = est;
            go.SetActive(true);
        }
    }


    /// 
    /// Funciones que se llaman desde los respectivos botones, para controlar los puntos
    /// 
    public void AgregarPuntoJ1()
    {     
        if (!eligiendoEstadistica)
        {
            seccionListaEstadisticas.SetActive(true);
            eligiendoEstadistica = true;
            agregandoJ1 = true;
        }
    }

    public void AgregarPuntoJ2()
    {
        if (!eligiendoEstadistica)
        {
            seccionListaEstadisticas.SetActive(true);
            eligiendoEstadistica = true;
            agregandoJ1 = false;
        }
    }

    public void AgregarPuntoTiebreakJ1()
    {
        setActual.AgregarPuntoTBJ1();
        if (!eligiendoEstadistica)
        {
            seccionListaEstadisticas.SetActive(true);
            eligiendoEstadistica = true;
            agregandoJ1 = false;
        }
    }

    public void AgregarPuntoTiebreakJ2()
    {
        setActual.AgregarPuntoTBJ2();
    }

    //Función que se llama al apretar alguna estadistica, se agrega "1" a la estadistica correspondiente para llevar un registro
    public void AgregarValorEstadistica(Text _nombreEst)
    {
        estadisticas.AgregarEstadisticas(_nombreEst.text, 1);
        seccionListaEstadisticas.SetActive(false);
        eligiendoEstadistica = false;
        if (agregandoJ1)
        {
            if (setActual.isTiebreak()) setActual.AgregarPuntoTBJ1();
            else                        setActual.AgregarPuntoJ1();
        }
        else
        {
            if (setActual.isTiebreak()) setActual.AgregarPuntoTBJ2();
            else                        setActual.AgregarPuntoJ2();
        }
    }

    ///ALGO ASI SERIA LA IDEA PARA RETROCEDER LA ACCION
    ///HABRIA QUE TENER EN CUENTA SI SE ESTA N UN TIEBREAK O NO,
    ///SI AL RETROCEDER, SE RETROCEDE AL GAME ANTERIOR, O SE SALE DEL TIEBREAK, ETC.
    ///HAY VARIOS CASOS A TENER EN CUENTA
    ///ANTES DE AGREGAR PUNTOS A UN JUGADOR, SE DEBERIA GUARDAR EL ESTADO ACTUAL DEL SET
    /*public void Retroceder()
    {
        estadisticas.QuitarEstadisticas(ultimaEstadisticaNombre);
        if(ultimoJugadorJ1) setActual.QuitarPuntoJ1();
        else                setActual.QuitarPuntoJ2();
    }*/


    /// 
    /// Funciones para controlar el display de los puntos en el panel
    /// 
    public void SetPuntosGameJ1(int _puntos)
    {
        puntosGameJ1Text.text = CodificarPuntosGame(_puntos);
    }

    public void SetPuntosGameJ2(int _puntos)
    {
        puntosGameJ2Text.text = CodificarPuntosGame(_puntos);
    }

    public void SetPuntosTBJ1(int _puntos)
    {
        puntosTBJ1Text.text = _puntos.ToString();
    }

    public void SetPuntosTBJ2(int _puntos)
    {
        puntosTBJ2Text.text = _puntos.ToString();
    }

    //Función para poder convertir los puntos llevados en enteros a un string
    private string CodificarPuntosGame(int _puntos)
    {
        string cod = "";

        switch (_puntos)
        {
            case 0: cod = "0"; break;
            case 1: cod = "15"; break;
            case 2: cod = "30"; break;
            case 3: cod = "40"; break;
            case 4: cod = "0"; break;
        }
        return cod;
    }

    //En caso de que se juegue con ventaja, desde el set se llama a esta función para poder mostrar adecuadamente los puntos
    public void SetPuntosGameVentaja(int puntosJ1, int puntosJ2)
    {
        if (puntosJ1 > puntosJ2) { puntosGameJ1Text.text = "AD"; puntosGameJ2Text.text = ""; }
        else if (puntosJ2 > puntosJ1) { puntosGameJ2Text.text = "AD"; puntosGameJ1Text.text = ""; }
        else { puntosGameJ1Text.text = "40"; puntosGameJ2Text.text = "40"; }
    }


    public override void GuardarEntradaDatos()
    {
        string tipoEntradaDato = isPartido ? "Partido" : "Practica";
        DateTime fecha = DateTime.Now;

        if (jugadorFocus.ContienePartido(tipoEntradaDato, nombrePartidoText.text))
        {
            mensajeErrorGuardado.SetText("Nombre existente!".ToString(), AppController.Idiomas.Español);
            mensajeErrorGuardado.SetText("Existing name!".ToString(), AppController.Idiomas.Ingles);
            mensajeErrorGuardado.Activar();
            panelConfirmacionGuardado.Cerrar();
            return;
        }

        Partido partido = new Partido(nombrePartidoText.text, estadisticas, fecha, isPartido);

        //jugadorFocus.GuardarEntradaDato(tipoEntradaDato, estadisticas, partido);

        CanvasController.instance.escenas.Add(1);
        CanvasController.instance.retrocesoPausado = false;
        CanvasController.instance.MostrarPanelAnterior();
        CanvasController.instance.botonDespliegueMenu.SetActive(true);
        Destroy(gameObject);
    }

    public override void DescartarDatos()
    {
        base.DescartarDatos();
        Destroy(gameObject);
        CanvasController.instance.botonDespliegueMenu.SetActive(true);
    }

    public override void CancelarGuardado()
    {
        if(!partidoFinalizado)
            panelConfirmacionGuardado.Cerrar();
    }
}
