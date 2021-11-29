Public Class FormPurchasing
    Private Sub FormPurchasing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        disableButton(btncetakPO, btndeletePO, btneditPO, btnsavedetailbayar, btnuploadbukti, btncancelPO)
        matiForm(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM)
        tampilkanData("SELECT * FROM Purchasing_PO", dgvPO)
        tglPO.Enabled = False
        DateTimePicker2.Enabled = False
        cmbTOP.Enabled = False

    End Sub

    ' input
    Private Sub btninputPO_Click(sender As Object, e As EventArgs) Handles btninputPO.Click
        nyalainForm(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM)
        clearForm(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM)
        enableButton(btnsavePO, btneditPO, btnexit, btninputPO, btncetakPO, btndeletePO, btncancelPO)
        tglPO.Enabled = True
        DateTimePicker2.Enabled = True
        cmbTOP.Enabled = True

    End Sub

    ' delete
    Private Sub btndeletePO_Click(sender As Object, e As EventArgs) Handles btndeletePO.Click
        If (checkEmpty(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM) = True) Then
            MessageBox.Show("Jangan kosong")
        Else
            hapusData("Purchasing_PO", "No_PO", txtnoPO.Text)
            tampilkanData("SELECT * FROM Purchasing_PO", dgvPO)
        End If

        enableButton(btnsavePO, btneditPO, btnexit, btninputPO, btncetakPO, btndeletePO, btncancelPO)
    End Sub

    ' cancel
    Private Sub btncancelPO_Click(sender As Object, e As EventArgs) Handles btncancelPO.Click
        clearForm(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM)

        matiForm(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM)
        tglPO.Enabled = False
        DateTimePicker2.Enabled = False
        cmbTOP.Enabled = False
    End Sub

    'exit
    Private Sub btnexit_Click(sender As Object, e As EventArgs) Handles btnexit.Click
        Dim a As New FormMenuUtama
        Me.Close()
        a.show
    End Sub

    'save
    Private Sub btnsavePO_Click(sender As Object, e As EventArgs) Handles btnsavePO.Click
        If (checkEmpty(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM) = True) Then
            MessageBox.Show("Jangan kosong")
        Else
            Dim winny As Boolean = checkDuplicate("Purchasing_PO", "No_PO", txtnoPO.Text)
            If winny = True Then
                MessageBox.Show("KODE Duplikat")
            Else
                'txtKelaminPeg.Text = Cbxkelamin.SelectedItem.ToString
                'txtJabatanPeg.Text = cbxjabatan.SelectedItem.ToString
                simpanData("Purchasing_PO", txtnoPO.Text, tglPO.Value.ToString, txtsupplier.Text, txtalamat.Text, txtpemesan.Text, DateTimePicker2.Value.ToString, cmbTOP.SelectedItem.ToString, RichTextBox1.Text, kodeRM.Text, namaRM.Text, jlhRM.Text, satuan.Text, hargaRM.Text)
                MessageBox.Show("Data Tersimpan")
                tampilkanData("SELECT * FROM Purchasing_PO", dgvPO)
                clearForm(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM)

            End If
        End If
    End Sub

    'edit
    Private Sub btneditPO_Click(sender As Object, e As EventArgs) Handles btneditPO.Click
        If (checkEmpty(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM) = True) Then
            MessageBox.Show("Lengkapi Dulu")
            Exit Sub
        Else
            Call koneksiDB()
            CMD = New OleDb.OleDbCommand("UPDATE Purchasing_PO SET Tgl_PO = '" & tglPO.Text & "',  Pemasok = '" & txtsupplier.Text & "',Alamat_Kirim = '" & txtalamat.Text & " ', Nama_Pemesan = '" & txtpemesan.Text & "', Tgl_Kirim ='" & DateTimePicker2.Text & "', TOP = '" & cmbTOP.Text & "', Keterangan ='" & RichTextBox1.Text & "',kode_RM = '" & kodeRM.Text & "', nama_RM = '" & namaRM.Text & "', jlh_RM = '" & jlhRM.Text & "', satuan_RM = '" & satuan.Text & "', harga_RM = '" & hargaRM.Text & "' WHERE No_PO ='" & txtnoPO.Text & "'", Conn)
            DM = CMD.ExecuteReader
            MessageBox.Show("Yatta! berhasil")
            clearForm(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM)

        End If
    End Sub

    'cek kebutuhan
    Private Sub btncekkebutuhan_Click(sender As Object, e As EventArgs) Handles btncekkebutuhan.Click
        tampilkanData("SELECT * FROM RM_inputRequirement", dgvReqRM)
    End Sub

    'KLIK DGV
    Private Sub dgvPO_MouseClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPO.CellClick
        On Error Resume Next
        txtnoPO.Text = dgvPO.Rows(e.RowIndex).Cells(0).Value
        tglPO.Text = dgvPO.Rows(e.RowIndex).Cells(1).Value
        txtsupplier.Text = dgvPO.Rows(e.RowIndex).Cells(2).Value
        txtalamat.Text = dgvPO.Rows(e.RowIndex).Cells(3).Value
        txtpemesan.Text = dgvPO.Rows(e.RowIndex).Cells(4).Value
        DateTimePicker2.Text = dgvPO.Rows(e.RowIndex).Cells(5).Value
        cmbTOP.Text = dgvPO.Rows(e.RowIndex).Cells(6).Value
        RichTextBox1.Text = dgvPO.Rows(e.RowIndex).Cells(7).Value
        kodeRM.Text = dgvPO.Rows(e.RowIndex).Cells(8).Value
        namaRM.Text = dgvPO.Rows(e.RowIndex).Cells(9).Value
        jlhRM.Text = dgvPO.Rows(e.RowIndex).Cells(10).Value
        satuan.Text = dgvPO.Rows(e.RowIndex).Cells(11).Value
        hargaRM.Text = dgvPO.Rows(e.RowIndex).Cells(12).Value

        nyalainForm(txtalamat, txtnoPO, txtpemesan, txtsupplier, namaRM, kodeRM, satuan, hargaRM, jlhRM)
        tglPO.Enabled = True
        DateTimePicker2.Enabled = True
        cmbTOP.Enabled = True
    End Sub

    Private Sub txtkode_RM_TextChanged(sender As Object, e As EventArgs) Handles kodeRM.TextChanged
        Try
            Call koneksiDB()
            CMD = New OleDb.OleDbCommand(" select * from RM_inventori where kode_RM ='" & kodeRM.Text & "'", Conn)
            DM = CMD.ExecuteReader

            If DM.HasRows = True Then
                DM.Read()
                kodeRM.Text = DM.Item("kode_RM")
                namaRM.Text = DM.Item("nama_RM")
                hargaRM.Text = DM.Item("harga_RM")
                satuan.Text = DM.Item("satuan_RM")
                jlhRM.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show("Data Barang Tidak Terdaftar")
        End Try
    End Sub
End Class