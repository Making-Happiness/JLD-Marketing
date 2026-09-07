-- Run this once as a local MySQL/MariaDB administrator after the server is installed.
-- The application itself connects only as realproperty_app, never as root.

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

-- Next, import Installer\realtydb.sql as an administrator into the realtydb database.
-- The recovered dump is incomplete; restore its objects first, then add the missing
-- tables, views, and stored procedures identified by the recovered application code.
