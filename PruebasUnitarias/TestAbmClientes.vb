Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports DAO
Imports entidades

<TestClass()> Public Class TestAbmClientes

    <TestMethod()> Public Sub ObtenerObjetoAcceso()
        Dim dao = ClientesDao.ObjetoAcceso()

        Assert.IsNotNull(dao, "El objeto no debería ser nulo")
    End Sub

    <TestMethod()> Public Sub InsertarUnCliente()
        Dim dao = ClientesDao.ObjetoAcceso()
        Dim cliente = New Clientes() With {.cliente = "lucas", .telefono = 1133447766, .correo = "lucas@gmail.com"}


        Dim ultimoId = dao.Add(cliente)


        Assert.IsTrue(ultimoId > 0, "La inserción no fue exitosa")
    End Sub

    <TestMethod()> Public Sub ObtenerTodosLosClientes()
        Dim dao = ClientesDao.ObjetoAcceso()
        Dim cliente = New Clientes() With {.cliente = "pepe", .telefono = 1133447766, .correo = "pepe@gmail.com"}


        dao.Add(cliente)
        Dim lista = dao.GetAll()


        Assert.IsTrue(lista.Count > 0, "El listado no fue exitoso")
    End Sub

    <TestMethod()> Public Sub EliminarUnCliente()
        Dim dao = ClientesDao.ObjetoAcceso()
        Dim listBefore = dao.GetAll()
        Dim cliente = New Clientes() With {.cliente = "clienteBorrar", .telefono = 1133447766, .correo = "borrar@gmail.com"}

        Dim ultimoID = dao.Add(cliente)
        dao.Delete(ultimoID)
        Dim listAfter = dao.GetAll()

        Assert.IsTrue(listBefore.Count = listAfter.Count, "La eliminacion no fue exitosa")
    End Sub

    <TestMethod()> Public Sub ActualizarUnCliente()
        Dim dao = ClientesDao.ObjetoAcceso()
        Dim cliente = New Clientes() With {.cliente = "nombre", .telefono = 1133447766, .correo = "nombre@gmail.com"}


        Dim idCliente = dao.Add(cliente)
        cliente = dao.GetById(idCliente)
        cliente.cliente = "nombreActualizado"
        dao.Update(cliente)
        cliente = dao.GetById(idCliente)


        Assert.IsTrue(cliente.cliente <> "nombre", "La actualizacion no fue exitosa")
    End Sub




End Class