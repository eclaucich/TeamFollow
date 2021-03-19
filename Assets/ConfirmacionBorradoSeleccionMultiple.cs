using System.Collections.Generic;
using UnityEngine;

public class ConfirmacionBorradoSeleccionMultiple : MensajeDesplegable
{

    private List<BotonJugador> listaBotonJugadores;
    private List<BotonHistorialAsistencia> listaBotonHistoriales;
    private List<BotonEquipo> listaBotonEquipos;
    private List<BotonImagen> listaBotonImagenes;
    private List<BotonCarpetaJugada> listaBotonCarpetas;
    private List<BotonPartido> listaBotonPartidos;

    private PanelJugadoresPrincipal panelJugadoresPrincipal = null;
    private PanelHistorialPlanillas panelHistorialAsistencias = null;
    private PanelPrincipal panelPrincipal = null;
    private PanelPrincipalBiblioteca panelPrincipalBiblioteca = null;
    private PanelPartidosEquipo panelPartidosEquipo = null;
    private PanelPartidos panelPartidos = null;

    private enum TipoBorrado
    {
        Jugadores,
        Asistencias,
        Equipos,
        Jugadas,
        Carpetas,
        PartidosEquipo,
        PartidosJugador
    }
    private TipoBorrado tipoBorrado;

