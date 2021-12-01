Imports System.Data.OleDb
Public Class FormWarehouseFG
    Friend idorderOOSstring As String
    Friend idDOstring As String
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'id produk otomatis saat user memilih produk jd gaperlu isi2 lagi yey :3
        Dim sql As String
        Dim cmd = New OleDbCommand
        Dim dt = New DataTable
        Dim da = New OleDbDataAdapter
        Try
            sql = "SELECT * FROM data_produk WHERE nama_produk = '" & ComboBox1.Text & "'"
            cmd.Connection = Conn
            cmd.CommandText = sql
            da.SelectCommand = cmd
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                TextBox1.Text = dt.Rows(0).Item("id_produk").ToString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
    Sub produk()
        Dim cmd = New OleDbCommand("SELECT * FROM data_produk", Conn)
        Dim rd = cmd.executereader
        ComboBox1.Items.Clear()
        Do While rd.read = True
            ComboBox1.Items.Add(rd.item("nama_produk"))
        Loop
    End Sub
    'load
    Private Sub FormWarehouseFG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData("SELECT * FROM marketing_inputOrder", DataGridView2)
        tampilkanData("SELECT * FROM FG_inventori", DataGridView1)
        tampilkanData("SELECT * FROM FG_outofstock", DataGridView3)
        tampilkanData("SELECT * FROM marketing_invoice", DataGridView4)
        tampilkanData("SELECT * FROM FG_pengiriman", DataGridView5)
        Call produk()

        Dim rnORDEROOS As New Random
        Dim tanggalan As DateTime = Now
        Dim formattanggalan As String = "yyyy"
        Dim oos As String = "OOS"
        Dim ubahtanggalanjadistring As String
        ubahtanggalanjadistring = tanggalan.ToString(formattanggalan)
        Dim randomidOOS As Double
        randomidOOS = rnORDEROOS.Next(9999, 99999)
        idorderOOSstring = Convert.ToString(oos & randomidOOS & ubahtanggalanjadistring)
        TextBox2.Text = idorderOOSstring


        Dim rnORDERDO As New Random
        Dim tanggalanDO As DateTime = Now
        Dim formattanggalanDO As String = "yyyy"
        Dim deliveryorder As String = "DO"
        Dim ubahtanggalanjadistringDO As String
        ubahtanggalanjadistringDO = tanggalanDO.ToString(formattanggalanDO)
        Dim randomidDO As Double
        randomidDO = rnORDERDO.Next(9999, 99999)
        idDOstring = Convert.ToString(deliveryorder & randomidDO & ubahtanggalanjadistringDO)
        TextBox14.Text = idDOstring

    End Sub
    'INPUT STOK DATA
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
    'Taruh ke textbox Stock
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            TextBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
            ComboBox1.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
            TextBox3.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value

            If TextBox1.Text = "FG287634" Then
                TextBox4.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            ElseIf TextBox1.Text = "FG431289" Then
                TextBox5.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'Taruh ke textbox order
    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Try
            TextBox7.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value
            TextBox6.Text = DataGridView2.Rows(e.RowIndex).Cells(3).Value
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'HITUNG OOS
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        TextBox9.Text = Val(TextBox4.Text) - Val(TextBox7.Text)
        TextBox8.Text = Val(TextBox5.Text) - Val(TextBox6.Text)
        'cup
        If Val(TextBox9.Text) < 0 Then
            TextBox11.Text = "OUT OF STOCK"
            TextBox12.Text = Math.Abs(Val(TextBox9.Text))
        Else
            If Val(TextBox9.Text) > 0 Or Val(TextBox9.Text) = 0 Then
                TextBox11.Text = "OK"
                TextBox12.Text = 0
            End If
        End If
        'botol
        If Val(TextBox8.Text) < 0 Then
            TextBox10.Text = "OUT OF STOCK"
            TextBox13.Text = Math.Abs(Val(TextBox9.Text))
        Else
            If Val(TextBox8.Text) > 0 Or Val(TextBox8.Text) = 0 Then
                TextBox10.Text = "OK"
                TextBox13.Text = 0
            End If
        End If
    End Sub
    'edit data stok
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (checkEmpty(TextBox2, TextBox12, TextBox13) = True) And (checkEmpty2(ComboBox1) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            updateData("FG_inventori", "kode_FG", TextBox1.Text, "nama_FG", ComboBox1.Text, "jlh_FG", TextBox3.Text)
            tampilkanData("SELECT * FROM FG_inventori", DataGridView1)
        End If

    End Sub
    'delete data stok
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim cek As Boolean = checkDuplicate("FG_inventori", "kode_FG", TextBox1.Text)
        If TextBox1.Text = "" Or cek = False Then
            MessageBox.Show("tidak ada data yang dipilih")
        Else
            If MessageBox.Show("Apakah anda ingin hapus data ?", "HAPUS DATA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                hapusData("FG_inventori", "kode_FG", TextBox1.Text)
                tampilkanData("SELECT * FROM FG_inventori", DataGridView1)
            End If
        End If
    End Sub
    'input oos
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If (checkEmpty(TextBox2, TextBox12, TextBox13) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            Dim winny As Boolean = checkDuplicate("FG_outofstock", "ID_oos", TextBox2.Text)
            If winny = True Then
                MessageBox.Show("Data jangan duplikat")
            Else
                simpanData("FG_outofstock", TextBox2.Text, DateTimePicker3.Text, TextBox12.Text, TextBox13.Text)
                tampilkanData("SELECT * FROM FG_outofstock", DataGridView3)
            End If
        End If

        Dim rnORDEROOS As New Random
        Dim tanggalan As DateTime = Now
        Dim formattanggalan As String = "yyyy"
        Dim oos As String = "OOS"
        Dim ubahtanggalanjadistring As String
        ubahtanggalanjadistring = tanggalan.ToString(formattanggalan)
        Dim randomidOOS As Double
        randomidOOS = rnORDEROOS.Next(9999, 99999)
        idorderOOSstring = Convert.ToString(oos & randomidOOS & ubahtanggalanjadistring)
        TextBox2.Text = idorderOOSstring
    End Sub
    'edit oos
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If (checkEmpty(TextBox2, TextBox12, TextBox13) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            updateData("FG_outofstock", "ID_oos", TextBox1.Text, "tgloos", DateTimePicker3.Text, "cup_kurangstok", TextBox12.Text, "botol_kurangstok", TextBox13.Text)
            tampilkanData("SELECT * FROM FG_outofstock", DataGridView3)
        End If
    End Sub
    'delete oos
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim cek As Boolean = checkDuplicate("FG_outofstock", "ID_oos", TextBox2.Text)
        If TextBox2.Text = "" Or cek = False Then
            MessageBox.Show("tidak ada data yang dipilih")
        Else
            If MessageBox.Show("Apakah anda ingin hapus data ?", "HAPUS DATA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                hapusData("FG_outofstock", "ID_oos", TextBox2.Text)
                tampilkanData("SELECT * FROM FG_outofstock", DataGridView3)
            End If
        End If
    End Sub
    'klik cell
    Private Sub DataGridView3_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView3.CellClick
        Try
            TextBox2.Text = DataGridView3.Rows(e.RowIndex).Cells(0).Value
            DateTimePicker3.Text = DataGridView3.Rows(e.RowIndex).Cells(1).Value
            TextBox12.Text = DataGridView3.Rows(e.RowIndex).Cells(2).Value
            TextBox13.Text = DataGridView3.Rows(e.RowIndex).Cells(3).Value
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    'Delivery Order
    'klik cell
    Private Sub DataGridView4_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView4.CellClick
        Try
            TextBox15.Text = DataGridView4.Rows(e.RowIndex).Cells(0).Value
            TextBox16.Text = DataGridView4.Rows(e.RowIndex).Cells(2).Value
            TextBox17.Text = DataGridView4.Rows(e.RowIndex).Cells(3).Value
            TextBox18.Text = DataGridView4.Rows(e.RowIndex).Cells(4).Value
            TextBox19.Text = DataGridView4.Rows(e.RowIndex).Cells(5).Value
            TextBox20.Text = DataGridView4.Rows(e.RowIndex).Cells(6).Value
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'kilk cell dari data DO
    Private Sub DataGridView5_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView5.CellClick
        Try
            TextBox14.Text = DataGridView5.Rows(e.RowIndex).Cells(0).Value
            TextBox15.Text = DataGridView5.Rows(e.RowIndex).Cells(1).Value
            TextBox16.Text = DataGridView5.Rows(e.RowIndex).Cells(2).Value
            TextBox17.Text = DataGridView5.Rows(e.RowIndex).Cells(3).Value
            DateTimePicker4.Text = DataGridView5.Rows(e.RowIndex).Cells(4).Value
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    'insert DO
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        If (checkEmpty(TextBox14, TextBox15, TextBox16, TextBox17, TextBox18, TextBox19, TextBox20) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            Dim winny As Boolean = checkDuplicate("FG_pengiriman", "IDkirimFG", TextBox14.Text)
            If winny = True Then
                MessageBox.Show("Data jangan duplikat")
            Else
                simpanData("FG_pengiriman", TextBox14.Text, TextBox15.Text, TextBox16.Text, TextBox17.Text, DateTimePicker4.Text)
                tampilkanData("SELECT * FROM FG_pengiriman", DataGridView5)
            End If
        End If

        Dim rnORDERDO As New Random
        Dim tanggalanDO As DateTime = Now
        Dim formattanggalanDO As String = "yyyy"
        Dim deliveryorder As String = "DO"
        Dim ubahtanggalanjadistringDO As String
        ubahtanggalanjadistringDO = tanggalanDO.ToString(formattanggalanDO)
        Dim randomidDO As Double
        randomidDO = rnORDERDO.Next(9999, 99999)
        idDOstring = Convert.ToString(deliveryorder & randomidDO & ubahtanggalanjadistringDO)
        TextBox14.Text = idDOstring
    End Sub
    'edit DO
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        If (checkEmpty(TextBox14, TextBox15, TextBox16, TextBox17, TextBox18, TextBox19, TextBox20) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            updateData("FG_pengiriman", "IDkirimFG", TextBox14.Text, "noinvoice", TextBox15.Text, "namapelanggan", TextBox16.Text, "alamatpelanggan", TextBox17.Text, "tglkirim", DateTimePicker4.Text)
            tampilkanData("SELECT * FROM FG_pengiriman", DataGridView5)
        End If
    End Sub
    'Delete DO
    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim cek As Boolean = checkDuplicate("FG_pengiriman", "IDkirimFG", TextBox14.Text)
        If TextBox14.Text = "" Or cek = False Then
            MessageBox.Show("tidak ada data yang dipilih")
        Else
            If MessageBox.Show("Apakah anda ingin hapus data ?", "HAPUS DATA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                hapusData("FG_pengiriman", "IDkirimFG", TextBox14.Text)
                tampilkanData("SELECT * FROM FG_pengiriman", DataGridView5)
            End If
        End If
    End Sub
End Class