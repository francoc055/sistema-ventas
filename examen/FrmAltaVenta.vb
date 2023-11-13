﻿Imports entidades
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

    Public Sub CargarDataGridCorreoConCliente(cliente As Clientes)
        Dim fila As DataGridViewRow = New DataGridViewRow()
        fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = cliente.id})
        fila.Cells.Add(New DataGridViewTextBoxCell With {.Value = cliente.correo})
        DataGridCorreos.Rows.Add(fila)
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

            ' Crear una nueva fila y asignar valores
            Dim nuevaFila As DataRow = dataTableProductos.NewRow()
            nuevaFila("ID") = producto.id
            nuevaFila("Nombre") = producto.nombre
            nuevaFila("Precio") = producto.precio
            nuevaFila("Cant") = cantidad

            ' Agregar la nueva fila al DataTable
            dataTableProductos.Rows.Add(nuevaFila)
        End If




    End Sub

    Private Sub LimpiarFiltros_Click(sender As Object, e As EventArgs) Handles LimpiarFiltros.Click
        DataGridProducto.Rows.Clear()
        txtCorreo.Text = String.Empty
        txtId.Text = String.Empty
    End Sub

    Private Sub btnCrearVenta_Click(sender As Object, e As EventArgs) Handles btnCrearVenta.Click
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

    'precio unitario, precio total 
    Private Function GenerarListaId()

    End Function

    Private Function CalcularTotal() As Decimal
        Dim precioTotal As Decimal = 0
        For Each fila As DataGridViewRow In DataGridProducto.Rows
            precioTotal += Convert.ToInt32(fila.Cells("Precio").Value) * Convert.ToInt32(fila.Cells("Cant").Value)
        Next

        Return precioTotal
    End Function

    Private Function CalcularTotalDeUnProducto(fila As DataGridViewRow) As Decimal
        Dim precioTotal As Decimal = 0
        precioTotal += Convert.ToInt32(fila.Cells("Precio").Value) * Convert.ToInt32(fila.Cells("Cant").Value)
        Return precioTotal
    End Function

    Public Function InsertarProductosDeUnaVenta(dao As VentasDao, idVenta As Integer)
        If DataGridProducto.Rows.Count > 0 And txtId.Text <> Nothing Then
            Dim conexion = dao.connection
            conexion.Open()
            Using transaction As SqlTransaction = conexion.BeginTransaction()
                Try
                    For Each fila As DataGridViewRow In DataGridProducto.Rows
                        Dim idProducto As Integer = Convert.ToInt32(fila.Cells("ID").Value)
                        Dim precioUnitario As Decimal = Convert.ToDecimal(fila.Cells("Precio").Value)
                        Dim cantidad As Integer = Convert.ToInt32(fila.Cells("Cant").Value)
                        Dim precioTotalVI = CalcularTotalDeUnProducto(fila)
                        dao.AddVentasItems(transaction, idVenta, idProducto, precioUnitario, cantidad, precioTotalVI)
                    Next
                    transaction.Commit()
                Catch ex As Exception
                    transaction.Rollback()
                    MessageBox.Show(ex.ToString())
                Finally
                    conexion.Close()
                End Try
            End Using
        End If
    End Function

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