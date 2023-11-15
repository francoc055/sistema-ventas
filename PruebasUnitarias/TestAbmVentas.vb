Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports DAO
Imports entidades
<TestClass()> Public Class TestAbmVentas

    <TestMethod()> Public Sub ObtenerObjetoAcceso()
        Dim dao = ProductosDao.ObjetoAcceso()

        Assert.IsNotNull(dao, "El objeto no debería ser nulo")
    End Sub

    <TestMethod()> Public Sub InsertarUnaVenta()
        Dim dao = VentasDao.ObjetoAcceso()
        Dim venta = New Ventas() With {.idCliente = 21, .fecha = New Date().Now(), .total = 1000}


        Dim ultimoId = dao.Add(venta)


        Assert.IsTrue(ultimoId > 0, "La inserción no fue exitosa")
    End Sub

    <TestMethod()> Public Sub ObtenerTodasLasVentas()
        Dim dao = VentasDao.ObjetoAcceso()
        Dim venta = New Ventas() With {.idCliente = 25, .fecha = New Date().Now(), .total = 1000}


        dao.Add(venta)
        Dim lista = dao.GetAll()


        Assert.IsTrue(lista.Count > 0, "El listado no fue exitoso")
    End Sub

    <TestMethod()> Public Sub ActualizarUnaVenta()
        Dim dao = VentasDao.ObjetoAcceso()
        Dim venta = New Ventas() With {.id = 56, .idCliente = 25, .fecha = New Date().Now(), .total = 3000}
        Try
            Dim ret = dao.Update(venta)

            Assert.IsTrue(ret = True)
        Catch ex As Exception
            Assert.Fail($"Se produjo una excepción: {ex.Message}")
        End Try
    End Sub

    <TestMethod()> Public Sub EliminarUnaVenta()
        Dim dao = VentasDao.ObjetoAcceso()
        Dim ventaNUeva = New Ventas() With {.idCliente = 25, .fecha = New Date().Now(), .total = 100}
        Try
            Dim ultimoId = dao.Add(ventaNUeva)
            Dim ret As Boolean = dao.Delete(ultimoId)


            Assert.IsTrue(ret = True, "Error en la eliminacion")
        Catch ex As Exception
            Assert.Fail($"Se produjo una excepción: {ex.Message}")
        End Try
    End Sub

End Class