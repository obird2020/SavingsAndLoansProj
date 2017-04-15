Imports MySql.Data.MySqlClient
Imports System.IO

Public Class trans
   Dim DBConnection As MySqlConnection
   Dim DBCommand As MySqlCommand

   Public Sub New()
      DBConnection = New MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("microConn").ConnectionString)
   End Sub

   Public Function GenAccNum(ID As Integer) As String
      Dim mnth As String = Now.Month.ToString("D2")
      Dim yr As Integer = Now.Year
      Dim lst As String = yr.ToString("D" & 12 - Len(mnth & ID))
      Return mnth & ID & lst
   End Function

   Public Function addCustomer(title As Integer, ByVal fname As String, ByVal lname As String, oname As String, gender As String, dob As Date, phone1 As String, phone2 As String, email As String, nationality As Integer, marital_status As Integer, education_level As Integer, spouse_name As String, spouse_phone As String, id_type As Integer, id_number As String, religion As Integer, religion_branch_name As String, religion_location As String, next_of_kin As String, next_of_kin_contact As String, next_of_kin_email As String, num_of_dependants As Integer, address As String, ByVal Inactive As Integer, CreatedBy As Integer, gdAdditional As GridView) As Integer
      On Error GoTo err

      Dim id As Integer = 0
      Dim acc_num As String = ""
      DBConnection.Open()

      DBCommand = New MySqlCommand("Start Transaction", DBConnection)
      DBCommand.ExecuteNonQuery()

      DBCommand = New MySqlCommand("insert into customers (title, first_name, last_name, other_names, gender, dob, phone1, phone2, email, nationality, marital_status, education_level, spouse_name, spouse_phone, id_type, id_number, religion, religion_branch_name, religion_location, next_of_kin, next_of_kin_contact, next_of_kin_email, num_of_dependants, address, date_created, last_updated, inactive, created_by, last_updated_by) values (" & title & ", @first_name, @last_name, @other_names, @gender, @dob, @phone1, @phone2, @email, " & nationality & ", " & marital_status & ", " & education_level & ", @spouse_name, @spouse_phone, " & id_type & ", @id_number, " & religion & ", @religion_branch_name, @religion_location, @next_of_kin, @next_of_kin_contact, @next_of_kin_email, " & num_of_dependants & ", @address, now(), now(), " & Inactive & ", " & CreatedBy & ", " & CreatedBy & ")", DBConnection)
      DBCommand.Parameters.AddWithValue("@first_name", fname)
      DBCommand.Parameters.AddWithValue("@last_name", lname)
      DBCommand.Parameters.AddWithValue("@other_names", oname)
      DBCommand.Parameters.AddWithValue("@gender", gender)
      DBCommand.Parameters.AddWithValue("@dob", dob)
      DBCommand.Parameters.AddWithValue("@phone1", phone1)
      DBCommand.Parameters.AddWithValue("@phone2", phone2)
      DBCommand.Parameters.AddWithValue("@email", email)
      DBCommand.Parameters.AddWithValue("@spouse_name", spouse_name)
      DBCommand.Parameters.AddWithValue("@spouse_phone", spouse_phone)
      DBCommand.Parameters.AddWithValue("@id_number", id_number)
      DBCommand.Parameters.AddWithValue("@religion_branch_name", religion_branch_name)
      DBCommand.Parameters.AddWithValue("@religion_location", religion_location)
      DBCommand.Parameters.AddWithValue("@next_of_kin", next_of_kin)
      DBCommand.Parameters.AddWithValue("@next_of_kin_contact", next_of_kin_contact)
      DBCommand.Parameters.AddWithValue("@next_of_kin_email", next_of_kin_email)
      DBCommand.Parameters.AddWithValue("@address", address)
      DBCommand.ExecuteNonQuery()
      id = DBCommand.LastInsertedId

      Dim accNum As String = GenAccNum(id)
      System.Web.HttpContext.Current.Session("AccNum") = accNum
      DBCommand = New MySqlCommand("update customers set account_num='" & accNum & "' where id=" & id, DBConnection)
      DBCommand.ExecuteNonQuery()


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
         DBCommand = New MySqlCommand("insert into customer_docs (customer_id, description, date_created, created_by, last_updated, last_updated_by, doc_ref) values (" & id & ", @description, now(), " & CreatedBy & ", now(), " & CreatedBy & ", @doc_ref)", DBConnection)
         DBCommand.Parameters.AddWithValue("@doc_ref", MisReferences(i))
         DBCommand.Parameters.AddWithValue("@description", MisDescs(i))
         DBCommand.ExecuteNonQuery()
      Next


      '''save docs
      For i As Integer = 0 To MisInc
         If MisDocs(i) <> "" Then
            Dim ext As String = Path.GetExtension(MisDocs(i))
            File.Move(HttpContext.Current.Server.MapPath(MisDocs(i)), HttpContext.Current.Server.MapPath("~/Customer_Docs/" & id & "_" & MisReferences(i) & "_" & ext))
            gdAdditional.Rows(i).Cells(6).Text = "~/Customer_Docs/" & id & "_" & MisReferences(i) & "_" & ext
         End If
      Next


      '''''save profile pic
      Dim directoryname As String = System.Net.Dns.GetHostName.ToLower & "matar2xxx"
      If Directory.Exists(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic")) Then
         Dim indexingFileName() As String
         indexingFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic"))
         If indexingFileName.Length > 0 Then
            Dim ext As String = Path.GetExtension(indexingFileName(0))
            Dim currentFile As String = Path.GetFileName(indexingFileName(0))
            File.Move(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic/" & currentFile), HttpContext.Current.Server.MapPath("~/Customer_Profiles/" & id & ext))
         End If
      End If

      '''''save profile sig
      If Directory.Exists(HttpContext.Current.Server.MapPath("~/" & directoryname & "/sig")) Then
         Dim indexingFileName() As String
         indexingFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/" & directoryname & "/sig"))
         If indexingFileName.Length > 0 Then
            Dim ext As String = Path.GetExtension(indexingFileName(0))
            Dim currentFile As String = Path.GetFileName(indexingFileName(0))
            File.Move(HttpContext.Current.Server.MapPath("~/" & directoryname & "/sig/" & currentFile), HttpContext.Current.Server.MapPath("~/Customer_Sigs/" & id & ext))
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


   Public Sub updateCustomer(title As Integer, ByVal fname As String, ByVal lname As String, oname As String, gender As String, dob As Date, phone1 As String, phone2 As String, email As String, nationality As Integer, marital_status As Integer, education_level As Integer, spouse_name As String, spouse_phone As String, id_type As Integer, id_number As String, religion As Integer, religion_branch_name As String, religion_location As String, next_of_kin As String, next_of_kin_contact As String, next_of_kin_email As String, num_of_dependants As Integer, address As String, ByVal Inactive As Integer, updatedBy As Integer, gdAdditional As GridView, id As Integer)

      On Error GoTo err
      Dim directoryname As String = System.Net.Dns.GetHostName.ToLower & "matar2xxx"

      DBConnection.Open()
      DBCommand = New MySqlCommand("Start Transaction", DBConnection)
      DBCommand.ExecuteNonQuery()

      DBCommand = New MySqlCommand("update customers set title=" & title & ", first_name=@first_name, last_name= @last_name, other_names=@other_names, gender=@gender, dob=@dob, phone1=@phone1, phone2=@phone2, email=@email, nationality=" & nationality & ", marital_status=" & marital_status & ", education_level=" & education_level & ", spouse_name=@spouse_name, spouse_phone=@spouse_phone, id_type=" & id_type & ", id_number=@id_number, religion=" & religion & ", religion_branch_name=@religion_branch_name, religion_location=@religion_location, next_of_kin=@next_of_kin, next_of_kin_contact=@next_of_kin_contact, next_of_kin_email=@next_of_kin_email, num_of_dependants=" & num_of_dependants & ", address=@address, last_updated=now(), inactive=" & Inactive & ", last_updated_by=" & updatedBy & " where id=" & id, DBConnection)
      DBCommand.Parameters.AddWithValue("@first_name", fname)
      DBCommand.Parameters.AddWithValue("@last_name", lname)
      DBCommand.Parameters.AddWithValue("@other_names", oname)
      DBCommand.Parameters.AddWithValue("@gender", gender)
      DBCommand.Parameters.AddWithValue("@dob", dob)
      DBCommand.Parameters.AddWithValue("@phone1", phone1)
      DBCommand.Parameters.AddWithValue("@phone2", phone2)
      DBCommand.Parameters.AddWithValue("@email", email)
      DBCommand.Parameters.AddWithValue("@spouse_name", spouse_name)
      DBCommand.Parameters.AddWithValue("@spouse_phone", spouse_phone)
      DBCommand.Parameters.AddWithValue("@id_number", id_number)
      DBCommand.Parameters.AddWithValue("@religion_branch_name", religion_branch_name)
      DBCommand.Parameters.AddWithValue("@religion_location", religion_location)
      DBCommand.Parameters.AddWithValue("@next_of_kin", next_of_kin)
      DBCommand.Parameters.AddWithValue("@next_of_kin_contact", next_of_kin_contact)
      DBCommand.Parameters.AddWithValue("@next_of_kin_email", next_of_kin_email)
      DBCommand.Parameters.AddWithValue("@address", address)
      DBCommand.ExecuteNonQuery()


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
      MisgFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Customer_Docs"), id & "_*")
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
               If File.Exists(HttpContext.Current.Server.MapPath("~/Customer_Docs/" & Path.GetFileName(MisgFileName(j).ToString))) Then
                  File.Delete(HttpContext.Current.Server.MapPath("~/Customer_Docs/" & Path.GetFileName(MisgFileName(j).ToString)))
               End If
            End If
         Next j
      End If


      '''save docs
      ''' 
      For i As Integer = 0 To MisInc
         If (MisDocs(i) <> "xxxxx" Or MisDocs(i) <> "") And MisDocs(i).Contains("~/" & directoryname & "/customers") Then
            For j As Integer = 0 To MisgFileName.Length - 1
               If MisgFileName(j).ToString.Contains(id & "_" & MisReferences(i)) Then
                  For Each _file As String In Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Customer_Docs"), id & "_" & MisReferences(i) & "_.*")
                     File.Delete(_file)
                  Next
                  Exit For
               End If
            Next

            Dim ext As String = Path.GetExtension(MisDocs(i))
            File.Move(HttpContext.Current.Server.MapPath(MisDocs(i)), HttpContext.Current.Server.MapPath("~/Customer_Docs/" & id & "_" & MisReferences(i) & "_" & ext))
            gdAdditional.Rows(i).Cells(6).Text = "~/Customer_Docs/" & id & "_" & MisReferences(i) & "_" & ext

         ElseIf (MisDocs(i) <> "xxxxx" Or MisDocs(i) <> "") And MisDocs(i).Contains("~/Customer_Docs/") Then
            Dim ext As String = Path.GetExtension(MisDocs(i))
            If HttpContext.Current.Server.MapPath(MisDocs(i)) <> HttpContext.Current.Server.MapPath("~/Customer_Docs/" & id & "_" & MisReferences(i) & "_" & ext) Then
               File.Copy(HttpContext.Current.Server.MapPath(MisDocs(i)), HttpContext.Current.Server.MapPath("~/Customer_Docs/" & id & "_" & MisReferences(i) & "_" & ext))
               File.Delete(HttpContext.Current.Server.MapPath(MisDocs(i)))
               gdAdditional.Rows(i).Cells(6).Text = "~/Customer_Docs/" & id & "_" & MisReferences(i) & "_" & ext
            End If

         End If
      Next


      '''save files...in db
      DBCommand = New MySqlCommand("delete from customer_docs where customer_id=" & id, DBConnection)
      DBCommand.ExecuteNonQuery()
      For i As Integer = 0 To MisInc
         DBCommand = New MySqlCommand("insert into customer_docs (customer_id, description, date_created, created_by, last_updated, last_updated_by, doc_ref) values (" & id & ", @description, now(), " & updatedBy & ", now(), " & updatedBy & ", @doc_ref)", DBConnection)
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
            indexingOldFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Customer_Profiles"), id & ".*")
            If indexingOldFileName.Length > 0 Then
               Dim oldExt As String = Path.GetExtension(indexingOldFileName(0))
               If File.Exists(HttpContext.Current.Server.MapPath("~/Customer_Profiles/" & Path.GetFileName(indexingOldFileName(0)))) Then
                  File.Delete(HttpContext.Current.Server.MapPath("~/Customer_Profiles/" & Path.GetFileName(indexingOldFileName(0))))
               End If
            End If

            File.Move(HttpContext.Current.Server.MapPath("~/" & directoryname & "/profPic/" & currentFile), HttpContext.Current.Server.MapPath("~/Customer_Profiles/" & id & ext))
         End If
      End If


      ''signature.....
      'Dim directoryname As String = System.Net.Dns.GetHostName.ToLower & "matar2xxx"
      If Directory.Exists(HttpContext.Current.Server.MapPath("~/" & directoryname & "/sig")) Then
         Dim indexingFileName() As String
         indexingFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/" & directoryname & "/sig"))
         If indexingFileName.Length > 0 Then
            Dim ext As String = Path.GetExtension(indexingFileName(0))
            Dim currentFile As String = Path.GetFileName(indexingFileName(0))
            Dim indexingOldFileName() As String
            indexingOldFileName = Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Customer_Sigs"), id & ".*")
            If indexingOldFileName.Length > 0 Then
               Dim oldExt As String = Path.GetExtension(indexingOldFileName(0))
               If File.Exists(HttpContext.Current.Server.MapPath("~/Customer_Sigs/" & Path.GetFileName(indexingOldFileName(0)))) Then
                  File.Delete(HttpContext.Current.Server.MapPath("~/Customer_Sigs/" & Path.GetFileName(indexingOldFileName(0))))
               End If
            End If

            File.Move(HttpContext.Current.Server.MapPath("~/" & directoryname & "/sig/" & currentFile), HttpContext.Current.Server.MapPath("~/Customer_Sigs/" & id & ext))
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


End Class
