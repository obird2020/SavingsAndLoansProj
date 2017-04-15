<%@ Page Title="Additional Parameters" Language="vb" AutoEventWireup="false" MasterPageFile="~/ndl.Master" CodeBehind="Params.aspx.vb" Inherits="ndl.Params" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
      .style1
      {
         width: 98px;
      }
   </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="pg">
   <h3>ADDITIONAL PARAMETERS</h3>

   <div class="formArea">
   <table>
            <tr>
               <td class="style1"> &nbsp;</td>
               <td style="height: 21px; " colspan="2" class="mySuccess">
                    <asp:Label ID="lblmsg" Text="" runat="server" /> 
               </td>
            </tr>
            <tr>
               <td class="style1">
                  Type*</td>
               <td style="width: 424px">
                  <asp:DropDownList ID="cboType" runat="server" CssClass="myText">
                     <asp:ListItem Value="0">- Select -</asp:ListItem>
                     <asp:ListItem>Business</asp:ListItem>
                     <asp:ListItem>Collateral</asp:ListItem>
                  </asp:DropDownList>
               </td>
               <td class="myError">
                  <asp:CustomValidator ID="CustomValidator1" runat="server" 
                     ControlToValidate="cboType" Display="Dynamic" 
                     ErrorMessage="Select Type" OnServerValidate="validatedeType" 
                     ValidateEmptyText="false" />
               </td>
            </tr>
            <tr>
               <td class="style1">
                  Description*</td>
               <td style="width: 424px">
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
               <td class="style1">
                  &nbsp;</td>
               <td style="width: 424px">
                  <asp:CheckBox ID="chkInactive" runat="server" Text="Inactive" CausesValidation="false"/>
               </td>
               <td  class="myError">
                  &nbsp;</td>
            </tr>
            <tr>
               <td class="style1">
                  &nbsp;</td>
               <td style="width: 424px">
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


   <asp:GridView ID="dgRetention" runat="server" 
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

         <asp:BoundField DataField="type" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Type">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="150" />
         </asp:BoundField>

         <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Description">
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
