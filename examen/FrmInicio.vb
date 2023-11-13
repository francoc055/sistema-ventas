Public Class FrmInicio

    Private Shared ObjetoFrmInicio

    Public Shared Function ObtenerForm() As FrmInicio
        If ObjetoFrmInicio Is Nothing Then
            ObjetoFrmInicio = New FrmInicio()
        End If
        Return ObjetoFrmInicio
    End Function

    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
        Dim formClientes As FrmClientes = New FrmClientes()
        formClientes.Show()
        Me.Hide()
    End Sub

    Private Sub btnProductos_Click(sender As Object, e As EventArgs) Handles btnProductos.Click
        Dim formProductos As FrmProductos = New FrmProductos()
        formProductos.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim formVentas As FrmVentas = New FrmVentas()
        formVentas.Show()
        Me.Hide()
    End Sub

    Private Sub FrmInicio_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub
End Class