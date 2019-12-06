using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Threading;
using System.Windows.Forms;

public class ColeccionJuegosUsuario
{
    public List<Juego> listaJuegos = new List<Juego>();
    public String autorConMasJuegos;
    public String numeroJuegosAutorConMasJuegos;
    public SortedDictionary<String, List<Juego>> autoresDictionary = new SortedDictionary<String, List<Juego>>();
    public SortedDictionary<String, Juego> idJuegosDictionary = new SortedDictionary<String, Juego>();
    public SortedDictionary<int, List<Juego>> numeroJugadoresDictionary = new SortedDictionary<int, List<Juego>>();
    public SortedDictionary<String, List<Juego>> mecanicasDictionary = new SortedDictionary<String, List<Juego>>();
    public SortedDictionary<String, List<Juego>> familiasDictionary = new SortedDictionary<String, List<Juego>>();
    public SortedDictionary<String, List<Juego>> categoriasDictionary = new SortedDictionary<String, List<Juego>>();

    public ColeccionJuegosUsuario(String rutaCacheJuego, String nombreUsuario)
	{
        String idJuego;
        XmlNodeList coleccionJuegos;
        XmlDocument documentoColeccion;
        Boolean band = true;
        if (File.Exists(rutaCacheJuego + nombreUsuario))
        {
            documentoColeccion = new XmlDocument();
            documentoColeccion.Load(rutaCacheJuego + nombreUsuario);
        }
        else
        {
            try
            {
                documentoColeccion = Consultas.consultarApiColeccion(nombreUsuario);
                documentoColeccion.Save(rutaCacheJuego + nombreUsuario);
                
            }
            catch (Exception e)
            {
                documentoColeccion = null;
                MessageBox.Show("No tienes conexión a internet." + e.Message.ToString());
                band = false;
            }
        }

        if (band) {
            coleccionJuegos = documentoColeccion.DocumentElement.SelectNodes("/items/item");
            foreach (XmlNode juego in coleccionJuegos)
            {
                idJuego = juego.Attributes["objectid"].Value;

                listaJuegos.Add(new Juego(idJuego, rutaCacheJuego));

                //Console.WriteLine("Juego con el id: " + idJuego + " agregado a la lista.");
            }
        }
    }
    public void obtenerAutorMasJuegos()
    {
        //public String autorConMasJuegos;
        //public String numeroJuegosautorConMasJuegos;
        int numeroJuegos = 0;
        String autorMasJuegos = "";
        foreach (var item in autoresDictionary)
        {
            if (item.Value.Count>numeroJuegos)
            {
                autorMasJuegos = item.Key;
                numeroJuegos = item.Value.Count;
            }
        }
        autorConMasJuegos = autorMasJuegos;
        numeroJuegosAutorConMasJuegos = numeroJuegos.ToString();
        Console.WriteLine("//" + autorMasJuegos);
        Console.WriteLine("++" + numeroJuegosAutorConMasJuegos);
    }

    public void agregarAutoresDictionary(String autorJuego, Juego juego)
    {
        if (autoresDictionary.ContainsKey(autorJuego))
        {
            List<Juego> listaJuegos = new List<Juego>();
            autoresDictionary.TryGetValue(autorJuego, out listaJuegos);
            listaJuegos.Add(juego);
            autoresDictionary[autorJuego] = listaJuegos;
        }
        else
        {
            List<Juego> listaJuegos = new List<Juego>();
            listaJuegos.Add(juego);
            autoresDictionary.Add(autorJuego, listaJuegos);
        }
    }

    public Juego obtenerObjetoJuego(String idJuego)
    {
        Juego objetoJuego;
        idJuegosDictionary.TryGetValue(idJuego, out objetoJuego);
        return objetoJuego;
    }

    public void agregarNumeroJugadoresDictionary(int numeroJugadores, Juego juego)
    {
        if (!numeroJugadoresDictionary.ContainsKey(numeroJugadores))
        {
            List<Juego> juegosLista = new List<Juego>();
            juegosLista.Add(juego);
            numeroJugadoresDictionary.Add(numeroJugadores, juegosLista);
        }
        else
        {
            List<Juego> juegosLista;
            numeroJugadoresDictionary.TryGetValue(numeroJugadores, out juegosLista);
            juegosLista.Add(juego);
            numeroJugadoresDictionary[numeroJugadores] = juegosLista;
        }
    }

    public void agregarMecanicasDictionary(String mecanica, Juego juego)
    {
        if (mecanicasDictionary.ContainsKey(mecanica))
        {
            List<Juego> listaJuegos = new List<Juego>();
            mecanicasDictionary.TryGetValue(mecanica, out listaJuegos);
            listaJuegos.Add(juego);
            mecanicasDictionary[mecanica] = listaJuegos;
        }
        else
        {
            List<Juego> listaJuegos = new List<Juego>();
            listaJuegos.Add(juego);
            mecanicasDictionary.Add(mecanica, listaJuegos);
        }
    }

    public void agregarFamiliasDictionary(String familia, Juego juego)
    {
        if (familiasDictionary.ContainsKey(familia))
        {
            List<Juego> listaJuegos = new List<Juego>();
            familiasDictionary.TryGetValue(familia, out listaJuegos);
            listaJuegos.Add(juego);
            familiasDictionary[familia] = listaJuegos;
        }
        else
        {
            List<Juego> listaJuegos = new List<Juego>();
            listaJuegos.Add(juego);
            familiasDictionary.Add(familia, listaJuegos);
        }
    }

    public void agregarCategoriasDictionary(String categoria, Juego juego)
    {
        if (categoriasDictionary.ContainsKey(categoria))
        {
            List<Juego> listaJuegos = new List<Juego>();
            categoriasDictionary.TryGetValue(categoria, out listaJuegos);
            listaJuegos.Add(juego);
            categoriasDictionary[categoria] = listaJuegos;
        }
        else
        {
            List<Juego> listaJuegos = new List<Juego>();
            listaJuegos.Add(juego);
            categoriasDictionary.Add(categoria, listaJuegos);
        }
    }

    public void agregarIdsDictionary(String idJuego, Juego juego)
    {
        idJuegosDictionary.Add(idJuego, juego);
    }
}
