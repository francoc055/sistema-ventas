Imports entidades
Imports DAO
Public Class FrmClientes
    Private Sub FrmClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BloquearBotones()

        Dim dao = ClientesDao.ObjetoAcceso()
        Dim lista As List(Of Clientes) = dao.GetAll()
        CargarDataGrid(DataGridClientes, lista)
    End Sub

    Public Function BloquearBotones()
        txtId.Visible = False
        LabelId.Visible = False
        btnActualizar.Enabled = False
        btnBorrar.Enabled = False
        btnCrear.Enabled = True
    End Function

    'creo un objeto Clientes y lo cargo en la BD
    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        Dim nombre As String = txtNombre.Text
        Dim telefono As String = txtTelefono.Text
        Dim correo As String = txtCorreo.Text

        If (String.IsNullOrEmpty(nombre) Or String.IsNullOrEmpty(telefono) Or String.IsNullOrEmpty(correo)) Or
            Not IsNumeric(telefono) Then
            MessageBox.Show("error")
            Return
        End If

        'MessageBox.Show("paso")

        Dim nuevoCliente As New Clientes()
        nuevoCliente.cliente = nombre
        nuevoCliente.telefono = telefono
        nuevoCliente.correo = correo

        Dim dao = ClientesDao.ObjetoAcceso()
        dao.Add(nuevoCliente)
        Dim lista As List(Of Clientes) = dao.GetAll()
        CargarDataGrid(DataGridClientes, lista)
        LimpiarGroupBox()
    End Sub

    'cargo datagrid en base a la lista que traigo de la BD
    Private Sub CargarDataGrid(data As DataGridView, lista As List(Of Clientes))
        data.DataSource = lista
    End Sub

    'cuando hago click en una celda verifico el indice y lo cargo en los textBox
    Private Sub DataGridClientes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridClientes.CellClick

        If e.RowIndex >= 0 Then
            Dim fila As DataGridViewRow = DataGridClientes.Rows(e.RowIndex)

            Dim nombre As String = fila.Cells("cliente").Value
            Dim telefono As String = fila.Cells("telefono").Value
            Dim correo As String = fila.Cells("correo").Value
            Dim id As String = fila.Cells("id").Value

            txtNombre.Text = nombre
            txtTelefono.Text = telefono
            txtCorreo.Text = correo
            txtId.Text = id

            txtId.Enabled = False
            txtId.Visible = True
            LabelId.Visible = True
            btnActualizar.Enabled = True
            btnBorrar.Enabled = True
            btnCrear.Enabled = False
        End If
    End Sub


    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        BloquearBotones()
        LimpiarGroupBox()
    End Sub

    Public Sub LimpiarGroupBox()
        For Each control As Control In GroupBoxCliente.Controls
            If TypeOf control Is TextBox Then
                DirectCast(control, TextBox).Text = String.Empty
            End If
        Next
        txtId.Visible = False
        LabelId.Visible = False
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Dim nombre As String = txtNombre.Text
        Dim telefono As String = txtTelefono.Text
        Dim correo As String = txtCorreo.Text
        Dim id As Integer = txtId.Text

        If String.IsNullOrEmpty(nombre) Or String.IsNullOrEmpty(telefono) Or String.IsNullOrEmpty(correo) Or String.IsNullOrEmpty(id) Then
            MessageBox.Show("error")
            Return
        End If

        Dim ActualizarCliente As New Clientes()
        ActualizarCliente.cliente = nombre
        ActualizarCliente.telefono = telefono
        ActualizarCliente.correo = correo
        ActualizarCliente.id = Convert.ToInt32(id)

        Dim dao = ClientesDao.ObjetoAcceso()
        dao.Update(ActualizarCliente)

        Dim lista As List(Of Clientes) = dao.GetAll()
        CargarDataGrid(DataGridClientes, lista)
        LimpiarGroupBox()
        BloquearBotones()


    End Sub

    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        If Not String.IsNullOrEmpty(txtId.Text) Then
            Dim id As Integer = Convert.ToInt32(txtId.Text)
            If id <> 0 Then
                Dim dao = ClientesDao.ObjetoAcceso()
                Dim borrarCliente As Clientes = dao.GetById(id)
                dao.Delete(id)
                Dim lista As List(Of Clientes) = dao.GetAll()
                CargarDataGrid(DataGridClientes, lista)
                LimpiarGroupBox()
                BloquearBotones()
            Else
                MessageBox.Show("no existe")
            End If

        Else
            MessageBox.Show("Debe seleccionar un cliente")
        End If

    End Sub

End Class
