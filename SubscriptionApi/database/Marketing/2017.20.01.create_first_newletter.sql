declare @id uniqueidentifier = '755302af-6569-40e2-a49a-74f7882d68c6'

IF NOT EXISTS(select * from marketing..Newsletters WHERE id = @id)
BEGIN	
    Insert into marketing..Newsletters (Id, Name, Start, [End])
	Values('755302af-6569-40e2-a49a-74f7882d68c6',
            'Sport Challenge',
			'01-01-2017',
			'01-01-2017')                
END