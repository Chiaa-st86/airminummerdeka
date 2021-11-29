Public Class FormMenuUtama
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim a = New LoginDistributor
        Me.Hide()
        a.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim b = New LoginMarketing
        Me.Hide()
        b.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim c = New LoginWarehouseFG
        Me.Hide()
        c.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim d = New LoginProduksi
        Me.Hide()
        d.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim f = New LoginPurchasing
        Me.Hide()
        f.Show()
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim g = New LoginWarehouseRM
        Me.Hide()
        g.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim h = New LoginQC
        Me.Hide()
        h.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim i = New LoginSupplier
        Me.Hide()
        i.Show()
    End Sub
End Class