using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour {

    [SerializeField] public Texture2D texturaPanelNormal = null;
    [SerializeField] public Texture2D texturaPanelFutbol = null;
    [SerializeField] public Texture2D texturaPanelHockeyPatines = null;
    [SerializeField] public Texture2D texturaPanelHockeyCesped = null;
    [SerializeField] public Texture2D texturaPanelHandball = null;
    [SerializeField] public Texture2D texturaPanelRugby= null;
    [SerializeField] public Texture2D texturaPanelTenis = null;
    [SerializeField] public Texture2D texturaPanelPadel = null;
    [SerializeField] public Texture2D texturaPanelBasket = null;
    [SerializeField] public Texture2D texturaPanelSoftball = null;
    [SerializeField] public Texture2D texturaPanelVoley = null;

    [SerializeField] public GameObject panelConfirmacionBorradoEquipo = null;
    [SerializeField] public GameObject panelConfirmacionBorradoJugador = null;

    public static AppController instance = null;                                            //Instancia estatica del controlador
    public List<Equipo> equipos;                                                            //Lista de equipos en la app
    public Equipo equipoActual;                                                            //Equipo al cual se le esta dando foco en el momento
    public Jugador jugadorActual;

    private Texture textureActual;

    private void Awake()
    {
        if(instance == null)                                                                //Control del singleton
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        equipos = new List<Equipo>();                                                       //Inicializar equipos
        equipoActual = null;                                                                //No hay equipo enfocado al comenzar
        jugadorActual = null;

        LoadSystem.LoadData();

        DontDestroyOnLoad(this);

        Screen.SetResolution(720, 1280, true);

        textureActual = texturaPanelNormal;
    }


    public void AgregarEquipo(Equipo equipo)
    {
        equipos.Add(equipo);
        SaveSystem.GuardarEquipo(equipo);
    }

    public void BorrarEquipo(string nombreEquipo)                                           //Borrar equipo de la lista de equipos
    {
        int index = BuscarPorNombre(nombreEquipo);                                          //Se busca el equipo por su nombre (el nombre de cada equipo es unico)

        if (index < 0) return;                                                              //Asegurar que se haya encontrado

        SaveSystem.BorrarEquipo(equipos[index]);

        equipos.RemoveAt(index);                                                            //Borrar equipo
    }

    public int BuscarPorNombre(string nombreEquipo)                                         //Devuelve el indice de un equipo en la lista
    {
        for (int i = 0; i < equipos.Count; i++)
        {
            if(equipos[i].GetNombre() == nombreEquipo)
            {
                return i;
            }
        }

        return -1;                                                                          //Equipo no encontrado -> indice invalido
    }

    public Equipo GetEquipoActual()                                                         //Devuelve el equipo enfocada actualmente
    {
        return equipoActual;
    }

    public void SetEquipoActual(Equipo equipo_)                                             //Setea el equipo enfocado
    {
        equipoActual = equipo_;
    }

    public void UpdateTexture()
    {
        if (equipoActual != null)
        {
            switch (equipoActual.GetDeporte())
            {
                case Deportes.Deporte.Futbol:
                    textureActual = texturaPanelFutbol;
                    break;
                case Deportes.Deporte.Basket:
                    textureActual = texturaPanelBasket;
                    break;
                case Deportes.Deporte.Handball:
                    textureActual = texturaPanelHandball;
                    break;
                case Deportes.Deporte.HockeyCesped:
                    textureActual = texturaPanelHockeyCesped;
                    break;
                case Deportes.Deporte.HockeyPatines:
                    textureActual = texturaPanelHockeyPatines;
                    break;
                case Deportes.Deporte.Padel:
                    textureActual = texturaPanelPadel;
                    break;
                case Deportes.Deporte.Rugby:
                    textureActual = texturaPanelRugby;
                    break;
                case Deportes.Deporte.Softball:
                    textureActual = texturaPanelSoftball;
                    break;
                case Deportes.Deporte.Tenis:
                    textureActual = texturaPanelTenis;
                    break;
                case Deportes.Deporte.Voley:
                    textureActual = texturaPanelVoley;
                    break;
                default:
                    textureActual = texturaPanelNormal;
                    break;
            }
        }
        else
        {
            textureActual = texturaPanelNormal;
        }
    }

    public Texture GetTextureActual()
    {
        UpdateTexture();
        return textureActual;
    }
}
