Imports MySql.Data.MySqlClient

Public Class Tenders
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
         Session("type") = HttpUtility.HtmlDecode(gvrow.Cells(4).Text)
         Session("status") = HttpUtility.HtmlDecode(gvrow.Cells(5).Text)

         If e.CommandName = "editCmd" Then
            txtDescription.Text = Session("description")
            txtCode.Text = Session("code")
            cboType.SelectedValue = Session("type")
            lblmsg.Text = ""
            If Session("status") = "Inactive" Then
               chkInactive.Checked = True
            Else
               chkInactive.Checked = False
            End If

            If e.CommandName = "dltCmd" Then
               Dim obj As New setups
               obj.delItm(Session("rowsID"), "payment_modes")
               populateGrid()
            End If
            dgRetention.Columns(1).Visible = False
         End If
      End If
   End Sub

   Protected Sub validateCode(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      Using con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand
         If Session("rowsID") = 0 Then
            cmd = New MySqlCommand("SELECT * FROM payment_modes WHERE code=@code", con)
            cmd.Parameters.AddWithValue("@code", arguments.Value)
         Else
            If txtCode.Text.Trim = Session("code") Then
               cmd = New MySqlCommand("SELECT * FROM payment_modes WHERE code='POLKJHGTFDERRYYUUJJFDESOLMGDS'", con)
            Else
               cmd = New MySqlCommand("SELECT * FROM payment_modes WHERE code=@code", con)
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
            cmd = New MySqlCommand("SELECT * FROM payment_modes WHERE description=@description", con)
            cmd.Parameters.AddWithValue("@description", arguments.Value)
         Else
            If txtDescription.Text.Trim = Session("description") Then
               cmd = New MySqlCommand("SELECT * FROM payment_modes WHERE description='POLKJHGTFDERRYYUUJJFDESOLMGDS'", con)
            Else
               cmd = New MySqlCommand("SELECT * FROM payment_modes WHERE description=@description", con)
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

   Protected Sub validatedeType(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      If cboType.SelectedValue = "0" Then
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
      dt.Columns.Add("code")
      dt.Columns.Add("Description")
      dt.Columns.Add("type")
      dt.Columns.Add("Status")

      Using con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand = New MySqlCommand("SELECT * from payment_modes where ID > 0 order by description, code", con)
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
               dr("type") = reader("type")
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
            config.addTender(txtCode.Text.Trim, txtDescription.Text.Trim, cboType.SelectedValue, Math.Abs(CInt(chkInactive.Checked)), Session("ID"))
            lblmsg.Text = "Security type Saved"
         Else
            config.updateTender(txtCode.Text.Trim, txtDescription.Text.Trim, cboType.SelectedValue, Math.Abs(CInt(chkInactive.Checked)), Session("rowsID"), Session("ID"))
            lblmsg.Text = "Security type Updated"
         End If
         txtDescription.Text = ""
         txtCode.Text = ""
         cboType.SelectedIndex = 0
         chkInactive.Checked = False
         Session("rowsID") = 0
         Session("code") = ""
         Session("description") = ""
         Session("type") = ""
         Session("status") = ""
         populateGrid()
      End If
   End Sub

   Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
      txtDescription.Text = ""
      txtCode.Text = ""
      cboType.SelectedIndex = 0
      chkInactive.Checked = False
      Session("rowsID") = 0
      Session("code") = ""
      Session("description") = ""
      Session("type") = ""
      Session("status") = ""
      lblmsg.Text = ""
   End Sub

   Private Sub dgRetention_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgRetention.PageIndexChanging
      dgRetention.PageIndex = e.NewPageIndex
      populateGrid()
   End Sub
End Class