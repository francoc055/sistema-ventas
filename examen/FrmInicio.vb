Public Class FrmInicio
    Private Sub btnClientes_Click(sender As Object, e As EventArgs) Handles btnClientes.Click
        Dim formClientes As FrmClientes = New FrmClientes()
        formClientes.Show()
    End Sub

    Private Sub btnProductos_Click(sender As Object, e As EventArgs) Handles btnProductos.Click
        Dim formProductos As FrmProductos = New FrmProductos()
        formProductos.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim formVentas As FrmVentas = New FrmVentas()
        formVentas.Show()
    End Sub
End Class