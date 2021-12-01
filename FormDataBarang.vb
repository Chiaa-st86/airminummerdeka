Public Class FormDataBarang
    Private Sub FormDataBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData("SELECT * FROM data_produk", DataGridView1)
    End Sub
End Class