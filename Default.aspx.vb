Imports MySql.Data.MySqlClient

Public Class _Default
   Inherits System.Web.UI.Page

   Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnLog.Click
      Dim err As String = ValidateUser(txtUserID.Text.Trim, txtPassword.Text)
      If err = "success" Then
         Response.Redirect("~/Main.aspx")
      Else
         lblError.Text = err
      End If
   End Sub

   Protected Function ValidateUser(user As String, pass As String) As String
      Dim ret As String = ""
      Dim config As New setups
      Dim con As New MySqlConnection(ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
      Dim cmd As MySqlCommand = New MySqlCommand("SELECT * FROM Users where user_id = @Username and pwd=@Pword", con)
      cmd.Parameters.AddWithValue("@username", user)
      If pass = "" Then
         cmd.Parameters.AddWithValue("@Pword", pass)
      Else
         cmd.Parameters.AddWithValue("@Pword", config.Encode(pass))
      End If
      con.Open()
      Dim reader As MySqlDataReader = cmd.ExecuteReader()
      If reader.HasRows Then
         While reader.Read
            If Convert.ToInt16(reader("Inactive")) = 1 Then
               ret = "User is inactive"
               Exit While
            Else
               ret = "success"
            End If
            Session("user_id") = reader("user_id")
            If reader("pwd") = "" Then
               Session("pwd") = ""
            Else
               Session("pwd") = reader("pwd")
            End If
            Session("Name") = reader("first_name")
            Session("Inact") = Convert.ToInt16(reader("Inactive"))
            'Session("Priviledge") = IIf(IsDBNull(reader("Priviledge")), "", reader("Priviledge"))
            Session("Em") = reader("email")
            'Session("Ph") = reader("Phone")
            Session("ID") = reader("ID")
         End While

      Else
         ret = "Invalid login credentials"
      End If
      reader.Close()
      con.Close()

      Return ret

   End Function
End Class