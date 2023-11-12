Imports entidades
Imports DAO

Public Class FrmVentas
    Private Sub FrmVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarDataGridVentas()


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


End Class