Imports System.Data.SqlClient
Imports entidades
Public Class ProductosDao
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

    Public Shared Function ObjetoAcceso() As ProductosDao
        If ObjetoDao Is Nothing Then
            ObjetoDao = New ProductosDao()
        End If
        Return ObjetoDao
    End Function

    Public Function Add(producto As Productos)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"insert into productos (Nombre,Precio,Categoria) values(@Nombre,@Precio,@Categoria)"
            comando.Parameters.AddWithValue("@Nombre", producto.nombre)
            comando.Parameters.AddWithValue("@Precio", producto.precio)
            comando.Parameters.AddWithValue("@Categoria", producto.categoria)


            comando.ExecuteNonQuery()
            Console.WriteLine("producto insertado")

        Catch e As Exception
            Console.WriteLine(e.ToString())
        Finally
            conexion.Close()
        End Try

    End Function


    Public Function GetAll() As List(Of Productos)
        Dim listaProductos As New List(Of Productos)
        Try
            conexion.Open()
            comando.CommandText = $"select * from productos"
            Using dataReader As SqlDataReader = comando.ExecuteReader()
                While dataReader.Read()
                    Dim producto As New Productos()
                    producto.id = Convert.ToInt32(dataReader("ID"))
                    producto.nombre = dataReader("Nombre").ToString()
                    producto.precio = dataReader("Precio").ToString()
                    producto.categoria = dataReader("Categoria").ToString()
                    listaProductos.Add(producto)
                End While

                Return listaProductos
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function GetById(id As Integer) As Productos

        Dim producto As New Productos()
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"select * from productos where ID = @id"
            comando.Parameters.AddWithValue("@id", id)
            Using dataReader As SqlDataReader = comando.ExecuteReader()
                While dataReader.Read()
                    producto.id = Convert.ToInt32(dataReader("ID"))
                    producto.nombre = dataReader("Nombre").ToString()
                    producto.precio = dataReader("Precio").ToString()
                    producto.categoria = dataReader("Categoria").ToString()
                End While

                Return producto
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function Update(producto As Productos)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = $"update productos 
                                    set Nombre = @nombre, 
                                    Precio = @precio,
                                    Categoria = @categoria
                                    where ID = @id"
            comando.Parameters.AddWithValue("@nombre", producto.nombre)
            comando.Parameters.AddWithValue("@precio", producto.precio)
            comando.Parameters.AddWithValue("@categoria", producto.categoria)
            comando.Parameters.AddWithValue("@id", producto.id)

            comando.ExecuteNonQuery()
            Console.WriteLine("porducto modificado")

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
            comando.CommandText = "delete from productos where ID = @id"
            comando.Parameters.AddWithValue("@id", id)
            comando.ExecuteNonQuery()

            Console.WriteLine("producto eliminado")


        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try
    End Function

    Public Function FiltrarPorPrecio(min As Integer, max As Integer) As List(Of Productos)
        Dim listaProductos As New List(Of Productos)
        Try
            comando.Parameters.Clear()
            conexion.Open()
            comando.CommandText = "SELECT * FROM productos WHERE Precio >= @min AND Precio <= @max"
            comando.Parameters.AddWithValue("@min", min)
            comando.Parameters.AddWithValue("@max", max)

            Using dataReader As SqlDataReader = comando.ExecuteReader()
                While dataReader.Read()
                    Dim producto As New Productos()
                    producto.id = Convert.ToInt32(dataReader("ID"))
                    producto.nombre = dataReader("Nombre").ToString()
                    producto.precio = Convert.ToDecimal(dataReader("Precio")) ' Ajustar al tipo de dato correcto
                    producto.categoria = dataReader("Categoria").ToString()
                    listaProductos.Add(producto)
                End While
            End Using

            Return listaProductos
        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        Finally
            conexion.Close()
        End Try

        Return Nothing
    End Function

End Class
