Public Class FormWarehouseRM

    ' input kebutuhan
    Private Sub btninputREQ_Click(sender As Object, e As EventArgs) Handles btninputREQ.Click
        nyalainForm(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM)
        clearForm(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM)
        DateTimePicker1.Enabled = True
        enableButton(btnsaveREQ, btndeleteREQ, btneditREQ, btncancelREQ, btnexitPURC)
    End Sub

    'LOAD
    Private Sub FormWarehouseRM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        matiForm(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM)
        DateTimePicker1.Enabled = False
        clearForm(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM)
        tampilkanData("SELECT * FROM RM_inputRequirement", DataGridView1)
        tampilkanData("SELECT * FROM RM_inventori", DataGridView2)
        disableButton(btncancelInventori, btncancelREQ, btndeleteinventori, btndeleteREQ, btneditInventori, btneditREQ, btnsaveInventori, btnsaveREQ)
    End Sub

    'exit
    Private Sub btnexitPURC_Click(sender As Object, e As EventArgs) Handles btnexitPURC.Click
        Dim a As New FormMenuUtama
        a.Show()
        Me.Close()
    End Sub

    'cancel
    Private Sub btncancelREQ_Click(sender As Object, e As EventArgs) Handles btncancelREQ.Click
        matiForm(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM)
        DateTimePicker1.Enabled = False
        clearForm(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM)
    End Sub

    'DELETE
    Private Sub btndeleteREQ_Click(sender As Object, e As EventArgs) Handles btndeleteREQ.Click
        If (checkEmpty(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM) = True) Then
            MessageBox.Show("Jangan kosong")
        Else
            hapusData("RM_inputRequirement", "No_PO", txtkodeREQ.Text)
            tampilkanData("SELECT * FROM RM_inputRequirement", DataGridView1)
        End If

        enableButton(btncancelREQ, btndeleteREQ, btneditREQ, btnsaveREQ, btninputREQ)
    End Sub

    'EDIT
    Private Sub btneditREQ_Click(sender As Object, e As EventArgs) Handles btneditREQ.Click
        If (checkEmpty(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM) = True) Then
            MessageBox.Show("Lengkapi Dulu")
            Exit Sub
        Else
            Call koneksiDB()
            CMD = New OleDb.OleDbCommand("UPDATE RM_inputRequirement SET tgl_req ='" & DateTimePicker1.Text & "',  kode_RM = '" & txtkodeRMi.Text & "',nama_RM = '" & txtnamaRMi.Text & " ', kebutuhan = '" & txtkebutuhan.Text & "', satuan_RM ='" & txtsatuanRM.Text & "' WHERE kode_req ='" & txtkodeREQ.Text & "'", Conn)
            DM = CMD.ExecuteReader
            MessageBox.Show("Yatta! berhasil")
            clearForm(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM)

        End If
    End Sub

    'SAVE   
    Private Sub btnsaveREQ_Click(sender As Object, e As EventArgs) Handles btnsaveREQ.Click
        If (checkEmpty(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM) = True) Then
            MessageBox.Show("Jangan kosong")
        Else
            Dim winny As Boolean = checkDuplicate("RM_inputRequirement", "kode_req", txtkodeREQ.Text)
            If winny = True Then
                MessageBox.Show("KODE Duplikat")
            Else
                'txtKelaminPeg.Text = Cbxkelamin.SelectedItem.ToString
                'txtJabatanPeg.Text = cbxjabatan.SelectedItem.ToString
                ' simpanData("RM_inputRequirement", txtnoPO.Text, tglPO.Value.ToString, txtsupplier.Text, txtalamat.Text, txtpemesan.Text, DateTimePicker2.Value.ToString, cmbTOP.SelectedItem.ToString, RichTextBox1.Text, kodeRM.Text, namaRM.Text, jlhRM.Text, satuan.Text, hargaRM.Text)
                MessageBox.Show("Data Tersimpan")
                tampilkanData("SELECT * FROM RM_inputRequirement", DataGridView1)
                clearForm(txtkebutuhan, txtkodeREQ, txtnamaadminRM, txtkodeRM, txtnamaRM, txtsatuanRM)

            End If
        End If
    End Sub

    'BAGIAN INPUT INVENTORI

    'input
    Private Sub btninputInventori_Click(sender As Object, e As EventArgs) Handles btninputInventori.Click
        nyalainForm(txtkodeRMi, txtnamaRMi, txtsatuanRM, txtstokRM)
        enableButton(btnsaveInventori, btneditInventori, btndeleteinventori, btncancelInventori, btnsaveInventori, btnexitPURC)
        clearForm(txtkodeRMi, txtnamaRMi, txtsatuanRM, txtstokRM)
    End Sub

    'edit
    Private Sub btneditInventori_Click(sender As Object, e As EventArgs) Handles btneditInventori.Click
        If (checkEmpty(txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM) = True) Then
            MessageBox.Show("Lengkapi Dulu")
            Exit Sub
        Else
            Call koneksiDB()
            CMD = New OleDb.OleDbCommand("UPDATE RM_inventori SET nama_RM ='" & txtnamaRMi.Text & "',  stok = '" & txtstokRM.Text & "',satuan_RM = '" & txtsatuanRM.Text & "' WHERE kode_RM ='" & txtkodeRMi.Text & "'", Conn)
            DM = CMD.ExecuteReader
            MessageBox.Show("Yatta! berhasil")
            clearForm(txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM)
        End If
    End Sub

    'cancel
    Private Sub btncancelInventori_Click(sender As Object, e As EventArgs) Handles btncancelInventori.Click
        clearForm(txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM)
        matiForm(txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM)
    End Sub

    'delete
    Private Sub btndeleteinventori_Click(sender As Object, e As EventArgs) Handles btndeleteinventori.Click
        If (checkEmpty(txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM) = True) Then
            MessageBox.Show("Jangan kosong")
        Else
            hapusData("RM_inventori", "kode_RM", txtkodeRMi.Text)
            tampilkanData("SELECT * FROM RM_inventori", DataGridView2)
        End If

        enableButton(btncancelInventori, btndeleteinventori, btneditInventori, btnexitPURC, btnsaveInventori)
    End Sub

    'SAVE
    Private Sub btnsaveInventori_Click(sender As Object, e As EventArgs) Handles btnsaveInventori.Click
        If (checkEmpty(txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM) = True) Then
            MessageBox.Show("Jangan kosong")
        Else
            Dim winny As Boolean = checkDuplicate("RM_inventori", "kode_RM", txtkodeRMi.Text)
            If winny = True Then
                MessageBox.Show("KODE Duplikat")
            Else
                'txtKelaminPeg.Text = Cbxkelamin.SelectedItem.ToString
                'txtJabatanPeg.Text = cbxjabatan.SelectedItem.ToString
                simpanData("RM_inventori", txtkodeRMi.Text, txtnamaRMi.Text, txtstokRM.Text, txtsatuanRM.Text)
                MessageBox.Show("Data Tersimpan")
                tampilkanData("SELECT * FROM RM_inventori", DataGridView2)
                clearForm(txtsatuanRM, txtkodeRMi, txtnamaRMi, txtstokRM)
            End If
        End If
    End Sub



End Class