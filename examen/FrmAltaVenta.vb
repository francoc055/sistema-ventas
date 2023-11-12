Imports entidades
Imports DAO


Public Class FrmAltaVenta
    Private Sub FrmAltaVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDataGridCorreos()
        CargarComboboxProductos()
        txtId.Enabled = False
        txtCorreo.Enabled = False
        txtId.Visible = False
        txtCorreo.Visible = False

        DataGridProducto.AllowUserToAddRows = False


    End Sub

    Public Sub CargarDataGridCorreos()
        Dim daoClientes = ClientesDao.ObjetoAcceso()
        Dim listaClientes As List(Of Clientes) = daoClientes.GetAll()

        For Each item As Clientes In listaClientes
            Dim fila As DataGridViewRow = New DataGridViewRow()
            fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = item.id})
            fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = item.correo})
            DataGridCorreos.Rows.Add(fila)
        Next
    End Sub

    Private Sub DataGridCorreos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridCorreos.CellClick
        If e.RowIndex >= 0 Then

            For Each f As DataGridViewRow In DataGridCorreos.Rows
                f.Selected = False
                f.DefaultCellStyle.BackColor = DataGridCorreos.DefaultCellStyle.BackColor ' Restablece el color de fondo al predeterminado
            Next
            DataGridCorreos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridCorreos.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Orange

            Dim fila As DataGridViewRow = DataGridCorreos.Rows(e.RowIndex)
            Dim id As String = fila.Cells("ColumnaId").Value
            Dim correo As String = fila.Cells("ColumnaCorreo").Value

            txtId.Visible = True
            txtCorreo.Visible = True
            txtId.Text = id
            txtCorreo.Text = correo
            txtId.Visible = True
            txtCorreo.Visible = True

        End If
    End Sub

    Private Sub CargarComboboxProductos()
        Dim daoProductos = ProductosDao.ObjetoAcceso()
        Dim listaProductos As List(Of Productos) = daoProductos.GetAll()

        cbProductos.DisplayMember = "Nombre"
        cbProductos.DataSource = listaProductos
    End Sub

    Private Function cbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProductos.SelectedIndexChanged
        Dim productoSeleccionado As Productos = TryCast(cbProductos.SelectedItem, Productos)

        If productoSeleccionado IsNot Nothing Then
            Return productoSeleccionado
        End If
    End Function

    Private Sub btnCargarProducto_Click(sender As Object, e As EventArgs) Handles btnCargarProducto.Click
        Dim producto As Productos = cbProductos_SelectedIndexChanged(cbProductos, EventArgs.Empty)

        For Each fila As DataGridViewRow In DataGridProducto.Rows
            If fila.Cells("ColumnaNombre").Value <> Nothing Then
                If fila.Cells("ColumnaNombre").Value = producto.nombre Then
                    fila.Cells("ColumnaCant").Value += cantidad.Value
                    Return
                End If
            Else
                Exit For
            End If
        Next

        Dim cantidadProducto As Integer = cantidad.Value
        CargarDataGridProductos(producto, cantidadProducto)


    End Sub

    Public Sub CargarDataGridProductos(producto As Productos, cantidad As Integer)
        Dim fila As DataGridViewRow = New DataGridViewRow()
        fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = producto.id})
        fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = producto.nombre})
        fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = producto.precio})
        fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = cantidad})
        DataGridProducto.Rows.Add(fila)
    End Sub

    Private Sub LimpiarFiltros_Click(sender As Object, e As EventArgs) Handles LimpiarFiltros.Click
        DataGridProducto.Rows.Clear()
    End Sub

    Private Sub btnCrearVenta_Click(sender As Object, e As EventArgs) Handles btnCrearVenta.Click
        Dim dao = VentasDao.ObjetoAcceso()
        'cargar ventasitems


        For Each fila As DataGridViewRow In DataGridProducto.Rows
            Dim idProducto As Integer = Convert.ToInt32(fila.Cells("ColumnID").Value)
            Dim precioUnitario As Decimal = Convert.ToDecimal(fila.Cells("ColumnaPrecio").Value)
            Dim cantidad As Integer = Convert.ToInt32(fila.Cells("ColumnaCant").Value)
            Dim idCliente As Integer = Convert.ToInt32(txtId.Text)

        Next


        'Dim venta As Ventas = New Ventas()
        'venta.idCliente = Convert.ToInt32(txtId)
        'venta.fecha = New DateTime().Now()

        'Me.DialogResult = DialogResult.OK
        'Me.Close()
    End Sub

    'precio unitario, precio total 
    Private Function GenerarListaId()

    End Function

    Private Function CalcularTotal() As Decimal
        Dim precioTotal As Decimal = 0
        For Each fila As DataGridViewRow In DataGridProducto.Rows
            If fila.Cells("ColumnaPrecio").Value Then
                precioTotal += fila.Cells("ColumnaPrecio").Value
            End If
        Next

        Return precioTotal
    End Function
End Class