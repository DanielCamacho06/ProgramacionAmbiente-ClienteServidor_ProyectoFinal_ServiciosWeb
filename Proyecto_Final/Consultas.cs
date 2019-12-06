using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Windows.Forms;

public class Consultas
{
	public Consultas()
	{
	}
    static private XmlDocument consultarApi(String URL)
    {
        WebRequest peticion = WebRequest.Create(URL);
        WebResponse respuesta=null;
        try
        {
            respuesta = peticion.GetResponse();
        }catch(WebException e)
        {
            if (e.Status == WebExceptionStatus.ProtocolError)
            {
                System.Threading.Thread.Sleep(2000);
                return consultarApi(URL);
            }
            else
            {
                MessageBox.Show(e.Message);
            }
        }
        Stream flujo = respuesta.GetResponseStream();
        StreamReader lectorFlujo = new StreamReader(flujo);
        String cadenaRespuesta = lectorFlujo.ReadToEnd();
        XmlDocument xmlRespuesta = new XmlDocument();
        xmlRespuesta.LoadXml(cadenaRespuesta);
        return xmlRespuesta;        
    }
    static public XmlDocument consultarApiUsuario(String usuario)
    {
        String urlConsulta = "https://www.boardgamegeek.com/xmlapi2/user?name=" + usuario;
        return consultarApi(urlConsulta);
    }

    static public XmlDocument consultarApiJuego(String juego)
    {
        String urlConsulta = "https://www.boardgamegeek.com/xmlapi2/thing?id=" + juego;
        return consultarApi(urlConsulta);
    }

    static public XmlDocument consultarApiColeccion(String usuario)
    {
        String urlConsulta = "https://www.boardgamegeek.com/xmlapi2/collection?own=1&username=" + usuario;
        return consultarApi(urlConsulta);
    }
   
    static public XmlDocument consultarApiJugadas(String usuario, String idJuego)
    {
        String urlConsulta = "https://www.boardgamegeek.com/xmlapi2/plays?username=" + usuario + "&id=" + idJuego;
        return consultarApi(urlConsulta);
    }
}
