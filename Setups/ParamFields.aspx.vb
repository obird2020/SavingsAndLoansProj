Imports MySql.Data.MySqlClient

Public Class ParamFields
   Inherits System.Web.UI.Page
   Dim dt As New DataTable()

   Protected Sub dgRegions_RowCommand(sender As Object, e As GridViewCommandEventArgs)
      dgRetention.Columns(1).Visible = True
      dgBranches.Columns(1).Visible = True
      Dim btndetails As LinkButton = DirectCast(e.CommandSource, LinkButton)
      Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)
      Session("rowsID") = HttpUtility.HtmlDecode(gvrow.Cells(1).Text)
      Session("type") = HttpUtility.HtmlDecode(gvrow.Cells(2).Text)
      Session("description") = HttpUtility.HtmlDecode(gvrow.Cells(3).Text)
      Session("status") = HttpUtility.HtmlDecode(gvrow.Cells(4).Text)
      lblBranch.Visible = True

      If e.CommandName = "editCmd" Then  ''if bank is clicked
         hdParamID.Value = Session("rowsID")
         populateGridBranches(hdParamID.Value)
         lnkNew.Visible = True
         dgBranches.Visible = True
         lblBranch.Text = HttpUtility.HtmlDecode(gvrow.Cells(2).Text).ToUpper & " - " & Session("description").ToString.ToUpper & " FIELDS"
         lnkNew.Text = "New " & Session("description") & " field"
      End If

      If e.CommandName = "editCmdBranch" Then  ''if branch is clicked
         txtDescription.Text = Session("description")
         hdParamID.Value = Session("rowsID")
         If Session("status") = "Inactive" Then
            chkInactive.Checked = True
         Else
            chkInactive.Checked = False
         End If
         Panel1.Visible = True
      End If

      dgRetention.Columns(1).Visible = False
      dgBranches.Columns(1).Visible = False
   End Sub

   Protected Sub validatedescription(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      Using con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand
         If hdParamID.Value = 0 Then
            cmd = New MySqlCommand("SELECT * FROM business_info_params WHERE description=@description and param_id=" & hdParamID.Value, con)
            cmd.Parameters.AddWithValue("@description", arguments.Value)
         Else
            If txtDescription.Text.Trim = Session("description") Then
               cmd = New MySqlCommand("SELECT * FROM business_info_params WHERE description='POLKJHGTFDERRYYUUJJFDESOLMGDS' and param_id=" & hdParamID.Value, con)
            Else
               cmd = New MySqlCommand("SELECT * FROM business_info_params WHERE description=@description and param_id=" & hdParamID.Value, con)
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
         reader.Close()
         con.Close()
      End Using
   End Sub

   Protected Sub populateGridBanks()
      dgRetention.Columns(1).Visible = True
      Dim dr As DataRow
      dt.Columns.Clear()
      dt.Clear()
      dt.Columns.Add("SN")
      dt.Columns.Add("ID")
      dt.Columns.Add("type")
      dt.Columns.Add("Description")
      dt.Columns.Add("Status")

      Using con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand = New MySqlCommand("SELECT * from business_info_cats where ID > 0 order by type, description", con)
         con.Open()
         Dim reader As MySqlDataReader = cmd.ExecuteReader()
         Dim increment As Integer = 0
         If reader.HasRows Then
            While reader.Read
               dr = dt.NewRow()
               increment += 1
               dr("SN") = increment
               dr("ID") = reader("ID")
               dr("type") = reader("type")
               dr("Description") = reader("description")
               If reader("Inactive") = 0 Then
                  dr("Status") = "Active"
               Else
                  dr("Status") = "Inactive"
               End If
               dt.Rows.Add(dr)
            End While
            dgRetention.DataSource = dt
            dgRetention.DataBind()

         End If
         reader.Close()
         con.Close()
      End Using
      dgRetention.Columns(1).Visible = False
   End Sub

   Protected Sub populateGridBranches(paramID As Integer)
      dgBranches.Columns(1).Visible = True
      Dim dr As DataRow
      dt.Columns.Clear()
      dt.Clear()
      dt.Columns.Add("SN")
      dt.Columns.Add("ID")
      'dt.Columns.Add("code")
      dt.Columns.Add("Description")
      dt.Columns.Add("Status")

      Using con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand = New MySqlCommand("SELECT * from business_info_params where param_id = " & paramID & " order by description", con)
         con.Open()
         Dim reader As MySqlDataReader = cmd.ExecuteReader()
         Dim increment As Integer = 0
         If reader.HasRows Then
            While reader.Read
               dr = dt.NewRow()
               increment += 1
               dr("SN") = increment
               dr("ID") = reader("ID")
               'dr("code") = reader("code")
               dr("Description") = reader("description")
               If reader("Inactive") = 0 Then
                  dr("Status") = "Active"
               Else
                  dr("Status") = "Inactive"
               End If
               dt.Rows.Add(dr)
            End While
         End If
         reader.Close()
         con.Close()
      End Using
      dgBranches.DataSource = dt
      dgBranches.DataBind()
      dgBranches.Columns(1).Visible = False
   End Sub

   Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      If Not Page.IsPostBack Then
         populateGridBanks()
      End If
   End Sub


   Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
      If Page.IsValid Then
         Dim config As New setups
         If hdFieldID.Value = 0 Then
            config.addCommonSetupForeignID("", txtDescription.Text.Trim, Math.Abs(CInt(chkInactive.Checked)), "business_info_params", "param_id", hdParamID.Value, Session("ID"))
         Else
            config.updateCommonSetupForeignID("", txtDescription.Text.Trim, Math.Abs(CInt(chkInactive.Checked)), "business_info_params", "param_id", hdParamID.Value, hdFieldID.Value, Session("ID"))
         End If
         populateGridBranches(hdParamID.Value)
         btnReset_Click(Nothing, Nothing)
      End If
   End Sub

   Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
      txtDescription.Text = ""
      'txtCode.Text = ""
      chkInactive.Checked = False
      Session("rowsID") = 0
      'Session("code") = ""
      Session("description") = ""
      Session("status") = ""
      hdFieldID.Value = 0
      Panel1.Visible = False
   End Sub

   Protected Sub lnkNew_Click(sender As Object, e As EventArgs) Handles lnkNew.Click
      If Panel1.Visible = True Then
         Panel1.Visible = False
      Else
         Panel1.Visible = True
         hdFieldID.Value = 0
      End If
   End Sub

End Class