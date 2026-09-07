-- Schema completion script for realtydb
USE realtydb;

-- 1. Update employees table with mode_of_salary
ALTER TABLE employees ADD COLUMN IF NOT EXISTS mode_of_salary INT(11) DEFAULT 1;

-- 2. Create missing tables
CREATE TABLE IF NOT EXISTS `claims` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idagent` int(11) NOT NULL,
  `claimby` varchar(127) DEFAULT NULL,
  `dateofclaim` datetime DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT '0.00',
  `remarks` varchar(255) DEFAULT '',
  `recordstatus` varchar(45) DEFAULT 'active',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `loans` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dateapplied` datetime DEFAULT NULL,
  `description` varchar(255) DEFAULT '',
  `amount` decimal(10,2) DEFAULT '0.00',
  `category` varchar(45) DEFAULT 'CASH ADVANCE',
  `penalty` decimal(10,2) DEFAULT '0.00',
  `duedate` datetime DEFAULT NULL,
  `amortization` decimal(10,2) DEFAULT '0.00',
  `remarks` varchar(255) DEFAULT '',
  `recordstatus` varchar(45) DEFAULT 'active',
  `idemployee` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `payrolls` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) DEFAULT '',
  `datefrom` datetime DEFAULT NULL,
  `dateto` datetime DEFAULT NULL,
  `recordstatus` varchar(45) DEFAULT 'active',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `payrolldeduction` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `datepaid` datetime DEFAULT NULL,
  `description` varchar(255) DEFAULT '',
  `amount` decimal(10,2) DEFAULT '0.00',
  `category` varchar(45) DEFAULT '',
  `remarks` varchar(255) DEFAULT '',
  `recordstatus` varchar(45) DEFAULT 'active',
  `idemployee` int(11) NOT NULL,
  `idpayroll` int(11) NOT NULL,
  `idloan` int(11) DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `payrolldetails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idemployee` int(11) NOT NULL,
  `idpayroll` int(11) NOT NULL,
  `sun` decimal(10,2) DEFAULT '0.00',
  `sat` decimal(10,2) DEFAULT '0.00',
  `mon` decimal(10,2) DEFAULT '0.00',
  `tue` decimal(10,2) DEFAULT '0.00',
  `wed` decimal(10,2) DEFAULT '0.00',
  `thu` decimal(10,2) DEFAULT '0.00',
  `fri` decimal(10,2) DEFAULT '0.00',
  `ca` decimal(10,2) DEFAULT '0.00',
  `merienda` decimal(10,2) DEFAULT '0.00',
  `egg` decimal(10,2) DEFAULT '0.00',
  `others` decimal(10,2) DEFAULT '0.00',
  `undertime` decimal(10,2) DEFAULT '0.00',
  `rice` decimal(10,2) DEFAULT '0.00',
  `emergencyfund` decimal(10,2) DEFAULT '0.00',
  `recordstatus` varchar(45) DEFAULT 'active',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `payrollsettings` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(255) DEFAULT '',
  `amount` decimal(10,2) DEFAULT '0.00',
  `category` varchar(45) DEFAULT 'ALL',
  `remarks` varchar(255) DEFAULT '',
  `recordstatus` varchar(45) DEFAULT 'active',
  `daterecorded` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `payslips` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) DEFAULT '',
  `payslip_type` varchar(45) DEFAULT 'MASTER PAYSLIP',
  `mode_of_posting` varchar(45) DEFAULT 'Auto',
  `for_month_of` varchar(45) DEFAULT '',
  `dategenerated` datetime DEFAULT NULL,
  `remarks` varchar(255) DEFAULT '',
  `recordstatus` varchar(45) DEFAULT 'new',
  `datefinalized` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE IF NOT EXISTS `payslipdetails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `idemployee` int(11) NOT NULL,
  `idpayslip` int(11) NOT NULL,
  `title` varchar(255) DEFAULT '',
  `amount` decimal(10,2) DEFAULT '0.00',
  `category` varchar(45) DEFAULT 'OTHER DEDUCTION',
  `mode` varchar(45) DEFAULT 'FIXED',
  `datefrom` datetime DEFAULT NULL,
  `dateto` datetime DEFAULT NULL,
  `remarks` varchar(255) DEFAULT '',
  `recordstatus` varchar(45) DEFAULT 'active',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 3. Create missing views
CREATE OR REPLACE VIEW `view_employeeswithfullname` AS
SELECT
  employees.*,
  CONCAT(employees.lastname, ', ', employees.firstname, ' ', IF(ISNULL(employees.middlename), '', employees.middlename)) AS fullname
FROM employees;

CREATE OR REPLACE VIEW `view_employee_salary` AS
SELECT
  employees.*,
  CONCAT(employees.lastname, ', ', employees.firstname, ' ', IF(ISNULL(employees.middlename), '', employees.middlename)) AS fullname
FROM employees;

CREATE OR REPLACE VIEW `view_category_set` AS
SELECT DISTINCT category FROM payrollsettings;

CREATE OR REPLACE VIEW `view_payrollsettingscookies` AS
SELECT description, category, amount FROM payrollsettings WHERE recordstatus = 'active';

CREATE OR REPLACE VIEW `view_loans` AS
SELECT * FROM loans;

CREATE OR REPLACE VIEW `view_validloans` AS
SELECT
  loans.id AS id,
  loans.id AS idloan,
  loans.idemployee,
  loans.dateapplied,
  loans.description,
  loans.amount,
  loans.category,
  loans.penalty,
  loans.duedate,
  loans.amortization,
  loans.remarks,
  loans.recordstatus,
  (loans.amount + COALESCE(loans.penalty, 0) - COALESCE((SELECT SUM(pd.amount) FROM payrolldeduction pd WHERE pd.idloan = loans.id AND pd.recordstatus = 'active'), 0)) AS balance
FROM loans
WHERE loans.recordstatus = 'active';

CREATE OR REPLACE VIEW `view_payrolldeduction_withbalance` AS
SELECT
  payrolldeduction.*,
  COALESCE((SELECT vl.balance FROM view_validloans vl WHERE vl.id = payrolldeduction.idloan), 0) AS balance
FROM payrolldeduction;

CREATE OR REPLACE VIEW `view_payrolldetailswithemployee` AS
SELECT
  payrolldetails.*,
  CONCAT(employees.lastname, ', ', employees.firstname, ' ', IF(ISNULL(employees.middlename), '', employees.middlename)) AS fullname,
  employees.salary AS daily_rate
FROM payrolldetails
INNER JOIN employees ON payrolldetails.idemployee = employees.idemployee;

CREATE OR REPLACE VIEW `view_releasedclaims` AS
SELECT
  claims.id,
  claims.idagent,
  dicers.fullname,
  claims.dateofclaim,
  claims.description,
  claims.claimby,
  claims.remarks,
  claims.amount,
  claims.recordstatus
FROM claims
INNER JOIN dicers ON claims.idagent = dicers.id;

CREATE OR REPLACE VIEW `view_paidbyclient_dice` AS
SELECT
  purchasedetails.id,
  CONCAT(clients.lastname, ', ', clients.firstname, ' (', products.code, ' lot-', purchasedetails.lotno, ')') AS description,
  purchasedetails.lotprice,
  COALESCE((SELECT SUM(paymentdetails.amount) FROM paymentdetails WHERE paymentdetails.idpurchasedetails = purchasedetails.id), 0) AS amount,
  purchasedetails.idagent,
  purchasedetails.agentpercentage,
  0.00 AS otherfees,
  0.00 AS penalty,
  (COALESCE((SELECT SUM(paymentdetails.amount) FROM paymentdetails WHERE paymentdetails.idpurchasedetails = purchasedetails.id), 0) * (purchasedetails.agentpercentage / 100)) AS claim_amount,
  (purchasedetails.lotprice * (purchasedetails.agentpercentage / 100)) AS estimated_claims
FROM purchasedetails
INNER JOIN clients ON purchasedetails.idclients = clients.idclients
INNER JOIN products ON purchasedetails.idproducts = products.idproduct;

CREATE OR REPLACE VIEW `view_paymenthistory` AS
SELECT
  paymentdetails.id,
  paymentdetails.idpurchasedetails,
  payments.paidby AS idclients,
  payments.dateofpayment,
  payments.id AS idpayment,
  paymentdetails.description,
  paymentdetails.amount,
  payments.paymenttype
FROM paymentdetails
INNER JOIN payments ON paymentdetails.idpayment = payments.id;

CREATE OR REPLACE VIEW `rpt_soa` AS
SELECT
  clients.idclients,
  clients.firstname,
  clients.lastname,
  clients.middlename,
  clients.gender,
  clients.dateofbirth,
  clients.placeofbirth,
  clients.spousename,
  clients.contactno,
  purchasedetails.id,
  purchasedetails.blockno,
  purchasedetails.lotno,
  purchasedetails.area,
  purchasedetails.lotprice,
  purchasedetails.amortization,
  purchasedetails.terms,
  purchasedetails.idagent,
  purchasedetails.agentpercentage,
  purchasedetails.idproducts,
  0.00 AS otherfees,
  0.00 AS penalty,
  purchasedetails.remarks,
  products.location,
  NOW() AS duedate
FROM purchasedetails
INNER JOIN clients ON purchasedetails.idclients = clients.idclients
INNER JOIN products ON purchasedetails.idproducts = products.idproduct;

CREATE OR REPLACE VIEW `rpt_soadetails` AS
SELECT
  paymentdetails.id,
  paymentdetails.idpurchasedetails,
  payments.paidby AS idclients,
  payments.dateofpayment,
  paymentdetails.description,
  paymentdetails.amount,
  payments.paymenttype,
  payments.orderreceipt
FROM paymentdetails
INNER JOIN payments ON paymentdetails.idpayment = payments.id;

CREATE OR REPLACE VIEW `rpt_payroll` AS
SELECT
  payrolls.title,
  payrolls.datefrom,
  payrolls.dateto,
  payrolls.recordstatus,
  payrolldetails.id,
  payrolldetails.idemployee,
  payrolldetails.idpayroll,
  payrolldetails.sun,
  payrolldetails.sat,
  payrolldetails.mon,
  payrolldetails.tue,
  payrolldetails.wed,
  payrolldetails.thu,
  payrolldetails.fri,
  payrolldetails.merienda,
  payrolldetails.egg,
  payrolldetails.others,
  payrolldetails.undertime,
  payrolldetails.rice,
  payrolldetails.emergencyfund,
  CONCAT(employees.lastname, ', ', employees.firstname, ' ', IF(ISNULL(employees.middlename), '', employees.middlename)) AS fullname,
  employees.salary AS daily_rate,
  0.00 AS amout,
  0.00 AS amortization,
  '' AS category
FROM payrolldetails
INNER JOIN payrolls ON payrolldetails.idpayroll = payrolls.id
INNER JOIN employees ON payrolldetails.idemployee = employees.idemployee;

CREATE OR REPLACE VIEW `view_rpt_payslipdetails` AS
SELECT
  payslipdetails.id,
  payslipdetails.idpayslip,
  payslips.title AS payslip_title,
  payslips.for_month_of,
  payslipdetails.idemployee,
  employees.firstname,
  employees.lastname,
  employees.middlename,
  employees.designation,
  employees.salary,
  employees.jobcategory,
  payslipdetails.title,
  payslipdetails.amount,
  payslipdetails.category,
  payslipdetails.mode,
  payslipdetails.datefrom,
  payslipdetails.dateto,
  payslipdetails.remarks,
  payslipdetails.recordstatus
FROM payslipdetails
INNER JOIN payslips ON payslipdetails.idpayslip = payslips.id
INNER JOIN employees ON payslipdetails.idemployee = employees.idemployee;

-- 4. Create missing procedures
DROP PROCEDURE IF EXISTS proc_deletepaymentdetails;
DELIMITER $$
CREATE PROCEDURE proc_deletepaymentdetails(IN vidpayments INT, IN vidpaymentsdetails INT)
BEGIN
  DELETE FROM paymentdetails WHERE id = vidpaymentsdetails;
  UPDATE payments SET totalamount = (SELECT COALESCE(SUM(amount), 0) FROM paymentdetails WHERE idpayment = vidpayments) WHERE id = vidpayments;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS proc_delete_purchasedetails;
DELIMITER $$
CREATE PROCEDURE proc_delete_purchasedetails(IN vidpurchasedetails INT)
BEGIN
  DELETE FROM paymentdetails WHERE idpurchasedetails = vidpurchasedetails;
  DELETE FROM purchasedetails WHERE id = vidpurchasedetails;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS proc_generate_payrolldetails;
DELIMITER $$
CREATE PROCEDURE proc_generate_payrolldetails(IN vidpayroll INT)
BEGIN
  INSERT INTO payrolldetails (idemployee, idpayroll, recordstatus)
  SELECT idemployee, vidpayroll, 'active'
  FROM employees
  WHERE recordstatus = 'active'
    AND idemployee NOT IN (SELECT idemployee FROM payrolldetails WHERE idpayroll = vidpayroll);
  SELECT * FROM payrolldetails WHERE idpayroll = vidpayroll;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS proc_generate_payrolldetails_single;
DELIMITER $$
CREATE PROCEDURE proc_generate_payrolldetails_single(IN vidpayroll INT, IN videmployee INT)
BEGIN
  IF NOT EXISTS (SELECT 1 FROM payrolldetails WHERE idpayroll = vidpayroll AND idemployee = videmployee) THEN
    INSERT INTO payrolldetails (idemployee, idpayroll, recordstatus)
    VALUES (videmployee, vidpayroll, 'active');
  END IF;
  SELECT * FROM payrolldetails WHERE idpayroll = vidpayroll AND idemployee = videmployee;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS proc_generate_payslipdetails;
DELIMITER $$
CREATE PROCEDURE proc_generate_payslipdetails(IN vidpayslip INT)
BEGIN
  INSERT INTO payslipdetails (idemployee, idpayslip, title, amount, category, mode, recordstatus)
  SELECT ps.idemployee, vidpayslip, ps.description, ps.amount, ps.category, 'FIXED', 'active'
  FROM (SELECT e.idemployee, s.description, s.amount, s.category
        FROM employees e
        CROSS JOIN payrollsettings s
        WHERE e.recordstatus = 'active' AND s.recordstatus = 'active') ps;
  SELECT * FROM payslipdetails WHERE idpayslip = vidpayslip;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS proc_generate_payslipdetails_single;
DELIMITER $$
CREATE PROCEDURE proc_generate_payslipdetails_single(IN vidpayslip INT, IN videmployee INT)
BEGIN
  INSERT INTO payslipdetails (idemployee, idpayslip, title, amount, category, mode, recordstatus)
  SELECT videmployee, vidpayslip, s.description, s.amount, s.category, 'FIXED', 'active'
  FROM payrollsettings s
  WHERE s.recordstatus = 'active';
  SELECT * FROM payslipdetails WHERE idpayslip = vidpayslip AND idemployee = videmployee;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS proc_add_payrolldeduction;
DELIMITER $$
CREATE PROCEDURE proc_add_payrolldeduction(IN vidpayroll INT, IN vbalance DECIMAL(10,2), IN vidloan INT)
BEGIN
  INSERT INTO payrolldeduction (datepaid, description, amount, category, remarks, recordstatus, idemployee, idpayroll, idloan)
  SELECT NOW(), loans.description, LEAST(loans.amortization, vbalance), loans.category, '', 'active', loans.idemployee, vidpayroll, vidloan
  FROM loans WHERE loans.id = vidloan;
END$$
DELIMITER ;
