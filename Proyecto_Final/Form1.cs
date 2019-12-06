using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Proyecto_Final
{
    public partial class frmPrincipal : Form
    {
        String directorioCache;
        String directorioCacheUsuarios;
        String directorioCacheJuegos;
        String directorioCacheJugadas;

        String usuarioActual;

        ColeccionJuegosUsuario objetoClaseColeccion;
        JugadasJuegos objetoClaseJugadas;
        List<ListViewItem> listaItemsAnterior = new List<ListViewItem>();

        public frmPrincipal()
        {
            InitializeComponent();
        }


        public void asegurarExistenciaDirectorioCache()
        {
            directorioCache = Application.LocalUserAppDataPath;
            directorioCacheUsuarios = directorioCache + "/usuarios/";
            directorioCacheJuegos = directorioCache + "/juegos/";
            directorioCacheJugadas = directorioCache + "/usuarios/jugadas/";
            if (!Directory.Exists(directorioCacheUsuarios))
            {
                Directory.CreateDirectory(directorioCacheUsuarios);
            }
            if (!Directory.Exists(directorioCacheJuegos))
            {
                Directory.CreateDirectory(directorioCacheJuegos);
            }
            if (!Directory.Exists(directorioCacheJugadas))
            {
                Directory.CreateDirectory(directorioCacheJugadas);
            }
        }

        private void btnBuscarUsuario_Click(object sender, EventArgs e)
        {
            if (txtNombreUsuario.TextLength > 0)
            {
                usuarioActual = txtNombreUsuario.Text;
                try
                {
                    Usuario usuario = new Usuario(txtNombreUsuario.Text, directorioCacheUsuarios);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("El usuario no existe.");
                }

                objetoClaseColeccion = new ColeccionJuegosUsuario(directorioCacheJuegos, txtNombreUsuario.Text);

                List<String> idsJuegos = new List<String>();
                foreach (Juego juego in objetoClaseColeccion.listaJuegos)
                {
                    idsJuegos.Add(juego.idJuego);
                }

                objetoClaseJugadas = new JugadasJuegos(directorioCacheJugadas, txtNombreUsuario.Text, idsJuegos);

                foreach (Juego juego in objetoClaseColeccion.listaJuegos)
                {
                    objetoClaseJugadas.añadirResultadosJuegoCooperativo_adversariosJuegosDictionary(juego, objetoClaseColeccion);
                }

                tvArbol.Nodes.Clear();
                lvLista.Clear();

                procesarInformacionUsuario(objetoClaseColeccion);
                txtNombreUsuario.Text = "";

                btnActualizar.Enabled = true;
                lblUsuarioActual.Text = "Usuario Actual: " + usuarioActual;
            }
            else
            {
                MessageBox.Show("Escribe el nombre de un usuario.");
                txtNombreUsuario.Focus();
            }
        }

        public void procesarInformacionUsuario(ColeccionJuegosUsuario coleccion)
        {
            agregarAutoresDictionary(coleccion);
            crearNodoTreeNode("Autores", 0, "Autores", "0");
            añadirDatosNodoTreeNode(coleccion.autoresDictionary, tvArbol.GetNodeAt(0, 0), "autor");

            crearNodoTreeNode("Juegos", 5, "Juegos", "0");

            agregarNumeroJugadoresDictionary(coleccion);
            crearNodoHijoTreeNode("Jugadores", 1, "Jugadores", "0", tvArbol.GetNodeAt(0, 0).NextNode);
            añadirDatosNodoTreeNode(coleccion.numeroJugadoresDictionary, tvArbol.GetNodeAt(0, 0).NextNode.FirstNode);

            agregarMecanicasDictionary(coleccion);
            crearNodoHijoTreeNode("Mecanicas", 2, "Mecanicas", "0", tvArbol.GetNodeAt(0, 0).NextNode);
            añadirDatosNodoTreeNode(coleccion.mecanicasDictionary, tvArbol.GetNodeAt(0, 0).NextNode.FirstNode.NextNode, "mecanica");

            agregarFamiliasDictionary(coleccion);
            crearNodoHijoTreeNode("Familias", 3, "Familias", "0", tvArbol.GetNodeAt(0, 0).NextNode);
            añadirDatosNodoTreeNode(coleccion.familiasDictionary, tvArbol.GetNodeAt(0, 0).NextNode.FirstNode.NextNode.NextNode, "familia");

            agregarCategoriasDictionary(coleccion);
            crearNodoHijoTreeNode("Categorias", 4, "Categorias", "0", tvArbol.GetNodeAt(0, 0).NextNode);
            añadirDatosNodoTreeNode(coleccion.categoriasDictionary, tvArbol.GetNodeAt(0, 0).NextNode.FirstNode.NextNode.NextNode.NextNode, "categoria");

            crearNodoTreeNode("Adversarios", 6, "Adversarios", "0");

            crearNodoHijoTreeNode("Juegos", 6, "JuegosAdv", "0", tvArbol.GetNodeAt(0, 0).NextNode.NextNode);
            añadirDatosNodosAdversariosJuegos();

            crearNodoHijoTreeNode("Nombres", 6, "NombresAdv", "0", tvArbol.GetNodeAt(0, 0).NextNode.NextNode);
            añadirDatosNodosAdversariosNombres();

            crearNodoTreeNode("Resumen General", 7, "Resumen", "0");

            agregarDatosListView(tvArbol.Nodes);
            objetoClaseColeccion.obtenerAutorMasJuegos();
        }

        public void crearNodoTreeNode(String nombreNodo, int indexImagen, String name, String tag)
        {
            TreeNode nodo;

            nodo = new TreeNode(nombreNodo, indexImagen, indexImagen);
            nodo.Name = name;
            nodo.Tag = tag;

            tvArbol.Nodes.Add(nodo);
        }
        public void crearNodoHijoTreeNode(String nombreNodoHijo, int indexImagen, String name, String tag, TreeNode nodoRaiz)
        {
            TreeNode nodoHijo;

            nodoHijo = new TreeNode(nombreNodoHijo, indexImagen, indexImagen);
            nodoHijo.Name = name;
            nodoHijo.Tag = tag;

            nodoRaiz.Nodes.Add(nodoHijo);
        }

        public void añadirDatosNodoTreeNode(SortedDictionary<int, List<Juego>> diccionario, TreeNode ubicacionNodo)
        {
            foreach (var numeroJugadores in diccionario)
            {
                TreeNode nodoRaiz = ubicacionNodo;
                TreeNode hijoNodo;

                hijoNodo = new TreeNode(numeroJugadores.Key.ToString(), 1, 1);
                hijoNodo.Name = "jugadores" + numeroJugadores.Key.ToString();
                hijoNodo.Tag = "0";

                List<Juego> listaJuegos = numeroJugadores.Value;
                foreach (var juego in listaJuegos)
                {
                    String tituloJuego = juego.tituloJuego;

                    agregarImagenesImageList(juego.obtenerImagen(directorioCacheJuegos, juego.idJuego), juego.idJuego);

                    TreeNode nietoJugadores = new TreeNode(tituloJuego);
                    nietoJugadores.Tag = juego.idJuego;
                    nietoJugadores.Name = tituloJuego;
                    nietoJugadores.ImageIndex = imglImagenes.Images.Count - 1;
                    nietoJugadores.SelectedImageIndex = imglImagenes.Images.Count - 1;
                    hijoNodo.Nodes.Add(nietoJugadores);
                }
                nodoRaiz.Nodes.Add(hijoNodo);
            }
        }

        public void añadirDatosNodoTreeNode(SortedDictionary<String, List<Juego>> diccionario, TreeNode ubicacionNodo, String referencia)
        {
            foreach (var valor in diccionario)
            {
                TreeNode nodoRaiz = ubicacionNodo;
                TreeNode hijoNodo;

                hijoNodo = new TreeNode(valor.Key.ToString(), 1, 1);
                hijoNodo.Name = referencia + valor.Key;
                hijoNodo.Tag = "0";

                List<Juego> listaJuegos = valor.Value;
                foreach (var juego in listaJuegos)
                {
                    String tituloJuego = juego.tituloJuego;

                    agregarImagenesImageList(juego.obtenerImagen(directorioCacheJuegos, juego.idJuego), juego.idJuego);

                    TreeNode nietoJugadores = new TreeNode(tituloJuego);
                    nietoJugadores.Tag = juego.idJuego;
                    nietoJugadores.Name = tituloJuego;
                    nietoJugadores.ImageIndex = imglImagenes.Images.Count - 1;
                    nietoJugadores.SelectedImageIndex = imglImagenes.Images.Count - 1;
                    hijoNodo.Nodes.Add(nietoJugadores);
                }
                nodoRaiz.Nodes.Add(hijoNodo);
            }
        }

        public void añadirDatosNodosAdversariosJuegos()
        {
            Juego juego = null;
            foreach (var idJuego in objetoClaseJugadas.adversariosJuegosDictionary)
            {
                foreach (var objetoJuego in objetoClaseColeccion.listaJuegos)
                {
                    if (objetoJuego.idJuego.Equals(idJuego.Key))
                    {
                        juego = objetoJuego;
                    }
                }

                TreeNode nodoAdversarios_Juegos = tvArbol.GetNodeAt(0, 0).NextNode.NextNode.FirstNode;
                TreeNode Juego_hijoAdversarios_Juegos;

                Juego_hijoAdversarios_Juegos = new TreeNode(juego.tituloJuego);
                Juego_hijoAdversarios_Juegos.Name = "advjuego" + juego.idJuego;
                Juego_hijoAdversarios_Juegos.Tag = "0";

                agregarImagenesImageList(juego.obtenerImagen(directorioCacheJuegos, juego.idJuego), juego.idJuego);

                Juego_hijoAdversarios_Juegos.ImageIndex = imglImagenes.Images.Count - 1;
                Juego_hijoAdversarios_Juegos.SelectedImageIndex = imglImagenes.Images.Count - 1;

                nodoAdversarios_Juegos.Nodes.Add(Juego_hijoAdversarios_Juegos);

                SortedDictionary<String, int[]> valorIdJuegoDictionary = idJuego.Value;
                foreach (var nombreAdversario in valorIdJuegoDictionary)
                {
                    TreeNode nombreAdversario_nietoAdversarios_Juegos;
                    TreeNode datos;

                    nombreAdversario_nietoAdversarios_Juegos = new TreeNode(nombreAdversario.Key, 8, 8);
                    nombreAdversario_nietoAdversarios_Juegos.Name = nombreAdversario.Key;
                    nombreAdversario_nietoAdversarios_Juegos.Tag = "0";

                    int[] array = nombreAdversario.Value;

                    int victorias = array[0];
                    int derrotas = array[1];

                    datos = new TreeNode("Victorias: " + victorias + " Derrotas: " + derrotas, 7, 7);
                    datos.Name = "Victorias: " + victorias + " Derrotas: " + derrotas;
                    datos.Tag = "-1";

                    nombreAdversario_nietoAdversarios_Juegos.Nodes.Add(datos);

                    Juego_hijoAdversarios_Juegos.Nodes.Add(nombreAdversario_nietoAdversarios_Juegos);
                }
            }
        }

        public void añadirDatosNodosAdversariosNombres()
        {
            Juego juego = null;
            foreach (var nombreAdversario in objetoClaseJugadas.adversariosNombresDictionary)
            {
                TreeNode nodoAdversarios_Nombres = tvArbol.GetNodeAt(0, 0).NextNode.NextNode.FirstNode.NextNode;
                TreeNode hijoAdversarios_Nombres;

                hijoAdversarios_Nombres = new TreeNode(nombreAdversario.Key, 8, 8);
                hijoAdversarios_Nombres.Name = "advnombre" + nombreAdversario.Key;
                hijoAdversarios_Nombres.Tag = "0";

                nodoAdversarios_Nombres.Nodes.Add(hijoAdversarios_Nombres);

                SortedDictionary<String, int[]> valorNombreAdversarioDictionary = nombreAdversario.Value;
                foreach (var idJuego in valorNombreAdversarioDictionary)
                {
                    foreach (var objetoJuego in objetoClaseColeccion.listaJuegos)
                    {
                        if (objetoJuego.idJuego.Equals(idJuego.Key))
                        {
                            juego = objetoJuego;
                            break;
                        }
                    }
                    TreeNode nietoAdversarios_Juegos;

                    nietoAdversarios_Juegos = new TreeNode(juego.tituloJuego, 4, 4);
                    nietoAdversarios_Juegos.Name = juego.tituloJuego;
                    nietoAdversarios_Juegos.Tag = juego.idJuego;

                    agregarImagenesImageList(juego.obtenerImagen(directorioCacheJuegos, juego.idJuego), juego.idJuego);

                    nietoAdversarios_Juegos.ImageIndex = imglImagenes.Images.Count - 1;
                    nietoAdversarios_Juegos.SelectedImageIndex = imglImagenes.Images.Count - 1;

                    hijoAdversarios_Nombres.Nodes.Add(nietoAdversarios_Juegos);
                }
            }
        }

        public void agregarAutoresDictionary(ColeccionJuegosUsuario coleccion)
        {
            foreach (var coleccionJuego in coleccion.listaJuegos)
            {
                Juego juego = new Juego(coleccionJuego.idJuego, directorioCacheJuegos);

                String autores = "";
                foreach (var autor in juego.autorJuego)
                {
                    autores += autor + "\n";
                    coleccion.agregarAutoresDictionary(autor, juego);
                }

                String autorJuego = autores;
                String idJuego = juego.idJuego;
                String tituloJuego = juego.tituloJuego;


                objetoClaseColeccion.agregarIdsDictionary(idJuego, juego);
            }
        }

        public void agregarMecanicasDictionary(ColeccionJuegosUsuario coleccion)
        {
            foreach (var coleccionJuego in coleccion.listaJuegos)
            {
                Juego juego = new Juego(coleccionJuego.idJuego, directorioCacheJuegos);

                List<String> listaMecanicas = juego.listaMecanicas;
                foreach (var mecanica in listaMecanicas)
                {
                    coleccion.agregarMecanicasDictionary(mecanica, juego);
                }
            }
        }

        public void agregarFamiliasDictionary(ColeccionJuegosUsuario coleccion)
        {
            foreach (var coleccionJuego in coleccion.listaJuegos)
            {
                Juego juego = new Juego(coleccionJuego.idJuego, directorioCacheJuegos);

                List<String> listaFamilias = juego.listaFamilias;
                foreach (var familia in listaFamilias)
                {
                    coleccion.agregarFamiliasDictionary(familia, juego);
                }
            }
        }

        public void agregarCategoriasDictionary(ColeccionJuegosUsuario coleccion)
        {
            foreach (var coleccionJuego in coleccion.listaJuegos)
            {
                Juego juego = new Juego(coleccionJuego.idJuego, directorioCacheJuegos);

                List<String> listaCategorias = juego.listaCategorias;
                foreach (var categoria in listaCategorias)
                {
                    coleccion.agregarCategoriasDictionary(categoria, juego);
                }
            }
        }

        public void agregarNumeroJugadoresDictionary(ColeccionJuegosUsuario coleccion)
        {
            foreach (var coleccionJuego in coleccion.listaJuegos)
            {
                Juego juego = new Juego(coleccionJuego.idJuego, directorioCacheJuegos);
                int numMinJugadores = Int32.Parse(juego.minJugadoresJuego);
                int numMaxJugadores = Int32.Parse(juego.maxJugadoresJuego);

                for (int jugadores = numMinJugadores; jugadores <= numMaxJugadores; jugadores++)
                {
                    coleccion.agregarNumeroJugadoresDictionary(jugadores, juego);
                }
            }
        }

        public void agregarImagenesImageList(Image portadaJuego, String nombrePortada)
        {
            imglImagenes.Images.Add(nombrePortada, portadaJuego);
        }

        public void contarNodos(TreeNode nodo)
        {
            lblDescripcionEncontrados.Visible = true;
            lblNumeroEncontrados.Visible = true;

            int numeroNodos = nodo.Nodes.Count;

            lblNumeroEncontrados.Text = numeroNodos + " coincidencias encontradas en " + nodo.Name;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            asegurarExistenciaDirectorioCache();
        }

        private void tvArbol_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            pbPortadaJuego.Visible = false;
            lblAutorJuego.Visible = false;
            lblNombreJuego.Visible = false;
            lblIlustradorJuego.Visible = false;
            lblNumeroVecesJugadoJuego.Visible = false;
            lblTituloAutor.Visible = false;
            lblTituloIlustrador.Visible = false;
            lblTituloNombre.Visible = false;
            lblTituloNumeroVecesJugado.Visible = false;
            lblTituloJuegosPerdidos.Visible = false;
            lblTituloJuegosGanados.Visible = false;
            listBoxJuegosGanados.Visible = false;
            listBoxJuegosPerdidos.Visible = false;
        }

        public void mostrarJuego(String idJuego)
        {
            Juego juego = objetoClaseColeccion.obtenerObjetoJuego(idJuego);
            pbPortadaJuego.Image = juego.obtenerImagen(directorioCacheJuegos, juego.idJuego);
            lblNombreJuego.Text = juego.tituloJuego;

            String autores = "";
            foreach (var autor in juego.autorJuego)
            {
                autores += autor + "\n";
            }

            lblAutorJuego.Text = autores;
            lblIlustradorJuego.Text = juego.ilustradorJuego;

            listBoxJuegosGanados.Items.Clear();
            listBoxJuegosPerdidos.Items.Clear();

            String numeroVecesJugado;
            objetoClaseJugadas.vecesJugadoDictionary.TryGetValue(idJuego, out numeroVecesJugado);
            lblNumeroVecesJugadoJuego.Text = numeroVecesJugado;

            SortedDictionary<int, int[]> numeroJugadoresDictionaryGanadas = objetoClaseJugadas.obtenerJuegosGanados(idJuego);
            if (numeroJugadoresDictionaryGanadas != null)
            {
                foreach (var item in numeroJugadoresDictionaryGanadas)
                {
                    listBoxJuegosGanados.Items.Add("Ganó " + item.Value[0] + " veces con " + item.Key + " Jugadores");
                }
            }
            else
            {
                listBoxJuegosGanados.Items.Add("El usuario no ha ganado en este juego.");
            }

            SortedDictionary<int, int[]> numeroJugadoresDictionaryPerdidas = objetoClaseJugadas.obtenerJuegosPerdidos(idJuego);
            if (numeroJugadoresDictionaryPerdidas != null)
            {
                foreach (var item in numeroJugadoresDictionaryPerdidas)
                {
                    listBoxJuegosPerdidos.Items.Add("Perdió " + item.Value[1] + " veces con " + item.Key + " Jugadores");
                }
            }
            else
            {
                listBoxJuegosGanados.Items.Add("El usuario no ha perdido en este juego.");
            }

            ponerVisibilidadItems();
        }

        public void ponerVisibilidadItems()
        {
            pbPortadaJuego.Visible = true;
            lblAutorJuego.Visible = true;
            lblNombreJuego.Visible = true;
            lblIlustradorJuego.Visible = true;
            lblNumeroVecesJugadoJuego.Visible = true;
            listBoxJuegosGanados.Visible = true;
            listBoxJuegosPerdidos.Visible = true;

            lblTituloAutor.Visible = true;
            lblTituloIlustrador.Visible = true;
            lblTituloNombre.Visible = true;
            lblTituloJuegosGanados.Visible = true;
            lblTituloJuegosPerdidos.Visible = true;
            lblTituloNumeroVecesJugado.Visible = true;
        }

        public void agregarDatosListView(SortedDictionary<String, List<Juego>> diccionario, int indexImagen, String referencia)
        {
            lvLista.Items.Clear();
            foreach (var dato in diccionario)
            {
                ListViewItem item = new ListViewItem(dato.Key, indexImagen);
                item.Name = referencia + dato.Key;
                item.Tag = "0";

                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosNombresAdvListView(SortedDictionary<String, SortedDictionary<String, int[]>> diccionario, int indexImagen, String referencia)
        {
            lvLista.Items.Clear();
            foreach (var dato in diccionario)
            {
                ListViewItem item = new ListViewItem(dato.Key, indexImagen);
                item.Name = referencia + dato.Key;
                item.Tag = "0";

                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosJuegosAdvListView(SortedDictionary<String, SortedDictionary<String, int[]>> diccionario, int indexImagen, String referencia)
        {
            lvLista.Items.Clear();
            foreach (var dato in diccionario)
            {
                Juego juego = null;
                foreach (var j in objetoClaseColeccion.listaJuegos)
                {
                    if (dato.Key.Equals(j.idJuego))
                    {
                        juego = j;
                        break;
                    }
                }

                ListViewItem item = new ListViewItem(juego.tituloJuego, indexImagen);
                item.Name = referencia + juego.idJuego;
                item.Tag = "0";

                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosListView(SortedDictionary<int, List<Juego>> diccionario, int indexImagen, String referencia)
        {
            lvLista.Items.Clear();
            foreach (var dato in diccionario)
            {
                ListViewItem item = new ListViewItem(dato.Key.ToString(), indexImagen);
                item.Name = referencia + dato.Key.ToString();
                item.Tag = "0";

                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosListView(TreeNode nodo)
        {
            lvLista.Items.Clear();
            foreach (TreeNode subnodo in nodo.Nodes)
            {
                ListViewItem item = new ListViewItem(subnodo.Text, subnodo.ImageIndex);
                item.Name = subnodo.Name;
                item.Tag = subnodo.Tag;

                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosListView(TreeNodeCollection nodos)
        {
            lvLista.Items.Clear();
            foreach (TreeNode nodo in nodos)
            {
                ListViewItem item = new ListViewItem(nodo.Name, nodo.ImageIndex);
                item.Name = nodo.Name;
                item.Tag = "0";

                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosListView(List<Juego> listaJuegos)
        {
            lvLista.Items.Clear();
            foreach (var juego in listaJuegos)
            {
                ListViewItem item = new ListViewItem(juego.tituloJuego, imglImagenes.Images.IndexOfKey(juego.idJuego));
                item.Name = juego.tituloJuego;
                item.Tag = juego.idJuego;
                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosListView(SortedDictionary<String, int[]> valorDic)
        {
            lvLista.Items.Clear();
            foreach (var llave in valorDic)
            {
                Juego juego = null;
                foreach (var juegoLista in objetoClaseColeccion.listaJuegos)
                {
                    if (llave.Key.Equals(juegoLista.idJuego))
                    {
                        juego = juegoLista;
                        break;
                    }
                }

                agregarImagenesImageList(juego.obtenerImagen(directorioCacheJuegos, juego.idJuego), juego.idJuego);

                ListViewItem item = new ListViewItem(juego.tituloJuego);
                item.Name = juego.tituloJuego;
                item.Tag = juego.idJuego;
                item.ImageIndex = imglImagenes.Images.Count - 1;
                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosListView(SortedDictionary<String, int[]> valorDic, String referencia, int indexImagen, String tag)
        {
            lvLista.Items.Clear();
            foreach (var llave in valorDic)
            {
                ListViewItem item = new ListViewItem(llave.Key, indexImagen);
                item.Name = referencia;
                item.Tag = tag;
                lvLista.Items.Add(item);
            }
        }

        public void agregarDatosListView(int[] arreglo)
        {
            lvLista.Items.Clear();

            ListViewItem item = new ListViewItem("Victorias: " + arreglo[0] + " Derrotas: " + arreglo[1], 7);
            item.Tag = "0";
            lvLista.Items.Add(item);
        }

        public void agregarDatosListView(String titulo, int indexImagen, String tag)
        {
            ListViewItem item = new ListViewItem(titulo, indexImagen);
            item.Name = titulo;
            item.Tag = tag;
            lvLista.Items.Add(item);
        }

        public void mostrarResumen()
        {
            String tituloJuego = "";
            foreach (var juegoLista in objetoClaseColeccion.listaJuegos)
            {
                if (objetoClaseJugadas.idJuegoMasJugado.Equals(juegoLista.idJuego))
                {
                    tituloJuego = juegoLista.tituloJuego;
                    break;
                }
            }
            MessageBox.Show("RESUMEN GENERAL:\n" + "- Autor con mas juegos: " + objetoClaseColeccion.autorConMasJuegos
                + "\n-Total de juegos de ese autor: " + objetoClaseColeccion.numeroJuegosAutorConMasJuegos
                + "\n-Juego mas jugado: " + tituloJuego
                + "\n-Total de jugadas en ese juego: " + objetoClaseJugadas.numeroJugadasJuegoMasJugado
                + "\n-Adversario con mas habilidad: " + objetoClaseJugadas.mejorAdversario
                + "\n-Adversario con menos habilidad: " + objetoClaseJugadas.peorAdversario);
        }

        private void lvLista_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListViewItem item = e.Item;
            if (item.Selected)
            {
                if (!e.Item.Tag.Equals("-1"))
                {
                    if (e.Item.Name.Equals("Resumen"))
                    {
                        mostrarResumen();
                    }
                    else if (!e.Item.Tag.Equals("0"))
                    {
                        mostrarJuego(e.Item.Tag.ToString());
                    }
                    else if (e.Item.Name.Equals("Autores"))
                    {
                        agregarDatosListView(objetoClaseColeccion.autoresDictionary, 0, "autor");
                    }
                    else if (e.Item.Name.StartsWith("autor"))
                    {
                        String nombreAutor = e.Item.Name.Substring(5);
                        List<Juego> listaJuegos;
                        objetoClaseColeccion.autoresDictionary.TryGetValue(nombreAutor, out listaJuegos);
                        agregarDatosListView(listaJuegos);
                    }
                    else if (e.Item.Name.Equals("Juegos"))
                    {
                        lvLista.Items.Clear();
                        agregarDatosListView("Jugadores", 1, "0");
                        agregarDatosListView("Mecanicas", 2, "0");
                        agregarDatosListView("Familias", 3, "0");
                        agregarDatosListView("Categorias", 4, "0");
                    }
                    else if (e.Item.Name.Equals("Jugadores"))
                    {
                        agregarDatosListView(objetoClaseColeccion.numeroJugadoresDictionary, 1, "jugadores");
                    }
                    else if (e.Item.Name.StartsWith("jugadores"))
                    {
                        int numeroJugadores = Int32.Parse(e.Item.Name.Substring(9));
                        List<Juego> listaJuegos;
                        objetoClaseColeccion.numeroJugadoresDictionary.TryGetValue(numeroJugadores, out listaJuegos);
                        agregarDatosListView(listaJuegos);
                    }
                    else if (e.Item.Name.Equals("Mecanicas"))
                    {
                        agregarDatosListView(objetoClaseColeccion.mecanicasDictionary, 2, "mecanica");
                    }
                    else if (e.Item.Name.StartsWith("mecanica"))
                    {
                        String mecanica = e.Item.Name.Substring(8);
                        List<Juego> listaJuegos;
                        objetoClaseColeccion.mecanicasDictionary.TryGetValue(mecanica, out listaJuegos);
                        agregarDatosListView(listaJuegos);
                    }
                    else if (e.Item.Name.Equals("Familias"))
                    {
                        agregarDatosListView(objetoClaseColeccion.familiasDictionary, 3, "familia");
                    }
                    else if (e.Item.Name.StartsWith("familia"))
                    {
                        String familia = e.Item.Name.Substring(7);
                        List<Juego> listaJuegos;
                        objetoClaseColeccion.familiasDictionary.TryGetValue(familia, out listaJuegos);
                        agregarDatosListView(listaJuegos);
                    }
                    else if (e.Item.Name.Equals("Categorias"))
                    {
                        agregarDatosListView(objetoClaseColeccion.categoriasDictionary, 4, "categoria");
                    }
                    else if (e.Item.Name.StartsWith("categoria"))
                    {
                        String categoria = e.Item.Name.Substring(9);
                        List<Juego> listaJuegos;
                        objetoClaseColeccion.categoriasDictionary.TryGetValue(categoria, out listaJuegos);
                        agregarDatosListView(listaJuegos);
                    }
                    else if (e.Item.Name.Equals("Adversarios"))
                    {
                        lvLista.Items.Clear();
                        agregarDatosListView("JuegosAdv", 6, "0");
                        agregarDatosListView("NombresAdv", 6, "0");
                    }
                    else if (e.Item.Name.Equals("JuegosAdv"))
                    {
                        agregarDatosJuegosAdvListView(objetoClaseJugadas.adversariosJuegosDictionary, 6, "advjuego");
                    }
                    else if (e.Item.Name.Contains("advjuego"))
                    {
                        String juego = e.Item.Name.Substring(8);
                        SortedDictionary<String, int[]> listaJuegosDictionary;
                        objetoClaseJugadas.adversariosJuegosDictionary.TryGetValue(juego, out listaJuegosDictionary);

                        Console.WriteLine("Buscando: " + juego);
                        foreach (var a in objetoClaseJugadas.adversariosJuegosDictionary)
                        {
                            Console.WriteLine(a.Key);
                        }

                        agregarDatosListView(listaJuegosDictionary, "advpersona" + juego, 8, "0");
                    }
                    else if (e.Item.Name.Contains("advpersona"))
                    {
                        String persona = e.Item.Text;
                        String idJuego = e.Item.Name.Substring(10);
                        int[] resultados;
                        SortedDictionary<String, int[]> listaJuegos;
                        objetoClaseJugadas.adversariosJuegosDictionary.TryGetValue(idJuego, out listaJuegos);
                        listaJuegos.TryGetValue(persona, out resultados);
                        agregarDatosListView(resultados);
                    }
                    else if (e.Item.Name.Equals("NombresAdv"))
                    {
                        agregarDatosNombresAdvListView(objetoClaseJugadas.adversariosNombresDictionary, 8, "advnombre");
                    }
                    else if (e.Item.Name.Contains("advnombre"))
                    {
                        String nombre = e.Item.Name.Substring(9);
                        SortedDictionary<String, int[]> listaJuegosDictionary;
                        objetoClaseJugadas.adversariosNombresDictionary.TryGetValue(nombre, out listaJuegosDictionary);
                        agregarDatosListView(listaJuegosDictionary);
                    }
                }
            }
        }

        private void tvArbol_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag.Equals("0"))
            {
                agregarDatosListView(e.Node);
            }

            contarNodos(e.Node);
        }

        private void tvArbol_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.Tag.Equals("0") && !e.Node.Tag.Equals("-1"))
            {
                mostrarJuego(e.Node.Tag.ToString());
            }
            if (e.Node.Name.Equals("Resumen"))
            {
                mostrarResumen();
            }
        }

        private void tvArbol_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            XmlDocument documentoColeccion;
            XmlDocument documentoJugadas;

            documentoColeccion = Consultas.consultarApiColeccion(usuarioActual);
            documentoColeccion.Save(directorioCacheJuegos + usuarioActual);

            List<String> idsJuegos = new List<String>();
            foreach (Juego juego in objetoClaseColeccion.listaJuegos)
            {
                idsJuegos.Add(juego.idJuego);
            }

            foreach (var idJuego in idsJuegos)
            {
                documentoJugadas = Consultas.consultarApiJugadas(usuarioActual, idJuego);
                Directory.CreateDirectory(directorioCacheJugadas + usuarioActual + "/");
                documentoJugadas.Save(directorioCacheJugadas + usuarioActual + "/" + idJuego);
            }

            tvArbol.Nodes.Clear();
            lvLista.Clear();

            procesarInformacionUsuario(objetoClaseColeccion);
            txtNombreUsuario.Text = "";

            MessageBox.Show("Datos Actualizados");
        }

    }
}
