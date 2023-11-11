Imports System.Data.SqlClient
Imports entidades

Public Class ClientesDao
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

    Public Shared Function ObjetoAcceso() As ClientesDao
        If ObjetoDao Is Nothing Then
            ObjetoDao = New ClientesDao()
        End If
        Return ObjetoDao
    End Function

    Public Function Add(cliente As Clientes)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"insert into clientes (Cliente,Telefono,Correo) values(@Cliente,@Telefono,@Correo)"
            comando.Parameters.AddWithValue("@Cliente", cliente.cliente)
            comando.Parameters.AddWithValue("@Telefono", cliente.telefono)
            comando.Parameters.AddWithValue("@Correo", cliente.correo)


            comando.ExecuteNonQuery()
            Console.WriteLine("cliente insertado")

        Catch e As Exception
            Console.WriteLine(e.ToString())
        Finally
            conexion.Close()
        End Try

    End Function


    Public Function GetAll() As List(Of Clientes)
        Dim listaClientes As New List(Of Clientes)
        Try
            conexion.Open()
            comando.CommandText = $"select * from clientes"
            Using dataReader As SqlDataReader = comando.ExecuteReader()
                While dataReader.Read()
                    Dim cliente As New Clientes()
                    cliente.id = Convert.ToInt32(dataReader("ID"))
                    cliente.cliente = dataReader("Cliente").ToString()
                    cliente.telefono = dataReader("Telefono").ToString()
                    cliente.correo = dataReader("Correo").ToString()
                    listaClientes.Add(cliente)
                End While

                Return listaClientes
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function GetById(id As Integer) As Clientes

        Dim cliente As New Clientes()
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"select * from clientes where ID = @id"
            comando.Parameters.AddWithValue("@id", id)
            Using dataReader As SqlDataReader = comando.ExecuteReader()
                While dataReader.Read()
                    cliente.id = Convert.ToInt32(dataReader("ID"))
                    cliente.cliente = dataReader("Cliente").ToString()
                    cliente.telefono = dataReader("Telefono").ToString()
                    cliente.correo = dataReader("Correo").ToString()
                End While

                Return cliente
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function Update(cliente As Clientes)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"update clientes 
                                    set Cliente = @cliente, 
                                    Telefono = @telefono,
                                    Correo = @correo
                                    where ID = @id"
            comando.Parameters.AddWithValue("@cliente", cliente.cliente)
            comando.Parameters.AddWithValue("@telefono", cliente.telefono)
            comando.Parameters.AddWithValue("@correo", cliente.correo)
            comando.Parameters.AddWithValue("@id", cliente.id)

            comando.ExecuteNonQuery()
            Console.WriteLine("cliente modificado")

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
            comando.CommandText = "delete from clientes where ID = @id"
            comando.Parameters.AddWithValue("@id", id)
            comando.ExecuteNonQuery()

            Console.WriteLine("cliente eliminado")


        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

End Class
