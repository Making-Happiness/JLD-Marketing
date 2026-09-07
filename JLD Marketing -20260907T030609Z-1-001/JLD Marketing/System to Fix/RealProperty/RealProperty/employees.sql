/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50529
Source Host           : 127.0.0.1:3306
Source Database       : realtydb

Target Server Type    : MYSQL
Target Server Version : 50529
File Encoding         : 65001

Date: 2023-01-16 14:34:56
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for employees
-- ----------------------------
DROP TABLE IF EXISTS `employees`;
CREATE TABLE `employees` (
  `idemployee` int(11) NOT NULL AUTO_INCREMENT,
  `firstname` varchar(45) NOT NULL,
  `lastname` varchar(45) NOT NULL,
  `middlename` varchar(45) DEFAULT NULL,
  `gender` varchar(45) DEFAULT NULL,
  `dateofbirth` datetime NOT NULL,
  `salary` decimal(10,2) DEFAULT NULL,
  `designation` varchar(255) DEFAULT '',
  `civilstatus` varchar(45) DEFAULT '',
  `contactno` varchar(15) DEFAULT '',
  `jobcategory` varchar(45) DEFAULT NULL,
  `recordstatus` varchar(45) DEFAULT 'active',
  `remarks` varchar(255) DEFAULT '',
  PRIMARY KEY (`idemployee`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of employees
-- ----------------------------
INSERT INTO `employees` VALUES ('1', 'Zius', 'Apresto', 'Decena', 'Male', '1981-05-27 00:00:00', '0.00', 'AREA MANAGER', 'Married', '09264426828', '', 'active', 'sample');

-- ----------------------------
-- Table structure for jobcategory
-- ----------------------------
DROP TABLE IF EXISTS `jobcategory`;
CREATE TABLE `jobcategory` (
  `category` varchar(45) NOT NULL,
  UNIQUE KEY `idxcategory` (`category`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of jobcategory
-- ----------------------------
INSERT INTO `jobcategory` VALUES ('DRIVER OPERATOR');
INSERT INTO `jobcategory` VALUES ('HOTEL STAFF');
INSERT INTO `jobcategory` VALUES ('J-LD STAFF');
INSERT INTO `jobcategory` VALUES ('MADAM GIRLS');
INSERT INTO `jobcategory` VALUES ('PLANTERS');
INSERT INTO `jobcategory` VALUES ('PURCHASER');
INSERT INTO `jobcategory` VALUES ('STRONGMEN');

-- ----------------------------
-- Table structure for jobdesignation
-- ----------------------------
DROP TABLE IF EXISTS `jobdesignation`;
CREATE TABLE `jobdesignation` (
  `designation` varchar(45) NOT NULL,
  PRIMARY KEY (`designation`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of jobdesignation
-- ----------------------------
INSERT INTO `jobdesignation` VALUES ('ACCOUNT STAFF');
INSERT INTO `jobdesignation` VALUES ('AREA MANAGER');
INSERT INTO `jobdesignation` VALUES ('CARPENTER');
INSERT INTO `jobdesignation` VALUES ('COOK/HOUSEKEEPER');
INSERT INTO `jobdesignation` VALUES ('DRIVER');
INSERT INTO `jobdesignation` VALUES ('FRONT DESK/RECEPTIONIST');
INSERT INTO `jobdesignation` VALUES ('LANDSCAPER');
INSERT INTO `jobdesignation` VALUES ('OPERATOR');
INSERT INTO `jobdesignation` VALUES ('PURCHASER');
INSERT INTO `jobdesignation` VALUES ('SITE TRIPPER');
INSERT INTO `jobdesignation` VALUES ('SUPERVISOR');
INSERT INTO `jobdesignation` VALUES ('WORKING STUDENT');
