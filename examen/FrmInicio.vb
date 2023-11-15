Public Class FrmInicio

    Private Shared ObjetoFrmInicio
    Private WithEvents timer As New Timer()
    Private currentIndex As Integer = 0
    Private textoCompleto As String = "*BIENVENIDO AL SISTEMA*"

    Public Sub New()
        InitializeComponent()

        ' Configurar el temporizador
        Timer.Interval = 100 ' Intervalo en milisegundos
        Timer.Start()
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        ' Verificar si hay más caracteres para mostrar
        If currentIndex < textoCompleto.Length Then
            ' Agregar el siguiente carácter al contenido de la etiqueta
            labelTitulo.Text += textoCompleto(currentIndex)
            currentIndex += 1
        Else
            ' Detener el temporizador cuando se ha mostrado todo el texto
            Timer.Stop()
        End If
    End Sub


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