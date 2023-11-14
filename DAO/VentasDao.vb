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

    ''' <summary>
    ''' Uso de patron singleton, verifica que ya exista un instancia, si es asi devuelvo la misma,
    ''' caso contrario creo un nuevo objeto
    ''' </summary>
    ''' <returns>retorna un objeto de acceso a datos</returns>
    Public Shared Function ObjetoAcceso() As VentasDao
        If ObjetoDao Is Nothing Then
            ObjetoDao = New VentasDao()
        End If
        Return ObjetoDao
    End Function

    ''' <summary>
    ''' realizo query que agrega un objeto venta a la base de datos
    ''' </summary>
    ''' <param name="venta">objeto venta</param>
    ''' <returns>ultimo id insertado</returns>
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

    ''' <summary>
    ''' realizo query para la obtencion de una lista de ventas de la base de datos
    ''' </summary>
    ''' <returns>lista de ventas</returns>
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


    ''' <summary>
    ''' realizo query para actualizar los campos de un objeto venta en la base de datos
    ''' </summary>
    ''' <param name="venta">objeto venta</param>
    Public Sub Update(venta As Ventas)
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

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Sub




    ''' <summary>
    ''' realizo query para la eliminacion de una venta y sus productos asociados en la base de datos
    ''' </summary>
    ''' <param name="id">id del objeto venta</param>
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
                Return True
            Catch ex As Exception
                transaction.Rollback()
                Console.WriteLine(ex.ToString())
            Finally
                conexion.Close()
            End Try
        End Using

    End Function





    '--------------------------------ventasitems-------------------------------'

    ''' <summary>
    ''' realizo query para la insercion de productos de una venta
    ''' </summary>
    ''' <param name="data">objeto dataTable</param>
    Public Sub AddVentasItems2(data As DataTable)
        comando.Parameters.Clear()
        conexion.Open()
        Using transaction As SqlTransaction = conexion.BeginTransaction()
            Try
                Dim adaptador As New SqlDataAdapter(comando)

                adaptador.SelectCommand = New SqlCommand("SELECT * FROM ventasitems", conexion)
                adaptador.InsertCommand = New SqlCommand("INSERT INTO ventasitems (IDVenta, IDProducto, PrecioUnitario, Cantidad, PrecioTotal) VALUES (@IDVenta, @IDProducto, @PrecioUnitario, @Cantidad, @PrecioTotal)", conexion)
                adaptador.InsertCommand.Parameters.Add("@IDVenta", SqlDbType.Int, 0, "IDVenta")
                adaptador.InsertCommand.Parameters.Add("@IDProducto", SqlDbType.Int, 0, "IDProducto")
                adaptador.InsertCommand.Parameters.Add("@PrecioUnitario", SqlDbType.Decimal, 0, "PrecioUnitario")
                adaptador.InsertCommand.Parameters.Add("@Cantidad", SqlDbType.Int, 0, "Cantidad")
                adaptador.InsertCommand.Parameters.Add("@PrecioTotal", SqlDbType.Decimal, 0, "PrecioTotal")

                adaptador.InsertCommand.Transaction = transaction

                adaptador.Update(data)
                transaction.Commit()

            Catch ex As Exception
                transaction.Rollback()
                Console.WriteLine(ex.ToString())
            Finally
                conexion.Close()

            End Try
        End Using
    End Sub

    ''' <summary>
    ''' realizo query para la obtencion de productos que tiene una venta
    ''' </summary>
    ''' <param name="dataGridProductos">objeto datagrid</param>
    ''' <param name="idVenta">id de objeto venta</param>
    ''' <returns>un datatable</returns>
    Public Function GetProductosDeUnaVenta(dataGridProductos, idVenta)
        Dim tablaProductos As New DataTable()

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

    ''' <summary>
    ''' realizo query para la eliminacion de productos asociados a una venta
    ''' </summary>
    ''' <param name="idVenta">id de objeto venta</param>
    Public Sub DeleteProductosDeUnaVenta(idVenta As Integer)
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

    End Sub

    ''' <summary>
    ''' realizo query para la obtencion de todos los datos asociados a una venta
    ''' </summary>
    ''' <param name="datagridDatosVentas">objeto datagrid</param>
    ''' <param name="idCliente">id de objeto cliente</param>
    ''' <returns>de dataTable</returns>
    Public Function GetDatosVentas(datagridDatosVentas, idCliente)
        Dim tablaDatosVentas As New DataTable()

        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = "SELECT ventas.ID as numeroDeVenta, clientes.Cliente, clientes.Telefono, clientes.Correo, productos.Nombre, productos.Precio, productos.Categoria, ventasitems.Cantidad as Cant, ventasitems.PrecioTotal
                                    FROM productos 
                                    JOIN ventasitems ON productos.ID = ventasitems.IDProducto
                                    JOIN ventas ON ventasitems.IDVenta = ventas.ID
                                    join clientes on ventas.IDCliente = clientes.ID
                                    WHERE ventas.IDCliente = @idCliente order by ventasitems.ID;"

            comando.Parameters.AddWithValue("@idCliente", idCliente)

            Dim adaptador As New SqlDataAdapter(comando)

            adaptador.Fill(tablaDatosVentas)

            datagridDatosVentas.DataSource = tablaDatosVentas

            Return tablaDatosVentas
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try

        Return New DataTable()
    End Function

    ''' <summary>
    ''' realizo query para la obtencion de productos vendidos con sus datos en un mes determinado
    ''' </summary>
    ''' <param name="datagridProductosMensuales">objeto data grid</param>
    ''' <param name="fechaMin">fecha minima</param>
    ''' <param name="fechaMax">fecha maxima</param>
    ''' <returns>de dataTable</returns>
    Public Function GetProductosMensuales(datagridProductosMensuales, fechaMin, fechaMax)
        Dim tablaProductosMensuales As New DataTable()
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = "SELECT
                                        productos.Nombre,
                                        SUM(ventasitems.Cantidad) as TotalCantidad,
                                        productos.Precio,
                                        productos.Categoria,
                                        SUM(ventasitems.PrecioTotal) as TotalPrecio
                                    FROM
                                        productos 
                                    JOIN
                                        ventasitems ON productos.ID = ventasitems.IDProducto
                                    JOIN
                                        ventas ON ventasitems.IDVenta = ventas.ID
                                    WHERE
                                        ventas.Fecha >= @fechaMin AND ventas.Fecha <= @fechaMax
                                    GROUP BY
                                        productos.Nombre, productos.Precio, productos.Categoria;"

            comando.Parameters.AddWithValue("@fechaMin", fechaMin)
            comando.Parameters.AddWithValue("@fechaMax", fechaMax)

            Dim adaptador As New SqlDataAdapter(comando)

            adaptador.Fill(tablaProductosMensuales)


            datagridProductosMensuales.DataSource = tablaProductosMensuales

            Return tablaProductosMensuales
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try

        Return New DataTable()
    End Function
End Class

