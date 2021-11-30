﻿Public Class FormWarehouseFG
    Friend idorderOOSstring As String
    Friend idDOstring As String
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

    'load
    Private Sub FormWarehouseFG_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        tampilkanData("SELECT * FROM marketing_inputOrder", DataGridView2)
        tampilkanData("SELECT * FROM FG_inventori", DataGridView1)
        If DataGridView1.Columns.Contains("FG287634") = True And DataGridView1.Columns.Contains("FG431289") = True Then
            Button1.Enabled = False
        End If

        Dim rnORDERFG As New Random
        Dim tanggalan As DateTime = Now
        Dim formattanggalan As String = "yyyy"
        Dim oos As String = "OOS"
        Dim ubahtanggalanjadistring As String
        ubahtanggalanjadistring = tanggalan.ToString(formattanggalan)
        Dim randomidFG As Double
        randomidFG = rnORDERFG.Next(9999, 99999)
        idorderoosstring = Convert.ToString(oos & randomidFG & ubahtanggalanjadistring)
        TextBox2.Text = idorderOOSstring
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
            End If
        End If
        'botol
        If Val(TextBox8.Text) < 0 Then
            TextBox10.Text = "OUT OF STOCK"
            TextBox13.Text = Math.Abs(Val(TextBox9.Text))
        Else
            If Val(TextBox8.Text) > 0 Or Val(TextBox8.Text) = 0 Then
                TextBox10.Text = "OK"
            End If
        End If
    End Sub
    'edit data
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If (checkEmpty(TextBox1, TextBox3) = True) And (checkEmpty2(ComboBox1) = True) Then
            MessageBox.Show("Data masih kosong")
        Else
            updateData("FG_inventori", "kode_FG", TextBox1.Text, "nama_FG", ComboBox1.Text, "jlh_FG", TextBox3.Text)
            tampilkanData("SELECT * FROM FG_inventori", DataGridView1)
        End If

    End Sub
    'delete data
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
End Class