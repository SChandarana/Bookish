INSERT INTO dbo.Books(isbn, title, authors)
VALUES
	('978-1407104027', 'Invisible City', 'MG Harris'),
	('978-1406360202', 'Point Blanc', 'Anthony Horowitz'),
	('978-0747532743', 'Harry Potter and the Philosopher''s Stone', 'J. K. Rowling'),
	('978-0141346809', 'Percy Jackson and the Lightning Thief', 'Rick Riordan');

SET IDENTITY_INSERT dbo.LibraryBooks ON
INSERT INTO dbo.LibraryBooks(bookId, isbn)
VALUES
	(1, '978-1407104027'),
	(2, '978-1407104027'),
	(3, '978-1407104027'),
	(4, '978-1406360202'),
	(5, '978-0747532743'),
	(6, '978-0141346809');
SET IDENTITY_INSERT dbo.LibraryBooks OFF

INSERT INTO dbo.Loans(userId, bookId, dueDate)
VALUES
	('13711e1b-cd06-4007-b0ae-dd4130457932', 1, '2020-10-28')