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
    private PanelMisEquipos panelMisEquipos;                                                    //Componente del panel
    [SerializeField] private Text nombreEquipoText = null;                                             //Texto que muestra el nombre del equipo
    
    [SerializeField] private RawImage spriteDeporte = null;

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

    private GameObject panelConfirmacionBorrado;

    private List<Sprite> listaSprites;

    private void Awake()
    {
        panelMisEquiposGO = GameObject.Find("PanelMisEquipos");                                 //Se busca el panel por nombre, solo puede haber uno
        panelMisEquipos = panelMisEquiposGO.GetComponent<PanelMisEquipos>();                    //Se obtiene el componente del panel
        panelConfirmacionBorrado = AppController.instance.panelConfirmacionBorradoEquipo;
        listaSprites = new List<Sprite>();

    }

    private void Start()
    {
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
        Debug.Log("DEPORTE: " + equipo.GetDeporteNombre() + ", " + (int)equipo.GetDeporte());
         switch (equipo.GetDeporte())
         {
             case Deportes.Deporte.Futbol:
                 spriteDeporte.texture = spriteFutbol.texture;
                 break;
             case Deportes.Deporte.HockeyCesped:
                 spriteDeporte.texture = spriteHockeyCesped.texture;
                 break;
             case Deportes.Deporte.Tenis:
                 spriteDeporte.texture = spriteTenis.texture;
                 break;
             case Deportes.Deporte.Softball:
                 spriteDeporte.texture = spriteSoftball.texture;
                 break;
             case Deportes.Deporte.Voley:
                 spriteDeporte.texture = spriteVoley.texture;
                 break;
             case Deportes.Deporte.HockeyPatines:
                 spriteDeporte.texture = spriteHockeyPatines.texture;
                 break;
             case Deportes.Deporte.Rugby:
                 spriteDeporte.texture = spriteRugby.texture;
                 break;
             case Deportes.Deporte.Basket:
                 spriteDeporte.texture = spriteBasket.texture;
                 break;
             case Deportes.Deporte.Handball:
                 spriteDeporte.texture = spriteHandball.texture;
                 break;
             case Deportes.Deporte.Padel:
                 spriteDeporte.texture = spritePadel.texture;
                 break;
         }

        //spriteDeporte.texture = listaSprites[(int)equipo.GetDeporte()].texture;

        spriteDeporte.transform.localScale.Set(0.8f, 0.9f, 1f);
    }


    public void AbrirPanelConfirmacionBorrado()
    {
        panelConfirmacionBorrado.GetComponent<ConfirmacionBorradoEquipo>().Activar(nombreEquipoText.text, gameObject);
    }
}
