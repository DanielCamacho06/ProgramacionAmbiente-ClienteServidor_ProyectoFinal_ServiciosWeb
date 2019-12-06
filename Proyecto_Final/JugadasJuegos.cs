using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

public class JugadasJuegos
{
    public String idJuegoMasJugado;
    public String numeroJugadasJuegoMasJugado;
    //key id juego, numero de veces que se ha jugado
    public SortedDictionary<String, SortedDictionary<int, int[]>> juegosResultadosDictionary = new SortedDictionary<String, SortedDictionary<int, int[]>>();
    public SortedDictionary<String, String> vecesJugadoDictionary = new SortedDictionary<String, String>();
    public SortedDictionary<String, SortedDictionary<String, int[]>> adversariosJuegosDictionary = new SortedDictionary<String, SortedDictionary<String, int[]>>();
    public String mejorAdversario;
    public String peorAdversario;
    public SortedDictionary<String, SortedDictionary<String, int[]>> adversariosNombresDictionary = new SortedDictionary<String, SortedDictionary<String, int[]>>();

    public JugadasJuegos(String directorioCacheJugadas, String nombreUsuario, List<String> idsJuegos)
    {
        foreach (var idJuego in idsJuegos)
        {
        XmlDocument documentoJugadas;
        if (File.Exists(directorioCacheJugadas + nombreUsuario + "/" + idJuego))
        {
            documentoJugadas = new XmlDocument();
            documentoJugadas.Load(directorioCacheJugadas + nombreUsuario + "/" + idJuego);
        }
        else
        {
            try
            {
                documentoJugadas = Consultas.consultarApiJugadas(nombreUsuario, idJuego);
                Directory.CreateDirectory(directorioCacheJugadas + nombreUsuario);
                documentoJugadas.Save(directorioCacheJugadas + nombreUsuario + "/" + idJuego);
            }
            catch (Exception e)
            {
                documentoJugadas = null;
                MessageBox.Show("No tienes conexión a internet." + e.Message.ToString());
            }
        }

        procesarInformacionJugadasXML(nombreUsuario, idJuego, documentoJugadas);
        }

        /*
        foreach (var a in adversariosJuegosDictionary)
        {
            Console.WriteLine("En el juego " + a.Key + " estan los nombres: ");
            foreach (var c in a.Value)
            {
                Console.WriteLine(c.Key);
            }
        }
        */
        obtenerResultadosMaximoMinimo();
    }//Fin constructor
    public void obtenerJuegoMasJugado()
    {
        String juegoMasJugado="";
        int control = 0;
        foreach (var item in vecesJugadoDictionary)
        {
            if (Int32.Parse(item.Value) > control)
            {
                juegoMasJugado = item.Key;
                control = Int32.Parse(item.Value);
            }
        }
        this.idJuegoMasJugado = juegoMasJugado;
        this.numeroJugadasJuegoMasJugado = control.ToString();
        Console.WriteLine("---------------------\n" + this.idJuegoMasJugado);
        Console.WriteLine("--" + numeroJugadasJuegoMasJugado);
    }
    private void obtenerResultadosMaximoMinimo()
    {
        String nombrePeorAdversario = "";
        int sumaVecesDerrota = 0;
        int vecesDerrota = 0;
        String nombreMayorAdversario = "";
        int sumaVecesVictoria = 0;
        int vecesVictoria = 0;
        foreach (var item in adversariosNombresDictionary)
        {
            sumaVecesVictoria = 0;
            sumaVecesDerrota = 0;
            if (!item.Key.ToLower().Equals("anonymous player"))
            {
                foreach (var item2 in item.Value)
                {
                    sumaVecesVictoria = sumaVecesVictoria + item2.Value[0];
                    sumaVecesDerrota = sumaVecesDerrota + item2.Value[1];
                }
                if (sumaVecesVictoria > vecesVictoria)
                {
                    nombreMayorAdversario = item.Key;
                    vecesVictoria = sumaVecesVictoria;
                }
                if (sumaVecesDerrota > vecesDerrota)
                {
                    nombrePeorAdversario = item.Key;
                    vecesDerrota = sumaVecesDerrota;
                }
                //Console.WriteLine("++" + item.Key + " ganadas " + sumaVecesVictoria + " perdidas " + sumaVecesDerrota);
            }
        }
        mejorAdversario = nombreMayorAdversario;
        peorAdversario = nombrePeorAdversario;
        Console.WriteLine("***********************\n" + mejorAdversario);
        Console.WriteLine("**" + peorAdversario);
    }

