--
-- Create user and privileges
--
USE bugreport;
CREATE USER 'botuser'@'%' IDENTIFIED BY 'unicorn';
GRANT ALL PRIVILEGES ON * . * TO 'botuser'@'%';
commit;
