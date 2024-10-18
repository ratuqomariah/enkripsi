Imports System.IO

Public Class Form1
    ' Membuka file
    Private Sub BtnBuka_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnBuka.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            If openFileDialog.ShowDialog() = DialogResult.OK Then
                txtCari.Text = openFileDialog.FileName ' Menampilkan path file di txtCari
                txtHasil.Text = File.ReadAllText(openFileDialog.FileName) ' Menampilkan isi file di txtHasil
            End If
        End Using
    End Sub

    ' Menyimpan file
    Private Sub BtnSimpan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnSimpan.Click
        If String.IsNullOrWhiteSpace(txtCari.Text) Then
            Using saveFileDialog As New SaveFileDialog()
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    txtCari.Text = saveFileDialog.FileName ' Menyimpan path ke txtCari
                Else
                    Return ' Jika pengguna membatalkan, keluar dari fungsi
                End If
            End Using
        End If

        Try
            ' Mengonversi isi txtHasil menjadi representasi ASCII
            Dim asciiResult As New System.Text.StringBuilder()
            For Each ch As Char In txtHasil.Text
                asciiResult.Append(AscW(ch).ToString() & " ") ' Mengonversi ke ASCII dan menambahkan spasi
            Next
            File.WriteAllText(txtCari.Text, asciiResult.ToString().Trim()) ' Menyimpan isi ASCII ke file
            MessageBox.Show("File disimpan dengan sukses!")
        Catch ex As Exception
            MessageBox.Show("Error saat menyimpan file: " & ex.Message)
        End Try
    End Sub

    ' Menghapus file
    Private Sub BtnHapus_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnHapus.Click
        If String.IsNullOrWhiteSpace(txtCari.Text) Then
            MessageBox.Show("Silakan buka file atau tentukan path file terlebih dahulu.")
            Return
        End If

        Try
            If File.Exists(txtCari.Text) Then
                File.Delete(txtCari.Text)
                txtCari.Clear() ' Menghapus isi TextBox
                txtHasil.Clear() ' Menghapus isi TextBox
                MessageBox.Show("File dihapus dengan sukses!")
            Else
                MessageBox.Show("File tidak ditemukan.")
            End If
        Catch ex As Exception
            MessageBox.Show("Error saat menghapus file: " & ex.Message)
        End Try
    End Sub

    ' Mencari dan membuka file
    Private Sub BtnCari_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCari.Click
        Dim searchPath As String = txtCari.Text
        If File.Exists(searchPath) Then
            txtHasil.Text = File.ReadAllText(searchPath) ' Membaca isi file
            MessageBox.Show("File ditemukan dan dibuka.")
        Else
            MessageBox.Show("File tidak ditemukan.")
        End If
    End Sub
End Class