    private void procesarInformacionJugadasXML(string nombreUsuario, string idJuego, XmlDocument documentoJugadas)
    {
        String totalVecesJugado = documentoJugadas.DocumentElement.SelectSingleNode("/plays").Attributes["total"].Value;
        vecesJugadoDictionary.Add(idJuego, totalVecesJugado);

        XmlNodeList jugadasColeccionNodos = documentoJugadas.DocumentElement.SelectNodes("/plays/play");

        procesarJugadasXML(nombreUsuario, idJuego, jugadasColeccionNodos);
        obtenerJuegoMasJugado();
    }

    private void procesarJugadasXML(string nombreUsuario, string idJuego, XmlNodeList jugadasColeccionNodos)
    {
        foreach (XmlNode jugadaNodoSimple in jugadasColeccionNodos)
        {
            XmlNodeList jugadoresColeccionNodos = jugadaNodoSimple.SelectNodes("players/player");

            if (jugadoresColeccionNodos != null)
            {
                int numeroJugadores = jugadoresColeccionNodos.Count;

                bool juegoGanado = false;

                juegoGanado = procesarJugadores(nombreUsuario, jugadoresColeccionNodos, juegoGanado, idJuego);

                agregarResultadoPartida(idJuego, numeroJugadores, juegoGanado);
            }
        }
    }

    private bool procesarJugadores(string nombreUsuario, XmlNodeList jugadoresColeccionNodos, bool juegoGanado, String idJuego)
    {
        foreach (XmlNode jugadorNodo in jugadoresColeccionNodos)
        {
            String usuarioJugador = jugadorNodo.Attributes["username"].Value;
            if (usuarioJugador.Equals(nombreUsuario))
            {
                if (jugadorNodo.Attributes["win"].Value.Equals("1"))
                {
                    juegoGanado = true;
                }
            }
            else
            {
                bool adversarioGana;
                String nombreAdversario = jugadorNodo.Attributes["name"].Value;
                if (jugadorNodo.Attributes["win"].Value.Equals("1"))
                {
                    adversarioGana = true;
                }
                else
                {
                    adversarioGana = false;
                }
                //Console.WriteLine("agregando a " + nombreAdversario + " del juego " + idJuego);
                agregarDatosAdversarioJuegoDictionary(nombreAdversario, idJuego, adversarioGana);
                agregarAdversariosNombresDictionary(nombreAdversario, idJuego, adversarioGana);
            }
        }
        return juegoGanado;
    }

