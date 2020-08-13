using System.Collections;
using System.Collections.Generic;

public class CarpetaJugada
{
    private string nombreCarpeta;
    private List<ImagenBiblioteca> listaJugadas;

    public CarpetaJugada(string _nombre)
    {
        nombreCarpeta = _nombre;
        listaJugadas = new List<ImagenBiblioteca>();
    }

    public bool ExistsJugada(string _nombreJugada)
    {
        foreach (var jugada in listaJugadas)
        {
            if (jugada.GetNombre().ToUpper() == _nombreJugada)
                return true;
        }

        return false;
    }
    public ImagenBiblioteca BuscarJugada(string _nombre)
    {
        foreach (var jugada in listaJugadas)
        {
            if (jugada.GetNombre().ToUpper() == _nombre.ToUpper())
                return jugada;
        }
        return null;
    }

    public void BorrarJugada(ImagenBiblioteca _jugada)
    {
        if (!listaJugadas.Contains(_jugada))
            return;

        SaveSystem.BorrarJugada(_jugada, this);
        listaJugadas.Remove(_jugada);
    }

    public void AgregarJugada(ImagenBiblioteca _jugada)
    {
        listaJugadas.Add(_jugada);
    }

    public void BorrarCarpeta()
    {
        AppController.instance.BorrarCarpeta(this);
    }

    public void SetNombre(string _nombre)
    {
        nombreCarpeta = _nombre;
    }

    public List<ImagenBiblioteca> GetListaJugadas()
    {
        return listaJugadas;
    }

    public string GetNombre()
    {
        return nombreCarpeta;
    }
}
