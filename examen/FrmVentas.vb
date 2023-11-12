Imports entidades
Imports DAO

Public Class FrmVentas
    Private Sub FrmVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDataGridVentas()

        btnActualizar.Enabled = False
        btnBorrar.Enabled = False
        txtId.Visible = False
    End Sub

    Public Sub CargarDataGridVentas()
        Dim dao = VentasDao.ObjetoAcceso()
        Dim lista As List(Of Ventas) = dao.GetAll()
        DataGridVentas.DataSource = lista
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
            Dim id As String = fila.Cells("id").Value
            txtId.Text = id
        End If
    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        Dim dao = VentasDao.ObjetoAcceso()
        dao.Delete(Convert.ToInt32(txtId.Text))
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
        txtId.Text = String.Empty
        For Each f As DataGridViewRow In DataGridVentas.Rows
            f.Selected = False
            f.DefaultCellStyle.BackColor = DataGridVentas.DefaultCellStyle.BackColor
        Next
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim dao = ClientesDao.ObjetoAcceso()
        Dim cliente As Clientes = dao.GetById(Convert.ToInt32(txtId.Text)


        Dim FormAlta = New FrmAltaVenta(cliente)
        Dim result As DialogResult = FormAlta.ShowDialog()
        If result = DialogResult.OK Then
            CargarDataGridVentas()
        End If
    End Sub
End Class