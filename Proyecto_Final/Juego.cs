using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Collections.Generic;

public class Juego
{
    public String idJuego;
    public String tituloJuego;
    public String ilustradorJuego;
    public String minJugadoresJuego;
    public String maxJugadoresJuego;
    public String linkImagen;

    public bool esCooperativo;

    public List<String> autorJuego = new List<String>();
    public List<String> listaMecanicas = new List<String>();
    public List<String> listaFamilias = new List<String>();
    public List<String> listaCategorias = new List<String>();


    public Juego(String idJuego, String rutaCacheJuego)
	{
        bool band = true;
        XmlDocument documentoJuego;
        if (File.Exists(rutaCacheJuego + idJuego))
        {
            documentoJuego = new XmlDocument();
            documentoJuego.Load(rutaCacheJuego + idJuego);
        }
        else
        {
            documentoJuego = Consultas.consultarApiJuego(idJuego);
            documentoJuego.Save(rutaCacheJuego + idJuego);
            band = false;
        }

        this.idJuego = idJuego;
        tituloJuego = documentoJuego.DocumentElement.SelectSingleNode("/items/item/name").Attributes["value"].Value;
        try
        {
            XmlNodeList listaNodos = documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgamedesigner']");
            foreach (XmlNode nodo in listaNodos)
            {
                autorJuego.Add(nodo.Attributes["value"].Value);
            }

        }
        catch (Exception)
        {
            autorJuego.Add("Uncredited");
        }

        try
        {
            ilustradorJuego = documentoJuego.DocumentElement.SelectSingleNode("/items/item/link[@type='boardgameartist']").Attributes["value"].Value;
        }
        catch (Exception)
        {
            ilustradorJuego = "Uncredited";
        }

        minJugadoresJuego = documentoJuego.DocumentElement.SelectSingleNode("/items/item/minplayers").Attributes["value"].Value;
        maxJugadoresJuego = documentoJuego.DocumentElement.SelectSingleNode("/items/item/maxplayers").Attributes["value"].Value;

        try
        {
            XmlNodeList listaNodos = documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgamemechanic']");
            foreach (XmlNode nodo in listaNodos)
            {
                listaMecanicas.Add(nodo.Attributes["value"].Value);
                if (nodo.Attributes["value"].Value.Contains("Cooperative"))
                {
                    esCooperativo = true;
                }
            }
        }
        catch (Exception)
        {
            listaMecanicas.Add("Uncredited");
        }

        try
        {
            XmlNodeList listaNodos = documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgamefamily']");
            foreach (XmlNode nodo in listaNodos)
            {
                listaFamilias.Add(nodo.Attributes["value"].Value);
            }
        }
        catch (Exception)
        {
            listaFamilias.Add("Uncredited");
        }

        try
        {
            XmlNodeList listaNodos = documentoJuego.DocumentElement.SelectNodes("/items/item/link[@type='boardgamecategory']");
            foreach (XmlNode nodo in listaNodos)
            {
                listaCategorias.Add(nodo.Attributes["value"].Value);
            }
        }
        catch (Exception)
        {
            listaCategorias.Add("Uncredited");
        }

        linkImagen = documentoJuego.DocumentElement.SelectSingleNode("/items/item/thumbnail").InnerText;

        if (band == false)
        {
            descargarIconoJuego(rutaCacheJuego, idJuego);
        }

        documentoJuego.Save(rutaCacheJuego + idJuego);
    }

    public void descargarIconoJuego(String rutaCacheJuego, String idJuego)
    {
        try
        {
            WebClient descargaImagen = new WebClient();
            descargaImagen.DownloadFile(this.linkImagen, rutaCacheJuego + idJuego + ".jpg");
        }
        catch (Exception e)
        {
            MessageBox.Show("No tienes conexión a internet.");
        }
    }

    public Image obtenerImagen(String rutaCacheJuego, String idJuego)
    {
        return Image.FromFile(rutaCacheJuego + idJuego + ".jpg");
    }
}
