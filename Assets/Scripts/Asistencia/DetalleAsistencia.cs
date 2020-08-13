using UnityEngine;
using UnityEngine.UI;

public class DetalleAsistencia : MonoBehaviour {

    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Button botonCambiarAsistencia = null;
    [SerializeField] private TextLanguage textBotonAsistencia = null;

    [SerializeField] private Color colorPresente;
    [SerializeField] private Color colorTarde;
    [SerializeField] private Color colorAusente;

    
    public string nombre;
    public int asistencia = 0; // 0 -> PRESENTE , 1 -> TARDE , 2 -> AUSENTE

    public DetalleAsistencia(SaveDataPlanilla detalle_)
    {
        nombre = detalle_.GetNombreJugador();
        asistencia = detalle_.GetAsistencia();
    }

    public DetalleAsistencia(DetalleAsistencia detalle)
    {
        nombre = detalle.GetNombre();
        asistencia = detalle.GetAsistencia();
    }

    public void SetNombreJugador(string nombre_)
    {
        nombre = nombre_;
        nombreJugadorText.text = nombre;
    }

    public void SetAsistenciaInicial(int asis)
    {
        asistencia = asis;
        SetBotonAsistencia();
    }
    
    public void SetAsistencia()
    {
        asistencia = ++asistencia % 3;
        SetBotonAsistencia();
    }

    public string GetNombre()
    {
        return nombre;
    }

    public int GetAsistencia()
    {
        return asistencia;
    }

    public void SetDetalle(DetalleAsistencia detalle_, bool activar)
    {
        nombreJugadorText.text = detalle_.GetNombre();
        asistencia = detalle_.GetAsistencia();

        SetBotonAsistencia();
        botonCambiarAsistencia.enabled = activar;
    }

    private void SetBotonAsistencia()
    {
        Image imagen = botonCambiarAsistencia.GetComponent<Image>();

        if (asistencia == 0)
        {
            imagen.color = colorPresente;
            textBotonAsistencia.SetText("PRESENTE", AppController.Idiomas.Español);
            textBotonAsistencia.SetText("PRESENT", AppController.Idiomas.Ingles);
        }
        else if (asistencia == 1)
        {
            imagen.color = colorTarde;
            textBotonAsistencia.SetText("TARDE", AppController.Idiomas.Español);
            textBotonAsistencia.SetText("LATE", AppController.Idiomas.Ingles);
        }
        else
        {
            imagen.color = colorAusente;
            textBotonAsistencia.SetText("AUSENTE", AppController.Idiomas.Español);
            textBotonAsistencia.SetText("ABSENT", AppController.Idiomas.Ingles);
        }
    }

}
