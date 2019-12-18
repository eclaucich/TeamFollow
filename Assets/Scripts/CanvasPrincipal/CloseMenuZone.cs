using UnityEngine;

/// <summary>
/// Se podría optimizar si en vez de ser un GameObject el panelFocus,
/// podría ser directamente un MensajeDespeglable, así se evita hacer el GetComponent.
/// Pero habría que actualizar las cosas en el inspector
/// </summary>

public class CloseMenuZone : MonoBehaviour {

    [SerializeField] private GameObject panelFocus = null;

    private bool closeZoneHabilitado = true;

    private void Start()
    {
        transform.SetAsLastSibling();
    }

    public void CloseMenu()
    {
        if (panelFocus.activeSelf && closeZoneHabilitado)
        {
            panelFocus.GetComponent<MensajeDesplegable>().Cerrar();
        }
    }

    public void DeshabilitarCloseZone()
    {
        closeZoneHabilitado = false;
    }
}
