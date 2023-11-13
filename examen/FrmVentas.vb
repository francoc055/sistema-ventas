Imports entidades
Imports DAO

Public Class FrmVentas

    Public flag As Boolean
    Public Sub New()

        ' Esta llamada es exigida por el diseñador.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        flag = False
    End Sub
    Private Sub FrmVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDataGridVentas()

        btnActualizar.Enabled = False
        btnBorrar.Enabled = False
        txtIdVenta.Visible = False
        txtIdCliente.Visible = False

        'reporte ventas---
        CargarComboboxCorreos()
        DataGridDatosVentas.AllowUserToAddRows = False
        DataGridProductosMensuales.AllowUserToAddRows = False
        cbCorreosClientes.SelectedIndex = -1
        cbMes.SelectedIndex = -1
        flag = True
    End Sub

    Private Sub txtFiltrar_TextChanged(sender As Object, e As EventArgs) Handles txtFiltrar.TextChanged
        If txtFiltrar.Text <> "" Then

            DataGridVentas.CurrentCell = Nothing
            For Each row As DataGridViewRow In DataGridVentas.Rows
                row.Visible = False
            Next

            For Each row As DataGridViewRow In DataGridVentas.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If (cell.Value.ToString().ToUpper()).IndexOf(txtFiltrar.Text.ToUpper()) = 0 Then
                        row.Visible = True
                        Exit For
                    End If
                Next
            Next
        Else
            CargarDataGridVentas()
        End If
    End Sub
    Public Sub CargarDataGridVentas()
        Dim dao = VentasDao.ObjetoAcceso()
        Dim lista As List(Of Ventas) = dao.GetAll()
        DataGridVentas.DataSource = lista
        For Each f As DataGridViewRow In DataGridVentas.Rows
            If Convert.ToInt32(f.Index.ToString()) Mod 2 = 0 Then
                DataGridVentas.Rows(f.Index).DefaultCellStyle.BackColor = Color.LightBlue
            End If
        Next
    End Sub

    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        Dim FormAlta = New FrmAltaVenta()
        Dim result As DialogResult = FormAlta.ShowDialog()
        If result = DialogResult.OK Then
            CargarDataGridVentas()
        End If
    End Sub

    Private Sub DataGridVentas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridVentas.CellClick
        If e.RowIndex >= 0 Then
            For Each f As DataGridViewRow In DataGridVentas.Rows
                f.Selected = False
                f.DefaultCellStyle.BackColor = DataGridVentas.DefaultCellStyle.BackColor
            Next
            DataGridVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DataGridVentas.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.OrangeRed

            btnActualizar.Enabled = True
            btnBorrar.Enabled = True
            btnCrear.Enabled = False

            Dim fila As DataGridViewRow = DataGridVentas.Rows(e.RowIndex)
            Dim idCliente As String = fila.Cells("idCliente").Value
            Dim idVenta As String = fila.Cells("id").Value

            txtIdVenta.Text = idVenta
            txtIdCliente.Text = idCliente
        End If
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        Dim dao = VentasDao.ObjetoAcceso()
        dao.Delete(Convert.ToInt32(txtIdVenta.Text))
        For Each f As DataGridViewRow In DataGridVentas.Rows
            f.Selected = False
            f.DefaultCellStyle.BackColor = DataGridVentas.DefaultCellStyle.BackColor
        Next
        CargarDataGridVentas()
        btnActualizar.Enabled = False
        btnBorrar.Enabled = False
        btnCrear.Enabled = True
    End Sub

    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        btnActualizar.Enabled = False
        btnBorrar.Enabled = False
        btnCrear.Enabled = True
        txtIdVenta.Text = String.Empty
        For Each f As DataGridViewRow In DataGridVentas.Rows
            f.Selected = False
            f.DefaultCellStyle.BackColor = DataGridVentas.DefaultCellStyle.BackColor
        Next
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim dao = ClientesDao.ObjetoAcceso()
        Dim cliente As Clientes = dao.GetById(Convert.ToInt32(txtIdCliente.Text))


        Dim FormAlta = New FrmAltaVenta(cliente, Convert.ToInt32(txtIdVenta.Text))
        Dim result As DialogResult = FormAlta.ShowDialog()
        If result = DialogResult.OK Then
            CargarDataGridVentas()
            For Each f As DataGridViewRow In DataGridVentas.Rows
                f.Selected = False
                f.DefaultCellStyle.BackColor = DataGridVentas.DefaultCellStyle.BackColor
            Next
        End If
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Dim formInicio = FrmInicio.ObtenerForm()
        formInicio.Show()
        Me.Close()
    End Sub

    Private Sub FrmVentas_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim formInicio = FrmInicio.ObtenerForm()
        formInicio.Show()
    End Sub

    '------------------REPORTE VENTAS------------------------------
    Private Sub CargarComboboxCorreos()
        Dim daoClientes = ClientesDao.ObjetoAcceso()
        Dim listaClientes As List(Of Clientes) = daoClientes.GetAll()

        cbCorreosClientes.DisplayMember = "Correo"
        cbCorreosClientes.DataSource = listaClientes
    End Sub

    Private Sub CargarDataGridDatosVentas(cliente As Clientes)
        Dim dao = VentasDao.ObjetoAcceso()
        dao.GetDatosVentas(DataGridDatosVentas, cliente.id)
        For Each f As DataGridViewRow In DataGridDatosVentas.Rows
            If Convert.ToInt32(f.Index.ToString()) Mod 2 = 0 Then
                DataGridDatosVentas.Rows(f.Index).DefaultCellStyle.BackColor = Color.LightBlue
            End If
        Next
    End Sub

    Private Sub cbCorreosClientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbCorreosClientes.SelectedIndexChanged
        If Not Me.flag Then
            Return
        End If
        Dim clienteSeleccionado As Clientes = TryCast(cbCorreosClientes.SelectedItem, Clientes)

        If clienteSeleccionado IsNot Nothing Then
            CargarDataGridDatosVentas(clienteSeleccionado)
        End If

    End Sub

    '------------------REPORTE PRODUCTOS------------------------------

    Private Sub cbMes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbMes.SelectedIndexChanged
        If Not Me.flag Then
            Return
        End If

        ElegirMes()



    End Sub

    Public Sub ElegirMes()
        Dim mesSeleccionado As String = TryCast(cbMes.SelectedItem, String)
        Dim dao = VentasDao.ObjetoAcceso()

        Select Case mesSeleccionado
            Case "enero"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 1, 1), New DateTime(2023, 11, 31))
            Case "febrero"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 2, 1), New DateTime(2023, 2, 28))

            Case "marzo"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 3, 1), New DateTime(2023, 3, 31))

            Case "abril"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 4, 1), New DateTime(2023, 4, 30))

            Case "mayo"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 5, 1), New DateTime(2023, 5, 31))

            Case "junio"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 6, 1), New DateTime(2023, 6, 30))

            Case "julio"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 7, 1), New DateTime(2023, 7, 31))

            Case "agosto"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 8, 1), New DateTime(2023, 8, 31))

            Case "septiembre"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 9, 1), New DateTime(2023, 9, 30))

            Case "octubre"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 10, 1), New DateTime(2023, 10, 31))

            Case "noviembre"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 11, 1), New DateTime(2023, 11, 30))

            Case "diciembre"
                dao.GetProductosMensuales(DataGridProductosMensuales, New DateTime(2023, 12, 1), New DateTime(2023, 12, 31))
        End Select

        For Each f As DataGridViewRow In DataGridProductosMensuales.Rows
            If Convert.ToInt32(f.Index.ToString()) Mod 2 = 0 Then
                DataGridProductosMensuales.Rows(f.Index).DefaultCellStyle.BackColor = Color.LightBlue
            End If
        Next

    End Sub


End Class