    private void agregarResultadoPartida(string idJuego, int numeroJugadores, bool juegoGanado)
    {
        if (juegoGanado == true)
        {
            if (juegosResultadosDictionary.ContainsKey(idJuego))
            {
                SortedDictionary<int, int[]> numeroJugadoresDictionary;
                juegosResultadosDictionary.TryGetValue(idJuego, out numeroJugadoresDictionary);

                if (numeroJugadoresDictionary.ContainsKey(numeroJugadores))
                {
                    int[] resultadosExistentes;
                    numeroJugadoresDictionary.TryGetValue(numeroJugadores, out resultadosExistentes);
                    resultadosExistentes[0] += 1;
                    numeroJugadoresDictionary[numeroJugadores] = resultadosExistentes;
                }
                else
                {
                    int[] nuevosResultados = new int[2];
                    nuevosResultados[0] = 1;
                    nuevosResultados[1] = 0;
                    numeroJugadoresDictionary.Add(numeroJugadores, nuevosResultados);
                }
                juegosResultadosDictionary[idJuego] = numeroJugadoresDictionary;
            }
            else
            {
                SortedDictionary<int, int[]> numeroJugadoresDictionary = new SortedDictionary<int, int[]>();

                int[] nuevosResultados = new int[2];
                nuevosResultados[0] = 1;
                nuevosResultados[1] = 0;
                numeroJugadoresDictionary.Add(numeroJugadores, nuevosResultados);

                juegosResultadosDictionary.Add(idJuego, numeroJugadoresDictionary);
            }
        }
        else
        {
            if (juegosResultadosDictionary.ContainsKey(idJuego))
            {
                SortedDictionary<int, int[]> numeroJugadoresDictionary;
                juegosResultadosDictionary.TryGetValue(idJuego, out numeroJugadoresDictionary);

                if (numeroJugadoresDictionary.ContainsKey(numeroJugadores))
                {
                    int[] resultadosExistentes;
                    numeroJugadoresDictionary.TryGetValue(numeroJugadores, out resultadosExistentes);
                    resultadosExistentes[1] += 1;
                    numeroJugadoresDictionary[numeroJugadores] = resultadosExistentes;
                }
                else
                {
                    int[] nuevosResultados = new int[2];
                    nuevosResultados[0] = 0;
                    nuevosResultados[1] = 1;
                    numeroJugadoresDictionary.Add(numeroJugadores, nuevosResultados);
                }
                juegosResultadosDictionary[idJuego] = numeroJugadoresDictionary;
            }
            else
            {
                SortedDictionary<int, int[]> numeroJugadoresDictionary = new SortedDictionary<int, int[]>();

                int[] nuevosResultados = new int[2];
                nuevosResultados[0] = 0;
                nuevosResultados[1] = 1;
                numeroJugadoresDictionary.Add(numeroJugadores, nuevosResultados);

                juegosResultadosDictionary.Add(idJuego, numeroJugadoresDictionary);
            }
        }
    }

    public SortedDictionary<int, int[]> obtenerJuegosPerdidos(String idJuego)
    {
        SortedDictionary<int, int[]> numeroJugadoresDictionary = new SortedDictionary<int, int[]>();
        juegosResultadosDictionary.TryGetValue(idJuego, out numeroJugadoresDictionary);

        return numeroJugadoresDictionary;
    }

    public SortedDictionary<int, int[]> obtenerJuegosGanados(String idJuego)
    {
        SortedDictionary<int, int[]> numeroJugadoresDictionary = new SortedDictionary<int, int[]>();
        juegosResultadosDictionary.TryGetValue(idJuego, out numeroJugadoresDictionary);

        return numeroJugadoresDictionary;
    }

    public void agregarDatosAdversarioJuegoDictionary(String nombreAdversario, String idJuego, bool adversarioGana)
    {
        SortedDictionary<String, int[]> valorDic;
        //int[0] son las veces que ha ganado
        //int[1] son las veces que ha perdido
        adversariosJuegosDictionary.TryGetValue(idJuego, out valorDic);
        if (valorDic == null)
        {
            valorDic = new SortedDictionary<String, int[]>();
        }

        if (adversarioGana)
        {
            if (valorDic.ContainsKey(nombreAdversario))
            {
                int[] array;
                valorDic.TryGetValue(nombreAdversario, out array);

                int victoriasAcomuladas = array[0];
                int derrotasAcomuladas = array[1];
                array.SetValue(victoriasAcomuladas + 1, 0);
                array.SetValue(derrotasAcomuladas, 1);

                valorDic[nombreAdversario] = array;
            }
            else
            {
                int[] array = new int[2];
                array.SetValue(1, 0);
                array.SetValue(0, 1);

                valorDic.Add(nombreAdversario, array);
            }
        }
        else
        {
            if (valorDic.ContainsKey(nombreAdversario))
            {
                int[] array;
                valorDic.TryGetValue(nombreAdversario, out array);

                int victoriasAcomuladas = array[0];
                int derrotasAcomuladas = array[1];
                array.SetValue(victoriasAcomuladas, 0);
                array.SetValue(derrotasAcomuladas + 1, 1);

                valorDic[nombreAdversario] = array;
            }
            else
            {
                int[] array = new int[2];
                array.SetValue(0, 0);
                array.SetValue(1, 1);

                valorDic.Add(nombreAdversario, array);
            }
        }

        if (adversariosJuegosDictionary.ContainsKey(idJuego))
        {
            adversariosJuegosDictionary[idJuego] = valorDic;
        }
        else
        {
            adversariosJuegosDictionary.Add(idJuego, valorDic);
        }
    }

