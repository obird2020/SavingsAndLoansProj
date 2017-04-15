<%@ Page Title="Tenders" Language="vb" AutoEventWireup="false" MasterPageFile="~/ndl.Master" CodeBehind="Tenders.aspx.vb" Inherits="ndl.Tenders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
      .style2
      {
         width: 346px;
      }
      .style3
      {
         width: 123px;
      }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div id="pg">
   <h3>TENDERS</h3>

   <div class="formArea">
   <table>
            <tr>
               <td class="style3"> &nbsp;</td>
               <td style="height: 21px; " colspan="2" class="mySuccess">
                    <asp:Label ID="lblmsg" Text="" runat="server" /> 
               </td>
            </tr>
            <tr>
               <td class="style3">
                  Code*</td>
               <td class="style2">
                  <asp:TextBox ID="txtCode" runat="server" CssClass="myText"></asp:TextBox>
               </td>
               <td  class="myError">
                  <asp:RequiredFieldValidator ID="vtxtValue0" runat="server" 
                     ControlToValidate ="txtCode" ErrorMessage="Enter Code" />
                  <asp:CustomValidator ID="cvtxtDescription0" runat="server" 
                     ControlToValidate="txtCode" Display="Dynamic" 
                     ErrorMessage="Code already exists" OnServerValidate="validateCode" 
                     ValidateEmptyText="false" />
               </td>
            </tr>
            <tr>
               <td class="style3">
                  Type*</td>
               <td class="style2">
                  <asp:DropDownList ID="cboType" runat="server" CssClass="myText">
                     <asp:ListItem Value="0">- Select -</asp:ListItem>
                     <asp:ListItem>Cash</asp:ListItem>
                     <asp:ListItem>Cheque</asp:ListItem>
                     <asp:ListItem>Deposit</asp:ListItem>
                  </asp:DropDownList>
               </td>
               <td  class="myError">
                  <asp:CustomValidator ID="cvtxtDescription2" runat="server" 
                     ControlToValidate="cboType" Display="Dynamic" 
                     ErrorMessage="Select Type" OnServerValidate="validatedeType" 
                     ValidateEmptyText="false" />
               </td>
            </tr>
            <tr>
               <td class="style3">
                  Description*</td>
               <td class="style2">
                  <asp:TextBox ID="txtDescription" runat="server" CssClass="myText"></asp:TextBox>
               </td>
               <td  class="myError">
                  <asp:RequiredFieldValidator ID="vtxtValue2" runat="server" 
                     ControlToValidate ="txtDescription" ErrorMessage="Enter Description" />
                  <asp:CustomValidator ID="cvtxtDescription1" runat="server" 
                     ControlToValidate="txtDescription" Display="Dynamic" 
                     ErrorMessage="Description already exists" OnServerValidate="validatedescription" 
                     ValidateEmptyText="false" />
               </td>
            </tr>
            <tr>
               <td class="style3">
                  &nbsp;</td>
               <td class="style2">
                  <asp:CheckBox ID="chkInactive" runat="server" Text="Inactive" CausesValidation="false"/>
               </td>
               <td  class="myError">
                  &nbsp;</td>
            </tr>
            <tr>
               <td class="style3">
                  &nbsp;</td>
               <td class="style2">
                  <asp:Button ID="btnSave" runat="server" CssClass="myButton" Text="Save" 
                     onclick="btnSave_Click" />
                  &nbsp;<asp:Button ID="btnReset" runat="server" CssClass="myButton" Text="Reset" 
                     CausesValidation="false" />
               </td>
               <td style="width: 525px">
                  &nbsp;</td>
            </tr>

          </table>
          </div>


   <asp:GridView ID="dgRetention" runat="server" AllowPaging="True" 
      AllowSorting="True" AutoGenerateColumns="False" CssClass="myGrid" 
      OnRowCommand="dgRegions_RowCommand" PageSize="20">
      <RowStyle CssClass="gridRow" />
      <AlternatingRowStyle CssClass="gridAltRow" />
      <HeaderStyle CssClass="gridHeader" />
      <Columns>
         <asp:BoundField DataField="SN" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="SN">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="40px" />
         </asp:BoundField>

         <asp:BoundField DataField="ID" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="ID">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="30px" />
         </asp:BoundField>

         <asp:BoundField DataField="Code" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Code">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="150" />
         </asp:BoundField>

         <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Description">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="200px" />
         </asp:BoundField>

         <asp:BoundField DataField="Type" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Type">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="200px" />
         </asp:BoundField>
        
        
         <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Status">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="120px" />
         </asp:BoundField>
         <asp:TemplateField HeaderText="">
            <ItemTemplate>
               <asp:LinkButton ID="editCmd" runat="server" CausesValidation="false" commandName="editCmd" CssClass="gridUpdate">Edit</asp:LinkButton>
            </ItemTemplate>
         </asp:TemplateField>
      </Columns>
      <HeaderStyle CssClass="gridHeader" />
   </asp:GridView>

</div>


</asp:Content>
