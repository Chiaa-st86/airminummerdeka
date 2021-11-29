Public Class FormWarehouseFG
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem.ToString = "MerdekAir Cup 220ml (karton)" Then
            TextBox1.Text = "FG287634"
        ElseIf ComboBox1.SelectedItem.ToString = "MerdekAir Botol 600ml (karton)" Then
            TextBox1.Text = "FG431289"
        End If
    End Sub

    'logout
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim result As DialogResult = MessageBox.Show("Apakah anda ingin keluar ?", "LOG OUT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim a = New FormMenuUtama
            Me.Close()
            a.Show()
        ElseIf result = DialogResult.No Then
        End If
    End Sub

    Private Sub FormWarehouseFG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData("SELECT * FROM marketing_inputOrder", DataGridView2)
        tampilkanData("SELECT * FROM FG_inventori", DataGridView1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (checkEmpty(TextBox1, TextBox3) = True) And (checkEmpty2(ComboBox1) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            Dim winny As Boolean = checkDuplicate("FG_inventori", "kode_FG", TextBox1.Text)
            If winny = True Then
                MessageBox.Show("Data jangan duplikat")
            Else
                simpanData("FG_inventori", TextBox1.Text, ComboBox1.Text, TextBox3.Text)
                tampilkanData("SELECT * FROM FG_inventori", DataGridView1)
            End If
        End If
    End Sub
End Class