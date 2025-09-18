Imports System.Data.SqlClient

Public Class Login

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim result = MessageBox.Show("Are you sure you would like to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If (result = DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub loginBtn_Click(sender As Object, e As EventArgs) Handles loginBtn.Click
        Dim userId As String = txtUserId.Text
        Dim password As String = txtPassword.Text

        If txtUserId.Text = "" Or txtPassword.Text = "" Then
            MessageBox.Show("Please fill in the blank before login!!!", "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Try
                If con.State = ConnectionState.Open Then
                    con.Close()
                End If
                con.Open()

                cmd = con.CreateCommand()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "select StaffId, Password, StaffType from Staffs where StaffId='" + userId + "' and Password='" + password + "'"
                cmd.ExecuteNonQuery()

                Dim da As New SqlDataAdapter(cmd)
                Dim dt As New DataTable
                da.Fill(dt)

                If dt.Rows.Count > 0 Then
                    Dim message As String = "You have successfully login. Please click ok to proceed"
                    Dim caption As String = "Success"
                    Dim resuls = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    If dt.Rows(0)("StaffType") = "Admin" Then
                        Dim adminForm As New MainForm
                        adminForm.Show()
                    ElseIf dt.Rows(0)("StaffType") = "Staff" Then
                        Dim staffForm As New StaffMainForm
                        staffForm.staffNameTxt.Text = userId
                        staffForm.Show()
                    End If
                    txtUserId.Clear()
                    txtPassword.Clear()
                Else
                    MessageBox.Show("Staff Id or Password incorrect, Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    txtUserId.Clear()
                    txtPassword.Clear()
                    txtUserId.Focus()
                    Exit Sub
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtUserId.Clear()
        txtPassword.Clear()
        txtUserId.Focus()
    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\MobileStoreManagement.mdf;Integrated Security=True"
    End Sub
End Class