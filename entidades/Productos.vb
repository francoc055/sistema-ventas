Public Class Productos
    Private _id As Integer
    Private _nombre As String
    Private _precio As Decimal
    Private _categoria As String


    Public Property id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            _nombre = value
        End Set
    End Property

    Public Property precio As Decimal
        Get
            Return _precio
        End Get
        Set(value As Decimal)
            _precio = value
        End Set
    End Property



    Public Property categoria As String
        Get
            Return _categoria
        End Get
        Set(value As String)
            _categoria = value
        End Set
    End Property

    ''' <summary>
    ''' verifica que no exista otro producto con el mismo nombre y categoria
    ''' </summary>
    ''' <param name="listaProductos">lista de productos existenten en la base de datos</param>
    ''' <param name="nombre">nombre del producto a dar de alta</param>
    ''' <param name="categoria">categoria del producto a dar de alta</param>
    ''' <returns>retorna TRUE si existe el producto, caso contrario FALSE</returns>
    Public Shared Function VerificarProductoExistente(listaProductos As List(Of Productos), nombre As String, categoria As String)
        If listaProductos IsNot Nothing Then
            For Each productos As Productos In listaProductos
                If productos.nombre = nombre And productos.categoria = categoria Then
                    Return True
                End If
            Next
        End If


        Return False
    End Function
End Class
