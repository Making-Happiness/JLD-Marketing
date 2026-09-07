-- Run this as the MySQL root administrator in MySQL Workbench.
-- It repairs error 1045 for the recovered MySql.Data 6.5.4 application.

CREATE DATABASE IF NOT EXISTS realtydb
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

CREATE USER IF NOT EXISTS 'realproperty_app'@'localhost'
  IDENTIFIED WITH mysql_native_password BY 'RpsLocal_2026-ChangeMe!';

CREATE USER IF NOT EXISTS 'realproperty_app'@'127.0.0.1'
  IDENTIFIED WITH mysql_native_password BY 'RpsLocal_2026-ChangeMe!';

ALTER USER 'realproperty_app'@'localhost'
  IDENTIFIED WITH mysql_native_password BY 'RpsLocal_2026-ChangeMe!';

ALTER USER 'realproperty_app'@'127.0.0.1'
  IDENTIFIED WITH mysql_native_password BY 'RpsLocal_2026-ChangeMe!';

GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE
  ON realtydb.* TO 'realproperty_app'@'localhost';

GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE
  ON realtydb.* TO 'realproperty_app'@'127.0.0.1';

FLUSH PRIVILEGES;

SELECT user, host, plugin
FROM mysql.user
WHERE user = 'realproperty_app';
