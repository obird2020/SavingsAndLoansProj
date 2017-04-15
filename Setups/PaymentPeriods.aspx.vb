Imports MySql.Data.MySqlClient

Public Class PaymentPeriods
   Inherits System.Web.UI.Page
   Dim dt As New DataTable()

   Protected Sub dgRegions_RowCommand(sender As Object, e As GridViewCommandEventArgs)
      If e.CommandName = "editCmd" Or e.CommandName = "dltCmd" Then
         dgRetention.Columns(1).Visible = True
         Dim btndetails As LinkButton = DirectCast(e.CommandSource, LinkButton)
         Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

         Session("rowsID") = HttpUtility.HtmlDecode(gvrow.Cells(1).Text)
         Session("code") = HttpUtility.HtmlDecode(gvrow.Cells(2).Text)
         Session("description") = HttpUtility.HtmlDecode(gvrow.Cells(3).Text)
         Session("interest_rate") = HttpUtility.HtmlDecode(gvrow.Cells(4).Text)
         Session("pay_months") = HttpUtility.HtmlDecode(gvrow.Cells(5).Text)
         Session("pay_days") = HttpUtility.HtmlDecode(gvrow.Cells(6).Text)
         Session("status") = HttpUtility.HtmlDecode(gvrow.Cells(7).Text)

         If e.CommandName = "editCmd" Then
            txtCode.Text = Session("code")
            txtDescription.Text = Session("description")
            txtInterestRate.Text = Session("interest_rate")
            txtPaymentMonths.Text = Session("pay_months")
            txtPaymentDays.Text = Session("pay_days")
            lblmsg.Text = ""
            If Session("status") = "Inactive" Then
               chkInactive.Checked = True
            Else
               chkInactive.Checked = False
            End If
         End If

         If e.CommandName = "dltCmd" Then
            Dim obj As New setups
            obj.delItm(Session("rowsID"), "loan_pay_periods")
            populateGrid()
         End If
         dgRetention.Columns(1).Visible = False
      End If
   End Sub

   Protected Sub validateCode(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      Using con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand
         If Session("rowsID") = 0 Then
            cmd = New MySqlCommand("SELECT * FROM loan_pay_periods WHERE code=@code", con)
            cmd.Parameters.AddWithValue("@code", arguments.Value)
         Else
            If txtCode.Text.Trim = Session("code") Then
               cmd = New MySqlCommand("SELECT * FROM loan_pay_periods WHERE code='POLKJHGTFDERRYYUUJJFDESOLMGDS'", con)
            Else
               cmd = New MySqlCommand("SELECT * FROM loan_pay_periods WHERE code=@code", con)
               cmd.Parameters.AddWithValue("@code", arguments.Value)
            End If
         End If
         con.Open()
         Dim reader As MySqlDataReader = cmd.ExecuteReader()
         If reader.HasRows Then
            arguments.IsValid = False
         Else
            arguments.IsValid = True
         End If
         con.Close()
      End Using
   End Sub

   Protected Sub validatedescription(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      Using con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand
         If Session("rowsID") = 0 Then
            cmd = New MySqlCommand("SELECT * FROM loan_pay_periods WHERE description=@description", con)
            cmd.Parameters.AddWithValue("@description", arguments.Value)
         Else
            If txtDescription.Text.Trim = Session("description") Then
               cmd = New MySqlCommand("SELECT * FROM loan_pay_periods WHERE description='POLKJHGTFDERRYYUUJJFDESOLMGDS'", con)
            Else
               cmd = New MySqlCommand("SELECT * FROM loan_pay_periods WHERE description=@description", con)
               cmd.Parameters.AddWithValue("@description", arguments.Value)
            End If
         End If
         con.Open()
         Dim reader As MySqlDataReader = cmd.ExecuteReader()
         If reader.HasRows Then
            arguments.IsValid = False
         Else
            arguments.IsValid = True
         End If
         con.Close()
      End Using
   End Sub

   Protected Sub validateinterest(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      Dim out As Double
      If Double.TryParse(txtInterestRate.Text.Trim, out) = False Then
         arguments.IsValid = False
      Else
         arguments.IsValid = True
      End If
   End Sub

   Protected Sub validatemonths(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      Dim out As Integer
      If Double.TryParse(txtPaymentMonths.Text.Trim, out) = False Then
         arguments.IsValid = False
      Else
         arguments.IsValid = True
      End If
   End Sub

   Protected Sub validatedays(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      Dim out As Integer
      If Double.TryParse(txtPaymentDays.Text.Trim, out) = False Then
         arguments.IsValid = False
      Else
         arguments.IsValid = True
      End If
   End Sub


   Protected Sub populateGrid()
      dgRetention.Columns(1).Visible = True
      Dim dr As DataRow
      dt.Columns.Clear()
      dt.Clear()
      dt.Columns.Add("SN")
      dt.Columns.Add("ID")
      dt.Columns.Add("Code")
      dt.Columns.Add("Description")
      dt.Columns.Add("interest_rate")
      dt.Columns.Add("payment_days")
      dt.Columns.Add("payment_months")
      dt.Columns.Add("Status")

      Using con As New mySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand = New MySqlCommand("SELECT * from loan_pay_periods where ID > 0 order by description,code", con)
         con.Open()
         Dim reader As MySqlDataReader = cmd.ExecuteReader()
         Dim increment As Integer = 0
         If reader.HasRows Then
            While reader.Read
               dr = dt.NewRow()
               increment += 1
               dr("SN") = increment
               dr("ID") = reader("ID")
               dr("code") = reader("code")
               dr("Description") = reader("description")
               dr("interest_rate") = reader("interest_rate")
               dr("payment_days") = reader("payment_days")
               dr("payment_months") = reader("payment_months")
               If reader("Inactive") = 0 Then
                  dr("Status") = "Active"
               Else
                  dr("Status") = "Inactive"
               End If
               dt.Rows.Add(dr)
            End While
            dgRetention.DataSource = dt
            dgRetention.DataBind()
            dgRetention.Columns(1).Visible = False
         End If
         reader.Close()
         con.Close()
      End Using
   End Sub

   Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      If Not Page.IsPostBack Then
         populateGrid()
      End If
   End Sub

   Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
      If Page.IsValid Then
         Dim config As New setups
         If Session("rowsID") = 0 Then
            config.addPayPeriod(txtCode.Text.Trim, txtDescription.Text.Trim, txtInterestRate.Text.Trim, txtPaymentMonths.Text.Trim, txtPaymentDays.Text.Trim, Math.Abs(CInt(chkInactive.Checked)), Session("ID"))
            lblmsg.Text = "Payment duration Saved"
         Else
            config.updatePayPeriod(txtCode.Text.Trim, txtDescription.Text.Trim, txtInterestRate.Text.Trim, txtPaymentMonths.Text.Trim, txtPaymentDays.Text.Trim, Math.Abs(CInt(chkInactive.Checked)), Session("ID"), Session("rowsID"))
            lblmsg.Text = "Payment duration Updated"
         End If
         txtCode.Text = ""
         txtDescription.Text = ""
         txtInterestRate.Text = ""
         txtPaymentDays.Text = ""
         txtPaymentMonths.Text = ""
         chkInactive.Checked = False
         Session("rowsID") = 0
         Session("code") = ""
         Session("description") = ""
         Session("interest_rate") = ""
         Session("status") = ""
         Session("payment_days") = ""
         Session("payment_months") = ""
         populateGrid()
      End If

   End Sub

   Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
      txtCode.Text = ""
      txtDescription.Text = ""
      txtInterestRate.Text = ""
      txtPaymentDays.Text = ""
      txtPaymentMonths.Text = ""
      chkInactive.Checked = False
      Session("rowsID") = 0
      Session("code") = ""
      Session("description") = ""
      Session("interest_rate") = ""
      Session("status") = ""
      Session("payment_days") = ""
      Session("payment_months") = ""
      lblmsg.Text = ""
   End Sub

   Private Sub dgRetention_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgRetention.PageIndexChanging
      dgRetention.PageIndex = e.NewPageIndex
      populateGrid()
   End Sub
End Class