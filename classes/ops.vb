Imports MySql.Data.MySqlClient

Public Class ops
   Dim DBConnection As MySqlConnection
   Dim DBCommand As MySqlCommand

   Public Sub New()
      DBConnection = New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
   End Sub

   Public Sub populateCombo(combo As DropDownList, table As String, textField As String, valueField As String)
      DBConnection.Open()
      DBCommand = New MySqlCommand("select * from " & table & " where inactive=0 order by " & textField, DBConnection)
      Dim dr As MySqlDataReader = DBCommand.ExecuteReader
      combo.DataSource = dr
      combo.DataValueField = valueField
      combo.DataTextField = textField
      combo.DataBind()
      dr.Close()
      combo.Items.Insert(0, New ListItem("- Select -", 0))
      DBConnection.Close()
   End Sub

   Public Sub populateNationalityCombo(combo As DropDownList, textField As String, valueField As String)
      DBConnection.Open()
      DBCommand = New MySqlCommand("select * from countries where inactive=0 order by " & textField, DBConnection)
      Dim dr As MySqlDataReader = DBCommand.ExecuteReader
      combo.DataSource = dr
      combo.DataValueField = valueField
      combo.DataTextField = textField
      combo.DataBind()
      dr.Close()

      DBCommand = New MySqlCommand("select id from countries where inactive=0 and def=1", DBConnection)
      dr = DBCommand.ExecuteReader
      If dr.HasRows Then
         While dr.Read
            combo.SelectedValue = dr("id")
         End While
      Else
         combo.Items.Insert(0, New ListItem("- Select -", 0))
      End If
      dr.Close()
      DBConnection.Close()
   End Sub

   Public Sub populateDaysCombo(combo As DropDownList, mnth As Integer, yr As Integer)
      combo.Items.Clear()
      combo.Items.Insert(0, New ListItem("Day", 0))
      Select Case mnth
         Case 4, 6, 9, 11
            For i As Integer = 1 To 30
               combo.Items.Insert(i, New ListItem(i, i))
            Next

         Case 1, 3, 5, 7, 8, 10, 12, 0
            For i As Integer = 1 To 31
               combo.Items.Insert(i, New ListItem(i, i))
            Next

         Case 2
            If (yr > 0) And (yr Mod 4 = 0) Then '' for leap year
               For i As Integer = 1 To 29
                  combo.Items.Insert(i, New ListItem(i, i))
               Next
            Else
               For i As Integer = 1 To 28
                  combo.Items.Insert(i, New ListItem(i, i))
               Next
            End If
      End Select
   End Sub

   Public Sub populateMnthsComb(combo As DropDownList)
      combo.Items.Clear()
      combo.Items.Insert(0, New ListItem("Month", 0))
      For i As Integer = 1 To 12
         combo.Items.Insert(i, New ListItem(MonthName(i), i))
      Next
   End Sub

   Public Sub populateYrsCombo(combo As DropDownList, startYr As Integer, loops As Integer)
      combo.Items.Clear()
      Dim yr As Integer = startYr
      combo.Items.Insert(0, New ListItem("Year", 0))
      For i As Integer = 1 To loops
         combo.Items.Insert(i, New ListItem(yr, yr))
         yr -= 1
      Next
   End Sub

End Class
