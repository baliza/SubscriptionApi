declare @id uniqueidentifier = 'BADC86EE-741E-4C55-8D54-85AF5C0EA8B4'

IF NOT EXISTS(select * from marketing.[dbo].[Subscriptions] WHERE id = @id)
BEGIN
	
    Insert into marketing..[Subscriptions] (Id, Gender, FirstName, Email, DateOfBirth, AllowConsentForMarketing, NewsletterId)
	Values(@id,'M', 'Eduardo Salvador','eduardo@challenge.com', '01-01-1995',0,'755302af-6569-40e2-a49a-74f7882d68c6')
END