Imports entidades
Imports DAO
Imports System.Data.SqlClient

Public Class FrmAltaVenta

    Private cliente As Clientes
    Private idVenta As Integer
    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub New(cliente As Clientes, id As Integer)
        InitializeComponent()
        Me.cliente = cliente
        Me.idVenta = id
    End Sub

    Private Sub FrmAltaVenta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If cliente Is Nothing Then
            CargarDataGridCorreos()
        Else
            DataGridProducto.Columns.Clear()
            CargarDataGridCorreoConCliente(cliente)
            Dim dao = VentasDao.ObjetoAcceso()
            Dim dataTable As DataTable = dao.GetProductosDeUnaVenta(DataGridProducto, Me.idVenta)
            LimpiarFiltros.Visible = False
            txtId.Text = Me.cliente.id
        End If

        CargarComboboxProductos()


        txtId.Enabled = False
        txtCorreo.Enabled = False
        txtId.Visible = False
        txtCorreo.Visible = False

        DataGridProducto.AllowUserToAddRows = False


    End Sub

    ''' <summary>
    ''' cargo data grid con el correo del cliente a actualizar 
    ''' </summary>
    ''' <param name="cliente"></param>
    Public Sub CargarDataGridCorreoConCliente(cliente As Clientes)
        Dim fila As DataGridViewRow = New DataGridViewRow()
        fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = cliente.id})
        fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = cliente.correo})
        DataGridCorreos.Rows.Add(fila)
    End Sub

    ''' <summary>
    ''' cargo en el data grid los correos de los clientes guardados en la base de datos
    ''' </summary>
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

    ''' <summary>
    ''' guardo el id y el correo del cliente al cual se le hizo click en el data grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridCorreos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridCorreos.CellClick
        If e.RowIndex >= 0 Then

            For Each f As DataGridViewRow In DataGridCorreos.Rows
                f.Selected = False
                f.DefaultCellStyle.BackColor = DataGridCorreos.DefaultCellStyle.BackColor
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

    ''' <summary>
    ''' cargo el combobox con los productos existentes en la base de datos
    ''' </summary>
    Private Sub CargarComboboxProductos()
        Dim daoProductos = ProductosDao.ObjetoAcceso()
        Dim listaProductos As List(Of Productos) = daoProductos.GetAll()

        cbProductos.DisplayMember = "Nombre"
        cbProductos.DataSource = listaProductos
    End Sub

    ''' <summary>
    ''' verifico si hubo un cambio el combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <returns>el elemento seleccionado en el combobox</returns>
    Private Function cbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbProductos.SelectedIndexChanged
        Dim productoSeleccionado As Productos = TryCast(cbProductos.SelectedItem, Productos)

        If productoSeleccionado IsNot Nothing Then
            Return productoSeleccionado
        End If
    End Function

    ''' <summary>
    ''' al presionar el boton se carga en el data grid el elemento establecido en el combobox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCargarProducto_Click(sender As Object, e As EventArgs) Handles btnCargarProducto.Click
        Dim producto As Productos = cbProductos_SelectedIndexChanged(cbProductos, EventArgs.Empty)

        For Each fila As DataGridViewRow In DataGridProducto.Rows
            If fila.Cells("Nombre").Value <> Nothing Then
                If fila.Cells("Nombre").Value = producto.nombre Then
                    fila.Cells("Cant").Value += cantidad.Value
                    Return
                End If
            Else
                Exit For
            End If
        Next

        Dim cantidadProducto As Integer = cantidad.Value
        CargarDataGridProductos(producto, cantidadProducto)


    End Sub


    ''' <summary>
    ''' carga el data grid de productos
    ''' </summary>
    ''' <param name="producto">producto elegido</param>
    ''' <param name="cantidad">cantidad del producto establecida</param>
    Public Sub CargarDataGridProductos(producto As Productos, cantidad As Integer)
        If Me.cliente Is Nothing Then
            Dim fila As DataGridViewRow = New DataGridViewRow()
            fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = producto.id})
            fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = producto.nombre})
            fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = producto.precio})
            fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = cantidad})
            DataGridProducto.Rows.Add(fila)

        Else
            Dim dataTableProductos As DataTable = DirectCast(DataGridProducto.DataSource, DataTable)

            Dim nuevaFila As DataRow = dataTableProductos.NewRow()
            nuevaFila("ID") = producto.id
            nuevaFila("Nombre") = producto.nombre
            nuevaFila("Precio") = producto.precio
            nuevaFila("Cant") = cantidad


            dataTableProductos.Rows.Add(nuevaFila)
        End If

    End Sub

    ''' <summary>
    ''' limpia los todo lo que se haya seleccionado, tanto cliente como productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LimpiarFiltros_Click(sender As Object, e As EventArgs) Handles LimpiarFiltros.Click
        DataGridProducto.Rows.Clear()
        txtCorreo.Text = String.Empty
        txtId.Text = String.Empty
    End Sub

    ''' <summary>
    ''' mediante una query se realiza la insercion de la venta con sus respectivos productos
    ''' validando que estos hayan sido seleccionados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCrearVenta_Click(sender As Object, e As EventArgs) Handles btnCrearVenta.Click
        If String.IsNullOrEmpty(txtId.Text) Or DataGridProducto.Rows.Count = 0 Then
            MessageBox.Show("Error. debe seleccionar cliente y producto")
            Return
        End If
        Dim dao = VentasDao.ObjetoAcceso()

        Dim venta As Ventas = New Ventas()
        venta.idCliente = Convert.ToInt32(txtId.Text)
        venta.fecha = New DateTime().Now()
        venta.total = CalcularTotal()
        Dim idVenta As Integer
        MessageBox.Show(idVenta)

        If Me.cliente Is Nothing Then
            idVenta = dao.Add(venta)
        Else
            idVenta = dao.Update(venta)
            dao.DeleteProductosDeUnaVenta(Me.idVenta)
        End If

        InsertarProductosDeUnaVenta(dao, idVenta)


        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub


    ''' <summary>
    ''' calcula el total de una venta
    ''' </summary>
    ''' <returns>totald de una venta</returns>
    Private Function CalcularTotal() As Decimal
        Dim precioTotal As Decimal = 0
        For Each fila As DataGridViewRow In DataGridProducto.Rows
            precioTotal += Convert.ToInt32(fila.Cells("Precio").Value) * Convert.ToInt32(fila.Cells("Cant").Value)
        Next

        Return precioTotal
    End Function

    ''' <summary>
    ''' calcula el total de un producto de la venta
    ''' </summary>
    ''' <param name="fila"></param>
    ''' <returns>total de un producto de la venta</returns>
    Private Function CalcularTotalDeUnProducto(fila As DataGridViewRow) As Decimal
        Dim precioTotal As Decimal = 0
        precioTotal += Convert.ToInt32(fila.Cells("Precio").Value) * Convert.ToInt32(fila.Cells("Cant").Value)
        Return precioTotal
    End Function


    ''' <summary>
    ''' recorre el data grid de productos para hacer la insercion de estos en la base de datos
    ''' </summary>
    ''' <param name="dao"></param>
    ''' <param name="idVenta"></param>
    ''' <returns></returns>
    Public Function InsertarProductosDeUnaVenta(dao As VentasDao, idVenta As Integer)
        Dim data = New DataTable()
        data.Columns.Add("IDVenta")
        data.Columns.Add("IDProducto")
        data.Columns.Add("PrecioUnitario")
        data.Columns.Add("Cantidad")
        data.Columns.Add("PrecioTotal")


        If DataGridProducto.Rows.Count > 0 And txtId.Text <> Nothing Then

            For Each fila As DataGridViewRow In DataGridProducto.Rows
                Dim dataRow As DataRow = data.NewRow()
                dataRow("IDVenta") = idVenta
                dataRow("IDProducto") = Convert.ToInt32(fila.Cells("ID").Value)
                dataRow("PrecioUnitario") = Convert.ToDecimal(fila.Cells("Precio").Value)
                dataRow("Cantidad") = Convert.ToInt32(fila.Cells("Cant").Value)
                dataRow("PrecioTotal") = CalcularTotalDeUnProducto(fila)
                data.Rows.Add(dataRow)

            Next
            If data.Rows.Count > 0 Then
                dao.AddVentasItems2(data)
            Else
                MessageBox.Show("error")
            End If
        End If
    End Function

    ''' <summary>
    ''' al hacer click en algun elemento del data grid de productos existe la posibilidad mediante una ventana modal
    ''' eliminar dicho elemento individualmente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridProducto_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridProducto.CellClick
        If e.RowIndex >= 0 Then
            Dim indice = DataGridProducto.Rows(e.RowIndex).Index
            Dim formCartel = New FrmCartel()
            Dim result As DialogResult = formCartel.ShowDialog()
            If result = DialogResult.OK Then
                DataGridProducto.Rows.RemoveAt(indice)
            End If

        End If
    End Sub
End Class