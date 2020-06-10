Option Strict On
Imports Microsoft.VisualBasic
Imports System.Net.Mail
Namespace Security
	Public Class Mailer
		Private server As String = "localhost"
		''' <summary>
		''' Send Email notifying user that Account has been set up.
		''' </summary>
		''' <param name="myUser"></param>
		''' <param name="newpassword"></param>
		''' <param name="isNewUser"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function sendNewPassword(ByVal myUser As Security.UserBase, ByVal newpassword As String, _
		   ByVal isNewUser As Boolean, ByVal userWhoAdded As Security.UserBase, ByVal sConfig As SiteConfigBase, _
		   Optional ByVal customMessage As String = Nothing) As Integer


			'Dim myToAddress As New MailAddress(myUser.UserName, myUser.FullName)
			'Dim myFromAddress As New MailAddress(sConfig.AdministratorEmailAddress, sConfig.Title & " Administrator")
			'Dim myMessage As New MailMessage
			Dim subject, body As String

			If isNewUser Then
				subject = "New Account Request"
				body = myUser.FirstName & "," & vbCrLf _
	 & vbCrLf _
	 & "You have been added to the " & sConfig.Title & "." & vbCrLf _
	 & "Your login username is:" & vbCrLf _
	 & myUser.UserName & vbCrLf _
	 & "Your new password is:" & vbCrLf _
	 & newpassword & vbCrLf _
	 & vbCrLf _
	 & "You may log in and change you password at " _
	 & sConfig.SiteAddress & "/"

				If customMessage IsNot Nothing Then
                    body &= "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" & vbCrLf _
                 & "Message From: " & userWhoAdded.OfficialName & vbCrLf _
                 & "------------------------------------------------------------" & vbCrLf _
                 & customMessage & vbCrLf _
                 & "++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++"
				End If
				body &= vbCrLf _
	 & vbCrLf _
	 & "Sincerely," & vbCrLf _
	  & sConfig.Title & " Administrator" & vbCrLf _
	  & sConfig.AdministratorEmailAddress
			Else
				subject = "Password Change Request"
				body = "A password change for the " & sConfig.Title & " has been requested"

				If userWhoAdded IsNot Nothing AndAlso myUser.ID <> userWhoAdded.ID Then	' requested by other than user
                    body &= " by " & userWhoAdded.OfficialName
				End If

				body &= "." & vbCrLf _
	 & "Your new password is:" & vbCrLf _
	 & newpassword & vbCrLf _
	 & vbCrLf _
	 & "You may log in and change you password at " _
	 & sConfig.SiteAddress & "/"

				'If Not myUser.Role.HasCreatorRights Then myMessage.Body &= "Public"

				body &= "Settings.aspx" & vbCrLf _
	 & vbCrLf _
	 & "Thank you," & vbCrLf _
	 & sConfig.Title & " Administrator" & vbCrLf _
	 & sConfig.AdministratorEmailAddress

			End If
			'Try
			'	smtpC.Send(myMessage)
			'	successAnswer = 1
			'Catch e As Exception
			'	successAnswer = -1
			'End Try
			Return sendEmail(myUser, subject, body, sConfig) 'successAnswer
		End Function

		Private Function sendEmail(ByVal toUser As Security.UserBase, ByVal subject As String, _
	 ByVal message As String, ByVal sConfig As SiteConfigBase) As Integer
			Dim successAnswer As Integer = 0
			'Dim smtpC As New Net.Mail.SmtpClient
			'smtpC.Host = server
			'smtpC.Port = 25
			Dim toaddress As String = toUser.Email
			If SiteConfigBase.VersionClass = SiteConfigBase.verClass.dev Then toaddress = sConfig.AdministratorEmailAddress
            Dim myToAddress As New Net.Mail.MailAddress(toUser.Email, toUser.OfficialName)
			Dim myFromAddress As New Net.Mail.MailAddress(sConfig.AdministratorEmailAddress, sConfig.Title & " Administrator")
			Dim myMessage As New Net.Mail.MailMessage
			myMessage.To.Add(myToAddress)
			myMessage.From = myFromAddress
			'            myMessage.IsBodyHtml = True
			myMessage.Subject = subject
			myMessage.Body = message & vbCrLf _
			  & vbCrLf _
			  & adminSig(sConfig)

			Try
				If Security.SiteConfigBase.TestingSite Then
					Throw New Exception("The site is being tested.")
				End If
				Dim netCredentials As New System.Net.NetworkCredential("", "6Ab=?revap$2QE!") 'eEncryptor.Decrypt(sConfig.EmailServerPassword))

				'smtpC.UseDefaultCredentials = False
				'smtpC.Credentials = CType(netCredentials.GetCredential(sConfig.EmailServerAddress, sConfig.EmailServerSMTPPort, "NTLM"), System.Net.ICredentialsByHost)
				Dim mynewsmtp As New Net.Mail.SmtpClient(sConfig.Email_ServerAddress)
				mynewsmtp.UseDefaultCredentials = False
				mynewsmtp.Port = 25
				mynewsmtp.Credentials = New Net.NetworkCredential("", "6Ab=?revap$2QE!")
				'mynewsmtp.Credentials = CType(netCredentials.GetCredential(sConfig.EmailServerAddress, sConfig.EmailServerSMTPPort, "Basic"), System.Net.ICredentialsByHost)
				mynewsmtp.Send(myMessage)
				'smtpC.Send(myMessage)

				successAnswer = 1
			Catch e As Exception
				successAnswer = -1
			End Try
			Return successAnswer
		End Function
		Private Function adminSig(ByVal sConfig As SiteConfigBase) As String
			Return "Sincerely," & vbCrLf _
			 & sConfig.Title & " Administrator " & vbCrLf _
			 & sConfig.AdministratorEmailAddress
		End Function

	End Class

End Namespace
