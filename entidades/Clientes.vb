Public Class Clientes
    Private _id As Integer
    Private _cliente As String
    Private _telefono As String
    Private _correo As String


    Public Property id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property cliente As String
        Get
            Return _cliente
        End Get
        Set(value As String)
            _cliente = value
        End Set
    End Property

    Public Property telefono As String
        Get
            Return _telefono
        End Get
        Set(value As String)
            _telefono = value
        End Set
    End Property



    Public Property correo As String
        Get
            Return _correo
        End Get
        Set(value As String)
            _correo = value
        End Set
    End Property


    ''' <summary>
    ''' verifica que no exista otro cliente con el mismo correo
    ''' </summary>
    ''' <param name="listaClientes">lista de clientes existenten en la base de datos</param>
    ''' <param name="correo">correo del cliente a dar de alta</param>
    ''' <returns>retorna TRUE si existe el cliente, caso contrario FALSE</returns>
    Public Shared Function VerificarClienteExistente(listaClientes As List(Of Clientes), correo As String)
        For Each cliente As Clientes In listaClientes
            If cliente.correo = correo Then
                Return True
            End If
        Next

        Return False
    End Function
End Class
