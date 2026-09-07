-- Real Property System recovery patch: proc_cashinflows
--
-- Evidence recovered from the application:
--   input parameter: vdateasof (DateTime)
--   result columns: asof, description, amount
--   report dataset:  dscashinflows
--
-- The original procedure body was not present in the supplied binaries or SQL
-- dumps. This is a SOURCE-DERIVED RECONSTRUCTION, not an exact recovery. It
-- reports active payment-detail rows through the requested as-of date using
-- the payments/paymentdetails schema preserved in Installer\realtydb.sql.
--
-- The join and output columns are confirmed by recovered code. The inclusive
-- "as of" date rule and active-record filter are documented business-rule
-- assumptions; validate them against a known historical report if one exists.
--
-- Run this script as MySQL root in MySQL Workbench after importing both dumps.

USE realtydb;

DROP PROCEDURE IF EXISTS proc_cashinflows;

DELIMITER $$

CREATE PROCEDURE proc_cashinflows(IN vdateasof DATETIME)
BEGIN
  SELECT
    payments.dateofpayment AS asof,
    paymentdetails.description AS description,
    paymentdetails.amount AS amount
  FROM payments
  INNER JOIN paymentdetails
    ON paymentdetails.idpayment = payments.id
  WHERE payments.dateofpayment IS NOT NULL
    AND payments.dateofpayment < DATE_ADD(DATE(vdateasof), INTERVAL 1 DAY)
    AND COALESCE(payments.recordstatus, 'active') = 'active'
  ORDER BY payments.dateofpayment, paymentdetails.id;
END$$

DELIMITER ;

-- Verify the report contract.
CALL proc_cashinflows('2020-11-21');
