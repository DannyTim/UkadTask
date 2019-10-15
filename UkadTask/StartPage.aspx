<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="UkadTask.StartPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <form method="post" action="/Controllers/DiagnosticsController.cs">
                <table>
                    <tr><td><p>Enter the URL:</p></td>
                        <td><input type="text" name="URL" /> </td></tr>
                    <tr><td><input type="submit" value="Submit" /> </td>
                        <td></td></tr>
                </table>
            </form>
        </div>
    </form>
</body>
</html>
