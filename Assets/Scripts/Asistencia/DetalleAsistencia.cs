using UnityEngine;
using UnityEngine.UI;

public class DetalleAsistencia : MonoBehaviour {

    [SerializeField] private Text nombreJugadorText = null;
    [SerializeField] private Toggle toggleAsistencia = null;
    [SerializeField] private InputField inputObservacion = null;

    [SerializeField] private Text observacionText = null;
    [SerializeField] private Text observacionPlaceHolderText = null;

    public string nombre;
    public bool asistencia;
    public string observacion;

    public DetalleAsistencia(SaveDataPlanilla detalle_)
    {
        nombre = detalle_.GetNombreJugador();
        asistencia = detalle_.GetAsistencia();
        observacion = detalle_.GetObservacion();
    }

    public void SetNombreJugador(string nombre_)
    {
        nombre = nombre_;
        nombreJugadorText.text = nombre;
    }
    
    public void SetAsistencia()
    {
        asistencia = !asistencia;
    }
    
    public void SetObservacion()
    {
        observacion = observacionText.text;
    }

    public string GetNombre()
    {
        return nombre;
    }

    public bool GetAsistencia()
    {
        return asistencia;
    }

    public string GetObservacion()
    {
        return observacion;
    }

    public void SetDetalle(DetalleAsistencia detalle_)
    {
        nombreJugadorText.text = detalle_.GetNombre();
        toggleAsistencia.isOn = detalle_.GetAsistencia();
        observacionPlaceHolderText.text = detalle_.GetObservacion();

        DeshabilitarInputObservacion();
        DeshabilitarToggle();
    }

    private void DeshabilitarToggle()
    {
        toggleAsistencia.enabled = false;
    }

    private void DeshabilitarInputObservacion()
    {
        inputObservacion.enabled = false;
    }
}
