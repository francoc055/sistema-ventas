Imports System.Data.SqlClient
Imports entidades
Imports System.Configuration

Public Class ClientesDao
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
    Public Shared Function ObjetoAcceso() As ClientesDao
        If ObjetoDao Is Nothing Then
            ObjetoDao = New ClientesDao()
        End If
        Return ObjetoDao
    End Function



    ''' <summary>
    ''' realizo query que agrega un objeto cliente a la base de datos
    ''' </summary>
    ''' <param name="cliente">objeto cliente</param>
    Public Sub Add(cliente As Clientes)
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

    End Sub

    ''' <summary>
    ''' realizo query para la obtencion de una lista de clientes de la base de datos
    ''' </summary>
    ''' <returns>retorno lista de clientes </returns>
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

    ''' <summary>
    ''' realizo query para la obtecion de un objeto cliente
    ''' </summary>
    ''' <param name="id">campo id de un objeto cliente</param>
    ''' <returns>retorno objeto cliente</returns>
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

    ''' <summary>
    ''' realizo query para actualizar los campos de un objeto cliente en la base de datos
    ''' </summary>
    ''' <param name="cliente">objeto cliente</param>
    Public Sub Update(cliente As Clientes)
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
    End Sub

    ''' <summary>
    ''' realizo query que elimina un cliente de la base de datos
    ''' </summary>
    ''' <param name="id">campo id de cliente</param>
    Public Sub Delete(id As Integer)
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
    End Sub



End Class
