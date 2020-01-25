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
    [SerializeField] public OverlayPanel overlayPanel = null;

    public static AppController instance = null;                                            //Instancia estatica del controlador
    public List<Equipo> equipos;                                                            //Lista de equipos en la app
    public Equipo equipoActual;                                                            //Equipo al cual se le esta dando foco en el momento
    public Jugador jugadorActual;

    private List<Texture> listaTexturas;
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

        listaTexturas = new List<Texture>();
        listaTexturas.Add(texturaPanelBasket);
        listaTexturas.Add(texturaPanelFutbol);
        listaTexturas.Add(texturaPanelHandball);
        listaTexturas.Add(texturaPanelHockeyCesped);
        listaTexturas.Add(texturaPanelHockeyPatines);
        listaTexturas.Add(texturaPanelPadel);
        listaTexturas.Add(texturaPanelRugby);
        listaTexturas.Add(texturaPanelSoftball);
        listaTexturas.Add(texturaPanelTenis);
        listaTexturas.Add(texturaPanelVoley);
        listaTexturas.Add(texturaPanelNormal);

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

    public void ChangeTexture(int i)
    {
        Debug.Log("Entro1");
        if(i < 0)
        {
            Debug.Log("Entro2");
            textureActual = texturaPanelNormal;
        }
    }

    public void UpdateTexture()
    {
        Debug.Log("UPDATED");
        if (equipoActual != null)
        {
            textureActual = listaTexturas[(int)equipoActual.GetDeporte()];
        }
        else
        {
            textureActual = texturaPanelNormal;
        }
    }

    public Texture GetTextureActual()
    {
        //UpdateTexture();
        return textureActual;
    }
}
