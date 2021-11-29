Public Class FormMarketing
    Friend idorderFGstring As String
    Private Sub FormMarketing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'ID random buat order distributor
        Dim rn As New Random
        Dim dist As String = "DIST"
        Dim x As Double
        Dim y As DateTime = Now
        Dim z As String
        Dim formatdatetime As String = "yyyyMMdd"
        Dim id As String
        z = y.ToString(formatdatetime)
        x = rn.Next(9999, 99999)
        id = Convert.ToString(dist & z & x)
        TextBox1.Text = id

        'ID random buat order FG
        Dim rnORDERFG As New Random
        Dim tanggalan As DateTime = Now
        Dim formattanggalan As String = "yyyy"
        Dim ofg As String = "OFG"
        Dim ubahtanggalanjadistring As String
        ubahtanggalanjadistring = tanggalan.ToString(formattanggalan)
        Dim randomidFG As Double
        randomidFG = rnORDERFG.Next(9999, 99999)
        idorderFGstring = Convert.ToString(ofg & randomidFG & ubahtanggalanjadistring)
        TextBox7.Text = idorderFGstring
    End Sub

    'log out
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("Apakah anda ingin keluar ?", "LOG OUT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim a = New FormMenuUtama
            Me.Close()
            a.Show()
        ElseIf result = DialogResult.No Then
        End If
    End Sub

    'menu order distributor
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If ComboBox1.Text = "MerdekAir Cup 220ml (karton)" Then
            TextBox4.Text = hargakartoncup * Val(TextBox3.Text)
        ElseIf ComboBox1.Text = "MerdekAir Botol 600ml (karton)" Then
            TextBox4.Text = hargakartonbotol * Val(TextBox3.Text)
        End If
    End Sub
    'refresh ID apabila ditemukan duplikat ID
    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Dim rn As New Random
        Dim dist As String = "DIST"
        Dim x As Double
        Dim y As DateTime = Now
        Dim z As String
        Dim formatdatetime As String = "yyyyMMdd"
        Dim id As String
        z = y.ToString(formatdatetime)
        x = rn.Next(9999, 99999)
        id = Convert.ToString(dist & z & x)
        TextBox1.Text = id
    End Sub
    'menu order ke FG
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

    End Sub
    'refresh ID apabila ditemukan duplikat ID
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        Dim rnORDERFG As New Random
        Dim tanggalan As DateTime = Now
        Dim formattanggalan As String = "yyyy"
        Dim ofg As String = "OFG"
        Dim ubahtanggalanjadistring As String
        ubahtanggalanjadistring = tanggalan.ToString(formattanggalan)
        Dim randomidFG As Double
        randomidFG = rnORDERFG.Next(9999, 99999)
        idorderFGstring = Convert.ToString(ofg & randomidFG & ubahtanggalanjadistring)
        TextBox7.Text = idorderFGstring
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class