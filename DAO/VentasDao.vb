Imports entidades
Imports System.Data.SqlClient
Imports System.Configuration

Public Class VentasDao
    Private cadenaConexion As String
    Private comando As SqlCommand
    Private conexion As SqlConnection
    Private Shared ObjetoDao

    Private Sub New()
        cadenaConexion = ConfigurationManager.ConnectionStrings("MiConexion").ConnectionString
        comando = New SqlCommand()
        conexion = New SqlConnection(cadenaConexion)
        comando.CommandType = System.Data.CommandType.Text
        comando.Connection = conexion
    End Sub

    Public Shared Function ObjetoAcceso() As VentasDao
        If ObjetoDao Is Nothing Then
            ObjetoDao = New VentasDao()
        End If
        Return ObjetoDao
    End Function

    Public Function Add(venta As Ventas)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"insert into ventas (IDCliente,Fecha,Total) values(@idCliente,@fecha,@total)"
            comando.Parameters.AddWithValue("@idCliente", venta.idCliente)
            comando.Parameters.AddWithValue("@fecha", venta.fecha)
            comando.Parameters.AddWithValue("@total", venta.total)

            comando.ExecuteNonQuery()

            comando.CommandText = "SELECT IDENT_CURRENT('ventas')"
            Dim ultimoId As Integer = Convert.ToInt32(comando.ExecuteScalar())
            Return ultimoId

        Catch e As Exception
            Console.WriteLine(e.ToString())
        Finally
            conexion.Close()
        End Try

    End Function


    Public Function GetAll() As List(Of Ventas)
        Dim listaVentas As New List(Of Ventas)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"select * from ventas"
            Using dataReader As SqlDataReader = comando.ExecuteReader()
                While dataReader.Read()
                    Dim venta As New Ventas()
                    venta.id = Convert.ToInt32(dataReader("ID"))
                    venta.idCliente = dataReader("IDCliente").ToString()
                    venta.fecha = dataReader("Fecha")
                    venta.total = Convert.ToDecimal(dataReader("total"))
                    listaVentas.Add(venta)
                End While

                Return listaVentas
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try

        Return Nothing
    End Function

    Public Function GetById(id As Integer) As Ventas

        Dim venta As New Ventas()
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"select * from ventas where ID = @id"
            comando.Parameters.AddWithValue("@id", id)
            Using dataReader As SqlDataReader = comando.ExecuteReader()
                While dataReader.Read()
                    venta.id = Convert.ToInt32(dataReader("ID"))
                    venta.idCliente = dataReader("IDCliente").ToString()
                    venta.fecha = dataReader("Fecha")
                    venta.total = Convert.ToDecimal(dataReader("total"))
                End While

                Return venta
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function Update(venta As Ventas)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"update ventas 
                                    set IDCliente = @idCiente, 
                                    Fecha = @fecha,
                                    Total = @total
                                    where ID = @id"
            comando.Parameters.AddWithValue("@idCiente", venta.idCliente)
            comando.Parameters.AddWithValue("@fecha", venta.fecha)
            comando.Parameters.AddWithValue("@total", venta.total)
            comando.Parameters.AddWithValue("@id", venta.id)

            comando.ExecuteNonQuery()
            comando.CommandText = "SELECT IDENT_CURRENT('ventas')"
            Dim ultimoId As Integer = Convert.ToInt32(comando.ExecuteScalar())
            Return ultimoId

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function Delete(id As Integer)
        comando.Parameters.Clear()
        conexion.Open()
        Using transaction As SqlTransaction = conexion.BeginTransaction
            Try
                comando.CommandText = "delete from ventas where ID = @id;
                                        delete from ventasitems where IDventa = @id;"
                comando.Parameters.AddWithValue("@id", id)
                comando.Transaction = transaction
                comando.ExecuteNonQuery()

                transaction.Commit()
            Catch ex As Exception
                transaction.Rollback()
                Console.WriteLine(ex.ToString())
            Finally
                conexion.Close()
            End Try
        End Using

    End Function


    Public Property connection As SqlConnection
        Get
            Return conexion
        End Get
        Set(value As SqlConnection)
            conexion = value
        End Set
    End Property


    '--------------------------------ventasitems-------------------------------'
    Public Function AddVentasItems(transaction As SqlTransaction, idVenta As Integer, idProducto As Integer, precioUnitario As Decimal, cantidad As Integer, precioTotal As Decimal)
        Try
            comando.Parameters.Clear()
            comando.Transaction = transaction
            comando.CommandText = $"insert into ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) values
                                    (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)"
            comando.Parameters.AddWithValue("@IDVenta", idVenta)
            comando.Parameters.AddWithValue("@IDProducto", idProducto)
            comando.Parameters.AddWithValue("@PrecioUnitario", precioUnitario)
            comando.Parameters.AddWithValue("@Cantidad", cantidad)
            comando.Parameters.AddWithValue("@PrecioTotal", precioTotal)

            comando.ExecuteNonQuery()


        Catch e As Exception
            Console.WriteLine(e.ToString())
        End Try
    End Function


    Public Function GetProductosDeUnaVenta(dataGridProductos, idVenta)
        Dim tablaProductos As New DataTable("ProductosDeUnaVenta")

        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = "SELECT productos.ID, productos.Nombre, productos.Precio, ventasitems.Cantidad as Cant
                                    FROM productos 
                                    JOIN ventasitems ON productos.ID = ventasitems.IDProducto
                                    JOIN ventas ON ventasitems.IDVenta = ventas.ID
                                    WHERE ventas.ID = @idVenta;"

            comando.Parameters.AddWithValue("@idVenta", idVenta)

            ' Asigna la consulta al adaptador
            Dim adaptador As New SqlDataAdapter(comando)

            ' Llena el DataTable con los datos
            adaptador.Fill(tablaProductos)

            ' Asigna el DataTable al DataGridView
            dataGridProductos.DataSource = tablaProductos

            Return tablaProductos
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try

        ' En caso de error, o si no se pudo llenar el DataTable, devuelve un DataTable vacío
        Return New DataTable()

    End Function

    Public Function DeleteProductosDeUnaVenta(idVenta As Integer)
       Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = "delete from ventasitems where IDventa = @idVenta;"
            comando.Parameters.AddWithValue("@idVenta", idVenta)
            comando.ExecuteNonQuery()


        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try

    End Function

End Class

