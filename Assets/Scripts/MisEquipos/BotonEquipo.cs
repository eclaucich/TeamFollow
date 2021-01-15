using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 
/// Prefab que se crea por cada Equipo existente.
/// Se crea en el PANEL MIS EQUIPOS
/// Prefab que tiene dos botones:
///     Uno permite borrar el borrar el equipo relacionado con el prefab
///     Otro para ver los detalles de ese equipo
/// 
/// </summary>

public class BotonEquipo : MonoBehaviour {

    private GameObject panelMisEquiposGO;                                                       //Panel Principal MIS EQUIPOS
    private PanelMisEquipos panelMisEquipos;
    [SerializeField] private PanelPrincipal panelPrincipal = null;
    [SerializeField] private Text nombreEquipoText = null;                                             //Texto que muestra el nombre del equipo
    
    [SerializeField] private RawImage spriteDeporte = null;

    ///Todos estos sprites no van a estar
    ///va a haber un prefab del boton por cada deporte
    ///entonces en PanelPrincipal va a haber una lista con todos los prefabs y segun el deporte del equipo se elige el correcto
    [SerializeField] private Sprite spriteFutbol = null;
    [SerializeField] private Sprite spriteHockeyCesped = null;
    [SerializeField] private Sprite spriteTenis = null;
    [SerializeField] private Sprite spriteSoftball = null;
    [SerializeField] private Sprite spriteVoley = null;
    [SerializeField] private Sprite spriteHockeyPatines = null;
    [SerializeField] private Sprite spriteRugby = null;
    [SerializeField] private Sprite spriteBasket = null;
    [SerializeField] private Sprite spriteHandball = null;
    [SerializeField] private Sprite spritePadel = null;

    [SerializeField] private TextLanguage cantidadJugadoresText = null;

    [SerializeField] private Image imagenFavorito = null;
    [SerializeField] private Sprite estrellaVacia = null;
    [SerializeField] private Sprite estrellaLlena = null;

    private List<Sprite> listaSprites;
    private Equipo equipoFocus;

    private void Awake()
    {
        panelMisEquiposGO = GameObject.Find("PanelMisEquipos");                                 //Se busca el panel por nombre, solo puede haber uno
        panelMisEquipos = panelMisEquiposGO.GetComponent<PanelMisEquipos>();                    //Se obtiene el componente del panel
    }

    private void Start()
    {
        listaSprites = new List<Sprite>();
        listaSprites.Add(spriteBasket);
        listaSprites.Add(spriteFutbol);
        listaSprites.Add(spriteHandball);
        listaSprites.Add(spriteHockeyCesped);
        listaSprites.Add(spriteHockeyPatines);
        listaSprites.Add(spritePadel);
        listaSprites.Add(spriteRugby);
        listaSprites.Add(spriteSoftball);
        listaSprites.Add(spriteTenis);
        listaSprites.Add(spriteVoley);
    }

    public void VerDetalleEquipo()                                                              //Función que se llama al apretar el nombre de un equipo, se muestran las opciones de ese equipo
    {
        panelMisEquipos.MostrarPanelDetalleEquipo(nombreEquipoText.text, gameObject);
    }

    public void SetSpriteBotonEquipo(Equipo equipo)
    {
        equipoFocus = equipo;

        int cantJugadores = equipo.GetJugadores().Count;
        if (cantJugadores == 0)
        {
            cantidadJugadoresText.SetText("Equipo vacio".ToUpper(), AppController.Idiomas.Español);
            cantidadJugadoresText.SetText("empy team".ToUpper(), AppController.Idiomas.Ingles);
        }
        else if (cantJugadores == 1)
        {
            cantidadJugadoresText.SetText(equipo.GetJugadores().Count + " jugador".ToUpper(), AppController.Idiomas.Español);
            cantidadJugadoresText.SetText(equipo.GetJugadores().Count + " player".ToUpper(), AppController.Idiomas.Ingles);
        }
        else
        {
            cantidadJugadoresText.SetText(equipo.GetJugadores().Count + " jugadores".ToUpper(), AppController.Idiomas.Español);
            cantidadJugadoresText.SetText(equipo.GetJugadores().Count + " players".ToUpper(), AppController.Idiomas.Ingles);
        }

        //Debug.Log("DEPORTE: " + equipo.GetDeporteNombre() + ", " + (int)equipo.GetDeporte());
        switch (equipo.GetDeporte())
         {
             case Deportes.DeporteEnum.Futbol:
                 spriteDeporte.texture = spriteFutbol.texture;
                 break;
             case Deportes.DeporteEnum.HockeyCesped:
                 spriteDeporte.texture = spriteHockeyCesped.texture;
                 break;
             case Deportes.DeporteEnum.Tenis:
                 spriteDeporte.texture = spriteTenis.texture;
                 break;
             case Deportes.DeporteEnum.Softball:
                 spriteDeporte.texture = spriteSoftball.texture;
                 break;
             case Deportes.DeporteEnum.Voley:
                 spriteDeporte.texture = spriteVoley.texture;
                 break;
             case Deportes.DeporteEnum.HockeyPatines:
                 spriteDeporte.texture = spriteHockeyPatines.texture;
                 break;
             case Deportes.DeporteEnum.Rugby:
                 spriteDeporte.texture = spriteRugby.texture;
                 break;
             case Deportes.DeporteEnum.Basket:
                 spriteDeporte.texture = spriteBasket.texture;
                 break;
             case Deportes.DeporteEnum.Handball:
                 spriteDeporte.texture = spriteHandball.texture;
                 break;
             case Deportes.DeporteEnum.Padel:
                 spriteDeporte.texture = spritePadel.texture;
                 break;
         }

        //spriteDeporte.texture = listaSprites[(int)equipo.GetDeporte()].texture;

        spriteDeporte.transform.localScale.Set(2f, 2f, 1f);
    }

    public void SetComoFavorito()
    {
        AppController.instance.SetTeamAsFavourite(equipoFocus.GetNombre());
        panelPrincipal.ResetFavouriteTeams();
        imagenFavorito.sprite = estrellaLlena;
        transform.SetAsFirstSibling();
    }

    public void SetComoFavoritoFeedBack(){
        SetComoFavorito();
        panelPrincipal.MensajeFavorito();
    }

    public void RemoverFavorito()
    {
        AppController.instance.SetTeamAsFavourite(null);
        imagenFavorito.sprite = estrellaVacia;
    }

    public void GuardarComoFavorito()
    {
        string _nombreEquipoFavoritoActual = AppController.instance.equipoFavorito;
        if(_nombreEquipoFavoritoActual !=null && _nombreEquipoFavoritoActual == equipoFocus.GetNombre())
        {
            RemoverFavorito();
            SaveSystem.SaveFavouriteTeam(null);
        }
        else
        {
            SetComoFavoritoFeedBack();
            SaveSystem.SaveFavouriteTeam(equipoFocus.GetNombre());
        }
    }

    public Equipo GetEquipoFocus()
    {
        return equipoFocus;
    }

    public void DesactivarFavorito()
    {
        imagenFavorito.sprite = estrellaVacia;
    }
}
