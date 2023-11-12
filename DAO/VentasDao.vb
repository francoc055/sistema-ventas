Imports entidades
Imports System.Data.SqlClient

Public Class VentasDao
    Private cadenaConexion As String
    Private comando As SqlCommand
    Private conexion As SqlConnection
    Private Shared ObjetoDao

    Private Sub New()
        cadenaConexion = $"Data Source=.;Initial Catalog=pruebademo;Integrated Security=true"
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
            Console.WriteLine("venta insertado")

        Catch e As Exception
            Console.WriteLine(e.ToString())
        Finally
            conexion.Close()
        End Try

    End Function


    Public Function GetAll() As List(Of Ventas)
        Dim listaVentas As New List(Of Ventas)
        Try
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
            Console.WriteLine("venta modificado")

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function Delete(id As Integer)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = "delete from ventas where ID = @id"
            comando.Parameters.AddWithValue("@id", id)
            comando.ExecuteNonQuery()

            Console.WriteLine("venta eliminada")


        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function


    Public Property connection As SqlConnection
        Get
            Return conexion
        End Get
        Set(value As SqlConnection)
            conexion = value
        End Set
    End Property

    Public Function AddVentasItems(idVenta As Integer, idProducto As Integer, precioUnitario As Decimal, cantidad As Integer, precioTotal As Decimal)
        Try
            comando.Parameters.Clear()
            conexion.Open()
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
        Finally
            conexion.Close()
        End Try
    End Function

End Class
