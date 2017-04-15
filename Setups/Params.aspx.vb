﻿Imports MySql.Data.MySqlClient

Public Class Params
   Inherits System.Web.UI.Page
   Dim dt As New DataTable()

   Protected Sub dgRegions_RowCommand(sender As Object, e As GridViewCommandEventArgs)
      If e.CommandName = "editCmd" Or e.CommandName = "dltCmd" Then
         dgRetention.Columns(1).Visible = True
         Dim btndetails As LinkButton = DirectCast(e.CommandSource, LinkButton)
         Dim gvrow As GridViewRow = DirectCast(btndetails.NamingContainer, GridViewRow)

         Session("rowsID") = HttpUtility.HtmlDecode(gvrow.Cells(1).Text)
         Session("type") = HttpUtility.HtmlDecode(gvrow.Cells(2).Text)
         Session("description") = HttpUtility.HtmlDecode(gvrow.Cells(3).Text)
         Session("status") = HttpUtility.HtmlDecode(gvrow.Cells(4).Text)

         If e.CommandName = "editCmd" Then
            txtDescription.Text = Session("description")
            cboType.SelectedValue = Session("type")
            lblmsg.Text = ""
            If Session("status") = "Inactive" Then
               chkInactive.Checked = True
            Else
               chkInactive.Checked = False
            End If

            If e.CommandName = "dltCmd" Then
               Dim obj As New setups
               obj.delItm(Session("rowsID"), "territories")
               populateGrid()
            End If
            dgRetention.Columns(1).Visible = False
         End If
      End If
   End Sub

   Protected Sub validatedeType(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      If cboType.SelectedIndex = 0 Then
         arguments.IsValid = False
      Else
         arguments.IsValid = True
      End If
   End Sub

   Protected Sub validatedescription(ByVal source As Object, ByVal arguments As ServerValidateEventArgs)
      Using con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
         Dim cmd As MySqlCommand
         If Session("rowsID") = 0 Then
            cmd = New MySqlCommand("SELECT * FROM business_info_cats WHERE description=@description and type='" & cboType.SelectedValue & "'", con)
            cmd.Parameters.AddWithValue("@description", arguments.Value)
         Else
            If txtDescription.Text.Trim = Session("description") Then
               cmd = New MySqlCommand("SELECT * FROM business_info_cats WHERE description='POLKJHGTFDERRYYUUJJFDESOLMGDS' and type='" & cboType.SelectedValue & "'", con)
            Else
               cmd = New MySqlCommand("SELECT * FROM business_info_cats WHERE description=@description and type='" & cboType.SelectedValue & "'", con)
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

   Protected Sub populateGrid()
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

            'dgRetention.Columns(8).Visible = False
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
            config.addAdditionalParam(cboType.SelectedValue, txtDescription.Text.Trim, Math.Abs(CInt(chkInactive.Checked)), Session("ID"))
            lblmsg.Text = "Parameter Saved"
         Else
            config.updateAdditionalParam(cboType.SelectedValue, txtDescription.Text.Trim, Math.Abs(CInt(chkInactive.Checked)), Session("rowsID"), Session("ID"))
            lblmsg.Text = "Parameter Updated"
         End If
         txtDescription.Text = ""
         cboType.SelectedIndex = 0
         chkInactive.Checked = False
         Session("rowsID") = 0
         Session("type") = ""
         Session("description") = ""
         Session("status") = ""
         populateGrid()
      End If

   End Sub

   Protected Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
      txtDescription.Text = ""
      cboType.SelectedIndex = 0
      chkInactive.Checked = False
      Session("rowsID") = 0
      Session("code") = ""
      Session("description") = ""
      Session("status") = ""
      lblmsg.Text = ""
   End Sub

   Private Sub dgRetention_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgRetention.PageIndexChanging
      dgRetention.PageIndex = e.NewPageIndex
      populateGrid()
   End Sub
End Class