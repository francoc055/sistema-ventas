Imports entidades
Imports DAO
Public Class FrmClientes
    Private Sub FrmClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BloquearBotones()


        CargarDataGrid()
    End Sub


    ''' <summary>
    ''' bloque botones del form
    ''' </summary>
    Public Sub BloquearBotones()
        txtId.Visible = False
        LabelId.Visible = False
        btnActualizar.Enabled = False
        btnBorrar.Enabled = False
        btnCrear.Enabled = True
    End Sub

    ''' <summary>
    ''' carga informacion de los textbox y los carga para crear un cliente, invocando un metodo para
    ''' hacer la insercion en la base de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        Dim nombre As String = txtNombre.Text
        Dim telefono As String = txtTelefono.Text
        Dim correo As String = txtCorreo.Text

        If (String.IsNullOrEmpty(nombre) Or String.IsNullOrEmpty(telefono) Or Not EsCorreoElectronicoValido(correo)) Or
            Not IsNumeric(telefono) Then
            MessageBox.Show("error")
            Return
        End If

        Dim dao = ClientesDao.ObjetoAcceso()
        Dim listaClientes = dao.GetAll()
        If Clientes.VerificarClienteExistente(listaClientes, correo) Then
            MessageBox.Show("error. cliente existente")
            Return
        End If


        Dim nuevoCliente As New Clientes()
        nuevoCliente.cliente = nombre
        nuevoCliente.telefono = telefono
        nuevoCliente.correo = correo

        dao.Add(nuevoCliente)
        LimpiarGroupBox()
        CargarDataGrid()
    End Sub

    ''' <summary>
    ''' cargo el data grid view de cliente con la informacion que obtengo de la base de datos
    ''' </summary>
    Private Sub CargarDataGrid()
        Dim dao = ClientesDao.ObjetoAcceso()
        Dim lista As List(Of Clientes) = dao.GetAll()
        DataGridClientes.DataSource = lista
        For Each f As DataGridViewRow In DataGridClientes.Rows
            If Convert.ToInt32(f.Index.ToString()) Mod 2 = 0 Then
                DataGridClientes.Rows(f.Index).DefaultCellStyle.BackColor = Color.LightBlue
            End If
        Next
    End Sub

    ''' <summary>
    ''' verifico que el correo sea valido
    ''' </summary>
    ''' <param name="correo"></param>
    ''' <returns></returns>
    Function EsCorreoElectronicoValido(correo As String) As Boolean
        If String.IsNullOrEmpty(correo) Then
            Return False
        End If

        Dim indiceArroba As Integer = correo.IndexOf("@")
        If indiceArroba = -1 Or indiceArroba = 0 Or indiceArroba = correo.Length - 1 Then
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' al hacer click en una fila del data grid view, verifico el valor de la columnas y cargo los textbox con 
    ''' la informacion asociada
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridClientes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridClientes.CellClick

        If e.RowIndex >= 0 Then
            Dim fila As DataGridViewRow = DataGridClientes.Rows(e.RowIndex)
            fila.Selected = False

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

    ''' <summary>
    ''' bloquea botones y limpia el groupbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        BloquearBotones()
        LimpiarGroupBox()
    End Sub

    ''' <summary>
    ''' limpia el groupbox que contiene a los textbox
    ''' </summary>
    Public Sub LimpiarGroupBox()
        For Each control As Control In GroupBoxCliente.Controls
            If TypeOf control Is TextBox Then
                DirectCast(control, TextBox).Text = String.Empty
            End If
        Next
        txtId.Visible = False
        LabelId.Visible = False
    End Sub

    ''' <summary>
    '''carga informacion de los textbox y los carga para actualizar un cliente, invocando un metodo para
    ''' hacer el update en la base de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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
        CargarDataGrid()
        LimpiarGroupBox()
        BloquearBotones()


    End Sub

    ''' <summary>
    '''carga informacion de los textbox y los carga para eliminar un cliente, invocando un metodo para
    ''' hacer el delete en la base de datos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnBorrar_Click(sender As Object, e As EventArgs) Handles btnBorrar.Click
        If Not String.IsNullOrEmpty(txtId.Text) Then
            Dim id As Integer = Convert.ToInt32(txtId.Text)
            If id <> 0 Then
                Dim dao = ClientesDao.ObjetoAcceso()
                Dim borrarCliente As Clientes = dao.GetById(id)
                dao.Delete(id)
                CargarDataGrid()
                LimpiarGroupBox()
                BloquearBotones()
            Else
                MessageBox.Show("no existe")
            End If

        Else
            MessageBox.Show("Debe seleccionar un cliente")
        End If

    End Sub

    ''' <summary>
    ''' al haber un cambio en el textbox, se filtra buscando en todos los valores de las columnas
    ''' si hay alguna similitud se deja visible, de lo contrario no se deja visible en el data grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub txtFiltrar_TextChanged(sender As Object, e As EventArgs) Handles txtFiltrar.TextChanged
        If txtFiltrar.Text <> "" Then

            DataGridClientes.CurrentCell = Nothing
            For Each row As DataGridViewRow In DataGridClientes.Rows
                row.Visible = False
            Next

            For Each row As DataGridViewRow In DataGridClientes.Rows
                For Each cell As DataGridViewCell In row.Cells
                    If (cell.Value.ToString().ToUpper()).IndexOf(txtFiltrar.Text.ToUpper()) = 0 Then
                        row.Visible = True
                        Exit For
                    End If
                Next
            Next
        Else
            CargarDataGrid()
        End If
    End Sub

    ''' <summary>
    ''' cierra el formulario actual y abre una instancia del formulario de inicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Dim formInicio = FrmInicio.ObtenerForm()
        formInicio.Show()
        Me.Close()
    End Sub

    ''' <summary>
    ''' una ves cerrado el formulario, abre una instancia del formulario de inicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmClientes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim formInicio = FrmInicio.ObtenerForm()
        formInicio.Show()
    End Sub
End Class
