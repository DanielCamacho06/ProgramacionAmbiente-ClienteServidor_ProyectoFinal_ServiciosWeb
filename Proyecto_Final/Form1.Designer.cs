namespace Proyecto_Final
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.pnlBusqueda = new System.Windows.Forms.Panel();
            this.btnBuscarUsuario = new System.Windows.Forms.Button();
            this.lblNombreUsuario = new System.Windows.Forms.Label();
            this.txtNombreUsuario = new System.Windows.Forms.TextBox();
            this.pnlInformacion = new System.Windows.Forms.Panel();
            this.lblNumeroEncontrados = new System.Windows.Forms.Label();
            this.lblDescripcionEncontrados = new System.Windows.Forms.Label();
            this.tvArbol = new System.Windows.Forms.TreeView();
            this.imglImagenes = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.listBoxJuegosGanados = new System.Windows.Forms.ListBox();
            this.lblTituloJuegosPerdidos = new System.Windows.Forms.Label();
            this.listBoxJuegosPerdidos = new System.Windows.Forms.ListBox();
            this.lblTituloJuegosGanados = new System.Windows.Forms.Label();
            this.lblNumeroVecesJugadoJuego = new System.Windows.Forms.Label();
            this.lblIlustradorJuego = new System.Windows.Forms.Label();
            this.lblTituloNumeroVecesJugado = new System.Windows.Forms.Label();
            this.lblAutorJuego = new System.Windows.Forms.Label();
            this.lblNombreJuego = new System.Windows.Forms.Label();
            this.lblTituloIlustrador = new System.Windows.Forms.Label();
            this.lblTituloAutor = new System.Windows.Forms.Label();
            this.lblTituloNombre = new System.Windows.Forms.Label();
            this.pbPortadaJuego = new System.Windows.Forms.PictureBox();
            this.lvLista = new System.Windows.Forms.ListView();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblUsuarioActual = new System.Windows.Forms.Label();
            this.pnlBusqueda.SuspendLayout();
            this.pnlInformacion.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortadaJuego)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBusqueda
            // 
            this.pnlBusqueda.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlBusqueda.Controls.Add(this.lblUsuarioActual);
            this.pnlBusqueda.Controls.Add(this.btnActualizar);
            this.pnlBusqueda.Controls.Add(this.btnBuscarUsuario);
            this.pnlBusqueda.Controls.Add(this.lblNombreUsuario);
            this.pnlBusqueda.Controls.Add(this.txtNombreUsuario);
            this.pnlBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBusqueda.Location = new System.Drawing.Point(0, 0);
            this.pnlBusqueda.Name = "pnlBusqueda";
            this.pnlBusqueda.Size = new System.Drawing.Size(920, 47);
            this.pnlBusqueda.TabIndex = 1;
            // 
            // btnBuscarUsuario
            // 
            this.btnBuscarUsuario.Location = new System.Drawing.Point(244, 10);
            this.btnBuscarUsuario.Name = "btnBuscarUsuario";
            this.btnBuscarUsuario.Size = new System.Drawing.Size(75, 23);
            this.btnBuscarUsuario.TabIndex = 2;
            this.btnBuscarUsuario.Text = "Buscar";
            this.btnBuscarUsuario.UseVisualStyleBackColor = true;
            this.btnBuscarUsuario.Click += new System.EventHandler(this.btnBuscarUsuario_Click);
            // 
            // lblNombreUsuario
            // 
            this.lblNombreUsuario.AutoSize = true;
            this.lblNombreUsuario.Location = new System.Drawing.Point(46, 15);
            this.lblNombreUsuario.Name = "lblNombreUsuario";
            this.lblNombreUsuario.Size = new System.Drawing.Size(86, 13);
            this.lblNombreUsuario.TabIndex = 1;
            this.lblNombreUsuario.Text = "Nombre Usuario:";
            // 
            // txtNombreUsuario
            // 
            this.txtNombreUsuario.Location = new System.Drawing.Point(138, 12);
            this.txtNombreUsuario.Name = "txtNombreUsuario";
            this.txtNombreUsuario.Size = new System.Drawing.Size(100, 20);
            this.txtNombreUsuario.TabIndex = 0;
            // 
            // pnlInformacion
            // 
            this.pnlInformacion.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlInformacion.Controls.Add(this.lblNumeroEncontrados);
            this.pnlInformacion.Controls.Add(this.lblDescripcionEncontrados);
            this.pnlInformacion.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInformacion.Location = new System.Drawing.Point(0, 496);
            this.pnlInformacion.Name = "pnlInformacion";
            this.pnlInformacion.Size = new System.Drawing.Size(920, 47);
            this.pnlInformacion.TabIndex = 2;
            // 
            // lblNumeroEncontrados
            // 
            this.lblNumeroEncontrados.AutoSize = true;
            this.lblNumeroEncontrados.Location = new System.Drawing.Point(135, 22);
            this.lblNumeroEncontrados.Name = "lblNumeroEncontrados";
            this.lblNumeroEncontrados.Size = new System.Drawing.Size(16, 13);
            this.lblNumeroEncontrados.TabIndex = 1;
            this.lblNumeroEncontrados.Text = "...";
            this.lblNumeroEncontrados.Visible = false;
            // 
            // lblDescripcionEncontrados
            // 
            this.lblDescripcionEncontrados.AutoSize = true;
            this.lblDescripcionEncontrados.Location = new System.Drawing.Point(46, 22);
            this.lblDescripcionEncontrados.Name = "lblDescripcionEncontrados";
            this.lblDescripcionEncontrados.Size = new System.Drawing.Size(70, 13);
            this.lblDescripcionEncontrados.TabIndex = 0;
            this.lblDescripcionEncontrados.Text = "Encontrados:";
            this.lblDescripcionEncontrados.Visible = false;
            // 
            // tvArbol
            // 
            this.tvArbol.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvArbol.ImageIndex = 0;
            this.tvArbol.ImageList = this.imglImagenes;
            this.tvArbol.Location = new System.Drawing.Point(0, 47);
            this.tvArbol.Name = "tvArbol";
            this.tvArbol.SelectedImageKey = "juegosmesa.png";
            this.tvArbol.Size = new System.Drawing.Size(160, 449);
            this.tvArbol.TabIndex = 3;
            this.tvArbol.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvArbol_BeforeCollapse);
            this.tvArbol.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvArbol_BeforeExpand);
            this.tvArbol.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvArbol_AfterSelect);
            this.tvArbol.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvArbol_NodeMouseDoubleClick);
            // 
            // imglImagenes
            // 
            this.imglImagenes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglImagenes.ImageStream")));
            this.imglImagenes.TransparentColor = System.Drawing.Color.Transparent;
            this.imglImagenes.Images.SetKeyName(0, "autor.jpg");
            this.imglImagenes.Images.SetKeyName(1, "jugadores.png");
            this.imglImagenes.Images.SetKeyName(2, "mecanicas.png");
            this.imglImagenes.Images.SetKeyName(3, "familias.png");
            this.imglImagenes.Images.SetKeyName(4, "categoria.png");
            this.imglImagenes.Images.SetKeyName(5, "juegosmesa.png");
            this.imglImagenes.Images.SetKeyName(6, "adversarios.png");
            this.imglImagenes.Images.SetKeyName(7, "dato.png");
            this.imglImagenes.Images.SetKeyName(8, "persona.png");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Controls.Add(this.listBoxJuegosGanados);
            this.panel1.Controls.Add(this.lblTituloJuegosPerdidos);
            this.panel1.Controls.Add(this.listBoxJuegosPerdidos);
            this.panel1.Controls.Add(this.lblTituloJuegosGanados);
            this.panel1.Controls.Add(this.lblNumeroVecesJugadoJuego);
            this.panel1.Controls.Add(this.lblIlustradorJuego);
            this.panel1.Controls.Add(this.lblTituloNumeroVecesJugado);
            this.panel1.Controls.Add(this.lblAutorJuego);
            this.panel1.Controls.Add(this.lblNombreJuego);
            this.panel1.Controls.Add(this.lblTituloIlustrador);
            this.panel1.Controls.Add(this.lblTituloAutor);
            this.panel1.Controls.Add(this.lblTituloNombre);
            this.panel1.Controls.Add(this.pbPortadaJuego);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(590, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 449);
            this.panel1.TabIndex = 6;
            // 
            // listBoxJuegosGanados
            // 
            this.listBoxJuegosGanados.FormattingEnabled = true;
            this.listBoxJuegosGanados.Location = new System.Drawing.Point(107, 339);
            this.listBoxJuegosGanados.Name = "listBoxJuegosGanados";
            this.listBoxJuegosGanados.Size = new System.Drawing.Size(200, 43);
            this.listBoxJuegosGanados.TabIndex = 15;
            this.listBoxJuegosGanados.Visible = false;
            // 
            // lblTituloJuegosPerdidos
            // 
            this.lblTituloJuegosPerdidos.AutoSize = true;
            this.lblTituloJuegosPerdidos.Enabled = false;
            this.lblTituloJuegosPerdidos.Location = new System.Drawing.Point(20, 402);
            this.lblTituloJuegosPerdidos.Name = "lblTituloJuegosPerdidos";
            this.lblTituloJuegosPerdidos.Size = new System.Drawing.Size(88, 13);
            this.lblTituloJuegosPerdidos.TabIndex = 13;
            this.lblTituloJuegosPerdidos.Text = "Juegos Perdidos:";
            this.lblTituloJuegosPerdidos.Visible = false;
            // 
            // listBoxJuegosPerdidos
            // 
            this.listBoxJuegosPerdidos.FormattingEnabled = true;
            this.listBoxJuegosPerdidos.Location = new System.Drawing.Point(107, 388);
            this.listBoxJuegosPerdidos.Name = "listBoxJuegosPerdidos";
            this.listBoxJuegosPerdidos.Size = new System.Drawing.Size(200, 43);
            this.listBoxJuegosPerdidos.TabIndex = 16;
            this.listBoxJuegosPerdidos.Visible = false;
            // 
            // lblTituloJuegosGanados
            // 
            this.lblTituloJuegosGanados.AutoSize = true;
            this.lblTituloJuegosGanados.Enabled = false;
            this.lblTituloJuegosGanados.Location = new System.Drawing.Point(19, 351);
            this.lblTituloJuegosGanados.Name = "lblTituloJuegosGanados";
            this.lblTituloJuegosGanados.Size = new System.Drawing.Size(90, 13);
            this.lblTituloJuegosGanados.TabIndex = 11;
            this.lblTituloJuegosGanados.Text = "Juegos Ganados:";
            this.lblTituloJuegosGanados.Visible = false;
            // 
            // lblNumeroVecesJugadoJuego
            // 
            this.lblNumeroVecesJugadoJuego.AutoSize = true;
            this.lblNumeroVecesJugadoJuego.Enabled = false;
            this.lblNumeroVecesJugadoJuego.Location = new System.Drawing.Point(184, 291);
            this.lblNumeroVecesJugadoJuego.Name = "lblNumeroVecesJugadoJuego";
            this.lblNumeroVecesJugadoJuego.Size = new System.Drawing.Size(16, 13);
            this.lblNumeroVecesJugadoJuego.TabIndex = 9;
            this.lblNumeroVecesJugadoJuego.Text = "...";
            this.lblNumeroVecesJugadoJuego.Visible = false;
            // 
            // lblIlustradorJuego
            // 
            this.lblIlustradorJuego.AutoSize = true;
            this.lblIlustradorJuego.Enabled = false;
            this.lblIlustradorJuego.Location = new System.Drawing.Point(146, 260);
            this.lblIlustradorJuego.Name = "lblIlustradorJuego";
            this.lblIlustradorJuego.Size = new System.Drawing.Size(16, 13);
            this.lblIlustradorJuego.TabIndex = 6;
            this.lblIlustradorJuego.Text = "...";
            this.lblIlustradorJuego.Visible = false;
            // 
            // lblTituloNumeroVecesJugado
            // 
            this.lblTituloNumeroVecesJugado.AutoSize = true;
            this.lblTituloNumeroVecesJugado.Enabled = false;
            this.lblTituloNumeroVecesJugado.Location = new System.Drawing.Point(11, 291);
            this.lblTituloNumeroVecesJugado.Name = "lblTituloNumeroVecesJugado";
            this.lblTituloNumeroVecesJugado.Size = new System.Drawing.Size(129, 13);
            this.lblTituloNumeroVecesJugado.TabIndex = 8;
            this.lblTituloNumeroVecesJugado.Text = "Número de veces jugado:";
            this.lblTituloNumeroVecesJugado.Visible = false;
            // 
            // lblAutorJuego
            // 
            this.lblAutorJuego.AutoSize = true;
            this.lblAutorJuego.Enabled = false;
            this.lblAutorJuego.Location = new System.Drawing.Point(146, 219);
            this.lblAutorJuego.Name = "lblAutorJuego";
            this.lblAutorJuego.Size = new System.Drawing.Size(16, 13);
            this.lblAutorJuego.TabIndex = 5;
            this.lblAutorJuego.Text = "...";
            this.lblAutorJuego.Visible = false;
            // 
            // lblNombreJuego
            // 
            this.lblNombreJuego.AutoSize = true;
            this.lblNombreJuego.Enabled = false;
            this.lblNombreJuego.Location = new System.Drawing.Point(146, 176);
            this.lblNombreJuego.Name = "lblNombreJuego";
            this.lblNombreJuego.Size = new System.Drawing.Size(16, 13);
            this.lblNombreJuego.TabIndex = 4;
            this.lblNombreJuego.Text = "...";
            this.lblNombreJuego.Visible = false;
            // 
            // lblTituloIlustrador
            // 
            this.lblTituloIlustrador.AutoSize = true;
            this.lblTituloIlustrador.Enabled = false;
            this.lblTituloIlustrador.Location = new System.Drawing.Point(51, 260);
            this.lblTituloIlustrador.Name = "lblTituloIlustrador";
            this.lblTituloIlustrador.Size = new System.Drawing.Size(53, 13);
            this.lblTituloIlustrador.TabIndex = 3;
            this.lblTituloIlustrador.Text = "Ilustrador:";
            this.lblTituloIlustrador.Visible = false;
            // 
            // lblTituloAutor
            // 
            this.lblTituloAutor.AutoSize = true;
            this.lblTituloAutor.Enabled = false;
            this.lblTituloAutor.Location = new System.Drawing.Point(63, 219);
            this.lblTituloAutor.Name = "lblTituloAutor";
            this.lblTituloAutor.Size = new System.Drawing.Size(35, 13);
            this.lblTituloAutor.TabIndex = 2;
            this.lblTituloAutor.Text = "Autor:";
            this.lblTituloAutor.Visible = false;
            // 
            // lblTituloNombre
            // 
            this.lblTituloNombre.AutoSize = true;
            this.lblTituloNombre.Enabled = false;
            this.lblTituloNombre.Location = new System.Drawing.Point(54, 176);
            this.lblTituloNombre.Name = "lblTituloNombre";
            this.lblTituloNombre.Size = new System.Drawing.Size(47, 13);
            this.lblTituloNombre.TabIndex = 1;
            this.lblTituloNombre.Text = "Nombre:";
            this.lblTituloNombre.Visible = false;
            // 
            // pbPortadaJuego
            // 
            this.pbPortadaJuego.Enabled = false;
            this.pbPortadaJuego.Location = new System.Drawing.Point(118, 23);
            this.pbPortadaJuego.Name = "pbPortadaJuego";
            this.pbPortadaJuego.Size = new System.Drawing.Size(100, 122);
            this.pbPortadaJuego.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbPortadaJuego.TabIndex = 0;
            this.pbPortadaJuego.TabStop = false;
            this.pbPortadaJuego.Visible = false;
            // 
            // lvLista
            // 
            this.lvLista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLista.HideSelection = false;
            this.lvLista.LargeImageList = this.imglImagenes;
            this.lvLista.Location = new System.Drawing.Point(160, 47);
            this.lvLista.Name = "lvLista";
            this.lvLista.Size = new System.Drawing.Size(430, 449);
            this.lvLista.SmallImageList = this.imglImagenes;
            this.lvLista.TabIndex = 7;
            this.lvLista.UseCompatibleStateImageBehavior = false;
            this.lvLista.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvLista_ItemSelectionChanged);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Enabled = false;
            this.btnActualizar.Location = new System.Drawing.Point(325, 9);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 3;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // lblUsuarioActual
            // 
            this.lblUsuarioActual.AutoSize = true;
            this.lblUsuarioActual.Enabled = false;
            this.lblUsuarioActual.Location = new System.Drawing.Point(530, 19);
            this.lblUsuarioActual.Name = "lblUsuarioActual";
            this.lblUsuarioActual.Size = new System.Drawing.Size(34, 13);
            this.lblUsuarioActual.TabIndex = 4;
            this.lblUsuarioActual.Text = ".........";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 543);
            this.Controls.Add(this.lvLista);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tvArbol);
            this.Controls.Add(this.pnlInformacion);
            this.Controls.Add(this.pnlBusqueda);
            this.Name = "frmPrincipal";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.pnlBusqueda.ResumeLayout(false);
            this.pnlBusqueda.PerformLayout();
            this.pnlInformacion.ResumeLayout(false);
            this.pnlInformacion.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPortadaJuego)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlBusqueda;
        private System.Windows.Forms.Panel pnlInformacion;
        private System.Windows.Forms.TreeView tvArbol;
        private System.Windows.Forms.Button btnBuscarUsuario;
        private System.Windows.Forms.Label lblNombreUsuario;
        private System.Windows.Forms.TextBox txtNombreUsuario;
        private System.Windows.Forms.ImageList imglImagenes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lvLista;
        private System.Windows.Forms.Label lblIlustradorJuego;
        private System.Windows.Forms.Label lblAutorJuego;
        private System.Windows.Forms.Label lblNombreJuego;
        private System.Windows.Forms.Label lblTituloIlustrador;
        private System.Windows.Forms.Label lblTituloAutor;
        private System.Windows.Forms.Label lblTituloNombre;
        private System.Windows.Forms.PictureBox pbPortadaJuego;
        private System.Windows.Forms.Label lblNumeroEncontrados;
        private System.Windows.Forms.Label lblDescripcionEncontrados;
        private System.Windows.Forms.Label lblNumeroVecesJugadoJuego;
        private System.Windows.Forms.Label lblTituloNumeroVecesJugado;
        private System.Windows.Forms.Label lblTituloJuegosPerdidos;
        private System.Windows.Forms.Label lblTituloJuegosGanados;
        private System.Windows.Forms.ListBox listBoxJuegosGanados;
        private System.Windows.Forms.ListBox listBoxJuegosPerdidos;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label lblUsuarioActual;
    }
}

