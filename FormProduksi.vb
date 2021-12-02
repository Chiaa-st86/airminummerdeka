Imports System.Data.OleDb
Public Class FormProduksi
    'kelompok sub
    Sub produk()
        Dim cmd = New OleDbCommand("SELECT * FROM data_produk", Conn)
        Dim rd = cmd.ExecuteReader
        ComboBox2.Items.Clear()
        Do While rd.Read = True
            ComboBox2.Items.Add(rd.Item("nama_produk"))
        Loop
    End Sub

    'load
    Private Sub FormProduksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData("SELECT * FROM FG_outofstock", DataGridView1)
        tampilkanData("SELECT * FROM RM_inventori", DataGridView2)
        tampilkanData("SELECT * FROM MPS", DataGridView4)
        Call produk()
    End Sub

    'logout
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim result As DialogResult = MessageBox.Show("Apakah anda ingin keluar ?", "LOG OUT", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            Dim a = New FormMenuUtama
            Me.Close()
            a.Show()
        ElseIf result = DialogResult.No Then
        End If
    End Sub

    'refresh produk
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        Call produk()
    End Sub

    'LIST OUT OF STOCK

    'klik cell
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            TextBox23.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value 'ID_oos
            TextBox21.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value 'tgloos
            TextBox13.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value 'id_produk
            TextBox14.Text = DataGridView1.Rows(e.RowIndex).Cells(3).Value 'nama_produk
            TextBox16.Text = DataGridView1.Rows(e.RowIndex).Cells(4).Value 'kurang_stok
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Data Kebutuhan RM
    'RM dari data warehouse RM klik cell
    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        TextBox1.Text = DataGridView2.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = DataGridView2.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = DataGridView2.Rows(e.RowIndex).Cells(2).Value
        TextBox4.Text = DataGridView2.Rows(e.RowIndex).Cells(3).Value
    End Sub

    'Spesifikasi Produk
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        'id produk otomatis saat user memilih produk jd gaperlu isi2 lagi yey :3
        Dim sql As String
        Dim cmd = New OleDbCommand
        Dim dt = New DataTable
        Dim da = New OleDbDataAdapter
        Try
            sql = "SELECT * FROM data_produk WHERE nama_produk = '" & ComboBox2.Text & "'"
            cmd.Connection = Conn
            cmd.CommandText = sql
            da.SelectCommand = cmd
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                TextBox7.Text = dt.Rows(0).Item("id_produk").ToString
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub




End Class