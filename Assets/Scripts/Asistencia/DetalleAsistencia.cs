using UnityEngine;
using UnityEngine.UI;

public class DetalleAsistencia : MonoBehaviour {

    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Button botonCambiarAsistencia = null;
    [SerializeField] private Text textBotonAsistencia = null;

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

    public void SetNombreJugador(string nombre_)
    {
        nombre = nombre_;
        nombreJugadorText.text = nombre;
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

    public void SetDetalle(DetalleAsistencia detalle_)
    {
        nombreJugadorText.text = detalle_.GetNombre();
        asistencia = detalle_.GetAsistencia();

        SetBotonAsistencia();
        botonCambiarAsistencia.enabled = false;
    }

    private void SetBotonAsistencia()
    {
        Image imagen = botonCambiarAsistencia.GetComponent<Image>();

        if (asistencia == 0)
        {
            imagen.color = colorPresente;
            textBotonAsistencia.text = "presente";
        }
        else if (asistencia == 1)
        {
            imagen.color = colorTarde;
            textBotonAsistencia.text = "tarde";
        }
        else
        {
            imagen.color = colorAusente;
            textBotonAsistencia.text = "ausente";
        }
    }
}
