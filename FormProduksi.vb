Public Class FormProduksi
    Private Sub FormProduksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData("SELECT * FROM FG_outofstock", DataGridView1)
        tampilkanData("SELECT * FROM RM_inventori", DataGridView2)
        tampilkanData("SELECT * FROM MPS", DataGridView4)
    End Sub
End Class