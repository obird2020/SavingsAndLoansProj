<%@ Page Title="Payment Durations" Language="vb" AutoEventWireup="false" MasterPageFile="~/ndl.Master" CodeBehind="PaymentPeriods.aspx.vb" Inherits="ndl.PaymentPeriods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style type="text/css">
      .style1
      {
         width: 153px;
      }
      .style2
      {
         width: 332px;
      }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div id="pg">
   <h3>LOAN PAYMENT DURATIONS</h3>

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
               <td class="style1">
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
               <td class="style1">
                  Interest Rate*</td>
               <td class="style2">
                  <asp:TextBox ID="txtInterestRate" runat="server" CssClass="myText"></asp:TextBox>
               &nbsp;</td>
               <td  class="myError">
                  <asp:CustomValidator ID="cvtxtDescription2" runat="server" 
                     ControlToValidate="txtInterestRate" Display="Dynamic" 
                     ErrorMessage="Enter a valid interest rate" OnServerValidate="validateinterest" 
                     ValidateEmptyText="true" />
               </td>
            </tr>
            <tr>
               <td class="style1">
                  Payment Days*</td>
               <td class="style2">
                  <asp:TextBox ID="txtPaymentDays" runat="server" CssClass="myText"></asp:TextBox>
               </td>
               <td  class="myError">
                  <asp:CustomValidator ID="cvtxtDescription3" runat="server" 
                     ControlToValidate="txtPaymentDays" Display="Dynamic" 
                     ErrorMessage="Enter a valid payment days" OnServerValidate="validatedays" 
                     ValidateEmptyText="true" />
               </td>
            </tr>
            <tr>
               <td class="style1">
                  Payment Months*</td>
               <td class="style2">
                  <asp:TextBox ID="txtPaymentMonths" runat="server" CssClass="myText"></asp:TextBox>
               </td>
               <td  class="myError">
                  <asp:CustomValidator ID="cvtxtDescription4" runat="server" 
                     ControlToValidate="txtPaymentMonths" Display="Dynamic" 
                     ErrorMessage="Enter a valid payment months" OnServerValidate="validatemonths" 
                     ValidateEmptyText="false" />
               </td>
            </tr>
            <tr>
               <td class="style1">
                  &nbsp;</td>
               <td class="style2">
                  &nbsp;<asp:CheckBox ID="chkInactive" runat="server" Text="Inactive" CausesValidation="false"/>
               </td>
               <td  class="myError">
                  &nbsp;</td>
            </tr>
            <tr>
               <td class="style1">
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

         <asp:BoundField DataField="SN" HeaderStyle-HorizontalAlign="Left" HeaderText="SN">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="40px" />
         </asp:BoundField>

         <asp:BoundField DataField="ID" HeaderStyle-HorizontalAlign="Left" HeaderText="ID">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="30px" />
         </asp:BoundField>

         <asp:BoundField DataField="Code" HeaderStyle-HorizontalAlign="Left" HeaderText="Code">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="120px" />
         </asp:BoundField>

         <asp:BoundField DataField="description" HeaderStyle-HorizontalAlign="Left" HeaderText="Description">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="200px" />
         </asp:BoundField>

         <asp:BoundField DataField="interest_rate" HeaderStyle-HorizontalAlign="Left" HeaderText="Interest Rate">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="150px" />
         </asp:BoundField>

         <asp:BoundField DataField="payment_months" HeaderStyle-HorizontalAlign="Left" HeaderText="Payment Months">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="150px" />
         </asp:BoundField>

         <asp:BoundField DataField="payment_days" HeaderStyle-HorizontalAlign="Left" HeaderText="Payment Days">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Width="150px" />
         </asp:BoundField>
        
         <asp:BoundField DataField="Status" HeaderStyle-HorizontalAlign="Left" HeaderText="Status">
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