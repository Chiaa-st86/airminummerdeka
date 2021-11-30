Public Class Menu_Utama
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim a As New LoginDistributor
        a.Show()
        Me.Hide()

    End Sub
End Class