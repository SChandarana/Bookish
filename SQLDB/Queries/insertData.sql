INSERT INTO dbo.Users(username, userPw)
VALUES
	('Shiv', 'ShivPW'),
	('Benji', 'BenjiPW'),
	('Frank', 'FrankPW'),
	('Frida', 'FridaPW');

INSERT INTO dbo.Books(isbn, title, authors)
VALUES
	('978-1407104027', 'Invisible City', 'MG Harris'),
	('978-1406360202', 'Point Blanc', 'Anthony Horowitz'),
	('978-0747532743', 'Harry Potter and the Philosopher''s Stone', 'J. K. Rowling'),
	('978-0141346809', 'Percy Jackson and the Lightning Thief', 'Rick Riordan');

INSERT INTO dbo.LibraryBooks(isbn)
VALUES
	('978-1407104027'),
	('978-1407104027'),
	('978-1407104027'),
	('978-1406360202'),
	('978-0747532743'),
	('978-0141346809');

INSERT INTO dbo.Loans(userId, bookId, dueDate)
VALUES
	(1, 1, '2020-10-28')