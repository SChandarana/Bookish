Use [LibraryDB]

IF OBJECT_ID('Loans') IS NOT NULL
DROP TABLE Loans;

IF OBJECT_ID('LibraryBooks') IS NOT NULL
DROP TABLE LibraryBooks;

IF OBJECT_ID('Users') IS NOT NULL
DROP TABLE Users;

IF OBJECT_ID('Books') IS NOT NULL
DROP TABLE Books;



CREATE TABLE Users
(
	userId INT NOT NULL IDENTITY PRIMARY KEY,
	username VARCHAR(32) NOT NULL,
);

CREATE TABLE Books
(
	isbn VARCHAR(20) NOT NULL PRIMARY KEY,
	title VARCHAR(255) NOT NULL,
	authors VARCHAR(255) NOT NULL,
);

CREATE TABLE LibraryBooks
(
	bookId INT NOT NULL IDENTITY PRIMARY KEY,
	isbn VARCHAR(20) NOT NULL FOREIGN KEY REFERENCES Books(isbn),
);

CREATE TABLE Loans
(
	userId INT NOT NULL FOREIGN KEY REFERENCES Users(userId),
	bookId INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES LibraryBooks(bookId),
	dueDate DATE NOT NULL,
);
