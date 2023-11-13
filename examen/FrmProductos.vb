Imports entidades
Imports DAO

Public Class FrmProductos
    Private Sub FrmProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BloquearBotones()

        CargarDataGrid()
    End Sub

    Public Sub BloquearBotones()
        txtId.Visible = False
        LabelId.Visible = False
        btnActualizar.Enabled = False
        btnBorrar.Enabled = False
        btnCrear.Enabled = True
    End Sub

    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        Dim nombre As String = txtNombre.Text
        Dim precio As String = txtPrecio.Text
        Dim categoria As String = txtCategoria.Text

        If (String.IsNullOrEmpty(nombre) Or String.IsNullOrEmpty(categoria)) Or Not IsNumeric(precio) Then
            MessageBox.Show("error")
            Return
        End If

        Dim dao = ProductosDao.ObjetoAcceso()
        Dim listaProductos = dao.GetAll()
        If Productos.VerificarProductoExistente(listaProductos, nombre, categoria) Then
            MessageBox.Show("error. producto existente")
            Return
        End If

        Dim nuevoProducto As New Productos()
        nuevoProducto.nombre = nombre
        nuevoProducto.precio = Convert.ToDecimal(precio)
        nuevoProducto.categoria = categoria

        Dim ultimoID As Integer = dao.Add(nuevoProducto)
        CargarDataGrid()
        LimpiarGroupBox()
        MessageBox.Show(ultimoID)
    End Sub

    Private Sub CargarDataGrid()
        Dim dao = ProductosDao.ObjetoAcceso()
        Dim lista As List(Of Productos) = dao.GetAll()
        DataGridProductos.DataSource = lista
        For Each f As DataGridViewRow In DataGridProductos.Rows
            If Convert.ToInt32(f.Index.ToString()) Mod 2 = 0 Then
                DataGridProductos.Rows(f.Index).DefaultCellStyle.BackColor = Color.LightBlue
            End If
        Next
    End Sub

    Public Sub LimpiarGroupBox()
        For Each control As Control In GroupBoxProductos.Controls
            If TypeOf control Is TextBox Then
                DirectCast(control, TextBox).Text = String.Empty
            End If
        Next
        txtId.Visible = False
        LabelId.Visible = False
    End Sub

    Private Sub DataGridProductos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridProductos.CellClick
        If e.RowIndex >= 0 Then
            Dim fila As DataGridViewRow = DataGridProductos.Rows(e.RowIndex)
            fila.Selected = False
            Dim nombre As String = fila.Cells("nombre").Value
            Dim precio As String = fila.Cells("precio").Value
            Dim categoria As String = fila.Cells("categoria").Value
            Dim id As String = fila.Cells("id").Value

            txtNombre.Text = nombre
            txtPrecio.Text = precio
            txtCategoria.Text = categoria
            txtId.Text = id

            txtId.Enabled = False
            txtId.Visible = True
            LabelId.Visible = True
            btnActualizar.Enabled = True
            btnBorrar.Enabled = True
            btnCrear.Enabled = False
        End If
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        BloquearBotones()
        LimpiarGroupBox()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim nombre As String = txtNombre.Text
        Dim precio As String = txtPrecio.Text
        Dim categoria As String = txtCategoria.Text
        Dim id As String = txtId.Text

        If (String.IsNullOrEmpty(nombre) Or String.IsNullOrEmpty(categoria) Or String.IsNullOrEmpty(id)) Or Not IsNumeric(precio) Then
            MessageBox.Show("error")
            Return
        End If

        Dim ActualizarProducto As New Productos()
        ActualizarProducto.nombre = nombre
        ActualizarProducto.precio = Convert.ToDecimal(precio)
        ActualizarProducto.categoria = categoria
        ActualizarProducto.id = Convert.ToInt32(id)

        Dim dao = ProductosDao.ObjetoAcceso()
        dao.Update(ActualizarProducto)
        CargarDataGrid()
        LimpiarGroupBox()
        BloquearBotones()
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        If Not String.IsNullOrEmpty(txtId.Text) Then
            Dim id As Integer = Convert.ToInt32(txtId.Text)
            If id <> 0 Then
                Dim dao = ProductosDao.ObjetoAcceso()
                Dim borrarCliente As Productos = dao.GetById(id)
                dao.Delete(id)
                CargarDataGrid()
                LimpiarGroupBox()
                BloquearBotones()
            Else
                MessageBox.Show("no existe")
            End If

        Else
            MessageBox.Show("Debe seleccionar un cliente")
        End If
    End Sub

    Private Sub txtFiltrar_TextChanged(sender As Object, e As EventArgs) Handles txtFiltrar.TextChanged
        If txtFiltrar.Text <> "" Then

            DataGridProductos.CurrentCell = Nothing
            For Each row As DataGridViewRow In DataGridProductos.Rows
                row.Visible = False
            Next

            For Each row As DataGridViewRow In DataGridProductos.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If (cell.Value.ToString().ToUpper()).IndexOf(txtFiltrar.Text.ToUpper()) = 0 Then
                        row.Visible = True
                        Exit For
                    End If
                Next
            Next
        Else
            CargarDataGrid()
        End If
    End Sub

    Private Sub btnFiltrarPorPrecio_Click(sender As Object, e As EventArgs) Handles btnFiltrarPorPrecio.Click
        Dim precioMinimo As Integer = precioMin.Value
        Dim precioMaximo As Decimal = precioMax.Value

        Dim dao = ProductosDao.ObjetoAcceso()
        Dim lista As List(Of Productos) = dao.FiltrarPorPrecio(precioMinimo, precioMaximo)
        If lista IsNot Nothing Then
            DataGridProductos.DataSource = lista
        Else
            MessageBox.Show("no hay productos")
        End If
    End Sub

    Private Sub LimpiarFiltros_Click(sender As Object, e As EventArgs) Handles LimpiarFiltros.Click
        CargarDataGrid()
        txtFiltrar.Text = String.Empty
        precioMin.Value = precioMin.Minimum
        precioMax.Value = precioMax.Minimum
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Dim formInicio = FrmInicio.ObtenerForm()
        formInicio.Show()
        Me.Close()
    End Sub

    Private Sub FrmProductos_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim formInicio = FrmInicio.ObtenerForm()
        formInicio.Show()
    End Sub
End Class