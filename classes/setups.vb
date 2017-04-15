Imports MySql.Data.MySqlClient
Imports System.IO

Public Class setups
   Dim DBConnection As MySqlConnection
   Dim DBCommand As MySqlCommand

   Public Sub New()
      DBConnection = New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
   End Sub

   Public Function Encode(ByVal pssword As String) As String
      Dim R As Short
      Dim nmbr As String
      Dim txt As String
      Dim eTtxt As Short
      nmbr = ""
      txt = Trim(pssword)
      For R = 1 To CShort(Len(txt))
         eTtxt = CShort(Asc(Mid(txt, R, 1)))
         nmbr = nmbr & New String(CChar("0"), 3 - Len(Trim(Str(eTtxt)))) & Trim(Str(eTtxt))
      Next
      Return nmbr
   End Function

   Public Function Decode(ByVal pssword As String) As String
      Dim R As Short
      Dim nmbr As String
      Dim txt As String
      Dim eTtxt As Short
      nmbr = ""
      txt = ""
      For R = 1 To CShort(Len(pssword))
         If IsNumeric(Mid(pssword, R, 1)) Then
            txt = txt & Mid(pssword, R, 1)
         End If
      Next

      For R = 1 To CShort(Len(txt)) Step 3
         eTtxt = CShort(Mid(txt, R, 3))
         nmbr = nmbr & Chr(eTtxt)
      Next
      Return nmbr
   End Function

   Public Sub delItm(ByVal ID As Integer, ByVal table As String)
      DBConnection.Open()
      DBCommand = New mySqlCommand("delete from " & table & " where ID=" & ID, DBConnection)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub addCommonSetup(ByVal code As String, ByVal Description As String, ByVal Inactive As Integer, table As String, CreatedBy As Integer)
      DBConnection.Open()
      If code = "" Then
         DBCommand = New MySqlCommand("insert into " & table & " (description, date_created, last_updated, inactive, created_by, last_updated_by) values (@description, now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      Else
         DBCommand = New MySqlCommand("insert into " & table & " (code, description, date_created, last_updated, inactive, created_by, last_updated_by) values (@code, @description, now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
         DBCommand.Parameters.AddWithValue("@code", code)
      End If
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateCommonSetup(ByVal code As String, ByVal Description As String, ByVal Inactive As Integer, table As String, id As Integer, updatedby As Integer)
      DBConnection.Open()
      If code = "" Then
         DBCommand = New MySqlCommand("update " & table & " set description=@description, last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedby & " where id=" & id, DBConnection)
      Else
         DBCommand = New MySqlCommand("update " & table & " set code=@code, description=@description, last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedby & " where id=" & id, DBConnection)
         DBCommand.Parameters.AddWithValue("@code", code)
      End If
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub


   Public Sub addAdditionalParam(ByVal type As String, ByVal Description As String, ByVal Inactive As Integer, CreatedBy As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("insert into business_info_cats (type, description, date_created, last_updated, inactive, created_by, last_updated_by) values (@type, @description, now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      DBCommand.Parameters.AddWithValue("@type", type)
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateAdditionalParam(ByVal type As String, ByVal Description As String, ByVal Inactive As Integer, id As Integer, updatedby As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("update business_info_cats set type=@type, description=@description, last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedby & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@type", type)
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub addCommonSetupForeignID(ByVal code As String, ByVal Description As String, ByVal Inactive As Integer, table As String, otherField As String, otherID As Integer, CreatedBy As Integer)
      DBConnection.Open()
      If code = "" Then
         DBCommand = New MySqlCommand("insert into " & table & " (description, date_created, last_updated, inactive, " & otherField & ", created_by, last_updated_by) values (@description, now(), now(), " & Inactive & ", " & otherID & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      Else
         DBCommand = New MySqlCommand("insert into " & table & " (code, description, date_created, last_updated, inactive, " & otherField & ", created_by, last_updated_by) values (@code, @description, now(), now(), " & Inactive & ", " & otherID & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
         DBCommand.Parameters.AddWithValue("@code", code)
      End If
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateCommonSetupForeignID(ByVal code As String, ByVal Description As String, ByVal Inactive As Integer, table As String, otherField As String, otherID As Integer, id As Integer, updatedby As Integer)
      DBConnection.Open()
      If code = "" Then
         DBCommand = New MySqlCommand("update " & table & " set description=@description, last_updated=now(), inactive=" & Inactive & ", " & otherField & "=" & otherID & " ,last_updated_by=" & updatedby & " where id=" & id, DBConnection)
      Else
         DBCommand = New MySqlCommand("update " & table & " set code=@code, description=@description, last_updated=now(), inactive=" & Inactive & ", " & otherField & "=" & otherID & ", last_updated_by=" & updatedby & " where id=" & id, DBConnection)
         DBCommand.Parameters.AddWithValue("@code", code)
      End If
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub addCommonSetupParams(ByVal code As String, ByVal Description As String, ByVal Inactive As Integer, table As String, valueType As String, otherField As String, otherID As Integer, CreatedBy As Integer)
      DBConnection.Open()
      If code = "" Then
         DBCommand = New MySqlCommand("insert into " & table & " (description, value_type, date_created, last_updated, inactive, " & otherField & ", created_by, last_updated_by) values (@description, @value_type, now(), now(), " & Inactive & ", " & otherID & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      Else
         DBCommand = New MySqlCommand("insert into " & table & " (code, description, value_type, date_created, last_updated, inactive, " & otherField & "=" & otherID & ", created_by, last_updated_by) values (@code, @description, @value_type, now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
         DBCommand.Parameters.AddWithValue("@code", code)
      End If
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.Parameters.AddWithValue("@value_type", valueType)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateCommonSetupParams(ByVal code As String, ByVal Description As String, ByVal Inactive As Integer, table As String, valueType As String, otherField As String, otherID As Integer, id As Integer, updatedby As Integer)
      DBConnection.Open()
      If code = "" Then
         DBCommand = New MySqlCommand("update " & table & " set description=@description, value_type=@value_type, last_updated=now(), inactive=" & Inactive & ", " & otherField & "=" & otherID & ", last_updated_by=" & updatedby & " where id=" & id, DBConnection)
      Else
         DBCommand = New MySqlCommand("update " & table & " set code=@code, description=@description, value_type=@value_type, last_updated=now(), inactive=" & Inactive & ", " & otherField & "=" & otherID & ", last_updated_by=" & updatedby & " where id=" & id, DBConnection)
         DBCommand.Parameters.AddWithValue("@code", code)
      End If
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub addCountry(ByVal code As String, ByVal Description As String, nationality As String, ByVal Inactive As Integer, ByVal def As Integer, CreatedBy As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("insert into countries (code, description, nationality, date_created, last_updated, inactive, def, created_by, last_updated_by) values (@code, @description, @nationality, now(), now(), " & Inactive & ", " & def & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      DBCommand.Parameters.AddWithValue("@code", code)
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.Parameters.AddWithValue("@nationality", nationality)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateCountry(ByVal code As String, ByVal Description As String, nationality As String, ByVal Inactive As Integer, ByVal def As Integer, id As Integer, updatedby As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("update countries set code=@code, description=@description, nationality=@nationality, last_updated=now(), inactive=" & Inactive & ", def=" & def & ", last_updated_by=" & updatedby & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@code", code)
      DBCommand.Parameters.AddWithValue("@description", Description)
      DBCommand.Parameters.AddWithValue("@nationality", nationality)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Function addCreditOfficer(title As Integer, ByVal fname As String, ByVal lname As String, oname As String, gender As String, dob As Date, phone1 As String, phone2 As String, email As String, nationality As Integer, notes As String, ByVal Inactive As Integer, CreatedBy As Integer, gdTerritories As GridView, gdAdditional As GridView) As Integer
      On Error GoTo err

      Dim id As Integer = 0
      DBConnection.Open()

      'Dim trans As String = System.Net.Dns.GetHostName.ToLower & "matar2xxx"
      DBCommand = New MySqlCommand("Start Transaction", DBConnection)
      DBCommand.ExecuteNonQuery()

      DBCommand = New MySqlCommand("insert into credit_officers (title, first_name, last_name, other_names, gender, dob, phone1, phone2, email, nationality,  date_created, last_updated, inactive, created_by, last_updated_by) values (" & title & ", @first_name, @last_name, @other_names, @gender, @dob, @phone1, @phone2, @email, " & nationality & ", now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      DBCommand.Parameters.AddWithValue("@first_name", fname)
      DBCommand.Parameters.AddWithValue("@last_name", lname)
      DBCommand.Parameters.AddWithValue("@other_names", oname)
      DBCommand.Parameters.AddWithValue("@gender", gender)
      DBCommand.Parameters.AddWithValue("@dob", dob)
      DBCommand.Parameters.AddWithValue("@phone1", phone1)
      DBCommand.Parameters.AddWithValue("@phone2", phone2)
      DBCommand.Parameters.AddWithValue("@email", email)
      'DBCommand.Parameters.AddWithValue("@notes", notes)
      DBCommand.ExecuteNonQuery()
      id = DBCommand.LastInsertedId
      'DBConnection.Close()


      '''get and save territories
      If gdTerritories.Rows.Count > 0 Then
         gdTerritories.Columns(2).Visible = True
         Dim terrID As Integer = 0
         Dim sql As String = ""
         For Each row As GridViewRow In gdTerritories.Rows
            If row.RowType = DataControlRowType.DataRow Then
               terrID = CInt(HttpUtility.HtmlDecode(row.Cells(2).Text))
               sql = sql & "insert into credit_officer_territories (officer_id, territory_id, date_created, created_by) values (" & id & ", " & terrID & ", now(), " & CreatedBy & ");"
            End If
         Next row
         DBCommand = New MySqlCommand("delete from credit_officer_territories where officer_id=" & id, DBConnection)
         DBCommand.ExecuteNonQuery()

         DBCommand = New MySqlCommand(sql, DBConnection)
         DBCommand.ExecuteNonQuery()
         gdTerritories.Columns(2).Visible = False
      End If


      '''get and save other info
      ''' 
      Dim MisReferences(50) As String
      Dim MisDescs(50) As String
      Dim MisDocs(50) As String
      Dim MisInc As Integer = -1
      If gdAdditional.Rows.Count > 0 Then
         gdAdditional.Columns(6).Visible = True
         Dim sql As String = ""
         For Each row As GridViewRow In gdAdditional.Rows
            If row.RowType = DataControlRowType.DataRow Then
               Dim Reference As WebControls.TextBox = DirectCast(row.FindControl("Reference"), WebControls.TextBox)
               Dim Description As WebControls.TextBox = DirectCast(row.FindControl("Description"), WebControls.TextBox)
               Dim gvRow As GridViewRow = DirectCast(Reference.NamingContainer, GridViewRow)
               'Dim SN As Integer = CInt(HttpUtility.HtmlDecode(gvRow.Cells(0).Text))
               Dim filePath As String = CStr(HttpUtility.HtmlDecode(gvRow.Cells(6).Text))
               If Reference.Text.Trim <> "" And Description.Text.Trim <> "" Then
                  MisInc += 1
                  MisReferences(MisInc) = Reference.Text.Trim
                  MisDescs(MisInc) = Description.Text.Trim
                  MisDocs(MisInc) = filePath
               End If
            End If
         Next row
         gdAdditional.Columns(6).Visible = False
      End If


      For i As Integer = 0 To MisInc
         DBCommand = New MySqlCommand("insert into credit_officer_docs (officer_id, description, date_created, created_by, last_updated, last_updated_by, doc_ref) values (" & id & ", @description, now(), " & CreatedBy & ", now(), " & CreatedBy & ", @doc_ref)", DBConnection)
         DBCommand.Parameters.AddWithValue("@doc_ref", MisReferences(i))
         DBCommand.Parameters.AddWithValue("@description", MisDescs(i))
         DBCommand.ExecuteNonQuery()
      Next


      '''save docs
      For i As Integer = 0 To MisInc
         If MisDocs(i) <> "" Then
            Dim ext As String = Path.GetExtension(MisDocs(i))
            File.Move(HttpContext.Current.Server.MapPath(MisDocs(i)), HttpContext.Current.Server.MapPath("~/Officer_Docs/" & id & "_" & MisReferences(i) & "_" & ext))
            gdAdditional.Rows(i).Cells(6).Text = "~/Officer_Docs/" & id & "_" & MisReferences(i) & "_" & ext
         End If
      Next


      '''''save profile pic
      Dim directoryname As String = System.Net.Dns.GetHostName.ToLower & "matar2xxx"
      'HttpContext.Current.Server.MapPath(("~/" & directoryname & "/indexing"))
      If Directory.Exists(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic")) Then
         Dim indexingFileName() As String
         indexingFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic"))
         If indexingFileName.Length > 0 Then
            Dim ext As String = Path.GetExtension(indexingFileName(0))
            Dim currentFile As String = Path.GetFileName(indexingFileName(0))
            File.Move(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic/" & currentFile), HttpContext.Current.Server.MapPath("~/Officer_Pics/" & id & ext))
         End If
      End If

      DBCommand = New MySqlCommand("Commit", DBConnection)
      DBCommand.ExecuteNonQuery()

      DBConnection.Close()

      Return id

      Exit Function
err:
      DBCommand = New MySqlCommand("Rollback", DBConnection)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
      MsgBox("Error occured.." & Err.Description)
      Exit Function
   End Function

   Public Sub updateCreditOfficer(title As Integer, ByVal fname As String, ByVal lname As String, oname As String, gender As String, dob As Date, phone1 As String, phone2 As String, email As String, nationality As Integer, ByVal Inactive As Integer, updatedby As Integer, id As Integer, gdTerritories As GridView, gdAdditional As GridView)

      On Error GoTo err
      Dim directoryname As String = System.Net.Dns.GetHostName.ToLower & "matar2xxx"

      DBConnection.Open()
      DBCommand = New MySqlCommand("Start Transaction", DBConnection)
      DBCommand.ExecuteNonQuery()

      DBCommand = New MySqlCommand("update credit_officers set first_name=@first_name, last_name=@last_name, other_names=@other_names, gender=@gender, dob=@dob, phone1=@phone1, phone2=@phone2, email=@email, nationality=" & nationality & ", last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedby & ", title=" & title & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@first_name", fname)
      DBCommand.Parameters.AddWithValue("@last_name", lname)
      DBCommand.Parameters.AddWithValue("@other_names", oname)
      DBCommand.Parameters.AddWithValue("@gender", gender)
      DBCommand.Parameters.AddWithValue("@dob", dob)
      DBCommand.Parameters.AddWithValue("@phone1", phone1)
      DBCommand.Parameters.AddWithValue("@phone2", phone2)
      DBCommand.Parameters.AddWithValue("@email", email)
      'DBCommand.Parameters.AddWithValue("@notes", notes)
      DBCommand.ExecuteNonQuery()


      '''get and save territories
      ''' 
      DBCommand = New MySqlCommand("delete from credit_officer_territories where officer_id=" & id, DBConnection)
      DBCommand.ExecuteNonQuery()
      If gdTerritories.Rows.Count > 0 Then
         gdTerritories.Columns(2).Visible = True
         Dim terrID As Integer = 0
         Dim sql As String = ""
         For Each row As GridViewRow In gdTerritories.Rows
            If row.RowType = DataControlRowType.DataRow Then
               terrID = CInt(HttpUtility.HtmlDecode(row.Cells(2).Text))
               sql = sql & "insert into credit_officer_territories (officer_id, territory_id, date_created, created_by) values (" & id & ", " & terrID & ", now(), " & updatedby & ");"
            End If
         Next row
         DBCommand = New MySqlCommand(sql, DBConnection)
         DBCommand.ExecuteNonQuery()
         gdTerritories.Columns(2).Visible = False
      End If


      '''get and save other info
      ''' 
      Dim MisReferences(50) As String
      Dim MisDescs(50) As String
      Dim MisDocs(50) As String
      Dim MisInc As Integer = -1
      If gdAdditional.Rows.Count > 0 Then
         gdAdditional.Columns(6).Visible = True
         Dim sql As String = ""
         For Each row As GridViewRow In gdAdditional.Rows
            If row.RowType = DataControlRowType.DataRow Then
               Dim Reference As WebControls.TextBox = DirectCast(row.FindControl("Reference"), WebControls.TextBox)
               Dim Description As WebControls.TextBox = DirectCast(row.FindControl("Description"), WebControls.TextBox)
               Dim gvRow As GridViewRow = DirectCast(Reference.NamingContainer, GridViewRow)
               'Dim SN As Integer = CInt(HttpUtility.HtmlDecode(gvRow.Cells(0).Text))
               Dim filePath As String = CStr(HttpUtility.HtmlDecode(gvRow.Cells(6).Text))
               If Reference.Text.Trim <> "" And Description.Text.Trim <> "" Then
                  MisInc += 1
                  MisReferences(MisInc) = Reference.Text.Trim
                  MisDescs(MisInc) = Description.Text.Trim
                  MisDocs(MisInc) = filePath
               End If
            End If
         Next row
         gdAdditional.Columns(6).Visible = False
      End If


      '''delete unmatched attachments
      ''' 
      Dim MisgFileName() As String
      MisgFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Officer_Docs"), id & "_*")
      If MisgFileName.Length > 0 Then
         For j As Integer = 0 To MisgFileName.Length - 1   ''from directory
            Dim killFile As Boolean = True
            For i As Integer = 0 To MisInc     ''new to be saved
               If Path.GetFileName(MisgFileName(j)).Contains(id & "_" & MisReferences(i)) Then
                  killFile = False
                  Exit For
               End If
            Next i
            If killFile = True Then
               If File.Exists(HttpContext.Current.Server.MapPath("~/Officer_Docs/" & Path.GetFileName(MisgFileName(j).ToString))) Then
                  File.Delete(HttpContext.Current.Server.MapPath("~/Officer_Docs/" & Path.GetFileName(MisgFileName(j).ToString)))
               End If
            End If
         Next j
      End If


      '''save docs
      ''' 
      For i As Integer = 0 To MisInc
         If (MisDocs(i) <> "xxxxx" Or MisDocs(i) <> "") And MisDocs(i).Contains("~/" & directoryname & "/officers") Then
            For j As Integer = 0 To MisgFileName.Length - 1
               If MisgFileName(j).ToString.Contains(id & "_" & MisReferences(i)) Then
                  'If File.Exists(HttpContext.Current.Server.MapPath("~/Officer_Docs/" & Path.GetFileName(MisgFileName(j).ToString))) Then
                  '   File.Delete(HttpContext.Current.Server.MapPath("~/Officer_Docs/" & Path.GetFileName(MisgFileName(j).ToString)))
                  'End If

                  For Each _file As String In Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Officer_Docs"), id & "_" & MisReferences(i) & "_.*")
                     File.Delete(_file)
                  Next
                  Exit For
               End If
            Next

            Dim ext As String = Path.GetExtension(MisDocs(i))
            File.Move(HttpContext.Current.Server.MapPath(MisDocs(i)), HttpContext.Current.Server.MapPath("~/Officer_Docs/" & id & "_" & MisReferences(i) & "_" & ext))
            gdAdditional.Rows(i).Cells(6).Text = "~/Officer_Docs/" & id & "_" & MisReferences(i) & "_" & ext

         ElseIf (MisDocs(i) <> "xxxxx" Or MisDocs(i) <> "") And MisDocs(i).Contains("~/Officer_Docs/") Then
            Dim ext As String = Path.GetExtension(MisDocs(i))
            If HttpContext.Current.Server.MapPath(MisDocs(i)) <> HttpContext.Current.Server.MapPath("~/Officer_Docs/" & id & "_" & MisReferences(i) & "_" & ext) Then
               File.Copy(HttpContext.Current.Server.MapPath(MisDocs(i)), HttpContext.Current.Server.MapPath("~/Officer_Docs/" & id & "_" & MisReferences(i) & "_" & ext))
               File.Delete(HttpContext.Current.Server.MapPath(MisDocs(i)))
               gdAdditional.Rows(i).Cells(6).Text = "~/Officer_Docs/" & id & "_" & MisReferences(i) & "_" & ext
            End If

         End If
      Next


      '''save files...in db
      DBCommand = New MySqlCommand("delete from credit_officer_docs where officer_id=" & id, DBConnection)
      DBCommand.ExecuteNonQuery()
      For i As Integer = 0 To MisInc
         DBCommand = New MySqlCommand("insert into credit_officer_docs (officer_id, description, date_created, created_by, last_updated, last_updated_by, doc_ref) values (" & id & ", @description, now(), " & updatedby & ", now(), " & updatedby & ", @doc_ref)", DBConnection)
         DBCommand.Parameters.AddWithValue("@doc_ref", MisReferences(i))
         DBCommand.Parameters.AddWithValue("@description", MisDescs(i))
         DBCommand.ExecuteNonQuery()
      Next


      ''profile pic
      'Dim directoryname As String = System.Net.Dns.GetHostName.ToLower & "matar2xxx"
      If Directory.Exists(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic")) Then
         Dim indexingFileName() As String
         indexingFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic"))
         If indexingFileName.Length > 0 Then
            Dim ext As String = Path.GetExtension(indexingFileName(0))
            Dim currentFile As String = Path.GetFileName(indexingFileName(0))


            Dim indexingOldFileName() As String
            indexingOldFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Officer_Pics"), id & ".*")
            If indexingOldFileName.Length > 0 Then
               Dim oldExt As String = Path.GetExtension(indexingOldFileName(0))
               If File.Exists(HttpContext.Current.Server.MapPath("~/Officer_Pics/" & Path.GetFileName(indexingOldFileName(0)))) Then
                  File.Delete(HttpContext.Current.Server.MapPath("~/Officer_Pics/" & Path.GetFileName(indexingOldFileName(0))))
               End If
            End If

            File.Move(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic/" & currentFile), HttpContext.Current.Server.MapPath("~/Officer_Pics/" & id & ext))
         End If
      End If

      DBCommand = New MySqlCommand("Commit", DBConnection)
      DBCommand.ExecuteNonQuery()

      DBConnection.Close()

      Exit Sub
err:
      DBCommand = New MySqlCommand("Rollback", DBConnection)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
      MsgBox("Error occured.." & Err.Description)
      Exit Sub

   End Sub

   Public Sub addLoanPayModes(ByVal description As String, ByVal frequency As Integer, type As String, ByVal Inactive As Integer, CreatedBy As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("insert into loan_pay_modes (description, frequency, type, date_created, last_updated, inactive, created_by, last_updated_by) values (@description, " & frequency & ", '" & type & "', now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      DBCommand.Parameters.AddWithValue("@description", description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateLoanPayModes(ByVal description As String, ByVal frequency As Integer, type As String, ByVal Inactive As Integer, updatedBy As Integer, id As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("update loan_pay_modes set description=@description, frequency=" & frequency & ", type='" & type & "', last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedBy & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@description", description)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub addPayPeriod(code As String, ByVal description As String, ByVal interestRate As Double, paymentMonths As Integer, paymentDays As Integer, ByVal Inactive As Integer, CreatedBy As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("insert into loan_pay_periods (code, description, interest_rate, payment_months, payment_days, date_created, last_updated, inactive, created_by, last_updated_by) values (@code, @description, " & interestRate & ", " & paymentMonths & ", " & paymentDays & ", now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      DBCommand.Parameters.AddWithValue("@description", description)
      DBCommand.Parameters.AddWithValue("@code", code)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updatePayPeriod(code As String, ByVal description As String, ByVal interestRate As Double, paymentMonths As Integer, paymentDays As Integer, ByVal Inactive As Integer, updatedBy As Integer, id As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("update loan_pay_periods set code=@code, description=@description, interest_rate=" & interestRate & ", payment_months=" & paymentMonths & ", payment_days=" & paymentDays & ", last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedBy & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@description", description)
      DBCommand.Parameters.AddWithValue("@code", code)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub addLoanSecurityType(code As String, ByVal description As String, ByVal type As String, ByVal Inactive As Integer, CreatedBy As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("insert into loan_security_types (code, description, type, date_created, last_updated, inactive, created_by, last_updated_by) values (@code, @description, '" & type & "', now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      DBCommand.Parameters.AddWithValue("@description", description)
      DBCommand.Parameters.AddWithValue("@code", code)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateLoanSecurityType(code As String, ByVal description As String, ByVal type As String, ByVal Inactive As Integer, updatedBy As Integer, id As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("update loan_security_types set code=@code, description=@description, type='" & type & "', last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedBy & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@description", description)
      DBCommand.Parameters.AddWithValue("@code", code)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub addTender(code As String, ByVal description As String, ByVal type As String, ByVal Inactive As Integer, CreatedBy As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("insert into payment_modes (code, description, type, date_created, last_updated, inactive, created_by, last_updated_by) values (@code, @description, '" & type & "', now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      DBCommand.Parameters.AddWithValue("@description", description)
      DBCommand.Parameters.AddWithValue("@code", code)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateTender(code As String, ByVal description As String, ByVal type As String, ByVal Inactive As Integer, updatedBy As Integer, id As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("update payment_modes set code=@code, description=@description, type='" & type & "', last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedBy & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@description", description)
      DBCommand.Parameters.AddWithValue("@code", code)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub addUser(user_id As String, first_name As String, last_name As String, other_names As String, phone As String, email As String, ByVal Inactive As Integer, CreatedBy As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("insert into users (user_id, pwd, first_name, last_name, other_names, phone, email, status, date_created, last_updated, inactive, created_by, last_updated_by, first_time) values (@user_id, @pwd, @first_name, @last_name, @other_names, @phone, @email, 'NOT CONNECTED', now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ", 1)", DBConnection)
      DBCommand.Parameters.AddWithValue("@user_id", user_id)
      DBCommand.Parameters.AddWithValue("@pwd", Encode("pass"))
      DBCommand.Parameters.AddWithValue("@first_name", first_name)
      DBCommand.Parameters.AddWithValue("@last_name", last_name)
      DBCommand.Parameters.AddWithValue("@other_names", other_names)
      DBCommand.Parameters.AddWithValue("@phone", phone)
      DBCommand.Parameters.AddWithValue("@email", email)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateUser(user_id As String, first_name As String, last_name As String, other_names As String, phone As String, email As String, ByVal Inactive As Integer, updatedBy As Integer, id As Integer)
      DBConnection.Open()
      DBCommand = New MySqlCommand("update users set user_id=@user_id, first_name=@first_name, last_name=@last_name, other_names=@other_names, phone=@phone, email=@email, last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedBy & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@user_id", user_id)
      DBCommand.Parameters.AddWithValue("@first_name", first_name)
      DBCommand.Parameters.AddWithValue("@last_name", last_name)
      DBCommand.Parameters.AddWithValue("@other_names", other_names)
      DBCommand.Parameters.AddWithValue("@phone", phone)
      DBCommand.Parameters.AddWithValue("@email", email)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

   Public Sub updateProfile(ByVal CompanyName As String, ByVal Address1 As String, ByVal Address2 As String, ByVal City As String, ByVal Country As String, ByVal Phone As String, ByVal Fax As String, ByVal Email As String, ByVal Server As String, ByVal PortNumber As String, ByVal smEmail As String, ByVal user As String, ByVal pwd As String, enableSSL As Boolean, URL As String, website As String)
      DBConnection.Open()
      DBCommand = New MySqlCommand("update Profile set CompanyName=@CompanyName, Address1=@Address1, Address2=@Address2,City=@City, Country=@Country, Phone=@Phone, Fax=@Fax, Email=@Email, Server=@Server, PortNumber=@PortNumber, smEmail=@smEmail, UserName=@UserName, Password=@Password, Lastupdated=now(), EnableSSL=@EnableSSL, URL=@URL, website=@website", DBConnection)
      DBCommand.Parameters.AddWithValue("@CompanyName", CompanyName)
      DBCommand.Parameters.AddWithValue("@Address1", Address1)
      DBCommand.Parameters.AddWithValue("@Address2", Address2)
      DBCommand.Parameters.AddWithValue("@Country", Country)
      DBCommand.Parameters.AddWithValue("@City", City)
      DBCommand.Parameters.AddWithValue("@Phone", Phone)
      DBCommand.Parameters.AddWithValue("@Fax", Fax)
      DBCommand.Parameters.AddWithValue("@Email", Email)
      DBCommand.Parameters.AddWithValue("@Server", Server)
      DBCommand.Parameters.AddWithValue("@PortNumber", PortNumber)
      DBCommand.Parameters.AddWithValue("@smEmail", smEmail)
      DBCommand.Parameters.AddWithValue("@UserName", user)
      DBCommand.Parameters.AddWithValue("@Password", pwd)
      DBCommand.Parameters.AddWithValue("@EnableSSL", enableSSL)
      DBCommand.Parameters.AddWithValue("@URL", URL)
      DBCommand.Parameters.AddWithValue("@website", website)
      DBCommand.ExecuteNonQuery()
      DBConnection.Close()
   End Sub

End Class
