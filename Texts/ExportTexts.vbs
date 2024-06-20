Sub ExportTexts

	Dim application
	
	Dim fso

	Set fso = CreateObject("Scripting.FileSystemObject")

	Set application = CreateObject("Excel.Application")

	application.Workbooks.Open fso.GetAbsolutePathName(".") & "\Texts.xlsm"

	application.Run "ExportCSV.ExportCSV", fso.GetAbsolutePathName(".")  & "\..\Assets\Texts\Texts.csv"

	application.Quit

	Set application = Nothing 
	
	Set fso = Nothing

End Sub

ExportTexts