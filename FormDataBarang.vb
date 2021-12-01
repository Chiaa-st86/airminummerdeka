Public Class FormDataBarang
    Private Sub FormDataBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData("SELECT * FROM data_produk", DataGridView1)
    End Sub
    'add
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (checkEmpty(TextBox1, TextBox2) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            Dim winny As Boolean = checkDuplicate("data_produk", "id_produk", TextBox1.Text)
            If winny = True Then
                MessageBox.Show("Data jangan duplikat")
            Else
                simpanData("data_produk", TextBox1.Text, TextBox2.Text)
                tampilkanData("SELECT * FROM data_produk", DataGridView1)
            End If
        End If
    End Sub
    'edit data
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub
    'delete
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub
End Class