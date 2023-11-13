Imports entidades
Imports System.Data.SqlClient
Imports DAO

Module Module1

    Sub Main()


        Dim dao = VentasDao.ObjetoAcceso()

        dao.AddVentasItems2(1, 2, 100, 1, 100)

        'INSERT
        'Dim cliente As Clientes = New Clientes()
        'cliente.cliente = "pepe"
        'cliente.telefono = "1133987654"
        'cliente.correo = "pepe@gmail.com"

        'objeto.Guardar(cliente)


        'GET ALL    
        'Dim objeto2 = ClientesDao.ObjetoAcceso()

        'Dim lista As List(Of Clientes)

        'lista = objeto2.Leer()

        'For Each item As Clientes In lista
        '    Console.WriteLine(item.id)
        '    Console.WriteLine(item.cliente)
        '    Console.WriteLine(item.telefono)
        '    Console.WriteLine(item.correo)
        'Next

        'GET BY ID
        'Console.WriteLine(prueba.LeerPorId(14).correo)


        'MODIFICAR
        'prueba.Modificar("pepe", 9)

        'ELIMINAR
        'prueba.ELiminar(9)







        Console.ReadLine()

    End Sub

    'Public Class ClientesDao
    '    Public cadenaConexion As String
    '    Public comando As SqlCommand
    '    Public conexion As SqlConnection

    '    Public Sub New()
    '        cadenaConexion = $"Data Source=.;Initial Catalog=pruebademo;Integrated Security=true"
    '        comando = New SqlCommand()
    '        conexion = New SqlConnection(cadenaConexion)
    '        comando.CommandType = System.Data.CommandType.Text
    '        comando.Connection = conexion
    '    End Sub

    '    Public Function Guardar(cliente As Clientes)
    '        Try
    '            comando.Parameters.Clear()
    '            conexion.Open()
    '            comando.CommandText = $"insert into clientes (Cliente,Telefono,Correo) values(@Cliente,@Telefono,@Correo)"
    '            comando.Parameters.AddWithValue("@Cliente", cliente.cliente)
    '            comando.Parameters.AddWithValue("@Telefono", cliente.telefono)
    '            comando.Parameters.AddWithValue("@Correo", cliente.correo)


    '            comando.ExecuteNonQuery()
    '            Console.WriteLine("cliente insertado")

    '        Catch e As Exception
    '            Console.WriteLine(e.ToString())
    '        Finally
    '            conexion.Close()
    '        End Try

    '    End Function


    '    Public Function Leer() As List(Of Clientes)
    '        Dim listaClientes As New List(Of Clientes)
    '        Try
    '            conexion.Open()
    '            comando.CommandText = $"select * from clientes"
    '            Using dataReader As SqlDataReader = comando.ExecuteReader()
    '                While dataReader.Read()
    '                    Dim cliente As New Clientes()
    '                    cliente.id = Convert.ToInt32(dataReader("ID"))
    '                    cliente.cliente = dataReader("Cliente").ToString()
    '                    cliente.telefono = dataReader("Telefono").ToString()
    '                    cliente.correo = dataReader("Correo").ToString()
    '                    listaClientes.Add(cliente)
    '                End While

    '                Return listaClientes
    '            End Using
    '        Catch ex As Exception
    '            Console.WriteLine(ex.ToString())
    '        Finally
    '            conexion.Close()
    '        End Try
    '    End Function

    '    Public Function LeerPorId(id As Integer) As Clientes

    '        Dim cliente As New Clientes()
    '        Try
    '            comando.Parameters.Clear()
    '            conexion.Open()
    '            comando.CommandText = $"select * from clientes where ID = @id"
    '            comando.Parameters.AddWithValue("@id", id)
    '            Using dataReader As SqlDataReader = comando.ExecuteReader()
    '                While dataReader.Read()
    '                    cliente.id = Convert.ToInt32(dataReader("ID"))
    '                    cliente.cliente = dataReader("Cliente").ToString()
    '                    cliente.telefono = dataReader("Telefono").ToString()
    '                    cliente.correo = dataReader("Correo").ToString()
    '                End While

    '                Return cliente
    '            End Using
    '        Catch ex As Exception
    '            Console.WriteLine(ex.ToString())
    '        Finally
    '            conexion.Close()
    '        End Try
    '    End Function

    '    Public Function Modificar(nombre As String, id As Integer)
    '        Try
    '            comando.Parameters.Clear()
    '            conexion.Open()
    '            comando.CommandText = $"UPDATE clientes SET Cliente = @nombre WHERE ID = @id"
    '            comando.Parameters.AddWithValue("@nombre", nombre)
    '            comando.Parameters.AddWithValue("@id", id)

    '            comando.ExecuteNonQuery()
    '            Console.WriteLine("cliente modificado")

    '        Catch ex As Exception
    '            Console.WriteLine(ex.ToString())
    '        Finally
    '            conexion.Close()
    '        End Try
    '    End Function

    '    Public Function Eliminar(id As Integer)
    '        Try
    '            comando.Parameters.Clear()
    '            conexion.Open()
    '            comando.CommandText = "delete from clientes where ID = @id"
    '            comando.Parameters.AddWithValue("@id", id)
    '            comando.ExecuteNonQuery()

    '            Console.WriteLine("cliente eliminado")


    '        Catch ex As Exception
    '            Console.WriteLine(ex.ToString())
    '        Finally
    '            conexion.Close()
    '        End Try
    '    End Function



    'End Class



End Module
