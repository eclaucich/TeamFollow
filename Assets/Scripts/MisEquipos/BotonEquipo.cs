using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        panelMisEquiposGO = GameObject.Find("PanelMisEquipos");                                 //Se busca el panel por nombre, solo puede haber uno
        panelMisEquipos = panelMisEquiposGO.GetComponent<PanelMisEquipos>();                    //Se obtiene el componente del panel
        panelConfirmacionBorrado = AppController.instance.panelConfirmacionBorradoEquipo;
    }

    public void VerDetalleEquipo()                                                              //Función que se llama al apretar el nombre de un equipo, se muestran las opciones de ese equipo
    {
        panelMisEquipos.MostrarPanelDetalleEquipo(nombreEquipoText.text, gameObject);
    }

    public void SetSpriteBotonEquipo(Equipo equipo)
    {
        switch (equipo.GetDeporte())
        {
            case "Fútbol":
                spriteDeporte.texture = spriteFutbol.texture;
                break;
            case "Hockey Cesped":
                spriteDeporte.texture = spriteHockeyCesped.texture;
                break;
            case "Tenis":
                spriteDeporte.texture = spriteTenis.texture;
                break;
            case "Softball":
                spriteDeporte.texture = spriteSoftball.texture;
                break;
            case "Voley":
                spriteDeporte.texture = spriteVoley.texture;
                break;
            case "Hockey Patines":
                spriteDeporte.texture = spriteHockeyPatines.texture;
                break;
            case "Rugby":
                spriteDeporte.texture = spriteRugby.texture;
                break;
            case "Basket":
                spriteDeporte.texture = spriteBasket.texture;
                break;
            case "Handball":
                spriteDeporte.texture = spriteHandball.texture;
                break;
            case "Padel":
                spriteDeporte.texture = spritePadel.texture;
                break;
        }

        spriteDeporte.transform.localScale.Set(0.8f, 0.9f, 1f);
    }


    public void AbrirPanelConfirmacionBorrado()
    {
        panelConfirmacionBorrado.GetComponent<ConfirmacionBorradoEquipo>().Activar(nombreEquipoText.text, gameObject);
    }
}
