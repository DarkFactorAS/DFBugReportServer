--
-- Create user and privileges
--
drop user 'buguser'@'%';
flush privileges;
CREATE USER 'buguser'@'%' IDENTIFIED BY 'unicorn';
GRANT ALL PRIVILEGES ON * . * TO 'buguser'@'%' IDENTIFIED BY 'unicorn';
GRANT CREATE ON *.* TO buguser@'%' IDENTIFIED BY 'unicorn';
commit;
flush privileges;
