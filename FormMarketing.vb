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

        tampilkanData("SELECT * FROM marketing_invoice", DataGridView1)
        tampilkanData("SELECT * FROM marketing_inputOrder", DataGridView2)
        Button2.Enabled = False
        Button3.Enabled = False
        Button10.Enabled = False
        Button11.Enabled = False
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
    'input
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If (checkEmpty(TextBox1, TextBox7, TextBox2, TextBox10, TextBox3, TextBox4) = True) And (checkEmpty2(ComboBox1) = True) And (checkEmpty3(DateTimePicker1) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            Dim winny As Boolean = checkDuplicate("marketing_invoice", "noinvoice", TextBox1.Text)
            If winny = True Then
                MessageBox.Show("Data jangan duplikat")
            Else
                simpanData("marketing_invoice", TextBox1.Text, TextBox7.Text, TextBox2.Text, TextBox10.Text, DateTimePicker1.Text, ComboBox1.Text, TextBox3.Text, TextBox4.Text)
                tampilkanData("SELECT * FROM marketing_invoice", DataGridView1)

                'acak lagi id no invoice
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
            End If
        End If


    End Sub

    'klik cell
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        'showtoBox2(e.RowIndex, DataGridView1, TextBox1, TextBox7, TextBox2, TextBox10, DateTimePicker1, ComboBox1, TextBox3, TextBox4)
        Try
            TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            TextBox7.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            TextBox2.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            TextBox10.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value
            DateTimePicker1.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value
            ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(5).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(6).Value
            TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(7).Value

            'buat form order FG
            TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value

            Button1.Enabled = False
            Button2.Enabled = True
            Button3.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        'nyalainForm(txtIDAudi, txtNamaAudi, txtKelasAudi, txtJenisFnb)

        '        btnSave.Enabled = False
        '       btnInput.Enabled = False
        '      txtIDAudi.Enabled = False
    End Sub

    'edit data
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (checkEmpty(TextBox1, TextBox7, TextBox2, TextBox10, TextBox3, TextBox4) = True) And (checkEmpty2(ComboBox1) = True) And (checkEmpty3(DateTimePicker1) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            updateData("marketing_invoice", "noinvoice", TextBox1.Text, "kodeOrder", TextBox7.Text, "namapelanggan", TextBox2.Text, "alamatpelanggan", TextBox10.Text, "tglpesan", DateTimePicker1.Text, "nama_FG", ComboBox1.Text, "jlhbarang", TextBox3.Text, "jlhbayar", TextBox4.Text)
            tampilkanData("SELECT * FROM marketing_invoice", DataGridView1)

            TextBox1.Clear()
            TextBox7.Clear()
            TextBox2.Clear()
            TextBox10.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            Button1.Enabled = True
        End If
    End Sub
    'delete data
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cek As Boolean = checkDuplicate("marketing_invoice", "noinvoice", TextBox1.Text)
        If TextBox1.Text = "" Or cek = False Then
            MessageBox.Show("tidak ada data yang dipilih")
        Else
            If MessageBox.Show("Apakah anda ingin hapus data ?", "HAPUS DATA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                hapusData("marketing_invoice", "noinvoice", TextBox1.Text)
                tampilkanData("SELECT * FROM marketing_invoice", DataGridView1)
                TextBox1.Clear()
                TextBox7.Clear()
                TextBox2.Clear()
                TextBox10.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
                Button1.Enabled = True
            End If
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
    'menu order ke FG
    'input
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If (checkEmpty(TextBox5, TextBox9, TextBox8) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            Dim winny As Boolean = checkDuplicate("marketing_inputOrder", "kodeOrder", TextBox1.Text)
            If winny = True Then
                MessageBox.Show("Data jangan duplikat")
            Else
                simpanData("marketing_inputOrder", TextBox5.Text, DateTimePicker2.Text, TextBox9.Text, TextBox8.Text)
                tampilkanData("SELECT * FROM marketing_inputOrder", DataGridView2)
            End If
        End If
    End Sub

    'klik cell
    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        'showtoBox2(e.RowIndex, DataGridView1, TextBox1, TextBox7, TextBox2, TextBox10, DateTimePicker1, ComboBox1, TextBox3, TextBox4)
        Try
            TextBox5.Text = DataGridView2.Rows(e.RowIndex).Cells(0).Value
            DateTimePicker2.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value
            TextBox9.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value
            TextBox8.Text = DataGridView2.Rows(e.RowIndex).Cells(3).Value

            Button9.Enabled = False
            Button10.Enabled = True
            Button11.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'edit data
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If (checkEmpty(TextBox5, TextBox9, TextBox8) = True) And (checkEmpty3(DateTimePicker1) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            updateData("marketing_inputOrder", "kodeOrder", TextBox5.Text, "tglpesanFG", DateTimePicker1.Text, "cup", TextBox9.Text, "botol", TextBox8.Text)
            tampilkanData("SELECT * FROM marketing_inputOrder", DataGridView2)

            TextBox5.Clear()
            TextBox9.Clear()
            TextBox8.Clear()
            Button9.Enabled = True
        End If
    End Sub
    'delete data
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim cek As Boolean = checkDuplicate("marketing_inputOrder", "kodeOrder", TextBox5.Text)
        If TextBox5.Text = "" Or cek = False Then
            MessageBox.Show("tidak ada data yang dipilih")
        Else
            If MessageBox.Show("Apakah anda ingin hapus data ?", "HAPUS DATA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                hapusData("marketing_inputOrder", "kodeOrder", TextBox5.Text)
                tampilkanData("SELECT * FROM marketing_inputOrder", DataGridView2)
                TextBox5.Clear()
                TextBox9.Clear()
                TextBox8.Clear()
                Button9.Enabled = True
            End If
        End If
    End Sub

    'refresh ID apabila ditemukan duplikat ID
    Private Sub Button14_Click(sender As Object, e As EventArgs)
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
End Class