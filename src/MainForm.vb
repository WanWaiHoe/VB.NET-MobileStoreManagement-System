Imports System.Data.SqlClient

Public Class MainForm

    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim salesId As Integer
    Dim staffsId As String
    Dim storesId As String

    Private Sub btnSalesLogout_Click(sender As Object, e As EventArgs) Handles btnSalesLogout.Click

        Dim result = MessageBox.Show("Are you sure you would like to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If (result = DialogResult.Yes) Then
            Me.Close()
        End If

    End Sub

    Private Sub btnStaffLogout_Click(sender As Object, e As EventArgs) Handles btnStaffLogout.Click

        Dim result = MessageBox.Show("Are you sure you would like to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If (result = DialogResult.Yes) Then
            Me.Close()
        End If

    End Sub

    Private Sub btnAttendanceLogout_Click(sender As Object, e As EventArgs) Handles btnAttendanceLogout.Click

        Dim result = MessageBox.Show("Are you sure you would like to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If (result = DialogResult.Yes) Then
            Me.Close()
        End If

    End Sub

    Private Sub btnStoreLogout_Click(sender As Object, e As EventArgs) Handles btnStoreLogout.Click

        Dim result = MessageBox.Show("Are you sure you would like to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If (result = DialogResult.Yes) Then
            Me.Close()
        End If

    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            con.ConnectionString = "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database\MobileStoreManagement.mdf;Integrated Security=True"
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            displaySales()
            displayStaffs()
            displayAttendance()
            displayStores()

            fillSalesProductName()
            fillSalesStaff()
            fillSalesStaffFilter()
            fillAttendanceStaff()

            If SalesProductNameTxt.Items.Count > 0 Then
                SalesProductNameTxt.SelectedIndex = 0
            End If
            If SalesStaff.Items.Count > 0 Then
                SalesStaff.SelectedIndex = 0
            End If
            StaffType.SelectedIndex = 0
            productStatusBox.SelectedIndex = 0
            productQuantityTxt.Text = "1"

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub displaySales()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Sales"
        cmd.ExecuteNonQuery()

        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        salesDataView.DataSource = dt

    End Sub

    Public Sub displayStaffs()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Staffs"
        cmd.ExecuteNonQuery()

        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        staffDataView.DataSource = dt

    End Sub

    Public Sub displayAttendance()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Attendances"
        cmd.ExecuteNonQuery()

        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        attendanceDataView.DataSource = dt

    End Sub

    Public Sub displayStores()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Stores"
        cmd.ExecuteNonQuery()

        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        storeDataView.DataSource = dt

    End Sub

    Private Sub fillSalesStaffFilter()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Staffs"
        cmd.ExecuteNonQuery()

        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        SalesFilterStaff.DataSource = dt
        SalesFilterStaff.DisplayMember = "StaffId"
        SalesFilterStaff.ValueMember = "StaffId"

    End Sub


    Public Sub fillSalesProductName()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Stores where ProductStatus='Available'"
        cmd.ExecuteNonQuery()

        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        SalesProductNameTxt.DataSource = dt
        SalesProductNameTxt.DisplayMember = "ProductName"
        SalesProductNameTxt.ValueMember = "ProductName"

    End Sub

    Public Sub fillSalesStaff()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Staffs"
        cmd.ExecuteNonQuery()

        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        SalesStaff.DataSource = dt
        SalesStaff.DisplayMember = "StaffId"
        SalesStaff.ValueMember = "StaffId"

    End Sub

    Public Sub fillAttendanceStaff()

        cmd = con.CreateCommand()
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "select * from Staffs"
        cmd.ExecuteNonQuery()

        Dim dt As New DataTable()
        Dim da As New SqlDataAdapter(cmd)
        da.Fill(dt)
        AttendanceFilterStaffId.DataSource = dt
        AttendanceFilterStaffId.DisplayMember = "StaffId"
        AttendanceFilterStaffId.ValueMember = "StaffId"

    End Sub

    'Sales Page'
    Private Sub btnAddSales_Click(sender As Object, e As EventArgs) Handles btnAddSales.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into Sales values('" + SalesDatePicker.Value + "', '" + SalesStaff.Text + "', '" + SalesQuantityTxt.Text + "', '" + SalesCustNameTxt.Text + "', '" + SalesCustConTxt.Text + "', '" + SalesPriceTxt.Text + "', '" + SalesProductNameTxt.Text + "')"

            Dim query2 = "update Stores set ProductStatus='Not Available', StockOut='" + DateTime.Now + "' where ProductName='" + SalesProductNameTxt.Text + "'"
            Dim com2 = New SqlCommand(query2, con)

            cmd.ExecuteNonQuery()
            com2.ExecuteNonQuery()

            If salesDataView.Rows.Count > 0 Then
                salesDataView.CurrentCell = salesDataView.Rows(0).Cells(0)
            End If

            If SalesFilterStaff.SelectedIndex > 0 Then
                SalesFilterStaff.SelectedIndex = 0
            End If

            DateStartSales.Value = DateTime.Now
            DateEndSales.Value = DateTime.Now
            SalesDatePicker.Value = DateTime.Now
            SalesIdTxt.Clear()

            If SalesStaff.SelectedIndex > 0 Then
                SalesStaff.SelectedIndex = 0
            End If

            SalesQuantityTxt.Text = 1
            SalesCustNameTxt.Clear()
            SalesCustConTxt.Clear()
            SalesPriceTxt.Clear()

            If SalesProductNameTxt.SelectedIndex > 0 Then
                SalesProductNameTxt.SelectedIndex = 0
            End If

            salesId = 0

            displaySales()
            displayStores()
            fillSalesProductName()
            MessageBox.Show("Record Added Successfully!!!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub salesDataView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles salesDataView.CellClick
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            salesId = Convert.ToInt32(salesDataView.SelectedCells.Item(0).Value.ToString())

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Sales where SalesId=" & salesId & ""
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                SalesIdTxt.Text = dr.GetInt32(0).ToString()
                SalesDatePicker.Value = dr.GetValue(1).ToString()
                SalesStaff.SelectedItem = dr.GetString(2).ToString()
                SalesQuantityTxt.Text = dr.GetInt32(3).ToString()
                SalesCustNameTxt.Text = dr.GetString(4).ToString()
                SalesCustConTxt.Text = dr.GetString(5).ToString()
                SalesPriceTxt.Text = dr.GetValue(6).ToString()
                SalesProductNameTxt.Text = dr.GetString(7).ToString()
            End While
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSalesSave_Click(sender As Object, e As EventArgs) Handles btnSalesSave.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "update Sales set SalesDate='" + SalesDatePicker.Value + "', StaffId='" + SalesStaff.Text.ToString() + "', Quantity='" + SalesQuantityTxt.Text + "', CustomerName='" + SalesCustNameTxt.Text + "', CustomerContact='" + SalesCustConTxt.Text + "', Price='" + SalesPriceTxt.Text + "', Productname='" + SalesProductNameTxt.Text + "' where SalesId=" & salesId & ""

            Dim query2 = "update Stores set ProductStatus='Not Available', StockOut='" + DateTime.Now + "' where ProductName='" + SalesProductNameTxt.Text + "'"
            Dim com2 = New SqlCommand(query2, con)

            cmd.ExecuteNonQuery()
            com2.ExecuteNonQuery()

            If salesDataView.Rows.Count > 0 Then
                salesDataView.CurrentCell = salesDataView.Rows(0).Cells(0)
            End If

            If SalesFilterStaff.SelectedIndex > 0 Then
                SalesFilterStaff.SelectedIndex = 0
            End If

            DateStartSales.Value = DateTime.Now
            DateEndSales.Value = DateTime.Now
            SalesDatePicker.Value = DateTime.Now
            SalesIdTxt.Clear()

            If SalesStaff.SelectedIndex > 0 Then
                SalesStaff.SelectedIndex = 0
            End If

            SalesQuantityTxt.Text = 1
            SalesCustNameTxt.Clear()
            SalesCustConTxt.Clear()
            SalesPriceTxt.Clear()

            If SalesProductNameTxt.SelectedIndex > 0 Then
                SalesProductNameTxt.SelectedIndex = 0
            End If

            salesId = 0

            displaySales()
            displayStores()
            fillSalesProductName()
            MessageBox.Show("Data Update Successfully!!!")
        Catch ex As Exception
            MessageBox.Show("Data update failed, " + ex.Message)
        End Try
    End Sub

    Private Sub btnSalesDelete_Click(sender As Object, e As EventArgs) Handles btnSalesDelete.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            If SalesIdTxt.Text = "" Then
                MessageBox.Show("Deletion fail, you must select a item first")
            Else
                cmd = con.CreateCommand()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "delete from Sales where SalesId='" & salesId & "'"

                Dim query2 = "update Stores set ProductStatus='Available', StockOut='' where ProductName='" + SalesProductNameTxt.Text + "'"
                Dim com2 = New SqlCommand(query2, con)

                cmd.ExecuteNonQuery()
                com2.ExecuteNonQuery()

                If salesDataView.Rows.Count > 0 Then
                    salesDataView.CurrentCell = salesDataView.Rows(0).Cells(0)
                End If

                If SalesFilterStaff.SelectedIndex > 0 Then
                    SalesFilterStaff.SelectedIndex = 0
                End If

                DateStartSales.Value = DateTime.Now
                DateEndSales.Value = DateTime.Now
                SalesDatePicker.Value = DateTime.Now
                SalesIdTxt.Clear()

                If SalesStaff.SelectedIndex > 0 Then
                    SalesStaff.SelectedIndex = 0
                End If

                SalesQuantityTxt.Text = 1
                SalesCustNameTxt.Clear()
                SalesCustConTxt.Clear()
                SalesPriceTxt.Clear()

                If SalesProductNameTxt.SelectedIndex > 0 Then
                    SalesProductNameTxt.SelectedIndex = 0
                End If

                salesId = 0

                displaySales()
                displayStores()
                fillSalesProductName()
                MessageBox.Show("Data Delete Successfully!!!")
            End If
        Catch ex As Exception
            MessageBox.Show("Data Delete failed, " + ex.Message)
        End Try
    End Sub

    Private Sub btnSalesSearch_Click(sender As Object, e As EventArgs) Handles btnSalesSearch.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            Dim total As Double = 0
            Dim query As String = "select Price from Sales where SalesDate >='" & DateStartSales.Value & "' and SalesDate <='" & DateEndSales.Value & "' and StaffId ='" & SalesFilterStaff.SelectedValue & "'"
            Dim com2 = New SqlCommand(query, con)

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Sales where SalesDate >='" & DateStartSales.Value & "' and SalesDate <='" & DateEndSales.Value & "' and StaffId ='" & SalesFilterStaff.SelectedValue & "'"
            cmd.ExecuteNonQuery()

            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            salesDataView.DataSource = dt
            Dim dr As SqlClient.SqlDataReader
            dr = com2.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                Dim price As Double = dr.GetValue(0)
                total += price
                totalTxt.Text = total.ToString()
            End While
        Catch ex As Exception
            MessageBox.Show("Data not found, " + ex.Message)
        End Try
    End Sub

    Private Sub btnSalesRefresh_Click(sender As Object, e As EventArgs) Handles btnSalesRefresh.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            If salesDataView.Rows.Count > 0 Then
                salesDataView.CurrentCell = salesDataView.Rows(0).Cells(0)
            End If

            If SalesFilterStaff.SelectedIndex > 0 Then
                SalesFilterStaff.SelectedIndex = 0
            End If

            DateStartSales.Value = DateTime.Now
            DateEndSales.Value = DateTime.Now
            SalesDatePicker.Value = DateTime.Now
            SalesIdTxt.Clear()

            If SalesStaff.SelectedIndex > 0 Then
                SalesStaff.SelectedIndex = 0
            End If

            SalesQuantityTxt.Text = 1
            SalesCustNameTxt.Clear()
            SalesCustConTxt.Clear()
            SalesPriceTxt.Clear()

            If SalesProductNameTxt.SelectedIndex > 0 Then
                SalesProductNameTxt.SelectedIndex = 0
            End If

            salesId = 0

            displaySales()
        Catch ex As Exception

        End Try
    End Sub

    'Staffs Page'
    Private Sub btnStaffAdd_Click(sender As Object, e As EventArgs) Handles btnStaffAdd.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "insert into Staffs values('" + StaffIdtxt.Text + "', '" + StaffPasswordTxt.Text + "', '" + StaffType.Text + "', '" + StaffContactTxt.Text + "')"
            cmd.ExecuteNonQuery()

            If staffDataView.Rows.Count > 0 Then
                staffDataView.CurrentCell = staffDataView.Rows(0).Cells(0)
            End If

            StaffIdtxt.Clear()
            StaffPasswordTxt.Clear()
            StaffType.SelectedIndex = 0
            StaffContactTxt.Clear()
            staffFilter.Clear()
            staffsId = ""

            displayStaffs()
            fillSalesStaff()
            fillSalesStaffFilter()
            fillAttendanceStaff()
            MessageBox.Show("Record Added Successfully!!!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnStaffDel_Click(sender As Object, e As EventArgs) Handles btnStaffDel.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            If StaffIdtxt.Text = "" Then
                MessageBox.Show("Deletion fail, you must select a item first")
            Else
                cmd = con.CreateCommand()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "delete from Staffs where StaffId='" & staffsId & "'"
                cmd.ExecuteNonQuery()

                If staffDataView.Rows.Count > 0 Then
                    staffDataView.CurrentCell = staffDataView.Rows(0).Cells(0)
                End If

                StaffIdtxt.Clear()
                StaffPasswordTxt.Clear()
                StaffType.SelectedIndex = 0
                StaffContactTxt.Clear()
                staffFilter.Clear()
                staffsId = ""

                displayStaffs()
                fillSalesStaff()
                fillSalesStaffFilter()
                fillAttendanceStaff()
                MessageBox.Show("Data Delete Successfully!!!")
            End If
        Catch ex As Exception
            MessageBox.Show("Data Delete failed, " + ex.Message)
        End Try
    End Sub

    Private Sub staffDataView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles staffDataView.CellClick
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            staffsId = staffDataView.SelectedCells.Item(0).Value.ToString()

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Staffs where StaffId='" & staffsId & "'"
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                StaffIdtxt.Text = dr.GetString(0).ToString()
                StaffPasswordTxt.Text = dr.GetString(1).ToString()
                StaffType.SelectedItem = dr.GetString(2).ToString()
                StaffContactTxt.Text = dr.GetString(3).ToString()
            End While

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnStaffSave_Click(sender As Object, e As EventArgs) Handles btnStaffSave.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "update Staffs set StaffId='" + StaffIdtxt.Text + "', Password='" + StaffPasswordTxt.Text + "', StaffType='" + StaffType.Text + "', Contact='" + StaffContactTxt.Text + "' where StaffId='" & staffsId & "'"
            cmd.ExecuteNonQuery()

            If staffDataView.Rows.Count > 0 Then
                staffDataView.CurrentCell = staffDataView.Rows(0).Cells(0)
            End If

            StaffIdtxt.Clear()
            StaffPasswordTxt.Clear()
            StaffType.SelectedIndex = 0
            StaffContactTxt.Clear()
            staffFilter.Clear()
            staffsId = ""

            displayStaffs()
            fillSalesStaff()
            fillSalesStaffFilter()
            fillAttendanceStaff()
            MessageBox.Show("Data Update Successfully!!!")
        Catch ex As Exception
            MessageBox.Show("Data update failed, " + ex.Message)
        End Try
    End Sub

    Private Sub staffFilter_TextChanged(sender As Object, e As EventArgs) Handles staffFilter.TextChanged
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Staffs where StaffId like'%" + staffFilter.Text + "%'"
            cmd.ExecuteNonQuery()

            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            staffDataView.DataSource = dt
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnStaffRefresh_Click(sender As Object, e As EventArgs) Handles btnStaffRefresh.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            If staffDataView.Rows.Count > 0 Then
                staffDataView.CurrentCell = staffDataView.Rows(0).Cells(0)
            End If

            StaffIdtxt.Clear()
            StaffPasswordTxt.Clear()
            StaffType.SelectedIndex = 0
            StaffContactTxt.Clear()
            staffFilter.Clear()
            staffsId = ""

            displayStaffs()
        Catch ex As Exception

        End Try
    End Sub

    'Attendance Page'
    Private Sub btnAttendanceSearch_Click(sender As Object, e As EventArgs) Handles btnAttendanceSearch.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Attendances where Date >='" & DateAttendanceStart.Value & "' and Date <='" & DateAttendanceEnd.Value & "' and StaffId ='" + AttendanceFilterStaffId.Text + "'"
            cmd.ExecuteNonQuery()

            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            attendanceDataView.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Data not found, " + ex.Message)
        End Try
    End Sub

    Private Sub btnAttendanceRefresh_Click(sender As Object, e As EventArgs) Handles btnAttendanceRefresh.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            If attendanceDataView.Rows.Count > 0 Then
                attendanceDataView.CurrentCell = attendanceDataView.Rows(0).Cells(0)
            End If

            AttendanceFilterStaffId.SelectedIndex = 0
            DateAttendanceStart.Value = DateTime.Now
            DateAttendanceEnd.Value = DateTime.Now

            displayAttendance()
        Catch ex As Exception

        End Try
    End Sub

    'Store Page'
    Private Sub btnProductSearch_Click(sender As Object, e As EventArgs) Handles btnProductSearch.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            Dim query As String = "select * from Stores where ProductName like'%" & productFilterName.Text & "%' "
            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text

            If productFilterStatus.SelectedIndex > 0 Then
                query += "and ProductStatus='" & productFilterStatus.Text & "'"
            End If

            If productStockInOutFilter.Text = "Stock In" Then
                query += "and StockIn >='" & productDateStart.Value & "' and StockIn <='" & productDateEnd.Value & "'"
            ElseIf productStockInOutFilter.Text = "Stock Out" Then
                query += "and StockOut >='" & productDateStart.Value & "' and StockOut <='" & productDateEnd.Value & "'"
            End If

            cmd.CommandText = query
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            storeDataView.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Data not found, " + ex.Message)
        End Try
    End Sub

    Private Sub btnProductRefresh_Click(sender As Object, e As EventArgs) Handles btnProductRefresh.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            If storeDataView.Rows.Count > 0 Then
                storeDataView.CurrentCell = storeDataView.Rows(0).Cells(0)
            End If

            productNameTxt.Clear()
            productQuantityTxt.Text = 1
            dateProductIn.Value = DateTime.Now
            dateProductOut.Value = DateTime.Now
            productRemarkTxt.Clear()
            productFilterName.Clear()
            productDateStart.Value = DateTime.Now
            productDateEnd.Value = DateTime.Now
            productFilterStatus.SelectedIndex = 0
            productStockInOutFilter.SelectedIndex = 0
            productStatusBox.SelectedIndex = 0

            displayStores()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnProductAdd_Click(sender As Object, e As EventArgs) Handles btnProductAdd.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            Dim query As String = ""
            If productStatusBox.Text = "Not Available" Then
                query = "insert into Stores values('" + productNameTxt.Text + "', '" + productQuantityTxt.Text + "', '" + productStatusBox.Text + "', '" + dateProductIn.Value + "', '" + dateProductOut.Value + "', '" + productRemarkTxt.Text + "')"
            ElseIf productStatusBox.Text = "Available" Then
                query = "insert into Stores values('" + productNameTxt.Text + "', '" + productQuantityTxt.Text + "', '" + productStatusBox.Text + "', '" + dateProductIn.Value + "', '', '" + productRemarkTxt.Text + "')"
            End If

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = query
            cmd.ExecuteNonQuery()

            If storeDataView.Rows.Count > 0 Then
                storeDataView.CurrentCell = storeDataView.Rows(0).Cells(0)
            End If

            productNameTxt.Clear()
            productQuantityTxt.Text = 1
            dateProductIn.Value = DateTime.Now
            dateProductOut.Value = DateTime.Now
            productRemarkTxt.Clear()
            productFilterName.Clear()
            productDateStart.Value = DateTime.Now
            productDateEnd.Value = DateTime.Now
            productFilterStatus.SelectedIndex = 0
            productStockInOutFilter.SelectedIndex = 0
            productStatusBox.SelectedIndex = 0

            displayStores()
            fillSalesProductName()
            MessageBox.Show("Record Added Successfully!!!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub productStatusBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles productStatusBox.SelectedIndexChanged
        If productStatusBox.SelectedItem = "Not Available" Then
            dateProductOut.Enabled = True
        Else
            dateProductOut.Enabled = False
        End If
    End Sub

    Private Sub storeDataView_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles storeDataView.CellClick
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            storesId = storeDataView.SelectedCells.Item(0).Value.ToString()

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "select * from Stores where ProductName='" & storesId & "'"
            cmd.ExecuteNonQuery()
            Dim dt As New DataTable()
            Dim da As New SqlDataAdapter(cmd)
            da.Fill(dt)
            Dim dr As SqlClient.SqlDataReader
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                productNameTxt.Text = dr.GetString(0).ToString()
                productQuantityTxt.Text = dr.GetInt32(1).ToString()
                productStatusBox.SelectedItem = dr.GetString(2).ToString()
                dateProductIn.Value = dr.GetValue(3).ToString()
                If productStatusBox.SelectedItem = "Not Available" Then
                    dateProductOut.Value = dr.GetValue(4).ToString()
                End If
                productRemarkTxt.Text = dr.GetString(5).ToString()
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnProductSave_Click(sender As Object, e As EventArgs) Handles btnProductSave.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            Dim query As String = ""
            If productStatusBox.Text = "Not Available" Then
                query = "update Stores Set ProductName='" + productNameTxt.Text + "', Quantity='" + productQuantityTxt.Text + "', ProductStatus='" + productStatusBox.Text + "', StockIn='" + dateProductIn.Value + "', StockOut='" + dateProductOut.Value + "', Remark='" + productRemarkTxt.Text + "' where ProductName='" + storesId + "'"
            ElseIf productStatusBox.Text = "Available" Then
                query = "update Stores Set ProductName='" + productNameTxt.Text + "', Quantity='" + productQuantityTxt.Text + "', ProductStatus='" + productStatusBox.Text + "', StockIn='" + dateProductIn.Value + "', StockOut='', Remark='" + productRemarkTxt.Text + "' where ProductName='" + storesId + "'"
            End If

            cmd = con.CreateCommand()
            cmd.CommandType = CommandType.Text
            cmd.CommandText = query
            cmd.ExecuteNonQuery()

            If storeDataView.Rows.Count > 0 Then
                storeDataView.CurrentCell = storeDataView.Rows(0).Cells(0)
            End If

            productNameTxt.Clear()
            productQuantityTxt.Text = 1
            dateProductIn.Value = DateTime.Now
            dateProductOut.Value = DateTime.Now
            productRemarkTxt.Clear()
            productFilterName.Clear()
            productDateStart.Value = DateTime.Now
            productDateEnd.Value = DateTime.Now
            productFilterStatus.SelectedIndex = 0
            productStockInOutFilter.SelectedIndex = 0
            productStatusBox.SelectedIndex = 0

            displayStores()
            fillSalesProductName()
            MessageBox.Show("Data Update Successfully!!!")
        Catch ex As Exception
            MessageBox.Show("Data update failed, " + ex.Message)
        End Try
    End Sub

    Private Sub btnProductDel_Click(sender As Object, e As EventArgs) Handles btnProductDel.Click
        Try
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            con.Open()

            If productNameTxt.Text = "" Then
                MessageBox.Show("Deletation fail, you must select a item first")
            Else
                cmd = con.CreateCommand()
                cmd.CommandType = CommandType.Text
                cmd.CommandText = "delete from Stores where ProductName='" & productNameTxt.Text & "'"
                cmd.ExecuteNonQuery()

                If storeDataView.Rows.Count > 0 Then
                    storeDataView.CurrentCell = storeDataView.Rows(0).Cells(0)
                End If

                productNameTxt.Clear()
                productQuantityTxt.Text = 1
                dateProductIn.Value = DateTime.Now
                dateProductOut.Value = DateTime.Now
                productRemarkTxt.Clear()
                productFilterName.Clear()
                productDateStart.Value = DateTime.Now
                productDateEnd.Value = DateTime.Now
                productFilterStatus.SelectedIndex = 0
                productStockInOutFilter.SelectedIndex = 0
                productStatusBox.SelectedIndex = 0

                displayStores()
                fillSalesProductName()
                MessageBox.Show("Data Delete Successfully!!!")
            End If
        Catch ex As Exception
            MessageBox.Show("Data Delete failed, " + ex.Message)
        End Try
    End Sub
End Class
