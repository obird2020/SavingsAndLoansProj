<%@ Page Title="Parameter Fields" Language="vb" AutoEventWireup="false" MasterPageFile="~/ndl.Master" CodeBehind="ParamFields.aspx.vb" Inherits="ndl.ParamFields" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="pg">
   <h3>PARAMETERS</h3>

   <asp:GridView ID="dgRetention" runat="server" 
      AllowSorting="True" AutoGenerateColumns="False" CssClass="myGrid" OnRowCommand="dgRegions_RowCommand"
       PageSize="20">
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
         <ItemStyle Width="200" />
         </asp:BoundField>

         <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Description">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="300px" />
         </asp:BoundField>
        
        
         <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Status">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="120px" />
         </asp:BoundField>

         <asp:TemplateField HeaderText="">
            <ItemTemplate>
               <asp:LinkButton ID="editCmd" runat="server" CausesValidation="false" commandName="editCmd" CssClass="gridUpdate">Fields</asp:LinkButton>
            </ItemTemplate>
         </asp:TemplateField>
      </Columns>
      <HeaderStyle CssClass="gridHeader" />
   </asp:GridView>

   <br />
   <h3><asp:Label ID="lblBranch" runat="server" Visible="False"></asp:Label></h3>

   <asp:GridView ID="dgBranches" runat="server" OnRowCommand="dgRegions_RowCommand"
      AllowSorting="True" AutoGenerateColumns="False" CssClass="myGrid" 
       PageSize="20" Visible="False">
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

         <%--<asp:BoundField DataField="Code" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Code">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="200" />
         </asp:BoundField>--%>

         <asp:BoundField DataField="Description" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Description">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="300px" />
         </asp:BoundField>
        
        
         <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" 
            HeaderText="Status">
         <HeaderStyle HorizontalAlign="Left" />
         <ItemStyle Width="120px" />
         </asp:BoundField>

         <asp:TemplateField HeaderText="">
            <ItemTemplate>
               <asp:LinkButton ID="editCmdBranch" runat="server" CausesValidation="false" 
                  commandName="editCmdBranch" CssClass="gridUpdate">Edit</asp:LinkButton>
            </ItemTemplate>
         </asp:TemplateField>
      </Columns>
      <HeaderStyle CssClass="gridHeader" />
   </asp:GridView>

   <asp:LinkButton ID="lnkNew" runat="server" Visible="False" CssClass="myLinks">LinkButton</asp:LinkButton>
&nbsp;<asp:HiddenField ID="hdParamID" runat="server" Value="0" />
   <asp:HiddenField ID="hdFieldID" runat="server" Value="0" />
   <br />
   <br />


   <asp:Panel ID="Panel1" runat="server" Visible="False">

      <div class="formArea">
   <table>
            <tr>
               <td class="style1">
                  &nbsp;</td>
               <td style="width: 424px">
                  &nbsp;</td>
               <td  class="myError">
                  &nbsp;</td>
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

   </asp:Panel>



</div>


</asp:Content>
