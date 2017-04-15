Imports MySql.Data.MySqlClient
Public Class ndl
   Inherits System.Web.UI.MasterPage

   Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
      If Not Page.IsPostBack Then
         If ((Session("user_id") Is Nothing) Or (Session("pwd") Is Nothing)) Then
            Response.Redirect(Server.MapPath("~/Main.aspx"))
         Else
            lnkLogOut.Text = "LOGOUT"
            lnkUser.Text = Session("Name").ToString.ToUpper
            hdUserID.Value = Session("ID")
            hdUserMail.Value = Session("Em")
         End If

         '''populate menus..
         ''' 
         'Dim conf As New setups
         'Dim theVal As String = IIf(IsDBNull(Session("Priviledge")), "XXX", Session("Priviledge"))
         'priv = conf.Decode(theVal)

         'Dim dt As DataTable = conf.GetNodeData("Select * from ParentNodes order by POS, Leaf")
         'conf.PopulateTreeView(dt, 0, Nothing, TreeView1)
         ''TreeView1.CollapseAll()
      End If
   End Sub

   Protected Sub lnkLogOut_Click(sender As Object, e As EventArgs) Handles lnkLogOut.Click
      Session("Name") = Nothing
      Session("Inact") = Nothing
      Session("Em") = Nothing
      Session("ID") = Nothing
      Response.Redirect("/Default.aspx")
   End Sub

End Class