using UnityEngine;
using UnityEngine.UI;

public class PanelEditarJugador : MonoBehaviour
{
    [SerializeField] private InputField inputPeso = null;
    [SerializeField] private InputField inputAltura = null;


    private void GuardarEdicion()
    {
        //AppController.instance.jugadorActual.Editar(int.Parse(inputPeso.text), int.Parse(inputAltura.text));
    }
}