    #region Activar
    public void Activar(List<BotonJugador> listaBotonJugadores_)
    {
        if(listaBotonJugadores_.Count==0)
            return;
        tipoBorrado = TipoBorrado.Jugadores;
        listaBotonJugadores = listaBotonJugadores_;
        text.SetText("Borrar los " + listaBotonJugadores.Count + " jugadores seleccionados?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete  the " + listaBotonJugadores.Count + " players selected?".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
    }

    public void Activar(List<BotonHistorialAsistencia> listaBotonHistoriales_)
    {
        if(listaBotonHistoriales_.Count==0)
            return;
        tipoBorrado = TipoBorrado.Asistencias;
        listaBotonHistoriales = listaBotonHistoriales_;
        text.SetText("Borrar las " + listaBotonHistoriales.Count + " asistencias seleccionadas?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete  the " + listaBotonHistoriales.Count + " assitence forms selected?".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
    }

    public void Activar(List<BotonEquipo> listaBotonEquipos_)
    {
        if(listaBotonEquipos_.Count==0)
            return;
        tipoBorrado = TipoBorrado.Equipos;
        listaBotonEquipos = listaBotonEquipos_;
        text.SetText("Borrar los " + listaBotonEquipos.Count + " equipos seleccionados?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete  the " + listaBotonEquipos.Count + " teams selected?".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
    }

    public void Activar(List<BotonImagen> listaBotonImagenes_)
    {
        if(listaBotonImagenes_.Count==0)
            return;
        tipoBorrado = TipoBorrado.Jugadas;
        listaBotonImagenes = listaBotonImagenes_;
        text.SetText("Borrar las " + listaBotonImagenes.Count + " jugadas seleccionadas?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete  the " + listaBotonImagenes.Count + " strategies selected?".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
    }

    public void Activar(List<BotonCarpetaJugada> listaBotonCarpetas_)
    {
        if(listaBotonCarpetas_.Count==0)
            return;
        tipoBorrado = TipoBorrado.Carpetas;
        listaBotonCarpetas = listaBotonCarpetas_;
        text.SetText("Borrar las " + listaBotonCarpetas.Count + " carpetas seleccionadas?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete  the " + listaBotonCarpetas.Count + " folders selected?".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
    }
    public void Activar(List<BotonPartido> listaBotonPartidos_, bool fromJugador)
    {
        if(listaBotonPartidos_.Count==0)
            return;
        if(fromJugador)
            tipoBorrado = TipoBorrado.PartidosJugador;
        else
            tipoBorrado = TipoBorrado.PartidosEquipo;
        listaBotonPartidos = listaBotonPartidos_;
        text.SetText("Borrar los " + listaBotonPartidos.Count + " partidos seleccionados?".ToUpper(), AppController.Idiomas.Español);
        text.SetText("Delete  the " + listaBotonPartidos.Count + " matches selected?".ToUpper(), AppController.Idiomas.Ingles);
        ToggleDesplegar();
    }
    #endregion
    

    #region Borrar
    private void BorrarSeleccionMultipleJugadores()
    {
        if(panelJugadoresPrincipal == null)
            panelJugadoresPrincipal = GameObject.Find("PanelJugadoresPrincipal").GetComponent<PanelJugadoresPrincipal>();

        foreach (var botonJugador in listaBotonJugadores)
        {
            AppController.instance.equipoActual.BorrarJugador(botonJugador.GetNombre(), false);    
            panelJugadoresPrincipal.BorrarBotonJugador(botonJugador.gameObject);
        }

        panelJugadoresPrincipal.ToggleSeleccionMultiple();
        Cerrar();
    }

    private void BorrarSeleccionMultipleAsistencias()
    {
        if(panelHistorialAsistencias == null)
            panelHistorialAsistencias = GameObject.Find("PanelHistorialPlanillas").GetComponent<PanelHistorialPlanillas>();

        Debug.Log("Borrar: " + listaBotonHistoriales.Count);

        foreach (var botonAsistencia in listaBotonHistoriales)
        {
            AppController.instance.equipoActual.BorrarAsistencia(botonAsistencia.GetNombre());
            panelHistorialAsistencias.BorrarPlanilla(botonAsistencia);
        }

        panelHistorialAsistencias.ToggleSeleccionMultiple();
        Cerrar();
    }

    public void BorrarSeleccionMultipleEquipos()
    {
        if(panelPrincipal == null)
            panelPrincipal = GameObject.Find("PanelPrincipal").GetComponent<PanelPrincipal>();

        foreach (var botonEquipo in listaBotonEquipos)
        {
            AppController.instance.BorrarEquipo(botonEquipo.GetEquipoFocus());
            panelPrincipal.BorrarBotonEquipo(botonEquipo.gameObject);
        }

        panelPrincipal.ToggleSeleccionMultiple();
        panelPrincipal.ActivarYDesactivarAdviceText();
        Cerrar();
    }

    public void BorrarSeleccionMultipleJugadas()
    {
        if(panelPrincipalBiblioteca == null)
            panelPrincipalBiblioteca = GameObject.Find("PanelPrincipalBiblioteca").GetComponent<PanelPrincipalBiblioteca>();

        foreach (var botonImagen in listaBotonImagenes)
        {
            botonImagen.BorrarJugadaFocus();
        }

        panelPrincipalBiblioteca.SetPanePrincipal();
        Cerrar();
    }

    public void BorrarSeleccionMultipleCarpetas()
    {
        if(panelPrincipalBiblioteca == null)
            panelPrincipalBiblioteca = GameObject.Find("PanelPrincipalBiblioteca").GetComponent<PanelPrincipalBiblioteca>();

        foreach (var carpeta in listaBotonCarpetas)
        {
            carpeta.GetCarpeta().BorrarCarpeta();
        }

        panelPrincipalBiblioteca.SetPanePrincipal();
        Cerrar();
    }

    public void BorrarSeleccionMultiplePartidos(bool fromJugador)
    {
        if(!fromJugador)
        {
            if(panelPartidosEquipo == null)
                panelPartidosEquipo = GameObject.Find("PanelEstadisticasGlobalesEquipo").GetComponent<PanelPartidosEquipo>();

            foreach (var partido in listaBotonPartidos)
            {
                panelPartidosEquipo.BorrarPartido(partido.GetPartido());
            }

            panelPartidosEquipo.SetearPanelPartidos();
        }
        else
        {
            if(panelPartidos == null)
                panelPartidos = GameObject.Find("PanelPartidosJugador").GetComponent<PanelPartidos>();

            foreach (var partido in listaBotonPartidos)
            {
                panelPartidos.BorrarPartido(partido.GetPartido());
            }
        }
        Cerrar();
    }

    public void BorrarSeleccionMultiple()
    {
        switch(tipoBorrado)
        {
            case TipoBorrado.Jugadores:
                BorrarSeleccionMultipleJugadores();
                break;

            case TipoBorrado.Asistencias:
                BorrarSeleccionMultipleAsistencias();
                break;

            case TipoBorrado.Equipos:
                BorrarSeleccionMultipleEquipos();
                break;

            case TipoBorrado.Jugadas:
                BorrarSeleccionMultipleJugadas();
                break;
            
            case TipoBorrado.Carpetas:
                BorrarSeleccionMultipleCarpetas();
                break;

            case TipoBorrado.PartidosEquipo:
                BorrarSeleccionMultiplePartidos(false);
                break;
            
            case TipoBorrado.PartidosJugador:
                BorrarSeleccionMultiplePartidos(true);
                break;

            default:
                Debug.Log("PROBLEMA EN EL BORRADO");
                break;
        }
    }
    #endregion
}
