Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports DAO
Imports entidades
<TestClass()> Public Class TestAbmProductos

    <TestMethod()> Public Sub ObtenerObjetoAcceso()
        Dim dao = ProductosDao.ObjetoAcceso()

        Assert.IsNotNull(dao, "El objeto no debería ser nulo")
    End Sub

    <TestMethod()> Public Sub InsertarUnProducto()
        Dim dao = ProductosDao.ObjetoAcceso()
        Dim producto = New Productos() With {.nombre = "fideos", .precio = 1000, .categoria = "carbohidratos"}


        Dim ultimoId = dao.Add(producto)


        Assert.IsTrue(ultimoId > 0, "La inserción no fue exitosa")
    End Sub

    <TestMethod()> Public Sub ObtenerTodosLosProductos()
        Dim dao = ProductosDao.ObjetoAcceso()
        Dim producto = New Productos() With {.nombre = "fideos", .precio = 1000, .categoria = "carbohidratos"}


        dao.Add(producto)
        Dim lista = dao.GetAll()


        Assert.IsTrue(lista.Count > 0, "El listado no fue exitoso")
    End Sub

    <TestMethod()> Public Sub EliminarUnProducto()
        Dim dao = ProductosDao.ObjetoAcceso()
        Dim listBefore = dao.GetAll()
        Dim producto = New Productos() With {.nombre = "borrar", .precio = 1000, .categoria = "carbohidratos"}

        Dim ultimoID = dao.Add(producto)
        dao.Delete(ultimoID)
        Dim listAfter = dao.GetAll()

        Assert.IsTrue(listBefore.Count = listAfter.Count, "La eliminacion no fue exitosa")
    End Sub

    <TestMethod()> Public Sub ActualizarUnProducto()
        Dim dao = ProductosDao.ObjetoAcceso()
        Dim producto = New Productos() With {.nombre = "nombre", .precio = 1000, .categoria = "carbohidratos"}


        Dim idProducto = dao.Add(producto)
        producto = dao.GetById(idProducto)
        producto.nombre = "nombreActualizado"
        dao.Update(producto)
        producto = dao.GetById(idProducto)


        Assert.IsTrue(producto.nombre <> "nombre", "La actualizacion no fue exitosa")
    End Sub

End Class