    public void agregarAdversariosNombresDictionary(String nombreAdversario, String idJuego, bool adversarioGana)
    {
        SortedDictionary<String, int[]> valorDic;
        //int[0] son las veces que ha ganado
        //int[1] son las veces que ha perdido

        adversariosNombresDictionary.TryGetValue(nombreAdversario, out valorDic);
        if (valorDic == null)
        {
            valorDic = new SortedDictionary<String, int[]>();
        }

        if (adversarioGana)
        {
            if (valorDic.ContainsKey(idJuego))
            {
                int[] array;
                valorDic.TryGetValue(idJuego, out array);

                int victoriasAcomuladas = array[0];
                int derrotasAcomuladas = array[1];
                array.SetValue(victoriasAcomuladas + 1, 0);
                array.SetValue(derrotasAcomuladas, 1);

                valorDic[idJuego] = array;
            }
            else
            {
                int[] array = new int[2];
                array.SetValue(1, 0);
                array.SetValue(0, 1);

                valorDic.Add(idJuego, array);
            }
        }
        else
        {
            if (valorDic.ContainsKey(idJuego))
            {
                int[] array;
                valorDic.TryGetValue(idJuego, out array);

                int victoriasAcomuladas = array[0];
                int derrotasAcomuladas = array[1];
                array.SetValue(victoriasAcomuladas, 0);
                array.SetValue(derrotasAcomuladas + 1, 1);

                valorDic[idJuego] = array;
            }
            else
            {
                int[] array = new int[2];
                array.SetValue(0, 0);
                array.SetValue(1, 1);

                valorDic.Add(idJuego, array);
            }
        }

        if (adversariosNombresDictionary.ContainsKey(nombreAdversario))
        {
            adversariosNombresDictionary[nombreAdversario] = valorDic;
        }
        else
        {
            adversariosNombresDictionary.Add(nombreAdversario, valorDic);
        }
    }

    public void añadirResultadosJuegoCooperativo_adversariosJuegosDictionary(Juego objetoJuego, ColeccionJuegosUsuario objetoClaseColeccion)
    {
        if (objetoJuego.esCooperativo)
        {
            SortedDictionary<string, int[]> valorAdversariosJuegosDictionary = new SortedDictionary<string, int[]>();

            /*
            adversariosJuegosDictionary.TryGetValue(objetoJuego.idJuego, out valorAdversariosJuegosDictionary);

            if (valorAdversariosJuegosDictionary == null)
            {
                valorAdversariosJuegosDictionary = new SortedDictionary<string, int[]>();
            }
            */
            SortedDictionary<int, int[]> resultadosDictionary;

            juegosResultadosDictionary.TryGetValue(objetoJuego.idJuego, out resultadosDictionary);
            int totalDerrotas = 0;
            int totalVictorias = 0;
            if (resultadosDictionary != null)
            {
                foreach (var arrayResultadosExistentes in resultadosDictionary)
                {
                    totalDerrotas += arrayResultadosExistentes.Value[0];
                    totalVictorias += arrayResultadosExistentes.Value[1];
                }
                int[] arrayNuevosResultados = new int[2];
                arrayNuevosResultados[0] = totalVictorias;
                arrayNuevosResultados[1] = totalDerrotas;

                valorAdversariosJuegosDictionary.Add(objetoJuego.tituloJuego, arrayNuevosResultados);
                adversariosJuegosDictionary[objetoJuego.idJuego] = valorAdversariosJuegosDictionary;


                SortedDictionary<string, int[]> valorAdversariosNombresDictionary = new SortedDictionary<string, int[]>();
                valorAdversariosNombresDictionary.Add(objetoJuego.idJuego, new int[2]);
                adversariosNombresDictionary.Add(objetoJuego.tituloJuego, valorAdversariosNombresDictionary);
            }
        }
    }

    public void añadiJuegoCooperativo_adversariosJuegosDictionary(String idJuego)
    {
        SortedDictionary<string, int[]> valorAdversariosJuegosDictionary = new SortedDictionary<string, int[]>();

        adversariosJuegosDictionary.Add(idJuego, valorAdversariosJuegosDictionary);
    }
}//Fin class
