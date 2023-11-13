<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAltaVenta
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAltaVenta))
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridCorreos = New System.Windows.Forms.DataGridView()
        Me.ColumnaId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnaCorreo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cantidad = New System.Windows.Forms.NumericUpDown()
        Me.cbProductos = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.DataGridProducto = New System.Windows.Forms.DataGridView()
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Nombre = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Precio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Cant = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnCargarProducto = New System.Windows.Forms.Button()
        Me.LimpiarFiltros = New System.Windows.Forms.Button()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.txtCorreo = New System.Windows.Forms.TextBox()
        Me.btnCrearVenta = New System.Windows.Forms.Button()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.DataGridCorreos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cantidad, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.DataGridProducto, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.DataGridCorreos, 0, 0)
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(12, 21)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 234.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(157, 234)
        Me.TableLayoutPanel2.TabIndex = 29
        '
        'DataGridCorreos
        '
        Me.DataGridCorreos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridCorreos.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridCorreos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.DataGridCorreos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridCorreos.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridCorreos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridCorreos.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnaId, Me.ColumnaCorreo})
        Me.DataGridCorreos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridCorreos.EnableHeadersVisualStyles = False
        Me.DataGridCorreos.Location = New System.Drawing.Point(3, 3)
        Me.DataGridCorreos.Name = "DataGridCorreos"
        Me.DataGridCorreos.ReadOnly = True
        Me.DataGridCorreos.RowHeadersVisible = False
        Me.DataGridCorreos.Size = New System.Drawing.Size(151, 228)
        Me.DataGridCorreos.TabIndex = 0
        '
        'ColumnaId
        '
        Me.ColumnaId.FillWeight = 40.60914!
        Me.ColumnaId.HeaderText = "ID"
        Me.ColumnaId.Name = "ColumnaId"
        Me.ColumnaId.ReadOnly = True
        '
        'ColumnaCorreo
        '
        Me.ColumnaCorreo.FillWeight = 159.3909!
        Me.ColumnaCorreo.HeaderText = "Correo"
        Me.ColumnaCorreo.Name = "ColumnaCorreo"
        Me.ColumnaCorreo.ReadOnly = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(379, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Cantidad"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(256, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Producto"
        '
        'cantidad
        '
        Me.cantidad.Location = New System.Drawing.Point(368, 21)
        Me.cantidad.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.cantidad.Name = "cantidad"
        Me.cantidad.Size = New System.Drawing.Size(78, 20)
        Me.cantidad.TabIndex = 43
        Me.cantidad.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cbProductos
        '
        Me.cbProductos.FormattingEnabled = True
        Me.cbProductos.Location = New System.Drawing.Point(235, 20)
        Me.cbProductos.Name = "cbProductos"
        Me.cbProductos.Size = New System.Drawing.Size(105, 21)
        Me.cbProductos.TabIndex = 42
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.DataGridProducto, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(232, 48)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 207.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(214, 207)
        Me.TableLayoutPanel3.TabIndex = 41
        '
        'DataGridProducto
        '
        Me.DataGridProducto.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridProducto.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridProducto.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised
        Me.DataGridProducto.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridProducto.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridProducto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridProducto.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.Nombre, Me.Precio, Me.Cant})
        Me.DataGridProducto.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridProducto.EnableHeadersVisualStyles = False
        Me.DataGridProducto.Location = New System.Drawing.Point(3, 3)
        Me.DataGridProducto.Name = "DataGridProducto"
        Me.DataGridProducto.ReadOnly = True
        Me.DataGridProducto.RowHeadersVisible = False
        Me.DataGridProducto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridProducto.Size = New System.Drawing.Size(208, 201)
        Me.DataGridProducto.TabIndex = 0
        '
        'ID
        '
        Me.ID.FillWeight = 45.68528!
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.ReadOnly = True
        '
        'Nombre
        '
        Me.Nombre.FillWeight = 195.9124!
        Me.Nombre.HeaderText = "Nombre"
        Me.Nombre.Name = "Nombre"
        Me.Nombre.ReadOnly = True
        '
        'Precio
        '
        Me.Precio.HeaderText = "Precio"
        Me.Precio.Name = "Precio"
        Me.Precio.ReadOnly = True
        '
        'Cant
        '
        Me.Cant.FillWeight = 58.40231!
        Me.Cant.HeaderText = "Cant"
        Me.Cant.Name = "Cant"
        Me.Cant.ReadOnly = True
        '
        'btnCargarProducto
        '
        Me.btnCargarProducto.Location = New System.Drawing.Point(287, 258)
        Me.btnCargarProducto.Name = "btnCargarProducto"
        Me.btnCargarProducto.Size = New System.Drawing.Size(91, 23)
        Me.btnCargarProducto.TabIndex = 46
        Me.btnCargarProducto.Text = "Cargar producto"
        Me.btnCargarProducto.UseVisualStyleBackColor = True
        '
        'LimpiarFiltros
        '
        Me.LimpiarFiltros.BackgroundImage = CType(resources.GetObject("LimpiarFiltros.BackgroundImage"), System.Drawing.Image)
        Me.LimpiarFiltros.Location = New System.Drawing.Point(184, 134)
        Me.LimpiarFiltros.Name = "LimpiarFiltros"
        Me.LimpiarFiltros.Size = New System.Drawing.Size(32, 36)
        Me.LimpiarFiltros.TabIndex = 47
        Me.LimpiarFiltros.UseVisualStyleBackColor = True
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(12, 261)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(100, 20)
        Me.txtId.TabIndex = 48
        '
        'txtCorreo
        '
        Me.txtCorreo.Location = New System.Drawing.Point(12, 287)
        Me.txtCorreo.Name = "txtCorreo"
        Me.txtCorreo.Size = New System.Drawing.Size(100, 20)
        Me.txtCorreo.TabIndex = 49
        '
        'btnCrearVenta
        '
        Me.btnCrearVenta.BackgroundImage = CType(resources.GetObject("btnCrearVenta.BackgroundImage"), System.Drawing.Image)
        Me.btnCrearVenta.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnCrearVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCrearVenta.Location = New System.Drawing.Point(534, 134)
        Me.btnCrearVenta.Name = "btnCrearVenta"
        Me.btnCrearVenta.Size = New System.Drawing.Size(32, 34)
        Me.btnCrearVenta.TabIndex = 50
        Me.btnCrearVenta.UseVisualStyleBackColor = True
        '
        'FrmAltaVenta
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(620, 316)
        Me.Controls.Add(Me.btnCrearVenta)
        Me.Controls.Add(Me.txtCorreo)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.LimpiarFiltros)
        Me.Controls.Add(Me.btnCargarProducto)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cantidad)
        Me.Controls.Add(Me.cbProductos)
        Me.Controls.Add(Me.TableLayoutPanel3)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Name = "FrmAltaVenta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmAltaVenta"
        Me.TableLayoutPanel2.ResumeLayout(False)
        CType(Me.DataGridCorreos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cantidad, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        CType(Me.DataGridProducto, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents DataGridCorreos As DataGridView
    Friend WithEvents ColumnaId As DataGridViewTextBoxColumn
    Friend WithEvents ColumnaCorreo As DataGridViewTextBoxColumn
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cantidad As NumericUpDown
    Friend WithEvents cbProductos As ComboBox
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents DataGridProducto As DataGridView
    Friend WithEvents btnCargarProducto As Button
    Friend WithEvents LimpiarFiltros As Button
    Friend WithEvents txtId As TextBox
    Friend WithEvents txtCorreo As TextBox
    Friend WithEvents btnCrearVenta As Button
    Friend WithEvents ID As DataGridViewTextBoxColumn
    Friend WithEvents Nombre As DataGridViewTextBoxColumn
    Friend WithEvents Precio As DataGridViewTextBoxColumn
    Friend WithEvents Cant As DataGridViewTextBoxColumn
End Class
