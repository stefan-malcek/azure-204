EXECUTE sp_set_database_firewall_rule N'Allow appServer database level rule', '157.56.162.80', '157.56.162.80';
GO
EXECUTE sp_delete_database_firewall_rule N'Allow appServer database level rule';
GO
CREATE USER ApplicationUser WITH PASSWORD = 'YourStrongPassword1';
GO
ALTER ROLE db_datareader ADD MEMBER ApplicationUser;
ALTER ROLE db_datawriter ADD MEMBER ApplicationUser;
GO
DENY SELECT ON SalesLT.Address TO ApplicationUser;
GO

SELECT FirstName, LastName, EmailAddress, Phone FROM SalesLT.Customer;
GO
SELECT * FROM SalesLT.Address;
GO