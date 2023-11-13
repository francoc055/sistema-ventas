Public Class FrmCartel
    Private Sub btnSi_Click(sender As Object, e As EventArgs) Handles btnSi.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
End Class