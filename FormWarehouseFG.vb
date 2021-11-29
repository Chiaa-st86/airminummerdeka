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
End Class