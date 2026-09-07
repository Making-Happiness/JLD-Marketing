/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50529
Source Host           : 127.0.0.1:3306
Source Database       : realtydb

Target Server Type    : MYSQL
Target Server Version : 50529
File Encoding         : 65001

Date: 2023-01-09 11:08:05
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for clients
-- ----------------------------
DROP TABLE IF EXISTS `clients`;
CREATE TABLE `clients` (
  `idclients` int(11) NOT NULL AUTO_INCREMENT,
  `firstname` varchar(64) DEFAULT NULL,
  `lastname` varchar(45) DEFAULT NULL,
  `middlename` varchar(45) DEFAULT NULL,
  `gender` varchar(7) DEFAULT NULL,
  `dateofbirth` datetime DEFAULT NULL,
  `placeofbirth` varchar(127) DEFAULT NULL,
  `spousename` varchar(64) DEFAULT NULL,
  `contactno` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`idclients`)
) ENGINE=InnoDB AUTO_INCREMENT=256 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of clients
-- ----------------------------
INSERT INTO `clients` VALUES ('1', 'Yolly Mae N', 'Amable', null, null, null, null, null, '9457898244');
INSERT INTO `clients` VALUES ('2', 'Martin Miguel V', 'Badoria', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('3', 'Queenilyn L', 'Capillo', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('4', 'Anamae (Deita,Cesar)', 'Deita', null, null, null, null, null, '9519822050');
INSERT INTO `clients` VALUES ('5', 'Lea B', 'Morales', null, null, null, null, null, '9072305489');
INSERT INTO `clients` VALUES ('6', 'Jessie', 'Panaligan', null, null, null, null, null, '9127212801');
INSERT INTO `clients` VALUES ('7', 'Dolly', 'Soriano', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('8', 'Jeramae C', 'Trinidad', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('9', 'Marvin', 'Veraque', null, null, null, null, null, '9655984273');
INSERT INTO `clients` VALUES ('10', 'Nilbert', 'Agcan', null, null, null, null, null, '9754094576');
INSERT INTO `clients` VALUES ('11', 'Maymay', 'Felongco', null, null, null, null, null, '9532541327');
INSERT INTO `clients` VALUES ('12', 'Shirley Yap', 'Frias', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('13', 'Felma L.', 'Funtecha', null, null, null, null, null, '9650381612');
INSERT INTO `clients` VALUES ('14', 'Jenelyn D', 'Jamerlarin', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('15', 'Edison', 'Layos', null, null, null, null, null, '9655189938');
INSERT INTO `clients` VALUES ('16', 'Ailen Carroll', 'Ortiz', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('17', 'Roel F.', 'Rapista', null, null, null, null, null, '9617346757');
INSERT INTO `clients` VALUES ('18', 'Marvie C', 'Sevilla', null, null, null, null, null, '9063200939');
INSERT INTO `clients` VALUES ('19', 'Dennis L.', 'Agtoto', null, null, null, null, null, '9473794562');
INSERT INTO `clients` VALUES ('20', 'Gregy', 'Aguilar', null, null, null, null, null, '9233695606');
INSERT INTO `clients` VALUES ('21', 'Cherryl', 'Arcega', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('22', 'Michelle C.', 'Arcega', null, null, null, null, null, '9128850036');
INSERT INTO `clients` VALUES ('23', 'Rodelyn B', 'Arcenas', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('24', 'Grace D.', 'Atuel', null, null, null, null, null, '9179791042');
INSERT INTO `clients` VALUES ('25', 'Josephine', 'Badjan', null, null, null, null, null, '9487738170');
INSERT INTO `clients` VALUES ('26', 'Johnson S.', 'Bahandi', null, null, null, null, null, '9175906762');
INSERT INTO `clients` VALUES ('27', 'Mafe S', 'Ballera', null, null, null, null, null, '9161237772');
INSERT INTO `clients` VALUES ('28', 'Jerry Van E.', 'Belarmino', null, null, null, null, null, '9187522464');
INSERT INTO `clients` VALUES ('29', 'Crissa Mae', 'Benjamin', null, null, null, null, null, 'N/A');
INSERT INTO `clients` VALUES ('30', 'Joevelyn T', 'Berdin', null, null, null, null, null, '9358465940');
INSERT INTO `clients` VALUES ('31', 'Marcelita (Rhea)', 'Bernabe', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('32', 'Meddy C.', 'Bernabe', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('33', 'Kristine D', 'Bernales', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('34', 'Jenny B.', 'Bornales', null, null, null, null, null, '9300643361');
INSERT INTO `clients` VALUES ('35', 'Marelou E', 'Bravante', null, null, null, null, null, '9189485954');
INSERT INTO `clients` VALUES ('36', 'Mary Jane (fr. Lumbay, Joan)', 'Brillantes', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('37', 'Henry', 'Cabales', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('38', 'Renz Marlon', 'Cabillo', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('39', 'Leda F', 'Cabantugan', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('40', 'Mary Ann (fr. Dalisay)', 'Cagalaban', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('41', 'Mae', 'Calderon', null, null, null, null, null, '9152529055');
INSERT INTO `clients` VALUES ('42', 'Ritchie L', 'Calvo', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('43', 'Jovilyn D.', 'Canlas', null, null, null, null, null, '9261979681');
INSERT INTO `clients` VALUES ('44', 'Reymark T', 'Capilitan', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('45', 'Polita, Dadivas,  Agrabante', 'Casana', null, null, null, null, null, '09560911104/09203123997');
INSERT INTO `clients` VALUES ('46', 'Fevie (as. fr.Maxian, John Michael)', 'Casarte', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('47', 'Elsa S.', 'Casiple', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('48', 'Elvie S.', 'Casiple', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('49', 'Joven', 'Castromayor', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('50', 'Alfred Lawrence A', 'Catedrilla', null, null, null, null, null, '9356965469');
INSERT INTO `clients` VALUES ('51', 'Jovert H.', 'Clavel', null, null, null, null, null, '9217719213');
INSERT INTO `clients` VALUES ('52', 'Jovert (Huinda, Juliard N.)', 'Clavel', null, null, null, null, null, '9121699263');
INSERT INTO `clients` VALUES ('53', 'Noeme F.', 'Condino', null, null, null, null, null, '9176051860');
INSERT INTO `clients` VALUES ('54', 'Rizel A', 'Cordero', null, null, null, null, null, '9269882198');
INSERT INTO `clients` VALUES ('55', 'Florante O', 'Dableo', null, null, null, null, null, '9456046703');
INSERT INTO `clients` VALUES ('56', 'Ma. Edelyn G.', 'Dapitan', null, null, null, null, null, '9759869217');
INSERT INTO `clients` VALUES ('57', 'Jhonarose S', 'Daquila', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('58', 'Rhea Mae P', 'Datungputi', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('59', 'Jonathan Solatorio', 'Dela Cruz', null, null, null, null, null, '9751795725');
INSERT INTO `clients` VALUES ('60', 'Jinky Mae F.', 'Del Rosario', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('61', 'Ma. Bennet Ballera', 'Dequilla', null, null, null, null, null, '9265592734');
INSERT INTO `clients` VALUES ('62', 'Sharven A.', 'Doronio', null, null, null, null, null, '9177027432');
INSERT INTO `clients` VALUES ('63', 'Maylyn D', 'Duno', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('64', 'Jocelyn B.', 'Durana', null, null, null, null, null, '9262428939');
INSERT INTO `clients` VALUES ('65', 'Vanieza L', 'Embedia', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('66', 'Joelen S', 'Espino', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('67', 'Jay B.', 'Eterno', null, null, null, null, null, '9162263259');
INSERT INTO `clients` VALUES ('68', 'Zaldy (A.S Bensorto, Cherry A )', 'Ellasgo', null, null, null, null, null, '9266086220');
INSERT INTO `clients` VALUES ('69', 'Elizabeth S.', 'Fajamolin', null, null, null, null, null, '9367757249');
INSERT INTO `clients` VALUES ('70', 'Joel', 'Falcis', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('71', 'Karen Mae', 'Falcis', null, null, null, null, null, '9051543788');
INSERT INTO `clients` VALUES ('72', 'Jovelyn N.', 'Fayloga', null, null, null, null, null, '9488893470');
INSERT INTO `clients` VALUES ('73', 'Liana F.', 'Fegurac', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('74', 'Lalyn C.', 'Felongco', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('75', 'Edna Mae', 'Fisher', null, null, null, null, null, '9305621862');
INSERT INTO `clients` VALUES ('76', 'Dino A', 'Flores', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('77', 'Edwin F. Sr', 'Fores', null, null, null, null, null, '9099933792');
INSERT INTO `clients` VALUES ('78', 'Jeany Rose', 'Francisco', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('79', 'Jovie V.', 'Fronda', null, null, null, null, null, '9555834755');
INSERT INTO `clients` VALUES ('80', 'Efren (Fuyonan, Ronie)', 'Fuyonan', null, null, null, null, null, '9772110975');
INSERT INTO `clients` VALUES ('81', 'Mary Ann A.', 'Garcia', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('82', 'Jelyn E', 'Gener', null, null, null, null, null, '9751953702');
INSERT INTO `clients` VALUES ('83', 'Aldreck Ben B.', 'Germin', null, null, null, null, null, '9676197872');
INSERT INTO `clients` VALUES ('84', 'Mary Grace B.', 'Germin', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('85', 'Alma A.', 'Glemada', null, null, null, null, null, '9363695290');
INSERT INTO `clients` VALUES ('86', 'Ivy Joy', 'Gonzales', null, null, null, null, null, '9217719213');
INSERT INTO `clients` VALUES ('87', 'Ramon S', 'Gorecho', null, null, null, null, null, '9533751238');
INSERT INTO `clients` VALUES ('88', 'Ricky Subong', 'Gorecho', null, null, null, null, null, '9358210313');
INSERT INTO `clients` VALUES ('89', 'May Joy', 'Guilaran', null, null, null, null, null, '9482501100');
INSERT INTO `clients` VALUES ('90', 'Arlyn B.', 'Gumabong', null, null, null, null, null, '9059323674');
INSERT INTO `clients` VALUES ('91', 'Gladys', 'Gumabong', null, null, null, null, null, '9479555330');
INSERT INTO `clients` VALUES ('92', 'Aireen', 'Gumabong', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('93', 'Maureen', 'Dawami', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('94', 'Nesa', 'Hanilap', null, null, null, null, null, '9610917237');
INSERT INTO `clients` VALUES ('95', 'Charity B', 'Huinda', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('96', 'Llaster', 'Huinda', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('97', 'Edralyn L.', 'Ignes', null, null, null, null, null, '9357889448');
INSERT INTO `clients` VALUES ('98', 'Rely Jean', 'Iligan', null, null, null, null, null, '9334361221');
INSERT INTO `clients` VALUES ('99', 'Bradford Felix', 'Jimenea', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('100', 'Jeffrey P.', 'Joaquin', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('101', 'Edwin', 'Kasan', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('102', 'Mary Grace L', 'Labao', null, null, null, null, null, '9976512916');
INSERT INTO `clients` VALUES ('103', 'Mary Grace L', 'Labao', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('104', 'Dhee O', 'Lacambra', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('105', 'Joeddy Adrian Franco', 'Lagahit', null, null, null, null, null, '9212008330');
INSERT INTO `clients` VALUES ('106', 'Eric S.', 'Lamery', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('107', 'Lilibeth Belves/Manilyn', 'Laureano', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('108', 'Eduard', 'Legaspi', null, null, null, null, null, '9168374208');
INSERT INTO `clients` VALUES ('109', 'Darlene', 'Lontiong', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('110', 'Stella Merced D', 'Loreno', null, null, null, null, null, '9197475721');
INSERT INTO `clients` VALUES ('111', 'Liza F', 'Luig', null, null, null, null, null, '9365321074');
INSERT INTO `clients` VALUES ('112', 'Larry D.', 'Macuro', null, null, null, null, null, '9368561748');
INSERT INTO `clients` VALUES ('113', 'Rene', 'Magbanua', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('114', 'Analyn C.', 'Magdaluyo', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('115', 'Arlene', 'Mansilla', null, null, null, null, null, '9756054413');
INSERT INTO `clients` VALUES ('116', 'Florendo S Jr', 'Marquez', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('117', 'Katrina Angelica S', 'Montibon', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('118', 'Arriane Marie E.', 'Morales', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('119', 'Sharlene', 'Morales', null, null, null, null, null, '94872282055');
INSERT INTO `clients` VALUES ('120', 'Dave Alwin B.', 'Moralidad', null, null, null, null, null, '9057989417');
INSERT INTO `clients` VALUES ('121', 'Ma. Lucy', 'Nabor', null, null, null, null, null, '9092651602');
INSERT INTO `clients` VALUES ('122', 'Leizel', 'Navarro', null, null, null, null, null, '9755436469');
INSERT INTO `clients` VALUES ('123', 'Marites', 'Nido', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('124', 'Ramel', 'Noble', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('125', 'Madonna S. (fr. Ong, Ron Ian)', 'Ong', null, null, null, null, null, '9293192017/9553572387');
INSERT INTO `clients` VALUES ('126', 'Marianne E.', 'Osano', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('127', 'Kit Caryl C', 'Otero', null, null, null, null, null, '9639460742');
INSERT INTO `clients` VALUES ('128', 'Penena Park', 'Otero', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('129', 'John Bryan', 'Otero', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('130', 'Vanjely Tortula', 'Pabila', null, null, null, null, null, '9352154366');
INSERT INTO `clients` VALUES ('131', 'Jade B.', 'Pacheco', null, null, null, null, null, '9454134119');
INSERT INTO `clients` VALUES ('132', 'Arlene Loreno', 'Pedregosa', null, null, null, null, null, '9169336445');
INSERT INTO `clients` VALUES ('133', 'Niline Gabriel', 'Pernal', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('134', 'Jimaris O.', 'Robilla', null, null, null, null, null, '9481357686');
INSERT INTO `clients` VALUES ('135', 'Jimely', 'Sajo', null, null, null, null, null, '9669843000');
INSERT INTO `clients` VALUES ('136', 'Jessie', 'Salibio', null, null, null, null, null, '9654607425');
INSERT INTO `clients` VALUES ('137', 'Leonard A.', 'Salindeho', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('138', 'Cyrhelle Formacion', 'Sarino', null, null, null, null, null, '9106699364');
INSERT INTO `clients` VALUES ('139', 'Bebelyn F.', 'Semiho', null, null, null, null, null, '9358647358');
INSERT INTO `clients` VALUES ('140', 'John Jeremy (Serrano, Elsie D.)', 'Serrano', null, null, null, null, null, '9555368224');
INSERT INTO `clients` VALUES ('141', 'Rita M.', 'Sindol', null, null, null, null, null, '9300649328');
INSERT INTO `clients` VALUES ('142', 'Rheelen S', 'Soco', null, null, null, null, null, '9489101800');
INSERT INTO `clients` VALUES ('143', 'Ricky', 'Soldevilla', null, null, null, null, null, '9495025251');
INSERT INTO `clients` VALUES ('144', 'Evy', 'Soriano', null, null, null, null, null, '9167178353');
INSERT INTO `clients` VALUES ('145', 'Nelfa B.', 'Susbilla', null, null, null, null, null, '9357145074');
INSERT INTO `clients` VALUES ('146', 'Nida', 'Tadiaque', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('147', 'Cherry', 'Tesnado', null, null, null, null, null, '9659174937');
INSERT INTO `clients` VALUES ('148', 'Madelyn M', 'Tijon', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('149', 'Earl H.', 'Velasco', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('150', 'Mae Grace', 'Venancio', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('151', 'Rosemarie A', 'Veñola', null, null, null, null, null, '9559006879');
INSERT INTO `clients` VALUES ('152', 'Marielyn Burgos', 'Vitar', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('153', 'Rogie B', 'Vitar', null, null, null, null, null, '9465424222');
INSERT INTO `clients` VALUES ('154', 'Rosfe C.', 'Volutante', null, null, null, null, null, '9361153196');
INSERT INTO `clients` VALUES ('155', 'Carlito R.', 'Abatay', null, null, null, null, null, '9482314321');
INSERT INTO `clients` VALUES ('156', 'Christine A.', 'Academia', null, null, null, null, null, '9917343163');
INSERT INTO `clients` VALUES ('157', 'Charlie Miranda', 'Claveria', null, null, null, null, null, '9068117980');
INSERT INTO `clients` VALUES ('158', 'Welly A.', 'Dado', null, null, null, null, null, '9101200536');
INSERT INTO `clients` VALUES ('159', 'Ahael Hadia', 'Dagsa', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('160', 'Joanna', 'David', null, null, null, null, null, '9661839517');
INSERT INTO `clients` VALUES ('161', 'Sharyl Nhey', 'Delos Santos', null, null, null, null, null, '9103353127');
INSERT INTO `clients` VALUES ('162', 'Liwayway B.', 'Dumrigue', null, null, null, null, null, '9068594157');
INSERT INTO `clients` VALUES ('163', 'Marytchel E.', 'Jopson', null, null, null, null, null, '9396001231');
INSERT INTO `clients` VALUES ('164', 'Emelyn O.', 'Labandero', null, null, null, null, null, '9382678655');
INSERT INTO `clients` VALUES ('165', 'Mary Rose', 'Lacangan', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('166', 'Aj', 'Magallanes', null, null, null, null, null, '9638391575');
INSERT INTO `clients` VALUES ('167', 'Lee V.', 'Mengullo', null, null, null, null, null, '9197891837');
INSERT INTO `clients` VALUES ('168', 'Dominador V.', 'Perez', null, null, null, null, null, '9501110433');
INSERT INTO `clients` VALUES ('169', 'Marilyn D.', 'Serilla', null, null, null, null, null, '9652899147');
INSERT INTO `clients` VALUES ('170', 'Fatima Andi', 'Sinarimbo', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('171', 'Janice E.', 'Sumagaysay', null, null, null, null, null, '9269310353');
INSERT INTO `clients` VALUES ('172', 'Rosalie C', 'Aguirre', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('173', 'Mary Jane', 'Alaer', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('174', 'Sherrylee C', 'Balansag', null, null, null, null, null, '9464660822');
INSERT INTO `clients` VALUES ('175', 'Ivy D', 'Bauson', null, null, null, null, null, '9269652362');
INSERT INTO `clients` VALUES ('176', 'Jerryvan', 'Belarmino', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('177', 'Gerson A', 'Benavidez', null, null, null, null, null, '9261814140');
INSERT INTO `clients` VALUES ('178', 'Zaradane H', 'Boctil', null, null, null, null, null, '9664388658');
INSERT INTO `clients` VALUES ('179', 'Nelson M', 'Borbon', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('180', 'Jocelyn', 'Borlaza', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('181', 'Antonio J', 'Burro', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('182', 'Jan Mar', 'Cabuhay', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('183', 'Ma. Lizl M', 'Corsiga', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('184', 'Richie', 'Dela Cruz', null, null, null, null, null, '9394753255');
INSERT INTO `clients` VALUES ('185', 'Danica Y', 'Dioleste', null, null, null, null, null, '9470223998');
INSERT INTO `clients` VALUES ('186', 'Lesa may T', 'Dioleste', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('187', 'Rosebeth L', 'Era', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('188', 'Juvie', 'Esquierda', null, null, null, null, null, '9513613822');
INSERT INTO `clients` VALUES ('189', 'Rhealiza C', 'Evasco', null, null, null, null, null, '9109463742');
INSERT INTO `clients` VALUES ('190', 'Rhomel B', 'Evasco', null, null, null, null, null, '9482334964');
INSERT INTO `clients` VALUES ('191', 'Esther Antoniette F', 'Falcis', null, null, null, null, null, '9367818743');
INSERT INTO `clients` VALUES ('192', 'Liziel Q', 'Fegurac', null, null, null, null, null, '9171586624');
INSERT INTO `clients` VALUES ('193', 'Mary Jane', 'Fuasan', null, null, null, null, null, '9350071627');
INSERT INTO `clients` VALUES ('194', 'Jufrex S', 'Fullon', null, null, null, null, null, '9355911098');
INSERT INTO `clients` VALUES ('195', 'Noel L', 'Gener', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('196', 'Concepcion C', 'Huinda', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('197', 'Sheldey/Figuerda, Ma. Lizel (fr. Emboltorio, Reman B)', 'Juayno', null, null, null, null, null, '9104108862');
INSERT INTO `clients` VALUES ('198', 'Rhofa Kaye S', 'Lucas', null, null, null, null, null, '9778037349');
INSERT INTO `clients` VALUES ('199', 'Junife', 'Malata', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('200', 'Christian Santos', 'Matociños', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('201', 'Leonel B', 'Moso', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('202', 'Lailyn A', 'Ortiz', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('203', 'Arthur James A', 'Perez', null, null, null, null, null, '9096412525');
INSERT INTO `clients` VALUES ('204', 'Jilord Laurence P', 'Prado', null, null, null, null, null, '');
INSERT INTO `clients` VALUES ('205', 'Jimmy A', 'Ramos', null, null, null, null, null, '9556110568');
INSERT INTO `clients` VALUES ('206', 'Evelyn B', 'Taculin', null, null, null, null, null, '9977552757');
INSERT INTO `clients` VALUES ('207', 'Gemma S', 'Taculin', null, null, null, null, null, '9755614979');
INSERT INTO `clients` VALUES ('208', 'Juvy B', 'Tubang', null, null, null, null, null, '9552535130');

-- ----------------------------
-- Table structure for dicers
-- ----------------------------
DROP TABLE IF EXISTS `dicers`;
CREATE TABLE `dicers` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `fullname` varchar(45) DEFAULT NULL,
  `contactno` varchar(45) DEFAULT NULL,
  `recordstatus` varchar(15) DEFAULT 'active',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of dicers
-- ----------------------------
INSERT INTO `dicers` VALUES ('1', 'M. Apresto', null, 'active');
INSERT INTO `dicers` VALUES ('2', 'Karen', null, 'active');
INSERT INTO `dicers` VALUES ('3', 'Mylyn', null, 'active');
INSERT INTO `dicers` VALUES ('4', 'Sanny', null, 'active');
INSERT INTO `dicers` VALUES ('5', 'Mona', null, 'active');
INSERT INTO `dicers` VALUES ('6', 'Janet', null, 'active');
INSERT INTO `dicers` VALUES ('7', 'Vern', null, 'active');
INSERT INTO `dicers` VALUES ('8', 'Vern/Leca', null, 'active');
INSERT INTO `dicers` VALUES ('9', 'Luz A.', null, 'active');
INSERT INTO `dicers` VALUES ('10', 'Precy', null, 'active');
INSERT INTO `dicers` VALUES ('11', 'AIMEE SEPI', null, 'active');
INSERT INTO `dicers` VALUES ('12', 'NEIL JOHN', null, 'active');
INSERT INTO `dicers` VALUES ('13', 'WEREN/J ARCEGA', null, 'active');
INSERT INTO `dicers` VALUES ('14', 'FERRARIZ/VERN', null, 'active');
INSERT INTO `dicers` VALUES ('15', 'MRS FAMULAG', null, 'active');
INSERT INTO `dicers` VALUES ('16', 'ARGELYN CASANO', null, 'active');
INSERT INTO `dicers` VALUES ('17', 'LIGUE', null, 'active');
INSERT INTO `dicers` VALUES ('18', 'MARY GRACE', null, 'active');
INSERT INTO `dicers` VALUES ('19', 'KAREN M.', null, 'active');
INSERT INTO `dicers` VALUES ('20', 'JOHNLY', null, 'active');
INSERT INTO `dicers` VALUES ('21', 'FONDOZO', null, 'active');
INSERT INTO `dicers` VALUES ('22', 'VERN/GENER', null, 'active');
INSERT INTO `dicers` VALUES ('23', 'WEREN/JOVERT', null, 'active');
INSERT INTO `dicers` VALUES ('24', 'MICHELLE ', null, 'active');
INSERT INTO `dicers` VALUES ('25', 'FRONDOZO', null, 'active');
INSERT INTO `dicers` VALUES ('26', 'SHERYL A', null, 'active');
INSERT INTO `dicers` VALUES ('27', 'WEREN', null, 'active');
INSERT INTO `dicers` VALUES ('28', 'WEREN/RMAGBANUA', null, 'active');
INSERT INTO `dicers` VALUES ('29', 'MATEO/FRONDOZO', null, 'active');
INSERT INTO `dicers` VALUES ('30', 'MONA A', null, 'active');
INSERT INTO `dicers` VALUES ('31', 'BENITEZ', null, 'active');
INSERT INTO `dicers` VALUES ('32', 'JANET/HANILAP', null, 'active');
INSERT INTO `dicers` VALUES ('33', 'LUZ', null, 'active');
INSERT INTO `dicers` VALUES ('34', 'GENER', null, 'active');
INSERT INTO `dicers` VALUES ('35', 'VERNZ', null, 'active');
INSERT INTO `dicers` VALUES ('36', 'MARCIANO', null, 'active');
INSERT INTO `dicers` VALUES ('37', 'DARLYN', null, 'active');
INSERT INTO `dicers` VALUES ('38', 'RIVKAH', null, 'active');
INSERT INTO `dicers` VALUES ('39', 'RACEL PONCIANO', null, 'active');
INSERT INTO `dicers` VALUES ('40', 'SHERYL', null, 'active');
INSERT INTO `dicers` VALUES ('41', 'PTRA MAY', null, 'active');
INSERT INTO `dicers` VALUES ('42', 'VERNZ/GARCIA', null, 'active');
INSERT INTO `dicers` VALUES ('43', 'VERN/GARCIA', null, 'active');
INSERT INTO `dicers` VALUES ('44', 'JARA/AMY ', null, 'active');
INSERT INTO `dicers` VALUES ('45', 'JANET/MARK S', null, 'active');
INSERT INTO `dicers` VALUES ('46', 'Ptr. Ike', null, 'active');
INSERT INTO `dicers` VALUES ('47', 'Ptr. Alegre', null, 'active');
INSERT INTO `dicers` VALUES ('48', 'Hanna', null, 'active');
INSERT INTO `dicers` VALUES ('49', 'Balisi', null, 'active');
INSERT INTO `dicers` VALUES ('50', 'Mike', null, 'active');
INSERT INTO `dicers` VALUES ('51', 'ANTONIO S', null, 'active');
INSERT INTO `dicers` VALUES ('52', 'A. CASIANO', null, 'active');
INSERT INTO `dicers` VALUES ('53', 'A. SEPI', null, 'active');
INSERT INTO `dicers` VALUES ('54', 'Vern/Mylene', null, 'active');
INSERT INTO `dicers` VALUES ('55', 'APRESTO', null, 'active');
INSERT INTO `dicers` VALUES ('56', 'MARK SANTOS', null, 'active');

-- ----------------------------
-- Table structure for expenses
-- ----------------------------
DROP TABLE IF EXISTS `expenses`;
CREATE TABLE `expenses` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `receiveby` int(11) DEFAULT NULL,
  `description` varchar(127) DEFAULT NULL,
  `purpose` varchar(45) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `releaseby` int(11) DEFAULT NULL,
  `daterelease` datetime DEFAULT NULL,
  `remarks` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of expenses
-- ----------------------------

-- ----------------------------
-- Table structure for info1
-- ----------------------------
DROP TABLE IF EXISTS `info1`;
CREATE TABLE `info1` (
  `id` int(11) DEFAULT NULL,
  `datepaid` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `sqm` varchar(255) DEFAULT NULL,
  `percent` varchar(255) DEFAULT NULL,
  `dicer` varchar(255) DEFAULT NULL,
  `lot` varchar(255) DEFAULT NULL,
  `cpno` varchar(255) DEFAULT NULL,
  `lp` varchar(255) DEFAULT NULL,
  `dp` varchar(255) DEFAULT NULL,
  `bal` varchar(255) DEFAULT NULL,
  `mon` varchar(255) DEFAULT NULL,
  `terms` varchar(255) DEFAULT NULL,
  `location` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of info1
-- ----------------------------
INSERT INTO `info1` VALUES ('1', '2020-07-18', 'Amable, Yolly Mae N', '158', '10', 'M. Apresto', '10', '9457898244', '108,000', '20,000', '0', '6,582', '1 yr', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('2', '2020-08-05', 'Badoria, Martin Miguel V', '209', '10', 'M. Apresto', '15', '', '167,200', '5,000', '94,700', '5,225', '2 yrs', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('3', '2020-07-11', 'Capillo, Queenilyn L', '202', '10', 'Karen', '8', '', '161,600', '5,000', '45,300', '5,050', '2 yrs', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('4', '2020-07-23', 'Deita, Anamae (Deita,Cesar)', '236', '10', 'Mylyn', '13', '9519822050', '188,800', '5,000', '46,600', '5,900', '2 yrs', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('5', '2020-07-31', 'Morales, Lea B', '500', '10', 'Sanny', '2,3', '9072305489', '500,000', '20,000', '0', '81,245/sm', '2 yrs', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('6', '2020-07-31', 'Morales, Lea B', '1,500', '10', 'Sanny', '2,3', '9072305489', '1,837,380', '100,000', '0', '81,245/sm', '2 yrs', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('7', '2020-07-18', 'Panaligan, Jessie', '296', '10', 'Mylyn', '6', '9127212801', '307,094', '5,000', '0', '-', '3 MOS', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('8', '2021-07-03', 'Soriano, Dolly', '180', '10', 'Karen', '9', '', '161,999', '5,000', '156,999', '5,249', '2 yrs', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('9', '2020-07-18', 'Trinidad, Jeramae C', '170', '10', 'Mona', '12', '', '187,000', '25,000', '110,875', '6,375', '2 yrs', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('10', '2020-07-21', 'Veraque, Marvin', '279', '10', 'Karen', '14', '9655984273', '233,200', '10,000', '223,200', '6,975', '2 yrs', 'Sto Nino (Tupaz)');
INSERT INTO `info1` VALUES ('11', '2022-08-31', 'Agcan, Nilbert', '200', '7', 'Janet', '24', '9754094576', '150,000', '40,000', '110,000', '-', 'CASH', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('12', '2022-08-11', 'Felongco, Maymay ', '400', '7', 'Vern', '1', '9532541327', '1,200,000', '10,000', '1,190,000', '15,556', '6 YRS', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('13', '2021-07-02', 'Frias, Shirley Yap', '200', '7', 'Vern/Leca', '34', '', '299,200', '5,000', '247,900', '10,800', '2 YRS', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('14', '2022-05-20', 'Funtecha, Felma L.', '400', '7', 'Luz A.', '20', '9650381612', '600,000', '20,000', '580,000', '-', 'CASH', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('15', '', 'Jamerlarin, Jenelyn D', '200', '7', 'Janet', '22', '', '338,800', '16,000', '298,800', '8,300', '3 yrs', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('16', '2022-03-04', 'Layos, Edison', '400', '7', 'Vern', '19', '9655189938', '698,400', '-', '673,400', '19,400', '3 yrs/sm', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('17', '2021-07-02', 'Ortiz, Ailen Carroll', '200', '7', 'Vern/Leca', '33', '', '299,200', '5,000', '237,400', '10,800', '2 YRS', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('18', '2022-08-11', 'Rapista, Roel F.', '600', '7', 'Precy', '52', '9617346757', '1,200,000', '5,000', '1,155,000', '45,000', '2 YRS', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('19', '2021-05-15', 'Sevilla, Marvie C', '400', '7', 'Vern', '2,3', '9063200939', '418,000', '10,000', '0', '-', 'CASH', 'Norala (SuganobSiauso)');
INSERT INTO `info1` VALUES ('20', '2020-07-23', 'Agtoto, Dennis L. ', '200', '10', 'AIMEE SEPI', '29', '9473794562', '259,984', '10,000', '249,984', '9,166', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('21', '2020-07-21', 'Aguilar, Gregy', '100', '5', 'NEIL JOHN', '94', '9233695606', '60,000', '60,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('22', '2020-07-22', 'Arcega, Cherryl', '100', '10', 'WEREN/J ARCEGA', '88', '', '82,200', '15,000', '67,200', '5,600', '1 yr', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('23', '2020-07-22', 'Arcega, Michelle C. ', '200', '10', 'WEREN/J ARCEGA', '83', '9128850036', '140,000', '140,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('24', '2020-08-13', 'Arcenas, Rodelyn B', '200', '10', 'FERRARIZ/VERN', '66', '', '164,400', '5,000', '159,400', '11,200', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('25', '2021-04-28', 'Atuel, Grace D. ', '-1,000', '5', 'MRS FAMULAG', '4', '9179791042', '200,000', '65,000', '135,000', '', '', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('26', '2020-07-22', 'Badjan, Josephine ', '200', '5', 'ARGELYN CASANO', '47', '9487738170', '188,400', '10,000', '178,400', '11,200', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('27', '2020-07-22', 'Bahandi, Johnson S. ', '200', '10', 'WEREN/J ARCEGA', '82', '9175906762', '188,400', '10,000', '178,400', '11,200', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('28', '2020-07-05', 'Ballera, Mafe S', '261', '10', 'LIGUE', '63', '9161237772', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('29', '2020-07-23', 'Belarmino, Jerry Van E. ', '200', '10', 'AIMEE SEPI', '27', '9187522464', '259,984', '10,000', '249,984', '9,166', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('30', '2020-07-21', 'Benjamin, Crissa Mae', '100', '10', 'MARY GRACE', '85', 'N/A', '129,992', '5,000', '124,992', '4,583', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('31', '2020-07-27', 'Berdin, Joevelyn T', '200', '10', 'KAREN M.', '76', '9358465940', '140,000', '15,000', '125,000', '-   ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('32', '2021-02-10', 'Bernabe, Marcelita (Rhea)', '201', '10', 'JOHNLY', '35', '', '299,200', '40,000', '259,200', '10,800', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('33', '2021-11-22', 'Bernabe, Meddy C.', '200', '7', 'KAREN M.', '10', '', '213,400', '10,000', '203,400', '- ', 'CASH/ DEC 2021', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('34', '2020-10-06', 'Bernales, Kristine D', '200(1,000)', '5', 'MRS FAMULAG', '5', '', '150,000', '75,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('35', '2020-07-21', 'Bornales, Jenny B.', '100', '10', 'WEREN/J ARCEGA', '89', '9300643361', '94,200', '5,000', '89,200', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('36', '2020-07-28', 'Bravante, Marelou E', '200', '5', 'FONDOZO', '48', '9189485954', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('37', '2020-08-07', 'Brillantes, Mary Jane (fr. Lumbay, Joan)', '100', '10', 'VERN/GENER', '103', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('38', '2020-07-22', 'Cabales, Henry', '100', '10', 'WEREN/J ARCEGA', '142', '', '82,200', '15,000', '67,200', '5,600', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('39', '2020-08-13', 'Cabillo, Renz Marlon', '100', '10', 'WEREN/JOVERT', '136', '', '70,000', '70,000', '0', '', '', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('40', '2020-09-29', 'Cabantugan, Leda F', '200', '5', 'MRS FAMULAG', '', '', '160,000', '160,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('41', '2020-07-11', 'Cagalaban, Mary Ann (fr. Dalisay)', '100', '10', 'MICHELLE ', '160', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('42', '2020-07-21', 'Calderon, Mae', '100', '5', 'FRONDOZO', '93', '9152529055', '60,000', '5,000', '55,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('43', '2020-10-15', 'Calvo, Ritchie L', '100', '10', 'SHERYL A', '170', '', '129,992', '5,000', '124,992', '4,583', '2 yrs', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('44', '2020-07-25', 'Canlas, Jovilyn D. ', '100', '5', 'FRONDOZO', '116', '9261979681', '75,000', '15,000', '60,000', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('45', '2020-07-30', 'Capilitan, Reymark T', '100', '10', 'WEREN', '124', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('46', '2020-07-20', 'Casana, Polita, Dadivas,  Agrabante', '1,000', '10', 'MICHELLE ', '2', '09560911104/09203123997', '1,100,000', '50,000', '1,050,000', '54,000', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('47', '2020-07-22', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '100', '10', 'WEREN/RMAGBANUA', '97', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('48', '2020-07-23', 'Casiple, Elsa S. ', '100', '5', 'MATEO/FRONDOZO', '137', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('49', '2020-07-23', 'Casiple, Elvie S. ', '200', '5', 'MATEO/FRONDOZO', '50', '', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('50', '2020-07-30', 'Castromayor, Joven', '200', '10', 'KAREN M.', '64', '', '140,000', '5,000', '0', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('51', '2021-02-16', 'Catedrilla, Alfred Lawrence A', '200', '5', 'FRONDOZO', '67', '9356965469', '299,200', '20,000', '279,200', '10,800', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('52', '2020-07-21', 'Clavel, Jovert H. ', '100', '10', 'WEREN/J ARCEGA', '171', '9217719213', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('53', '2020-07-21', 'Clavel, Jovert (Huinda, Juliard N.) ', '100', '10', 'WEREN/J ARCEGA', '169', '9121699263', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('54', '2020-07-22', 'Condino, Noeme F. ', '200', '5', 'FRONDOZO', '70', '9176051860', '150,000', '10,000', '140,000', '5,000', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('55', '2020-07-22', 'Condino, Noeme F. ', '100(1,000)', '5', 'MRS FAMULAG', '5', '9176051860', '75,000', '20,000', '55,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('56', '2020-07-28', 'Cordero, Rizel A', '100', '5', 'FRONDOZO', '134', '9269882198', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('57', '', 'Dableo, Florante O', '1,000', '10', 'MICHELLE ', '3', '9456046703', '1,100,000', '100,000', '1,000,000', '91,500', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('58', '2020-07-21', 'Dapitan, Ma. Edelyn G. ', '100', '5', 'FRONDOZO', '91', '9759869217', '75,000', '3,000', '72,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('59', '2020-09-03', 'Daquila, Jhonarose S', '100(1000)', '5', 'MRS FAMULAG', '5', '', '60,000', '60,000', '0', '', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('60', '2020-07-28', 'Datungputi, Rhea Mae P', '100', '10', 'MONA A', '95', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('61', '', 'Dela Cruz, Jonathan Solatorio', '351', '10', 'KAREN M.', '8', '9751795725', '524,996', '17,550', '507,446', '18,954', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('62', '2020-07-22', 'Del Rosario, Jinky Mae F. ', '200', '5', 'FRONDOZO', '108,110', '', '150,000', '15,000', '135,000', '5,000', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('63', '2020-08-21', 'Dequilla, Ma. Bennet Ballera', '100(200)', '10', 'JANET', '31', '9265592734', '82,200', '15,000', '67,200', '5,600', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('64', '2020-07-21', 'Doronio, Sharven A. ', '100', '10', 'WEREN/J ARCEGA', '87', '9177027432', '82,200', '5,000', '77,200', '5,600', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('65', '2020-07-29', 'Duno, Maylyn D', '100', '10', 'MICHELLE ', '131', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('66', '2020-07-25', 'Durana, Jocelyn B. ', '100(200)', '10', 'JANET', '31', '9262428939', '90,000', '5,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('67', '2020-08-05', 'Embedia, Vanieza L', '200', '-', 'MICHELLE ', '71', '', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('68', '2020-07-29', 'Espino, Joelen S', '200', '10', 'VERN/GENER', '74', '', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('69', '2020-07-22', 'Eterno, Jay B. ', '100', '10', 'WEREN/J ARCEGA', '140', '9162263259', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('70', '2021-09-23', 'Ellasgo, Zaldy (A.S Bensorto, Cherry A )', '200', '10', 'JANET', '34', '9266086220', '215,000', '215,000', '0', '', '', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('71', '2020-07-21', 'Fajamolin, Elizabeth S. ', '200', '5', 'FRONDOZO', '77', '9367757249', '127,500', '30,000', '97,500', '2,500', 'CASH/1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('72', '2020-07-22', 'Falcis, Joel', '100 (1000)', '5', 'MRS FAMULAG', '5', '', '100,000', '20,000', '80,000', '', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('73', '2020-09-02', 'Falcis, Karen Mae', '200', '10', 'JOHNLY', '9', '9051543788', '180,000', '10,000', '170,000', '9,166', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('74', '2020-07-22', 'Fayloga, Jovelyn N. ', '100', '5', 'FRONDOZO', '145', '9488893470', '60,000', '15,000', '45,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('75', '2020-07-22', 'Fegurac, Liana F. ', '100', '5', 'FRONDOZO', '144', '', '75,000', '15,000', '60,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('76', '2020-07-22', 'Felongco, Lalyn C. ', '100 (1,000)', '5', 'MRS FAMULAG', '4', '', '60,000', '40,000', '20,000', '', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('77', '2020-07-25', 'Felongco, Lalyn C. ', '100(1,000)', '5', 'MRS FAMULAG', '5', '', '60,000', '5,000', '55,000', '', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('78', '2020-07-25', 'Fisher, Edna Mae', '200(1000)', '5', 'MRS FAMULAG', '5', '9305621862', '190,000', '10,000', '180,000', '', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('79', '2020-07-23', 'Flores, Dino A', '200 (1000)', '5', 'BENITEZ', '5', '', '299,200', '40,000', '259,200', '10,800', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('80', '2020-07-25', 'Fores, Edwin F. Sr', '200(1000)', '5', 'MRS FAMULAG', '5', '9099933792', '150,000', '30,000', '120,000', '5,000', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('81', '2020-07-29', 'Francisco, Jeany Rose', '100', '10', 'JANET/HANILAP', '111', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('82', '2022-02-22', 'Fronda, Jovie V.', '100', '7', 'LUZ', '133', '9555834755', '110,000', '110,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('83', '2021-04-17', 'Fuyonan, Efren (Fuyonan, Ronie)', '200', '10', 'GENER', '52', '9772110975', '299,200', '10,000', '289,200', '10,800', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('84', '2020-07-20', 'Garcia, Mary Ann A. ', '200', '10', 'VERNZ', '80', '', '180,400', '5,000', '175,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('85', '2021-03-03', 'Gener, Jelyn E', '100', '10', 'GENER', '130', '9751953702', '149,600', '20,000', '124,600', '5,400', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('86', '2021-03-11', 'Gener, Jelyn E', '100', '10', 'GENER', '128', '9751953702', '149,600', '5,000', '144,600', '5,400', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('87', '2020-07-23', 'Germin, Aldreck Ben B. ', '200', '5(full)', 'MARCIANO', '154,155', '9676197872', '140,000', '30,000', '110,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('88', '2020-07-23', 'Germin, Mary Grace B. ', '400', '10', 'DARLYN', '156-159', '', '280,000', '60,000', '220,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('89', '2020-07-21', 'Glemada, Alma A. ', '200', '5', 'FRONDOZO', '78', '9363695290', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('90', '2020-08-20', 'Gonzales, Ivy Joy', '200', '10', 'LIGUE', '56', '9217719213', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('91', '2020-08-12', 'Gorecho, Ramon S', '200', '10', 'MICHELLE ', '40', '9533751238', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('92', '', 'Gorecho, Ricky Subong', '200', '10', 'MICHELLE ', '11', '9358210313', '259,984', '10,000', '249,984', '9,166', '2 yrs', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('93', '', 'Guilaran, May Joy', '200', '7', 'Mary Grace', '51', '9482501100', '338,800', '15,000', '323,800', '8,300', '3 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('94', '2020-07-21', 'Gumabong, Arlyn B. ', '200', '5', 'NEIL JOHN', '90,92', '9059323674', '120,000', '5,000', '110,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('95', '', 'Gumabong, Arlyn B. ', '100', '5', 'FRONDOZO', '143', '9059323674', '60,000', '5,000', '55,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('96', '2020-07-21', 'Gumabong, Gladys', '200', '10', 'WEREN/J ARCEGA', '162,163', '9479555330', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('97', '2020-07-21', 'Gumabong, Aireen', '200', '10', 'WEREN/J ARCEGA', '164-165', '', '152,200', '10,000', '142,200', '5,600', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('98', '2020-07-21', 'Dawami, Maureen', '200', '10', 'WEREN/J ARCEGA', '166-167', '', '140,000', '10,000', '130,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('99', '2020-07-25', 'Hanilap, Nesa', '200', '10', 'JANET/HANILAP', '98,99', '9610917237', '140,000', '10,000', '130,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('100', '2020-08-13', 'Huinda, Charity B', '100', '10', 'WEREN/JOVERT', '122', '', '94,200', '15,000', '79,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('101', '2020-07-30', 'Huinda, Llaster', '200', '10', 'WEREN/JOVERT', '21', '', '164,200', '5,000', '159,200', '3,925/sm ', 'CASH/2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('102', '2020-07-21', 'Ignes, Edralyn L. ', '200', '5', 'FRONDOZO', '13', '9357889448', '240,000', '10,000', '230,000', '8,332', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('103', '2022-02-02', 'Iligan, Rely Jean', '100', '7', 'GENER', '116', '9334361221', '110,000', '15,000', '95,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('104', '2020-07-22', 'Jimenea, Bradford Felix', '200', '10', 'WEREN/RMAGBANUA', '26', '', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('105', '2020-07-22', 'Joaquin, Jeffrey P. ', '600', '10', 'WEREN/J ARCEGA', '18,20,22', '', '565,200', '40,000', '525,200', '19,800', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('106', '2020-07-27', 'Kasan, Edwin', '200', '10', 'WEREN/J ARCEGA', '37,38', '', '280,000', '140,000', '140,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('107', '', 'Labao, Mary Grace L', '100', '10', 'KAREN M.', '23', '9976512916', '94,200', '25,000', '69,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('108', '2022-03-11', 'Labao, Mary Grace L', '104', '7', 'LIGUE', '23', '', '155,584', '', '', '10,800', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('109', '2020-07-27', 'Lacambra, Dhee O', '100', '10', 'WEREN/J ARCEGA', '61', '', '149,400', '5,000', '144,400', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('110', '2020-07-30', 'Lagahit, Joeddy Adrian Franco', '200', '10', 'VERN/GENER', '73', '9212008330', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('111', '2020-07-23', 'Lamery, Eric S. ', '100', '5', 'FRONDOZO', '139', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('112', '2020-07-30', 'Laureano, Lilibeth Belves/Manilyn ', '100', '10', 'JANET/HANILAP', '117', '', '75,500', '15,000', '60,500', '- ', '7 MOS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('113', '2022-02-01', 'Legaspi, Eduard', '200', '7', 'Precy', '62', '9168374208', '338,800', '10,000', '328,800', '8,300', '3 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('114', '2020-07-22', 'Lontiong, Darlene', '200', '5', 'FRONDOZO', '146,147', '', '150,000', '15,000', '135,000', '5,000', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('115', '2020-07-29', 'Loreno, Stella Merced D', '200', '10', 'RIVKAH', '44', '9197475721', '129,000', '10,000', '119,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('116', '2020-07-30', 'Luig, Liza F', '100', '10', 'WEREN', '119', '9365321074', '90,000', '5,000', '85,000', '- ', 'Dec. 2021', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('117', '2021-11-01', 'Macuro, Larry D.', '207', '7', 'KAREN M.', '24', '9368561748', '350,658', '5,000', '345,658', '8,591', '3 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('118', '2020-07-20', 'Magbanua, Rene', '200', '10', 'WEREN', '32', '', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('119', '2020-07-20', 'Magdaluyo, Analyn C. ', '500', '10', 'KAREN M.', '6', '', '748,000', '20,000', '728,000', '27,000', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('120', '2021-05-27', 'Mansilla, Arlene', '100', '10', 'RACEL PONCIANO', '129', '9756054413', '149,600', '20,000', '129,600', '5,400', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('121', '2020-07-20', 'Marquez, Florendo S Jr', '200', '5', 'SHERYL', '28', '', '180,000', '5,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('122', '2020-07-29', 'Montibon, Katrina Angelica S', '200', '10', 'WEREN', '75', '', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('123', '2020-07-22', 'Morales, Arriane Marie E. ', '100', '5', 'FRONDOZO', '112', '', '75,000', '15,000', '60,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('124', '2020-08-11', 'Morales, Sharlene', '200', '10', 'PTRA MAY', '42', '94872282055', '140,000', '10,000', '130,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('125', '2020-07-25', 'Moralidad, Dave Alwin B. ', '200(1000)', '5', 'MRS FAMULAG', '5', '9057989417', '75,000', '10,000', '65,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('126', '2020-08-06', 'Nabor, Ma. Lucy', '100', '5 (full)', 'DARLYN', '113', '9092651602', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('127', '1900-01-03', 'Navarro, Leizel', '200', '10', 'JANET', '68', '9755436469', '188,400', '37,050', '151,350', '6,306', '2 yrs', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('128', '2020-07-21', 'Nido, Marites ', '200', '10', 'WEREN/J ARCEGA', '84,86', '', '141,000', '10,000', '131,000', '5,500', '1 yr', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('129', '2020-07-21', 'Noble, Ramel', '100', '10', 'PTRA MAY', '96', '', '94,200', '5,000', '89,200', '3,300', '2 yrs/June 2022', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('130', '2020-07-28', 'Ong, Madonna S. (fr. Ong, Ron Ian)', '100', '10', 'WEREN', '127', '9293192017/9553572387', '82,200', '5,000', '77,200', '5,600', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('131', '2021-11-10', 'Osano, Marianne E.', '200', '7', 'JOHNLY', '36', '', '220,000', '10,000', '210,000', '', '', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('132', '2021-02-21', 'Otero, Kit Caryl C', '200', '10', 'VERNZ/GARCIA', '15', '9639460742', '259,600', '110,000', '149,600', '5,400', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('133', '2020-07-30', 'Otero, Penena Park', '200', '10', 'JANET', '12', '', '140,000', '10,000', '130,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('134', '2020-07-23', 'Otero, John Bryan ', '100', '10', 'VERNZ/GARCIA', '153', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('135', '2020-07-25', 'Pabila, Vanjely Tortula', '100', '10', 'VERN/GARCIA', '152', '9352154366', '94,200', '5,000', '89,200', '3,300', '2 yrs', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('136', '2020-07-21', 'Pacheco, Jade B. ', '199', '10', 'WEREN', '16', '9454134119', '70,000', '50,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('137', '2020-08-12', 'Pedregosa, Arlene Loreno', '100', '5', 'FRONDOZO', '126', '9169336445', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('138', '2020-07-29', 'Pernal, Niline Gabriel', '100', '10', 'JANET', '115', '', '79,000', '15,000', '64,000', '5,600', '9 MOS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('139', '2020-07-21', 'Robilla, Jimaris O. ', '200', '5', 'ARGELYN CASANO', '39', '9481357686', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('140', '2020-07-27', 'Sajo, Jimely', '100', '5', 'MICHELLE ', '118', '9669843000', '70,000', '5,000', '65,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('141', '2020-07-27', 'Salibio, Jessie', '210+100', '10', 'WEREN/J ARCEGA', '57,58', '9654607425', '217,000', '10,000', '207,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('142', '2020-07-22', 'Salindeho, Leonard A. ', '100', '5', 'JARA/AMY ', '109', '', '75,000', '15,000', '60,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('143', '2020-07-28', 'Sarino, Cyrhelle Formacion', '200', '5', 'FRONDOZO', '121,123', '9106699364', '164,400', '5,000', '159,400', '11,200', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('144', '2020-09-29', 'Semiho, Bebelyn F. ', '-1,000', '5', 'MRS FAMULAG', '4', '9358647358', '200,000', '200,000', '0', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('145', '2020-07-23', 'Serrano, John Jeremy (Serrano, Elsie D.)', '200', '10', 'WEREN/J ARCEGA', '81', '9555368224', '191,200', '10,000', '181,200', '6,300', '2 yrs', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('146', '2020-07-20', 'Sindol, Rita M. ', '200', '10', 'VERNZ/GARCIA', '17', '9300649328', '259,984', '15,000', '244,984', '9,166', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('147', '2020-07-30', 'Soco, Rheelen S', '200', '10', 'MICHELLE ', '72', '9489101800', '152,200', '30,000', '122,200', '- ', '6 MOS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('148', '2020-07-25', 'Soldevilla, Ricky', '100', '5', 'FRONDOZO', '138', '9495025251', '75,000', '15,000', '60,000', '2,500', '1 YR', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('149', '2020-07-24', 'Soriano, Evy', '200', '10', 'MICHELLE ', '41', '9167178353', '198,400', '5,000', '148,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('150', '2020-07-20', 'Susbilla, Nelfa B. ', '200', '5', 'FRONDOZO', '79', '9357145074', '120,000', '5,000', '115,000', '- ', 'CASH', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('151', '2020-07-20', 'Tadiaque, Nida', '200', '10', 'WEREN/RMAGBANUA', '30', '', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('152', '2020-08-14', 'Tesnado, Cherry', '200', '10', 'JANET', '45', '9659174937', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('153', '2020-07-31', 'Tijon, Madelyn M', '100', '10', 'VERN/GENER', '120', '', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('154', '2020-07-22', 'Velasco, Earl H. ', '100', '5', 'JARA/AMY ', '114', '', '75,000', '30,000', '45,000', '2,500', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('155', '', 'Venancio, Mae Grace', '200', '10', 'JANET/MARK S', '14', '', '259,984', '30,000', '229,984', '9,166', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('156', '2020-07-22', 'Veñola, Rosemarie A', '200', '10', 'WEREN/RMAGBANUA', '25', '9559006879', '188,400', '10,000', '178,400', '6,600', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('157', '2022-02-02', 'Vitar, Marielyn Burgos', '200', '7', 'GENER', '51', '', '299,200', '10,000', '289,200', '10,800', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('158', '2020-07-29', 'Vitar, Rogie B', '100', '10', 'VERN', '102', '9465424222', '94,200', '5,000', '89,200', '3,300', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('159', '2020-07-21', 'Volutante, Rosfe C. ', '200', '5', 'FRONDOZO', '19', '9361153196', '240,000', '10,000', '230,000', '8,332', '2 YRS', 'Norala (Frondozo-Famulag)');
INSERT INTO `info1` VALUES ('160', '2022-08-01', 'Abatay, Carlito R.', '600', '7', 'Ptr. Ike', '14,16,18', '9482314321', '690,000', '300,000', '390,000', '16,250', '2 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('161', '2022-05-05', 'Academia, Christine A.', '200', '7', 'Ptr. Alegre', '8', '9917343163', '339,952', '10,000', '329,952', '8,332', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('162', '2022-05-06', 'Claveria, Charlie Miranda', '200', '7', 'Ptr. Alegre', '19', '9068117980', '310,000', '10,000', '300,000', '11,250', '2 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('163', '2022-06-02', 'Dado, Welly A.', '100', '7', 'Sheryl A', '32', '9101200536', '160,004', '', '', '3,889', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('164', '2022-04-07', 'Dagsa, Ahael Hadia', '500', '5', 'Hanna', '11,12', '', '920,056', '15,000', '905,056', '34,169', '2 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('165', '2022-07-19', 'David, Joanna', '700', '7', 'Ptr. Alegre', '3,4,6', '9661839517', '1,330,088', '15,000', '1,315,088', '33,058', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('166', '2022-06-01', 'Delos Santos, Sharyl Nhey', '100', '7', 'Janet', '39', '9103353127', '200,004', '-', '200,004', '3,333', '5 YRS/SM', 'Tupi P3');
INSERT INTO `info1` VALUES ('167', '2022-05-20', 'Dumrigue, Liwayway B.', '400', '7', 'Vern', '55,56', '9068594157', '859,904', '20,000', '839,904', '10,832', '6 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('168', '2022-04-30', 'Jopson, Marytchel E.', '100 (300)', '7', 'Ptr. Alegre', '7', '9396001231', '170,000', '195,000', '-25,000', '-', 'CASH', 'Tupi P3');
INSERT INTO `info1` VALUES ('169', '2022-04-30', 'Jopson, Marytchel E.', '200 (300)', '7', 'Ptr. Alegre', '7', '9396001231', '340,000', '25,000', '315,000', '28,334', 'CASH', 'Tupi P3');
INSERT INTO `info1` VALUES ('170', '2022-04-24', 'Labandero, Emelyn O.', '300', '7', 'Karen', '15', '9382678655', '510,000', '15,000', '495,000', '-', 'CASH', 'Tupi P3');
INSERT INTO `info1` VALUES ('171', '2022-07-19', 'Lacangan, Mary Rose', '500', '7', 'Ptr. Alegre', '1,2', '', '1,010,080', '15,000', '995,080', '25,280', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('172', '2022-08-12', 'Magallanes, Aj ', '200', '3.5', 'Balisi', '68', '9638391575', '320,008', '5,000', '315,008', '7,778', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('173', '2022-05-04', 'Mengullo, Lee V.', '300', '7', 'Mike', '5', '9197891837', '690,072', '10,000', '680,072', '17,502', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('174', '2022-06-13', 'Perez, Dominador V.', '100', '7', 'Ptr. Alegre', '40', '9501110433', '215,004', '-', '215,004', '2,986', '6 YRS/SM', 'Tupi P3');
INSERT INTO `info1` VALUES ('175', '2022-04-26', 'Serilla, Marilyn D.', '312', '7', 'Luz', '17', '9652899147', '717,675', '', '717,675', '18,202', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('176', '', 'Sinarimbo, Fatima Andi', '500', '7', 'Karen', '9,10', '', '1,030,024', '210,000', '820,024', '25,834', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('177', '2022-06-06', 'Sumagaysay, Janice E.', '100', '7', 'Ptr. Alegre', '41', '9269310353', '115,000', '20,000', '95,000', '-', 'CASH', 'Tupi P3');
INSERT INTO `info1` VALUES ('178', '2020-10-07', 'Aguirre, Rosalie C', '200', '10', 'ANTONIO S', '37,38', '', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('179', '', 'Alaer, Mary Jane', '200', '5', 'A. CASIANO', '27', '', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('180', '2020-10-12', 'Alaer, Mary Jane', '200', '10', 'LIGUE', '28', '', '370,120', '40,000', '330,120', '9,170', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('181', '', 'Alaer, Mary Jane', '200', '5', 'A. CASIANO', '29', '', '370,120', '10,000', '360,120', '9,170', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('182', '2020-10-07', 'Balansag, Sherrylee C', '200', '10', 'ANTONIO S', '11', '9464660822', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('183', '2020-10-08', 'Bauson, Ivy D', '200', '10', 'ANTONIO S', '25', '9269652362', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('184', '2020-10-08', 'Belarmino, Jerryvan', '200', '10', 'A. SEPI', '20', '', '280,000', '5,000', '275,000', '- ', 'CASH', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('185', '2020-10-06', 'Benavidez, Gerson A', '100', '10', 'A. CASIANO', '40', '9261814140', '155,040', '5,000', '150,040', '3,890', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('186', '2020-10-06', 'Boctil, Zaradane H', '200', '10', 'VERN/GENER', '26', '9664388658', '310,000', '10,000', '300,000', '22,500', '1 YR', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('187', '2020-10-08', 'Borbon, Nelson M', '200', '10', 'VERN/GENER', '14', '', '370,120', '10,000', '360,120', '9,170', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('188', '2020-10-08', 'Borlaza, Jocelyn', '200', '10', 'VERN/GENER', '16', '', '370,120', '10,000', '360,120', '9,170', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('189', '2020-11-21', 'Borlaza, Jocelyn', '137', '10', 'VERN/GENER', '7', '', '171,304', '6,700', '164,604', '12,562', '1 YR', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('190', '2020-10-12', 'Burro, Antonio J', '100', '10', 'WEREN', '41', '', '155,040', '15,000', '140,040', '3,890', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('191', '2020-10-08', 'Cabuhay, Jan Mar', '200', '10', 'VERN/GENER', '44', '', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('192', '2020-10-06', 'Corsiga, Ma. Lizl M', '200', '10', 'VERN/GENER', '45', '', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('193', '2020-10-08', 'Dela Cruz, Richie', '200', '5(full)', 'DARLYN', '19', '9394753255', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('194', '2020-10-06', 'Dioleste, Danica Y', '151', '10', 'JANET', '43', '9470223998', '234,110', '5,000', '229,110', '11,747', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('195', '2020-10-06', 'Dioleste, Lesa may T', '100', '10', 'JANET', '42', '', '155,040', '5,000', '150,040', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('196', '2020-10-06', 'Era, Rosebeth L', '200', '10', 'VERN', '3', '', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('197', '2020-11-17', 'Esquierda, Juvie', '200', '10', 'VERN/GENER', '23', '9513613822', '250,080', '10,000', '240,080', '18,340', '2 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('198', '2021-07-22', 'Evasco, Rhealiza C', '200', '7', 'Vern/Mylene', '8', '9109463742', '220,000', '25,000', '195,000', '- ', 'CASH/DEC 21', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('199', '2021-07-22', 'Evasco, Rhomel B', '200', '7', 'Vern/Mylene', '9', '9482334964', '220,000', '25,000', '195,000', '- ', 'CASH/DEC 21', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('200', '2020-10-06', 'Falcis, Esther Antoniette F', '100', '5', 'A. CASIANO', '39', '9367818743', '140,040', '5,000', '135,040', '3,890', '2 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('201', '2020-11-30', 'Fegurac, Liziel Q', '100', '10', 'ANTONIO S', '34', '9171586624', '155,040', '15,000', '140,040', '3,890', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('202', '2020-10-12', 'Fuasan, Mary Jane', '100', '10', 'VERN/GENER', '32', '9350071627', '155,040', '5,000', '150,040', '3,890', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('203', '2020-11-17', 'Fullon, Jufrex S', '200', '10', 'VERN/GENER', '17', '9355911098', '220,000', '30,000', '190,000', '18,340', '1 YR', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('204', '2020-10-08', 'Gener, Noel L', '100', '10', 'VERN/GENER', '30', '', '185,060', '5,000', '180,060', '4,585', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('205', '', 'Huinda, Concepcion C', '200', '10', 'APRESTO', '24', '', '370,120', '40,000', '330,120', '9,170', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('206', '2020-10-06', 'Juayno, Sheldey/Figuerda, Ma. Lizel (fr. Emboltorio, Reman B)', '200', '10', 'JANET', '18', '9104108862', '370,120', '5,000', '365,120', '9,170', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('207', '2020-10-06', 'Lucas, Rhofa Kaye S', '200', '10', 'JANET', '4', '9778037349', '310,080', '5,000', '305,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('208', '2020-10-08', 'Malata, Junife', '100', '10', 'VERN/GENER', '31', '', '110,000', '10,000', '100,000', '3,890', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('209', '', 'Matociños, Christian Santos', '200', '10', 'MARK SANTOS', '22', '', '370,100', '20,000', '350,100', '8,586', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('210', '2020-10-06', 'Moso, Leonel B', '200', '10', 'VERN/GENER', '46', '', '310,080', '10,000', '300,080', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('211', '', 'Ortiz, Lailyn A', '200', '10', 'Rivkah', '15', '', '250,080', '20,000', '230,080', '18,340', '1 YR', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('212', '2020-10-08', 'Perez, Arthur James A', '200', '10', 'ANTONIO S', '21,35', '9096412525', '325,000', '15,000', '310,000', '-', 'CASH', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('213', '2020-10-12', 'Prado, Jilord Laurence P', '200', '10', 'VERN/GENER', '13', '', '212,404', '10,000', '202,404', '7,780', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('214', '2020-10-06', 'Ramos, Jimmy A', '100', '10', 'APRESTO', '33', '9556110568', '155,040', '10,000', '145,040', '3,890', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('215', '2020-10-08', 'Taculin, Evelyn B', '200', '10', 'MARK SANTOS', '12', '9977552757', '370,120', '10,000', '360,120', '9,170', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('216', '2020-10-08', 'Taculin, Gemma S', '200', '10', 'MARK SANTOS', '10', '9755614979', '280,000', '40,000', '240000', '- ', 'CASH', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('217', '2020-10-08', 'Tubang, Juvy B', '100', '5(full)', 'DARLYN', '36', '9552535130', '155,040', '5,000', '150,040', '3,890', '3 YRS', 'Norala (Lavega)');
INSERT INTO `info1` VALUES ('218', '2022-08-01', 'Abatay, Carlito R.', '600', '7', 'Ptr. Ike', '14,16,18', '9482314321', '690,000', '300,000', '390,000', '16,250', '2 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('219', '2022-05-05', 'Academia, Christine A.', '200', '7', 'Ptr. Alegre', '8', '9917343163', '339,952', '10,000', '329,952', '8,332', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('220', '2022-05-06', 'Claveria, Charlie Miranda', '200', '7', 'Ptr. Alegre', '19', '9068117980', '310,000', '10,000', '300,000', '11,250', '2 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('221', '2022-06-02', 'Dado, Welly A.', '100', '7', 'Sheryl A', '32', '9101200536', '160,004', '', '', '3,889', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('222', '2022-04-07', 'Dagsa, Ahael Hadia', '500', '5', 'Hanna', '11,12', '', '920,056', '15,000', '905,056', '34,169', '2 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('223', '2022-07-19', 'David, Joanna', '700', '7', 'Ptr. Alegre', '3,4,6', '9661839517', '1,330,088', '15,000', '1,315,088', '33,058', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('224', '2022-06-01', 'Delos Santos, Sharyl Nhey', '100', '7', 'Janet', '39', '9103353127', '200,004', '-', '200,004', '3,333', '5 YRS/SM', 'Tupi P3');
INSERT INTO `info1` VALUES ('225', '2022-05-20', 'Dumrigue, Liwayway B.', '400', '7', 'Vern', '55,56', '9068594157', '859,904', '20,000', '839,904', '10,832', '6 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('226', '2022-04-30', 'Jopson, Marytchel E.', '100 (300)', '7', 'Ptr. Alegre', '7', '9396001231', '170,000', '195,000', '-25,000', '-', 'CASH', 'Tupi P3');
INSERT INTO `info1` VALUES ('227', '2022-04-30', 'Jopson, Marytchel E.', '200 (300)', '7', 'Ptr. Alegre', '7', '9396001231', '340,000', '25,000', '315,000', '28,334', 'CASH', 'Tupi P3');
INSERT INTO `info1` VALUES ('228', '2022-04-24', 'Labandero, Emelyn O.', '300', '7', 'Karen', '15', '9382678655', '510,000', '15,000', '495,000', '-', 'CASH', 'Tupi P3');
INSERT INTO `info1` VALUES ('229', '2022-07-19', 'Lacangan, Mary Rose', '500', '7', 'Ptr. Alegre', '1,2', '', '1,010,080', '15,000', '995,080', '25,280', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('230', '2022-08-12', 'Magallanes, Aj ', '200', '3.5', 'Balisi', '68', '9638391575', '320,008', '5,000', '315,008', '7,778', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('231', '2022-05-04', 'Mengullo, Lee V.', '300', '7', 'Mike', '5', '9197891837', '690,072', '10,000', '680,072', '17,502', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('232', '2022-06-13', 'Perez, Dominador V.', '100', '7', 'Ptr. Alegre', '40', '9501110433', '215,004', '-', '215,004', '2,986', '6 YRS/SM', 'Tupi P3');
INSERT INTO `info1` VALUES ('233', '2022-04-26', 'Serilla, Marilyn D.', '312', '7', 'Luz', '17', '9652899147', '717,675', '', '717,675', '18,202', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('234', 'Tupi P2', 'Sinarimbo, Fatima Andi', '500', '7', 'Karen', '9,10', '', '1,030,024', '210,000', '820,024', '25,834', '3 YRS', 'Tupi P3');
INSERT INTO `info1` VALUES ('235', '2022-06-06', 'Sumagaysay, Janice E.', '100', '7', 'Ptr. Alegre', '41', '9269310353', '115,000', '20,000', '95,000', '-', 'CASH', 'Tupi P3');

-- ----------------------------
-- Table structure for paymentdetails
-- ----------------------------
DROP TABLE IF EXISTS `paymentdetails`;
CREATE TABLE `paymentdetails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `description` varchar(127) DEFAULT NULL,
  `idpurchasedetails` int(11) DEFAULT NULL,
  `amount` decimal(10,2) DEFAULT NULL,
  `paymentfor` varchar(15) DEFAULT NULL,
  `idpayment` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of paymentdetails
-- ----------------------------

-- ----------------------------
-- Table structure for payments
-- ----------------------------
DROP TABLE IF EXISTS `payments`;
CREATE TABLE `payments` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dateofpayment` datetime DEFAULT NULL,
  `orderreceipt` varchar(12) DEFAULT NULL,
  `totalamount` decimal(10,2) DEFAULT NULL,
  `paidby` int(11) DEFAULT NULL,
  `paymenttype` varchar(15) DEFAULT NULL,
  `referenceno` int(15) DEFAULT NULL,
  `paymentref` varchar(45) DEFAULT NULL,
  `inchargeby` int(11) DEFAULT NULL,
  `recordedby` int(11) DEFAULT NULL,
  `dateencoded` datetime DEFAULT NULL,
  `dateedited` datetime DEFAULT NULL,
  `recordstatus` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of payments
-- ----------------------------

-- ----------------------------
-- Table structure for products
-- ----------------------------
DROP TABLE IF EXISTS `products`;
CREATE TABLE `products` (
  `idproduct` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(45) DEFAULT NULL,
  `location` varchar(255) DEFAULT NULL,
  `totalblockno` int(11) DEFAULT NULL,
  `totallotno` int(11) DEFAULT NULL,
  `totalarea` decimal(10,2) DEFAULT NULL,
  `cashprice` decimal(10,2) DEFAULT NULL,
  PRIMARY KEY (`idproduct`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of products
-- ----------------------------
INSERT INTO `products` VALUES ('1', 'StoNinoTupaz', 'Sto Nino (Tupaz)', '10', '200', null, '100000.00');
INSERT INTO `products` VALUES ('2', 'NoralaSuganob', 'Norala (SuganobSiauso)', '10', '200', null, '100000.00');
INSERT INTO `products` VALUES ('3', 'NoralaFrondozo', 'Norala (Frondozo-Famulag)', '10', '200', null, '100000.00');
INSERT INTO `products` VALUES ('4', 'TupiP3', 'Tupi P3', '10', '200', null, '100000.00');
INSERT INTO `products` VALUES ('5', 'NoralaLavega', 'Norala (Lavega)', '10', '200', null, '100000.00');

-- ----------------------------
-- Table structure for purchasedetails
-- ----------------------------
DROP TABLE IF EXISTS `purchasedetails`;
CREATE TABLE `purchasedetails` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `blockno` int(11) DEFAULT NULL,
  `lotno` varchar(45) DEFAULT NULL,
  `area` varchar(45) DEFAULT NULL,
  `lotprice` decimal(10,2) DEFAULT NULL,
  `amortization` varchar(20) DEFAULT NULL,
  `terms` varchar(45) DEFAULT NULL,
  `idagent` int(11) DEFAULT NULL,
  `agentpercentage` decimal(10,2) DEFAULT NULL,
  `idclients` int(11) DEFAULT NULL,
  `idproducts` int(11) DEFAULT NULL,
  `remarks` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=256 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of purchasedetails
-- ----------------------------
INSERT INTO `purchasedetails` VALUES ('1', null, '10', '158', '108000.00', '6582', '1 yr', '1', '10.00', '1', '1', null);
INSERT INTO `purchasedetails` VALUES ('2', null, '15', '209', '167200.00', '5225', '2 yrs', '1', '10.00', '2', '1', null);
INSERT INTO `purchasedetails` VALUES ('3', null, '8', '202', '161600.00', '5050', '2 yrs', '2', '10.00', '3', '1', null);
INSERT INTO `purchasedetails` VALUES ('4', null, '13', '236', '188800.00', '5900', '2 yrs', '3', '10.00', '4', '1', null);
INSERT INTO `purchasedetails` VALUES ('5', null, '2,3', '500', '500000.00', '81245', '2 yrs', '4', '10.00', '5', '1', null);
INSERT INTO `purchasedetails` VALUES ('6', null, '2,3', '1,500', '1837380.00', '81245', '2 yrs', '4', '10.00', '5', '1', null);
INSERT INTO `purchasedetails` VALUES ('7', null, '6', '296', '307094.00', '0', '3 MOS', '3', '10.00', '6', '1', null);
INSERT INTO `purchasedetails` VALUES ('8', null, '9', '180', '161999.00', '5249', '2 yrs', '2', '10.00', '7', '1', null);
INSERT INTO `purchasedetails` VALUES ('9', null, '12', '170', '187000.00', '6375', '2 yrs', '5', '10.00', '8', '1', null);
INSERT INTO `purchasedetails` VALUES ('10', null, '14', '279', '233200.00', '6975', '2 yrs', '2', '10.00', '9', '1', null);
INSERT INTO `purchasedetails` VALUES ('11', null, '24', '200', '150000.00', '0', 'CASH', '6', '7.00', '10', '2', null);
INSERT INTO `purchasedetails` VALUES ('12', null, '1', '400', '1200000.00', '15556', '6 YRS', '7', '7.00', '11', '2', null);
INSERT INTO `purchasedetails` VALUES ('13', null, '34', '200', '299200.00', '10800', '2 YRS', '8', '7.00', '12', '2', null);
INSERT INTO `purchasedetails` VALUES ('14', null, '20', '400', '600000.00', '0', 'CASH', '9', '7.00', '13', '2', null);
INSERT INTO `purchasedetails` VALUES ('15', null, '22', '200', '338800.00', '8300', '3 yrs', '6', '7.00', '14', '2', null);
INSERT INTO `purchasedetails` VALUES ('16', null, '19', '400', '698400.00', '19400', '3 yrs/sm', '7', '7.00', '15', '2', null);
INSERT INTO `purchasedetails` VALUES ('17', null, '33', '200', '299200.00', '10800', '2 YRS', '8', '7.00', '16', '2', null);
INSERT INTO `purchasedetails` VALUES ('18', null, '52', '600', '1200000.00', '45000', '2 YRS', '10', '7.00', '17', '2', null);
INSERT INTO `purchasedetails` VALUES ('19', null, '2,3', '400', '418000.00', '0', 'CASH', '7', '7.00', '18', '2', null);
INSERT INTO `purchasedetails` VALUES ('20', null, '29', '200', '259984.00', '9166', '2 YRS', '11', '10.00', '19', '3', null);
INSERT INTO `purchasedetails` VALUES ('21', null, '94', '100', '60000.00', '0', 'CASH', '12', '5.00', '20', '3', null);
INSERT INTO `purchasedetails` VALUES ('22', null, '88', '100', '82200.00', '5600', '1 yr', '13', '10.00', '21', '3', null);
INSERT INTO `purchasedetails` VALUES ('23', null, '83', '200', '140000.00', '0', 'CASH', '13', '10.00', '22', '3', null);
INSERT INTO `purchasedetails` VALUES ('24', null, '66', '200', '164400.00', '11200', '1 YR', '14', '10.00', '23', '3', null);
INSERT INTO `purchasedetails` VALUES ('25', null, '4', '1,000', '200000.00', '0', '', '15', '5.00', '24', '3', null);
INSERT INTO `purchasedetails` VALUES ('26', null, '47', '200', '188400.00', '11200', '2 YRS', '16', '5.00', '25', '3', null);
INSERT INTO `purchasedetails` VALUES ('27', null, '82', '200', '188400.00', '11200', '2 YRS', '13', '10.00', '26', '3', null);
INSERT INTO `purchasedetails` VALUES ('28', null, '63', '261', '188400.00', '6600', '2 YRS', '17', '10.00', '27', '3', null);
INSERT INTO `purchasedetails` VALUES ('29', null, '27', '200', '259984.00', '9166', '2 YRS', '11', '10.00', '28', '3', null);
INSERT INTO `purchasedetails` VALUES ('30', null, '85', '100', '129992.00', '4583', '2 YRS', '18', '10.00', '29', '3', null);
INSERT INTO `purchasedetails` VALUES ('31', null, '76', '200', '140000.00', '0', 'CASH', '19', '10.00', '30', '3', null);
INSERT INTO `purchasedetails` VALUES ('32', null, '35', '201', '299200.00', '10800', '2 YRS', '20', '10.00', '31', '3', null);
INSERT INTO `purchasedetails` VALUES ('33', null, '10', '200', '213400.00', '0', 'CASH/ DEC 2021', '19', '7.00', '32', '3', null);
INSERT INTO `purchasedetails` VALUES ('34', null, '5', '200(1,000)', '150000.00', '0', 'CASH', '15', '5.00', '33', '3', null);
INSERT INTO `purchasedetails` VALUES ('35', null, '89', '100', '94200.00', '6600', '2 YRS', '13', '10.00', '34', '3', null);
INSERT INTO `purchasedetails` VALUES ('36', null, '48', '200', '188400.00', '6600', '2 YRS', '21', '5.00', '35', '3', null);
INSERT INTO `purchasedetails` VALUES ('37', null, '103', '100', '94200.00', '3300', '2 YRS', '22', '10.00', '36', '3', null);
INSERT INTO `purchasedetails` VALUES ('38', null, '142', '100', '82200.00', '5600', '1 YR', '13', '10.00', '37', '3', null);
INSERT INTO `purchasedetails` VALUES ('39', null, '136', '100', '70000.00', '0', '', '23', '10.00', '38', '3', null);
INSERT INTO `purchasedetails` VALUES ('40', null, '', '200', '160000.00', '0', 'CASH', '15', '5.00', '39', '3', null);
INSERT INTO `purchasedetails` VALUES ('41', null, '160', '100', '94200.00', '3300', '2 YRS', '24', '10.00', '40', '3', null);
INSERT INTO `purchasedetails` VALUES ('42', null, '93', '100', '60000.00', '0', 'CASH', '25', '5.00', '41', '3', null);
INSERT INTO `purchasedetails` VALUES ('43', null, '170', '100', '129992.00', '4583', '2 yrs', '26', '10.00', '42', '3', null);
INSERT INTO `purchasedetails` VALUES ('44', null, '116', '100', '75000.00', '3300', '2 YRS', '25', '5.00', '43', '3', null);
INSERT INTO `purchasedetails` VALUES ('45', null, '124', '100', '94200.00', '3300', '2 YRS', '27', '10.00', '44', '3', null);
INSERT INTO `purchasedetails` VALUES ('46', null, '2', '1,000', '1100000.00', '54000', '2 YRS', '24', '10.00', '45', '3', null);
INSERT INTO `purchasedetails` VALUES ('47', null, '97', '100', '94200.00', '3300', '2 YRS', '28', '10.00', '46', '3', null);
INSERT INTO `purchasedetails` VALUES ('48', null, '137', '100', '94200.00', '3300', '2 YRS', '29', '5.00', '47', '3', null);
INSERT INTO `purchasedetails` VALUES ('49', null, '50', '200', '188400.00', '6600', '2 YRS', '29', '5.00', '48', '3', null);
INSERT INTO `purchasedetails` VALUES ('50', null, '64', '200', '140000.00', '3300', '2 YRS', '19', '10.00', '49', '3', null);
INSERT INTO `purchasedetails` VALUES ('51', null, '67', '200', '299200.00', '10800', '2 YRS', '25', '5.00', '50', '3', null);
INSERT INTO `purchasedetails` VALUES ('52', null, '171', '100', '94200.00', '3300', '2 YRS', '13', '10.00', '51', '3', null);
INSERT INTO `purchasedetails` VALUES ('53', null, '169', '100', '94200.00', '3300', '2 YRS', '13', '10.00', '52', '3', null);
INSERT INTO `purchasedetails` VALUES ('54', null, '70', '200', '150000.00', '5000', '2 YRS', '25', '5.00', '53', '3', null);
INSERT INTO `purchasedetails` VALUES ('55', null, '5', '100(1,000)', '75000.00', '2500', '2 YRS', '15', '5.00', '53', '3', null);
INSERT INTO `purchasedetails` VALUES ('56', null, '134', '100', '94200.00', '3300', '2 YRS', '25', '5.00', '54', '3', null);
INSERT INTO `purchasedetails` VALUES ('57', null, '3', '1,000', '1100000.00', '91500', '1 YR', '24', '10.00', '55', '3', null);
INSERT INTO `purchasedetails` VALUES ('58', null, '91', '100', '75000.00', '2500', '2 YRS', '25', '5.00', '56', '3', null);
INSERT INTO `purchasedetails` VALUES ('59', null, '5', '100(1000)', '60000.00', '0', 'CASH', '15', '5.00', '57', '3', null);
INSERT INTO `purchasedetails` VALUES ('60', null, '8', '351', '524996.00', '18954', '2 YRS', '19', '10.00', '59', '3', null);
INSERT INTO `purchasedetails` VALUES ('61', null, '108,110', '200', '150000.00', '5000', '2 YRS', '25', '5.00', '60', '3', null);
INSERT INTO `purchasedetails` VALUES ('62', null, '31', '100(200)', '82200.00', '5600', '1 YR', '6', '10.00', '61', '3', null);
INSERT INTO `purchasedetails` VALUES ('63', null, '87', '100', '82200.00', '5600', '1 YR', '13', '10.00', '62', '3', null);
INSERT INTO `purchasedetails` VALUES ('64', null, '131', '100', '94200.00', '3300', '2 YRS', '24', '10.00', '63', '3', null);
INSERT INTO `purchasedetails` VALUES ('65', null, '31', '100(200)', '90000.00', '0', 'CASH', '6', '10.00', '64', '3', null);
INSERT INTO `purchasedetails` VALUES ('66', null, '71', '200', '188400.00', '6600', '2 YRS', '24', '0.00', '65', '3', null);
INSERT INTO `purchasedetails` VALUES ('67', null, '74', '200', '188400.00', '6600', '2 YRS', '22', '10.00', '66', '3', null);
INSERT INTO `purchasedetails` VALUES ('68', null, '140', '100', '94200.00', '3300', '2 YRS', '13', '10.00', '67', '3', null);
INSERT INTO `purchasedetails` VALUES ('69', null, '34', '200', '215000.00', '0', '', '6', '10.00', '68', '3', null);
INSERT INTO `purchasedetails` VALUES ('70', null, '77', '200', '127500.00', '2500', 'CASH/1 YR', '25', '5.00', '69', '3', null);
INSERT INTO `purchasedetails` VALUES ('71', null, '5', '100 (1000)', '100000.00', '0', 'CASH', '15', '5.00', '70', '3', null);
INSERT INTO `purchasedetails` VALUES ('72', null, '9', '200', '180000.00', '9166', '2 YRS', '20', '10.00', '71', '3', null);
INSERT INTO `purchasedetails` VALUES ('73', null, '145', '100', '60000.00', '2500', '2 YRS', '25', '5.00', '72', '3', null);
INSERT INTO `purchasedetails` VALUES ('74', null, '144', '100', '75000.00', '2500', '2 YRS', '25', '5.00', '73', '3', null);
INSERT INTO `purchasedetails` VALUES ('75', null, '4', '100 (1,000)', '60000.00', '0', 'CASH', '15', '5.00', '74', '3', null);
INSERT INTO `purchasedetails` VALUES ('76', null, '5', '100(1,000)', '60000.00', '0', 'CASH', '15', '5.00', '74', '3', null);
INSERT INTO `purchasedetails` VALUES ('77', null, '5', '200(1000)', '190000.00', '0', 'CASH', '15', '5.00', '75', '3', null);
INSERT INTO `purchasedetails` VALUES ('78', null, '5', '200(1000)', '150000.00', '5000', '2 YRS', '15', '5.00', '77', '3', null);
INSERT INTO `purchasedetails` VALUES ('79', null, '78', '200', '188400.00', '6600', '2 YRS', '25', '5.00', '85', '3', null);
INSERT INTO `purchasedetails` VALUES ('80', null, '56', '200', '188400.00', '6600', '2 YRS', '17', '10.00', '86', '3', null);
INSERT INTO `purchasedetails` VALUES ('81', null, '40', '200', '188400.00', '6600', '2 YRS', '24', '10.00', '87', '3', null);
INSERT INTO `purchasedetails` VALUES ('82', null, '11', '200', '259984.00', '9166', '2 yrs', '24', '10.00', '88', '3', null);
INSERT INTO `purchasedetails` VALUES ('83', null, '51', '200', '338800.00', '8300', '3 YRS', '18', '7.00', '89', '3', null);
INSERT INTO `purchasedetails` VALUES ('84', null, '90,92', '200', '120000.00', '0', 'CASH', '12', '5.00', '90', '3', null);
INSERT INTO `purchasedetails` VALUES ('85', null, '143', '100', '60000.00', '2500', '2 YRS', '25', '5.00', '90', '3', null);
INSERT INTO `purchasedetails` VALUES ('86', null, '162,163', '200', '188400.00', '6600', '2 YRS', '13', '10.00', '91', '3', null);
INSERT INTO `purchasedetails` VALUES ('87', null, '164-165', '200', '152200.00', '5600', '1 YR', '13', '10.00', '92', '3', null);
INSERT INTO `purchasedetails` VALUES ('88', null, '166-167', '200', '140000.00', '0', 'CASH', '13', '10.00', '93', '3', null);
INSERT INTO `purchasedetails` VALUES ('89', null, '122', '100', '94200.00', '3300', '2 YRS', '23', '10.00', '95', '3', null);
INSERT INTO `purchasedetails` VALUES ('90', null, '21', '200', '164200.00', '3925', 'CASH/2 YRS', '23', '10.00', '96', '3', null);
INSERT INTO `purchasedetails` VALUES ('91', null, '13', '200', '240000.00', '8332', '1 YR', '25', '5.00', '97', '3', null);
INSERT INTO `purchasedetails` VALUES ('92', null, '26', '200', '188400.00', '6600', '2 YRS', '28', '10.00', '99', '3', null);
INSERT INTO `purchasedetails` VALUES ('93', null, '18,20,22', '600', '565200.00', '19800', '2 YRS', '13', '10.00', '100', '3', null);
INSERT INTO `purchasedetails` VALUES ('94', null, '37,38', '200', '280000.00', '0', 'CASH', '13', '10.00', '101', '3', null);
INSERT INTO `purchasedetails` VALUES ('95', null, '23', '100', '94200.00', '3300', '2 YRS', '19', '10.00', '102', '3', null);
INSERT INTO `purchasedetails` VALUES ('96', null, '23', '104', '155584.00', '10800', '2 YRS', '17', '7.00', '102', '3', null);
INSERT INTO `purchasedetails` VALUES ('97', null, '23', '100', '94200.00', '3300', '2 YRS', '19', '10.00', '103', '3', null);
INSERT INTO `purchasedetails` VALUES ('98', null, '23', '104', '155584.00', '10800', '2 YRS', '17', '7.00', '103', '3', null);
INSERT INTO `purchasedetails` VALUES ('99', null, '61', '100', '149400.00', '3300', '2 YRS', '13', '10.00', '104', '3', null);
INSERT INTO `purchasedetails` VALUES ('100', null, '73', '200', '188400.00', '6600', '2 YRS', '22', '10.00', '105', '3', null);
INSERT INTO `purchasedetails` VALUES ('101', null, '139', '100', '94200.00', '3300', '2 YRS', '25', '5.00', '106', '3', null);
INSERT INTO `purchasedetails` VALUES ('102', null, '62', '200', '338800.00', '8300', '3 YRS', '10', '7.00', '108', '3', null);
INSERT INTO `purchasedetails` VALUES ('103', null, '146,147', '200', '150000.00', '5000', '2 YRS', '25', '5.00', '109', '3', null);
INSERT INTO `purchasedetails` VALUES ('104', null, '119', '100', '90000.00', '0', 'Dec. 2021', '27', '10.00', '111', '3', null);
INSERT INTO `purchasedetails` VALUES ('105', null, '24', '207', '350658.00', '8591', '3 YRS', '19', '7.00', '112', '3', null);
INSERT INTO `purchasedetails` VALUES ('106', null, '32', '200', '188400.00', '6600', '2 YRS', '27', '10.00', '113', '3', null);
INSERT INTO `purchasedetails` VALUES ('107', null, '6', '500', '748000.00', '27000', '2 YRS', '19', '10.00', '114', '3', null);
INSERT INTO `purchasedetails` VALUES ('108', null, '75', '200', '188400.00', '6600', '2 YRS', '27', '10.00', '117', '3', null);
INSERT INTO `purchasedetails` VALUES ('109', null, '112', '100', '75000.00', '2500', '2 YRS', '25', '5.00', '118', '3', null);
INSERT INTO `purchasedetails` VALUES ('110', null, '5', '200(1000)', '75000.00', '2500', '2 YRS', '15', '5.00', '120', '3', null);
INSERT INTO `purchasedetails` VALUES ('111', null, '68', '200', '188400.00', '6306', '2 yrs', '6', '10.00', '122', '3', null);
INSERT INTO `purchasedetails` VALUES ('112', null, '84,86', '200', '141000.00', '5500', '1 yr', '13', '10.00', '123', '3', null);
INSERT INTO `purchasedetails` VALUES ('113', null, '127', '100', '82200.00', '5600', '1 YR', '27', '10.00', '125', '3', null);
INSERT INTO `purchasedetails` VALUES ('114', null, '36', '200', '220000.00', '0', '', '20', '7.00', '126', '3', null);
INSERT INTO `purchasedetails` VALUES ('115', null, '12', '200', '140000.00', '0', 'CASH', '6', '10.00', '128', '3', null);
INSERT INTO `purchasedetails` VALUES ('116', null, '16', '199', '70000.00', '0', 'CASH', '27', '10.00', '131', '3', null);
INSERT INTO `purchasedetails` VALUES ('117', null, '126', '100', '94200.00', '3300', '2 YRS', '25', '5.00', '132', '3', null);
INSERT INTO `purchasedetails` VALUES ('118', null, '115', '100', '79000.00', '5600', '9 MOS', '6', '10.00', '133', '3', null);
INSERT INTO `purchasedetails` VALUES ('119', null, '39', '200', '188400.00', '6600', '2 YRS', '16', '5.00', '134', '3', null);
INSERT INTO `purchasedetails` VALUES ('120', null, '118', '100', '70000.00', '0', 'CASH', '24', '5.00', '135', '3', null);
INSERT INTO `purchasedetails` VALUES ('121', null, '57,58', '210+100', '217000.00', '0', 'CASH', '13', '10.00', '136', '3', null);
INSERT INTO `purchasedetails` VALUES ('122', null, '121,123', '200', '164400.00', '11200', '2 YRS', '25', '5.00', '138', '3', null);
INSERT INTO `purchasedetails` VALUES ('123', null, '4', '1,000', '200000.00', '0', 'CASH', '15', '5.00', '139', '3', null);
INSERT INTO `purchasedetails` VALUES ('124', null, '81', '200', '191200.00', '6300', '2 yrs', '13', '10.00', '140', '3', null);
INSERT INTO `purchasedetails` VALUES ('125', null, '72', '200', '152200.00', '0', '6 MOS', '24', '10.00', '142', '3', null);
INSERT INTO `purchasedetails` VALUES ('126', null, '138', '100', '75000.00', '2500', '1 YR', '25', '5.00', '143', '3', null);
INSERT INTO `purchasedetails` VALUES ('127', null, '41', '200', '198400.00', '6600', '2 YRS', '24', '10.00', '144', '3', null);
INSERT INTO `purchasedetails` VALUES ('128', null, '79', '200', '120000.00', '0', 'CASH', '25', '5.00', '145', '3', null);
INSERT INTO `purchasedetails` VALUES ('129', null, '30', '200', '188400.00', '6600', '2 YRS', '28', '10.00', '146', '3', null);
INSERT INTO `purchasedetails` VALUES ('130', null, '45', '200', '188400.00', '6600', '2 YRS', '6', '10.00', '147', '3', null);
INSERT INTO `purchasedetails` VALUES ('131', null, '120', '100', '94200.00', '3300', '2 YRS', '22', '10.00', '148', '3', null);
INSERT INTO `purchasedetails` VALUES ('132', null, '25', '200', '188400.00', '6600', '2 YRS', '28', '10.00', '151', '3', null);
INSERT INTO `purchasedetails` VALUES ('133', null, '102', '100', '94200.00', '3300', '2 YRS', '7', '10.00', '153', '3', null);
INSERT INTO `purchasedetails` VALUES ('134', null, '19', '200', '240000.00', '8332', '2 YRS', '25', '5.00', '154', '3', null);
INSERT INTO `purchasedetails` VALUES ('135', null, '32', '100', '160004.00', '3889', '3 YRS', '26', '7.00', '158', '4', null);
INSERT INTO `purchasedetails` VALUES ('136', null, '32', '100', '160004.00', '3889', '3 YRS', '26', '7.00', '158', '4', null);
INSERT INTO `purchasedetails` VALUES ('137', null, '39', '100', '200004.00', '3333', '5 YRS/SM', '6', '7.00', '161', '4', null);
INSERT INTO `purchasedetails` VALUES ('138', null, '39', '100', '200004.00', '3333', '5 YRS/SM', '6', '7.00', '161', '4', null);
INSERT INTO `purchasedetails` VALUES ('139', null, '55,56', '400', '859904.00', '10832', '6 YRS', '7', '7.00', '162', '4', null);
INSERT INTO `purchasedetails` VALUES ('140', null, '55,56', '400', '859904.00', '10832', '6 YRS', '7', '7.00', '162', '4', null);
INSERT INTO `purchasedetails` VALUES ('141', null, '15', '300', '510000.00', '0', 'CASH', '2', '7.00', '164', '4', null);
INSERT INTO `purchasedetails` VALUES ('142', null, '15', '300', '510000.00', '0', 'CASH', '2', '7.00', '164', '4', null);
INSERT INTO `purchasedetails` VALUES ('143', null, '9,10', '500', '1030024.00', '25834', '3 YRS', '2', '7.00', '170', '4', null);
INSERT INTO `purchasedetails` VALUES ('144', null, '9,10', '500', '1030024.00', '25834', '3 YRS', '2', '7.00', '170', '4', null);
INSERT INTO `purchasedetails` VALUES ('145', null, '28', '200', '370120.00', '9170', '3 YRS', '17', '10.00', '173', '5', null);
INSERT INTO `purchasedetails` VALUES ('146', null, '26', '200', '310000.00', '22500', '1 YR', '22', '10.00', '178', '5', null);
INSERT INTO `purchasedetails` VALUES ('147', null, '14', '200', '370120.00', '9170', '3 YRS', '22', '10.00', '179', '5', null);
INSERT INTO `purchasedetails` VALUES ('148', null, '16', '200', '370120.00', '9170', '3 YRS', '22', '10.00', '180', '5', null);
INSERT INTO `purchasedetails` VALUES ('149', null, '7', '137', '171304.00', '12562', '1 YR', '22', '10.00', '180', '5', null);
INSERT INTO `purchasedetails` VALUES ('150', null, '41', '100', '155040.00', '3890', '3 YRS', '27', '10.00', '181', '5', null);
INSERT INTO `purchasedetails` VALUES ('151', null, '44', '200', '310080.00', '7780', '3 YRS', '22', '10.00', '182', '5', null);
INSERT INTO `purchasedetails` VALUES ('152', null, '45', '200', '310080.00', '7780', '3 YRS', '22', '10.00', '183', '5', null);
INSERT INTO `purchasedetails` VALUES ('153', null, '43', '151', '234110.00', '11747', '3 YRS', '6', '10.00', '185', '5', null);
INSERT INTO `purchasedetails` VALUES ('154', null, '42', '100', '155040.00', '7780', '3 YRS', '6', '10.00', '186', '5', null);
INSERT INTO `purchasedetails` VALUES ('155', null, '3', '200', '310080.00', '7780', '3 YRS', '7', '10.00', '187', '5', null);
INSERT INTO `purchasedetails` VALUES ('156', null, '23', '200', '250080.00', '18340', '2 YRS', '22', '10.00', '188', '5', null);
INSERT INTO `purchasedetails` VALUES ('157', null, '32', '100', '155040.00', '3890', '3 YRS', '22', '10.00', '193', '5', null);
INSERT INTO `purchasedetails` VALUES ('158', null, '17', '200', '220000.00', '18340', '1 YR', '22', '10.00', '194', '5', null);
INSERT INTO `purchasedetails` VALUES ('159', null, '30', '100', '185060.00', '4585', '3 YRS', '22', '10.00', '195', '5', null);
INSERT INTO `purchasedetails` VALUES ('160', null, '18', '200', '370120.00', '9170', '3 YRS', '6', '10.00', '197', '5', null);
INSERT INTO `purchasedetails` VALUES ('161', null, '4', '200', '310080.00', '7780', '3 YRS', '6', '10.00', '198', '5', null);
INSERT INTO `purchasedetails` VALUES ('162', null, '31', '100', '110000.00', '3890', '3 YRS', '22', '10.00', '199', '5', null);
INSERT INTO `purchasedetails` VALUES ('163', null, '46', '200', '310080.00', '7780', '3 YRS', '22', '10.00', '201', '5', null);
INSERT INTO `purchasedetails` VALUES ('164', null, '13', '200', '212404.00', '7780', '3 YRS', '22', '10.00', '204', '5', null);
INSERT INTO `purchasedetails` VALUES ('165', null, '95', '100', '94200.00', '3300', '2 YRS', '30', '10.00', '58', '3', null);
INSERT INTO `purchasedetails` VALUES ('166', null, '5', '200 (1000)', '299200.00', '10800', '2 YRS', '31', '5.00', '76', '3', null);
INSERT INTO `purchasedetails` VALUES ('167', null, '111', '100', '94200.00', '3300', '2 YRS', '32', '10.00', '78', '3', null);
INSERT INTO `purchasedetails` VALUES ('168', null, '133', '100', '110000.00', '0', 'CASH', '33', '7.00', '79', '3', null);
INSERT INTO `purchasedetails` VALUES ('169', null, '52', '200', '299200.00', '10800', '2 YRS', '34', '10.00', '80', '3', null);
INSERT INTO `purchasedetails` VALUES ('170', null, '80', '200', '180400.00', '6600', '2 YRS', '35', '10.00', '81', '3', null);
INSERT INTO `purchasedetails` VALUES ('171', null, '130', '100', '149600.00', '5400', '2 YRS', '34', '10.00', '82', '3', null);
INSERT INTO `purchasedetails` VALUES ('172', null, '128', '100', '149600.00', '5400', '2 YRS', '34', '10.00', '82', '3', null);
INSERT INTO `purchasedetails` VALUES ('173', null, '154,155', '200', '140000.00', '0', 'CASH', '36', '5.00', '83', '3', null);
INSERT INTO `purchasedetails` VALUES ('174', null, '156-159', '400', '280000.00', '0', 'CASH', '37', '10.00', '84', '3', null);
INSERT INTO `purchasedetails` VALUES ('175', null, '98,99', '200', '140000.00', '0', 'CASH', '32', '10.00', '94', '3', null);
INSERT INTO `purchasedetails` VALUES ('176', null, '116', '100', '110000.00', '0', 'CASH', '34', '7.00', '98', '3', null);
INSERT INTO `purchasedetails` VALUES ('177', null, '117', '100', '75500.00', '0', '7 MOS', '32', '10.00', '107', '3', null);
INSERT INTO `purchasedetails` VALUES ('178', null, '44', '200', '129000.00', '0', 'CASH', '38', '10.00', '110', '3', null);
INSERT INTO `purchasedetails` VALUES ('179', null, '129', '100', '149600.00', '5400', '2 YRS', '39', '10.00', '115', '3', null);
INSERT INTO `purchasedetails` VALUES ('180', null, '28', '200', '180000.00', '0', 'CASH', '40', '5.00', '116', '3', null);
INSERT INTO `purchasedetails` VALUES ('181', null, '42', '200', '140000.00', '0', 'CASH', '41', '10.00', '119', '3', null);
INSERT INTO `purchasedetails` VALUES ('182', null, '113', '100', '94200.00', '3300', '2 YRS', '37', '5.00', '121', '3', null);
INSERT INTO `purchasedetails` VALUES ('183', null, '96', '100', '94200.00', '3300', '2 yrs/June 2022', '41', '10.00', '124', '3', null);
INSERT INTO `purchasedetails` VALUES ('184', null, '15', '200', '259600.00', '5400', '2 YRS', '42', '10.00', '127', '3', null);
INSERT INTO `purchasedetails` VALUES ('185', null, '153', '100', '94200.00', '3300', '2 YRS', '42', '10.00', '129', '3', null);
INSERT INTO `purchasedetails` VALUES ('186', null, '152', '100', '94200.00', '3300', '2 yrs', '43', '10.00', '130', '3', null);
INSERT INTO `purchasedetails` VALUES ('187', null, '109', '100', '75000.00', '2500', '2 YRS', '44', '5.00', '137', '3', null);
INSERT INTO `purchasedetails` VALUES ('188', null, '17', '200', '259984.00', '9166', '2 YRS', '42', '10.00', '141', '3', null);
INSERT INTO `purchasedetails` VALUES ('189', null, '114', '100', '75000.00', '2500', '2 YRS', '44', '5.00', '149', '3', null);
INSERT INTO `purchasedetails` VALUES ('190', null, '14', '200', '259984.00', '9166', '2 YRS', '45', '10.00', '150', '3', null);
INSERT INTO `purchasedetails` VALUES ('191', null, '51', '200', '299200.00', '10800', '2 YRS', '34', '7.00', '152', '3', null);
INSERT INTO `purchasedetails` VALUES ('192', null, '14,16,18', '600', '690000.00', '16250', '2 YRS', '46', '7.00', '155', '4', null);
INSERT INTO `purchasedetails` VALUES ('193', null, '14,16,18', '600', '690000.00', '16250', '2 YRS', '46', '7.00', '155', '4', null);
INSERT INTO `purchasedetails` VALUES ('194', null, '8', '200', '339952.00', '8332', '3 YRS', '47', '7.00', '156', '4', null);
INSERT INTO `purchasedetails` VALUES ('195', null, '8', '200', '339952.00', '8332', '3 YRS', '47', '7.00', '156', '4', null);
INSERT INTO `purchasedetails` VALUES ('196', null, '19', '200', '310000.00', '11250', '2 YRS', '47', '7.00', '157', '4', null);
INSERT INTO `purchasedetails` VALUES ('197', null, '19', '200', '310000.00', '11250', '2 YRS', '47', '7.00', '157', '4', null);
INSERT INTO `purchasedetails` VALUES ('198', null, '11,12', '500', '920056.00', '34169', '2 YRS', '48', '5.00', '159', '4', null);
INSERT INTO `purchasedetails` VALUES ('199', null, '11,12', '500', '920056.00', '34169', '2 YRS', '48', '5.00', '159', '4', null);
INSERT INTO `purchasedetails` VALUES ('200', null, '3,4,6', '700', '1330088.00', '33058', '3 YRS', '47', '7.00', '160', '4', null);
INSERT INTO `purchasedetails` VALUES ('201', null, '3,4,6', '700', '1330088.00', '33058', '3 YRS', '47', '7.00', '160', '4', null);
INSERT INTO `purchasedetails` VALUES ('202', null, '7', '100 (300)', '170000.00', '0', 'CASH', '47', '7.00', '163', '4', null);
INSERT INTO `purchasedetails` VALUES ('203', null, '7', '200 (300)', '340000.00', '28334', 'CASH', '47', '7.00', '163', '4', null);
INSERT INTO `purchasedetails` VALUES ('204', null, '7', '100 (300)', '170000.00', '0', 'CASH', '47', '7.00', '163', '4', null);
INSERT INTO `purchasedetails` VALUES ('205', null, '7', '200 (300)', '340000.00', '28334', 'CASH', '47', '7.00', '163', '4', null);
INSERT INTO `purchasedetails` VALUES ('206', null, '1,2', '500', '1010080.00', '25280', '3 YRS', '47', '7.00', '165', '4', null);
INSERT INTO `purchasedetails` VALUES ('207', null, '1,2', '500', '1010080.00', '25280', '3 YRS', '47', '7.00', '165', '4', null);
INSERT INTO `purchasedetails` VALUES ('208', null, '68', '200', '320008.00', '7778', '3 YRS', '49', '3.50', '166', '4', null);
INSERT INTO `purchasedetails` VALUES ('209', null, '68', '200', '320008.00', '7778', '3 YRS', '49', '3.50', '166', '4', null);
INSERT INTO `purchasedetails` VALUES ('210', null, '5', '300', '690072.00', '17502', '3 YRS', '50', '7.00', '167', '4', null);
INSERT INTO `purchasedetails` VALUES ('211', null, '5', '300', '690072.00', '17502', '3 YRS', '50', '7.00', '167', '4', null);
INSERT INTO `purchasedetails` VALUES ('212', null, '40', '100', '215004.00', '2986', '6 YRS/SM', '47', '7.00', '168', '4', null);
INSERT INTO `purchasedetails` VALUES ('213', null, '17', '312', '717675.00', '18202', '3 YRS', '33', '7.00', '169', '4', null);
INSERT INTO `purchasedetails` VALUES ('214', null, '40', '100', '215004.00', '2986', '6 YRS/SM', '47', '7.00', '168', '4', null);
INSERT INTO `purchasedetails` VALUES ('215', null, '17', '312', '717675.00', '18202', '3 YRS', '33', '7.00', '169', '4', null);
INSERT INTO `purchasedetails` VALUES ('216', null, '41', '100', '115000.00', '0', 'CASH', '47', '7.00', '171', '4', null);
INSERT INTO `purchasedetails` VALUES ('217', null, '41', '100', '115000.00', '0', 'CASH', '47', '7.00', '171', '4', null);
INSERT INTO `purchasedetails` VALUES ('218', null, '37,38', '200', '310080.00', '7780', '3 YRS', '51', '10.00', '172', '5', null);
INSERT INTO `purchasedetails` VALUES ('219', null, '27', '200', '310080.00', '7780', '3 YRS', '52', '5.00', '173', '5', null);
INSERT INTO `purchasedetails` VALUES ('220', null, '29', '200', '370120.00', '9170', '3 YRS', '52', '5.00', '173', '5', null);
INSERT INTO `purchasedetails` VALUES ('221', null, '11', '200', '310080.00', '7780', '3 YRS', '51', '10.00', '174', '5', null);
INSERT INTO `purchasedetails` VALUES ('222', null, '25', '200', '310080.00', '7780', '3 YRS', '51', '10.00', '175', '5', null);
INSERT INTO `purchasedetails` VALUES ('223', null, '20', '200', '280000.00', '0', 'CASH', '53', '10.00', '176', '5', null);
INSERT INTO `purchasedetails` VALUES ('224', null, '40', '100', '155040.00', '3890', '3 YRS', '52', '10.00', '177', '5', null);
INSERT INTO `purchasedetails` VALUES ('225', null, '19', '200', '310080.00', '7780', '3 YRS', '37', '5.00', '184', '5', null);
INSERT INTO `purchasedetails` VALUES ('226', null, '8', '200', '220000.00', '0', 'CASH/DEC 21', '54', '7.00', '189', '5', null);
INSERT INTO `purchasedetails` VALUES ('227', null, '9', '200', '220000.00', '0', 'CASH/DEC 21', '54', '7.00', '190', '5', null);
INSERT INTO `purchasedetails` VALUES ('228', null, '39', '100', '140040.00', '3890', '2 YRS', '52', '5.00', '191', '5', null);
INSERT INTO `purchasedetails` VALUES ('229', null, '34', '100', '155040.00', '3890', '3 YRS', '51', '10.00', '192', '5', null);
INSERT INTO `purchasedetails` VALUES ('230', null, '24', '200', '370120.00', '9170', '3 YRS', '55', '10.00', '196', '5', null);
INSERT INTO `purchasedetails` VALUES ('231', null, '22', '200', '370100.00', '8586', '3 YRS', '56', '10.00', '200', '5', null);
INSERT INTO `purchasedetails` VALUES ('232', null, '15', '200', '250080.00', '18340', '1 YR', '38', '10.00', '202', '5', null);
INSERT INTO `purchasedetails` VALUES ('233', null, '21,35', '200', '325000.00', '0', 'CASH', '51', '10.00', '203', '5', null);
INSERT INTO `purchasedetails` VALUES ('234', null, '33', '100', '155040.00', '3890', '3 YRS', '55', '10.00', '205', '5', null);
INSERT INTO `purchasedetails` VALUES ('235', null, '12', '200', '370120.00', '9170', '3 YRS', '56', '10.00', '206', '5', null);
INSERT INTO `purchasedetails` VALUES ('236', null, '10', '200', '280000.00', '0', 'CASH', '56', '10.00', '207', '5', null);
INSERT INTO `purchasedetails` VALUES ('237', null, '36', '100', '155040.00', '3890', '3 YRS', '37', '5.00', '208', '5', null);

-- ----------------------------
-- Table structure for test
-- ----------------------------
DROP TABLE IF EXISTS `test`;
CREATE TABLE `test` (
  `id` int(11) NOT NULL,
  `c1` varchar(45) DEFAULT NULL,
  `c2` tinyint(1) DEFAULT NULL,
  `c3` tinyint(1) DEFAULT NULL,
  `c4` tinyint(1) DEFAULT NULL,
  `c5` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of test
-- ----------------------------
INSERT INTO `test` VALUES ('10009', '2022-10-21 08:07:27', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('10041', '2022-10-28 13:40:47', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('10046', '2022-10-24 19:42:13', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('10065', '2022-10-28 12:02:26', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('10071', '2022-10-28 12:51:52', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('10075', '2022-10-28 12:40:07', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('10082', '2022-10-27 17:18:48', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('10088', '2022-10-18 12:29:27', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('10110', '2022-10-28 12:17:51', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('10115', '2022-10-18 12:28:36', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('12054', '2022-10-28 07:56:05', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('12055', '2022-10-28 16:24:54', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('12056', '2022-10-27 07:58:04', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13001', '2022-10-28 17:46:03', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13002', '2022-10-28 12:11:16', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13006', '2022-10-17 07:21:13', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13007', '2022-10-28 17:13:55', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13009', '2022-10-28 17:23:03', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13011', '2022-10-27 17:34:17', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13013', '2022-10-28 07:16:15', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13018', '2022-10-28 17:14:08', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13020', '2022-10-12 17:36:09', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13024', '2022-10-28 07:32:50', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13025', '2022-10-28 12:11:22', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13027', '2022-10-12 17:35:43', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13032', '2022-10-28 12:32:55', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13033', '2022-10-28 12:51:25', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13035', '2022-10-28 06:21:52', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13042', '2022-10-26 07:12:45', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13043', '2022-10-27 07:52:57', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13050', '2022-10-26 17:38:45', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13051', '2022-10-03 07:47:04', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13053', '2022-10-28 12:23:08', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13062', '2022-10-28 17:29:01', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13065', '2022-10-25 17:52:44', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13071', '2022-10-28 07:53:09', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13072', '2022-10-27 17:09:11', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13080', '2022-10-28 12:17:37', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13081', '2022-10-27 06:16:16', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13082', '2022-10-28 13:01:11', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13083', '2022-10-28 12:04:13', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13087', '2022-10-24 06:31:09', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13090', '2022-10-28 07:53:53', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13091', '2022-10-28 12:16:56', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13092', '2022-10-28 12:24:03', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13093', '2022-10-28 12:22:20', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13094', '2022-10-28 17:10:06', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13101', '2022-10-28 12:12:06', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13109', '2022-10-29 12:34:16', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13113', '2022-10-26 06:15:32', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13120', '2022-10-28 07:22:02', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13124', '2022-10-31 17:08:01', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13126', '2022-10-19 07:48:47', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13140', '2022-10-28 08:22:51', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13149', '2022-10-13 08:03:09', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13152', '2022-10-20 07:55:58', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13158', '2022-10-28 12:14:41', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13163', '2022-10-28 12:15:33', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13169', '2022-10-24 12:14:05', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13172', '2022-10-27 20:07:03', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13175', '2022-10-31 17:08:06', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13177', '2022-10-28 08:22:46', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13178', '2022-10-28 18:25:05', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13182', '2022-10-28 07:57:44', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13185', '2022-10-28 08:27:15', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13190', '2022-10-24 18:40:08', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13193', '2022-10-28 12:17:01', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13196', '2022-10-27 07:40:38', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13198', '2022-10-28 12:41:41', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13199', '2022-10-25 12:10:26', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('13200', '2022-10-28 12:18:21', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('13201', '2022-10-31 17:33:31', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('14036', '2022-10-28 12:12:01', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('14038', '2022-10-12 17:07:34', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('14039', '2022-10-27 17:02:43', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('14041', '2022-10-28 07:39:11', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('14042', '2022-10-28 12:33:40', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('14045', '2022-10-28 14:41:22', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('14046', '2022-10-28 05:38:22', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('14073', '2022-10-28 12:17:44', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('20229', '2022-10-28 12:15:48', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22001', '2022-10-03 07:57:30', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22002', '2022-10-25 12:11:01', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22003', '2022-10-28 09:14:07', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22005', '2022-10-28 12:15:41', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22007', '2022-10-07 07:14:41', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22008', '2022-10-28 12:08:18', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22009', '2022-10-28 07:32:16', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22010', '2022-10-28 12:24:09', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22020', '2022-10-28 12:12:11', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22031', '2022-10-28 12:14:54', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22041', '2022-10-28 12:15:58', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22042', '2022-10-03 07:35:48', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22045', '2022-10-28 12:57:03', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('22056', '2022-10-24 12:02:06', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('25920', '2022-10-03 08:08:52', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('26079', '2022-10-03 07:54:41', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('26288', '2022-10-03 08:08:49', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('30131', '2022-10-27 17:26:20', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('30132', '2022-10-28 17:14:16', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('30134', '2022-10-26 17:49:25', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('30138', '2022-10-12 07:43:28', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('30139', '2022-10-12 12:20:54', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('50004', '2022-10-25 17:11:10', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('50006', '2022-10-28 17:57:43', '1', '1', '1', '0');
INSERT INTO `test` VALUES ('50007', '2022-10-28 08:04:23', '1', '0', '1', '0');
INSERT INTO `test` VALUES ('50008', '2022-10-19 08:00:16', '1', '0', '1', '0');

-- ----------------------------
-- Table structure for tmppayments
-- ----------------------------
DROP TABLE IF EXISTS `tmppayments`;
CREATE TABLE `tmppayments` (
  `location` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `amount` varchar(255) DEFAULT NULL,
  `datepaid` varchar(255) DEFAULT NULL,
  `sqm` varchar(255) DEFAULT NULL,
  `remarks` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of tmppayments
-- ----------------------------
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '20000', '2020-07-18', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '11600', '2020-08-29', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2020-09-29', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2020-10-27', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2020-12-03', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2020-12-30', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2021-02-01', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2021-03-03', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2021-04-30', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2021-04-30', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2021-05-27', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '7000', '2021-06-28', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '6400', '2021-07-27', '158', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Amable, Yolly Mae N', '500', '', '158', 'DAS');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '5000', '2020-08-05', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '4700', '2020-08-08', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '2800', '2020-08-26', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '3000', '2020-09-07', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '3000', '2020-09-16', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '4000', '2020-11-04', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '6300', '2021-01-04', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '8900', '2021-01-27', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '13800', '2021-03-06', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '5100', '2021-05-15', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '6100', '2021-08-14', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '3900', '2021-09-30', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Badoria, Martin Miguel V', '5900', '2021-12-28', '209', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5000', '2020-07-11', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '25500', '2020-09-15', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '10000', '2020-09-22', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2020-11-03', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2020-12-05', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2021-01-05', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2021-02-01', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2021-03-03', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2021-04-10', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2021-05-13', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2021-07-10', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2021-08-09', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '10100', '2021-10-06', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2021-11-03', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2022-01-04', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2022-03-04', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5100', '2022-05-03', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2022-08-03', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Capillo, Queenilyn L', '5050', '2022-10-05', '202', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '5000', '2020-07-23', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6800', '2020-07-25', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '34500', '2020-09-07', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '5900', '2020-10-05', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2020-10-31', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2020-12-08', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-01-04', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-02-09', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-03-11', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-04-17', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-05-24', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-07-05', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-08-12', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-09-02', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-10-11', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-11-19', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2021-12-04', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2022-01-08', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2022-02-07', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2022-03-02', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2022-05-06', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Deita, Anamae (Deita,Cesar)', '6000', '2022-06-08', '236', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '20000', '2020-07-31', '500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '480000', '2020-09-07', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2020-07-31', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2020-12-02', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2020-12-28', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-01-22', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-03-02', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-03-31', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-04-30', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '50000', '2021-06-05', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '150000', '2021-07-03', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-08-12', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-09-08', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-10-07', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-11-04', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-12-04', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2021-12-23', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '110000', '2022-01-26', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '100000', '2022-02-26', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '127380', '2022-04-04', '1,500', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Morales, Lea B', '4000', '', '1,500', 'DAS');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '5000', '2020-07-18', '296', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '5000', '2020-07-21', '296', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '50000', '2020-08-04', '296', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '30000', '2020-09-26', '296', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '40000', '2020-10-12', '296', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '65000', '2020-10-30', '296', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '60000', '2020-11-17', '296', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '52094', '2020-12-01', '296', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Panaligan, Jessie', '1000', '', '296', 'DAS');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Soriano, Dolly', '5000', '2021-07-03', '180', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '25000', '2020-07-18', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '9000', '2020-08-13', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '6375', '2020-09-17', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '6375', '2020-10-19', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '6375', '2020-11-25', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '7000', '2021-03-03', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '6500', '2021-03-31', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '9500', '2022-02-02', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '9500', '2022-03-01', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '9500', '2022-03-29', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Trinidad, Jeramae C', '10000', '2022-09-06', '170', '');
INSERT INTO `tmppayments` VALUES ('Sto Nino (Tupaz)', 'Veraque, Marvin', '10000', '2020-07-21', '279', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Agcan, Nilbert', '40,000', '2022-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Felongco, Maymay ', '10,000', '2022-08-11', '400 ', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Frias, Shirley Yap', '5,000', '2021-07-02', '200 ', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Frias, Shirley Yap', '20,000 ', '2021-08-12', '200 ', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Frias, Shirley Yap', '21,500', '2021-08-26', '200 ', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Frias, Shirley Yap', '4,800', '2021-10-07', '200 ', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Frias, Shirley Yap', '10,800', '2021-11-09', '200 ', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Frias, Shirley Yap', '10,800', '2022-05-12', '200 ', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Funtecha, Felma L.', '20,000', '2022-05-20', '400 ', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Jamerlarin, Jenelyn D', '16,000', '', '200', 'kolambog a');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Jamerlarin, Jenelyn D', '8,000', '2021-05-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Jamerlarin, Jenelyn D', '8,000', '2021-06-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Jamerlarin, Jenelyn D', '8,000', '2021-09-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Layos, Edison', '0', '2022-03-04', '400', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Layos, Edison', '5,000', '2022-03-05', '400', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Layos, Edison', '20,000', '2022-06-16', '400', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Ortiz, Ailen Carroll', '5,000', '2021-07-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Ortiz, Ailen Carroll', '20,000', '2021-08-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Ortiz, Ailen Carroll', '15,000', '2021-08-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Ortiz, Ailen Carroll', '10,800', '2021-10-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Ortiz, Ailen Carroll', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Ortiz, Ailen Carroll', '11,000', '2021-11-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Rapista, Roel F.', '5,000', '2022-08-11', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Rapista, Roel F.', '10,000', '2022-08-12', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Rapista, Roel F.', '30,000', '2022-10-10', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Sevilla, Marvie C', '10,000', '2021-05-15', '400', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Sevilla, Marvie C', '30,000', '2021-06-08', '400', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Sevilla, Marvie C', '40,000', '2021-07-15', '400', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Sevilla, Marvie C', '338,000', '2021-08-18', '400', '');
INSERT INTO `tmppayments` VALUES ('Norala (SuganobSiauso)', 'Sevilla, Marvie C', '1,000', '', '400', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Agtoto, Dennis L. ', '30000', '2020-08-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Agtoto, Dennis L. ', '9166', '2021-02-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Agtoto, Dennis L. ', '18000', '2022-03-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Aguilar, Gregy', '600', '', '100', 'DAS ');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Arcega, Cherryl', '5500', '2020-08-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Arcega, Cherryl', '5500', '2020-10-12', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Arcega, Cherryl', '5500', '2020-11-07', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Arcega, Cherryl', '5500', '2021-03-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Arcega, Michelle C. ', '500', '', '200', 'ARWU');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Arcega, Michelle C. ', '500', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Arcenas, Rodelyn B', '25000', '2020-08-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Arcenas, Rodelyn B', '11200', '2021-08-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Atuel, Grace D. ', '135000', '2022-04-21', '1000', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Atuel, Grace D. ', '1000', '', '1000', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Badjan, Josephine ', '20000', '2020-12-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '20000', '2020-08-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '11200', '2020-08-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '11200', '2020-09-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '11200', '2020-11-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '5600', '2021-01-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '5600', '2021-02-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '6000', '2021-03-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '7000', '2021-03-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '7000', '2021-03-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '5000', '2021-05-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '8000', '2021-06-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '12000', '2021-08-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '3000', '2022-01-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '5000', '2022-09-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bahandi, Johnson S. ', '7000', '2022-10-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ballera, Mafe S', '10000', '2020-08-05', '261', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ballera, Mafe S', '10000', '2020-09-17', '261', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ballera, Mafe S', '10000', '2020-11-18', '261', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ballera, Mafe S', '10000', '2020-12-15', '261', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Belarmino, Jerry Van E. ', '30000', '2020-08-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2020-08-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '10100', '2020-10-10', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '4500', '2020-11-20', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '4600', '2020-12-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '2500', '2021-02-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '1250', '2021-02-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2021-03-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '500', '', '100', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '4600', '2021-03-31', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2021-05-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2021-06-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '750', '2021-06-05', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2021-07-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2021-09-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2021-11-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2021-12-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '10000', '2022-02-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2022-03-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2022-04-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '10000', '2022-06-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '5000', '2022-07-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '10000', '2022-09-13', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Benjamin, Crissa Mae', '11692', '2022-10-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Berdin, Joevelyn T', '25000', '2020-09-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Berdin, Joevelyn T', '70000', '2020-10-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Berdin, Joevelyn T', '30000', '2020-12-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Marcelita (Rhea)', '10800', '2021-04-12', '201', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Marcelita (Rhea)', '10800', '2021-05-31', '201', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Marcelita (Rhea)', '10800', '2021-07-30', '201', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Marcelita (Rhea)', '21600', '2021-09-30', '201', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Marcelita (Rhea)', '21600', '2021-11-22', '201', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Marcelita (Rhea)', '21600', '2022-02-10', '201', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Marcelita (Rhea)', '21600', '2022-05-27', '201', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Meddy C.', '100000', '2022-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Meddy C.', '110000', '2022-02-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernabe, Meddy C.', '10800', '2022-03-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bernales, Kristine D', '75000', '2020-12-15', '200(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bornales, Jenny B.', '10000', '2020-08-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bornales, Jenny B.', '3300', '2020-09-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bravante, Marelou E', '20000', '2020-08-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bravante, Marelou E', '30000', '2022-03-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bravante, Marelou E', '30000', '2022-05-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Bravante, Marelou E', '20000', '2022-08-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Brillantes, Mary Jane (fr. Lumbay, Joan)', '5000', '2020-09-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Brillantes, Mary Jane (fr. Lumbay, Joan)', '5000', '2020-10-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Brillantes, Mary Jane (fr. Lumbay, Joan)', '3000', '2021-02-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Brillantes, Mary Jane (fr. Lumbay, Joan)', '7000', '2022-03-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Brillantes, Mary Jane (fr. Lumbay, Joan)', '5000', '2022-06-30', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Brillantes, Mary Jane (fr. Lumbay, Joan)', '8000', '2022-09-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Brillantes, Mary Jane (fr. Lumbay, Joan)', '20000', '2022-10-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cabales, Henry', '5600', '2020-08-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cabales, Henry', '5500', '2020-09-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cabales, Henry', '11200', '2020-12-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cabales, Henry', '11000', '2021-02-10', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cabales, Henry', '5500', '2021-04-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cabales, Henry', '11000', '2021-09-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cabales, Henry', '5500', '2021-12-14', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cagalaban, Mary Ann (fr. Dalisay)', '6000', '2020-08-31', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cagalaban, Mary Ann (fr. Dalisay)', '17500', '2020-11-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Calderon, Mae', '10000', '2020-07-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Calderon, Mae', '45000', '2020-08-31', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Calvo, Ritchie L', '15000', '2020-11-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Calvo, Ritchie L', '500', '', '100', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Calvo, Ritchie L', '10000', '2021-02-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Calvo, Ritchie L', '5000', '2021-08-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Canlas, Jovilyn D. ', '500', '', '100', 'dcs');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Canlas, Jovilyn D. ', '8000', '2020-10-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Canlas, Jovilyn D. ', '42000', '2021-11-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '10000', '2020-10-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '500', '', '100', 'dcs');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2020-12-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-01-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-02-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-03-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-04-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-05-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-06-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-07-31', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-08-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-10-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2021-12-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2022-01-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2022-03-10', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2022-05-05', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2022-06-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2022-08-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Capilitan, Reymark T', '3300', '2022-10-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casana, Polita, Dadivas,  Agrabante', '150000', '2020-08-20', '1000', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casana, Polita, Dadivas,  Agrabante', '650000', '2020-09-29', '1000', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casana, Polita, Dadivas,  Agrabante', '250000', '2020-11-07', '1000', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '13000', '2020-09-07', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '3300', '2020-11-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '6600', '2020-12-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '3300', '2021-01-12', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '24900', '2021-06-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '8000', '2021-09-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '9400', '2021-10-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '5700', '2022-05-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '5000', '2022-07-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casarte, Fevie (as. fr.Maxian, John Michael)', '10000', '2022-10-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elsa S. ', '11600', '2020-07-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elsa S. ', '10000', '2020-07-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elsa S. ', '15000', '2020-08-31', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elsa S. ', '15000', '2020-10-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elsa S. ', '3000', '2022-03-10', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elsa S. ', '10400', '2022-09-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elsa S. ', '24200', '2022-10-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elvie S. ', '20000', '2020-07-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elvie S. ', '10000', '2020-08-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elvie S. ', '10000', '2022-03-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elvie S. ', '5000', '2022-06-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elvie S. ', '10000', '2022-09-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Casiple, Elvie S. ', '5000', '2022-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Castromayor, Joven', '65000', '2020-08-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Castromayor, Joven', '300', '', '200', 'ARWU');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Castromayor, Joven', '70000', '2021-09-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Castromayor, Joven', '500', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Catedrilla, Alfred Lawrence A', '5000', '2021-10-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '10000', '2020-08-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2020-08-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2020-09-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2020-10-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2020-11-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2020-12-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2021-02-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2021-03-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2021-04-27', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2021-06-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2021-06-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2021-07-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '3300', '2021-09-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '4000', '2022-07-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '4000', '2022-07-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '4000', '2022-08-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '4000', '2022-09-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert H. ', '4000', '2022-10-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '10000', '2020-08-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '3300', '2020-08-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '7900', '2020-09-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '5600', '2020-11-14', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '3300', '2021-02-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '3300', '2021-03-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '3300', '2021-04-27', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '3300', '2021-06-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '3300', '2021-06-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '3300', '2021-07-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '3300', '2021-09-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '4000', '2022-07-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '4000', '2022-07-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '4000', '2022-08-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '4000', '2022-09-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Clavel, Jovert (Huinda, Juliard N.) ', '4000', '2022-10-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '20000', '2020-07-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '5000', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '5000', '2020-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '5000', '2021-02-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '15000', '2021-04-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '15000', '2021-07-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '15000', '2021-09-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '15000', '2022-05-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '15000', '2022-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '2500', '2020-08-31', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '5000', '2020-10-06', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '10000', '2021-02-04', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '12600', '2021-04-09', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '5000', '2021-07-21', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '9000', '2021-09-30', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '5900', '2022-04-28', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '5000', '2022-04-28', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Condino, Noeme F. ', '500', '', '100(1,000)', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Cordero, Rizel A', '10000', '2021-11-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dableo, Florante O', '100000', '2020-08-12', '1000', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dableo, Florante O', '500', '', '1000', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dableo, Florante O', '900000', '2020-09-29', '1000', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '12000', '2020-07-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '2500', '2020-09-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '2500', '2020-10-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '5000', '2020-11-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '2500', '2020-12-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '2500', '2021-03-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '5000', '2021-05-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '5000', '2021-10-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '5000', '2022-02-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dapitan, Ma. Edelyn G. ', '18000', '2022-06-10', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Datungputi, Rhea Mae P', '10000', '2020-09-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Datungputi, Rhea Mae P', '7000', '2020-10-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Datungputi, Rhea Mae P', '7500', '2020-12-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Datungputi, Rhea Mae P', '7000', '2021-02-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dela Cruz, Jonathan Solatorio', '35000', '2021-04-07', '351', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dela Cruz, Jonathan Solatorio', '17550', '2021-05-13', '351', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dela Cruz, Jonathan Solatorio', '500', '', '351', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dela Cruz, Jonathan Solatorio', '35000', '2021-08-19', '351', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Del Rosario, Jinky Mae F. ', '5000', '2020-09-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Del Rosario, Jinky Mae F. ', '15000', '2020-10-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Del Rosario, Jinky Mae F. ', '20000', '2020-11-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Del Rosario, Jinky Mae F. ', '10000', '2021-05-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Del Rosario, Jinky Mae F. ', '5000', '2021-12-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dequilla, Ma. Bennet Ballera', '5500', '2020-10-05', '100(200)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dequilla, Ma. Bennet Ballera', '5500', '2020-11-09', '100(200)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dequilla, Ma. Bennet Ballera', '11000', '2021-01-06', '100(200)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dequilla, Ma. Bennet Ballera', '11000', '2021-03-06', '100(200)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dequilla, Ma. Bennet Ballera', '16500', '2021-05-08', '100(200)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dequilla, Ma. Bennet Ballera', '17700', '2021-07-07', '100(200)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dequilla, Ma. Bennet Ballera', '1000', '', '100(200)', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '15600', '2020-08-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '11200', '2021-02-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '5600', '2021-03-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '5600', '2021-03-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '5600', '2021-07-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '11200', '2022-02-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '5600', '2022-03-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '5600', '2022-03-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Doronio, Sharven A. ', '11200', '2022-05-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Duno, Maylyn D', '10000', '2020-09-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Duno, Maylyn D', '3800', '2020-12-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Duno, Maylyn D', '3200', '2021-01-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Durana, Jocelyn B. ', '35000', '2020-08-18', '100(200)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Durana, Jocelyn B. ', '50000', '2020-09-29', '100(200)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Embedia, Vanieza L', '20000', '2020-10-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Embedia, Vanieza L', '10000', '2021-03-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '10000', '2020-09-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '10000', '2020-10-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2020-11-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2020-12-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '13000', '2021-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2021-04-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2021-04-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2021-06-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '13200', '2021-07-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '13200', '2021-10-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '19800', '2022-01-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '13200', '2022-02-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2022-04-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2022-05-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2022-06-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6600', '2022-06-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '19800', '2022-09-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Espino, Joelen S', '6800', '2022-10-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Eterno, Jay B. ', '13300', '2020-08-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Eterno, Jay B. ', '3300', '2020-09-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Eterno, Jay B. ', '6600', '2020-12-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Eterno, Jay B. ', '6600', '2021-09-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Eterno, Jay B. ', '20000', '2022-02-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Eterno, Jay B. ', '9000', '2022-03-10', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ellasgo, Zaldy (A.S Bensorto, Cherry A )', '2150', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '45000', '2020-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '5000', '2020-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '5000', '2020-12-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '5000', '2021-01-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '10000', '2021-02-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '5000', '2021-04-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '15000', '2021-07-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '2500', '2021-10-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '2500', '2021-11-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '2500', '2021-12-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fajamolin, Elizabeth S. ', '1000', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Falcis, Joel', '20000', '2020-07-25', '100 (1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Falcis, Joel', '80000', '2020-08-31', '100 (1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Falcis, Joel', '500', '', '100 (1000)', 'ARWU');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Falcis, Karen Mae', '40000', '2020-09-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Falcis, Karen Mae', '45000', '2021-01-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fayloga, Jovelyn N. ', '30000', '2020-08-07', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fayloga, Jovelyn N. ', '5000', '2020-10-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fayloga, Jovelyn N. ', '10000', '2020-11-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fayloga, Jovelyn N. ', '600', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fegurac, Liana F. ', '2500', '2020-08-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fegurac, Liana F. ', '5000', '2020-10-12', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fegurac, Liana F. ', '5000', '2021-01-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fegurac, Liana F. ', '5000', '2021-02-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fegurac, Liana F. ', '2500', '2021-04-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fegurac, Liana F. ', '5000', '2021-06-12', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fegurac, Liana F. ', '5000', '2021-10-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fegurac, Liana F. ', '5000', '2022-04-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Felongco, Lalyn C. ', '55000', '2020-08-12', '100(1,000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Felongco, Lalyn C. ', '500', '', '100(1,000)', 'ARWU');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fisher, Edna Mae', '20000', '2020-07-28', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fisher, Edna Mae', '160000', '2020-08-31', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fisher, Edna Mae', '500', '', '200(1000)', 'ARWU');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Flores, Dino A', '22000', '2020-09-18', '200 (1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Flores, Dino A', '21200', '2020-11-28', '200 (1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Flores, Dino A', '10800', '2021-02-01', '200 (1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Flores, Dino A', '10800', '2021-03-31', '200 (1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Flores, Dino A', '10800', '2022-02-19', '200 (1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '30000', '2020-06-06', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '5000', '2020-08-31', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '5500', '2020-10-06', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '10000', '2020-12-15', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '5000', '2021-02-11', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '15000', '2021-08-12', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '20000', '2021-12-29', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '5000', '2022-03-07', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fores, Edwin F. Sr', '5000', '2022-04-05', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Francisco, Jeany Rose', '65000', '2020-08-07', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fronda, Jovie V.', '500', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Fuyonan, Efren (Fuyonan, Ronie)', '40000', '2021-04-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Garcia, Mary Ann A. ', '19500', '2020-08-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Garcia, Mary Ann A. ', '5500', '2020-08-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Garcia, Mary Ann A. ', '13200', '2020-10-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '500', '', '100', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '10800', '2021-06-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '4000', '2021-08-05', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '11300', '2021-10-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '11500', '2022-02-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '11000', '2022-05-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '11000', '2022-10-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '11000', '2022-10-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '15000', '2021-04-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '500', '', '100', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '6700', '2021-07-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '10900', '2021-09-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '11000', '2022-01-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '11500', '2022-02-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '11000', '2022-03-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gener, Jelyn E', '10000', '2022-06-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Germin, Aldreck Ben B. ', '110000', '2020-08-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Germin, Mary Grace B. ', '220000', '2020-08-06', '400', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Germin, Mary Grace B. ', '1000', '', '400', 'DAS ');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Glemada, Alma A. ', '20000', '2020-07-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Glemada, Alma A. ', '10000', '2020-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Glemada, Alma A. ', '15000', '2021-10-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Glemada, Alma A. ', '20000', '2022-02-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Glemada, Alma A. ', '20000', '2022-08-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Glemada, Alma A. ', '20000', '2022-09-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gonzales, Ivy Joy', '20000', '2020-09-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gonzales, Ivy Joy', '10000', '2020-09-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gonzales, Ivy Joy', '6600', '2020-11-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gonzales, Ivy Joy', '10000', '2020-11-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gonzales, Ivy Joy', '7000', '2021-01-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gonzales, Ivy Joy', '7000', '2021-03-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gonzales, Ivy Joy', '7000', '2021-06-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '20000', '2020-09-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '19800', '2020-10-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '26400', '2021-02-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '19800', '2021-06-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '19800', '2021-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '13200', '2022-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '19800', '2022-02-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '19800', '2022-05-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ramon S', '19800', '2022-06-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '30000', '2020-08-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '9166', '2020-09-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '9166', '2020-10-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '9166', '2020-11-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '9166', '2020-12-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '18332', '2021-02-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '18500', '2021-04-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '18500', '2021-06-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '18500', '2021-08-19', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '18500', '2021-10-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '18500', '2022-01-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '28000', '2022-03-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '20000', '2022-06-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gorecho, Ricky Subong', '24488', '2022-09-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Arlyn B. ', '25000', '2020-07-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Arlyn B. ', '45000', '2020-09-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Arlyn B. ', '600', '', '200', 'DAS LOT90');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Arlyn B. ', '2500', '2020-10-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Arlyn B. ', '2500', '2021-05-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Arlyn B. ', '10000', '2020-09-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Arlyn B. ', '45000', '', '100', '10/23/020');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Arlyn B. ', '600', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '26600', '2020-08-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '6600', '2020-09-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '6600', '2020-10-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '6600', '2020-11-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '6600', '2021-01-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '6600', '2021-01-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '6600', '2021-02-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '5600', '2021-03-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Gladys', '6600', '2021-05-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '50000', '2020-08-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '30600', '2020-08-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2020-09-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2020-10-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2020-12-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2021-01-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2021-01-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2021-02-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2021-03-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2021-04-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2021-05-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2021-06-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '5600', '2021-07-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Gumabong, Aireen', '6600', '2021-09-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dawami, Maureen', '30000', '2020-08-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dawami, Maureen', '50000', '2020-08-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Dawami, Maureen', '50000', '2020-08-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '65000', '2020-08-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5000', '2020-09-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5000', '2020-10-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2020-11-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-01-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-02-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-02-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-03-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-04-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-06-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-07-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-08-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '5500', '2021-10-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Hanilap, Nesa', '1000', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '4500', '2020-09-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '4000', '2020-10-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2020-11-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2020-12-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '4000', '2021-01-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '4000', '2021-02-20', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2021-03-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '4000', '2021-04-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '4000', '2021-05-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2021-06-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2021-08-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2021-08-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2021-09-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2021-10-27', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '7000', '2021-12-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Charity B', '3500', '2022-01-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '65000', '2020-08-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '15700', '2020-10-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-01-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-02-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-03-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-04-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-05-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-06-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-07-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-08-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-09-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-10-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-11-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2021-12-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2022-01-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2022-02-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2022-03-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2022-04-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Huinda, Llaster', '4000', '2022-05-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ignes, Edralyn L. ', '30000', '2020-07-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ignes, Edralyn L. ', '14000', '2020-08-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ignes, Edralyn L. ', '20000', '2021-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ignes, Edralyn L. ', '8000', '2021-03-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ignes, Edralyn L. ', '9000', '2021-07-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ignes, Edralyn L. ', '9000', '2021-10-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ignes, Edralyn L. ', '6000', '2022-01-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ignes, Edralyn L. ', '10000', '2022-04-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Iligan, Rely Jean', '500', '', '100', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Iligan, Rely Jean', '70000', '2022-02-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Iligan, Rely Jean', '25000', '2022-03-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Iligan, Rely Jean', '1000', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '20000', '2020-08-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2020-09-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2020-10-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2020-10-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2020-12-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6300', '2020-12-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2021-05-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2021-06-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2021-08-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2021-10-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2021-11-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2022-04-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2022-07-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Jimenea, Bradford Felix', '6600', '2022-09-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '25000', '2020-09-11', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '25000', '2020-10-21', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19800', '2020-11-26', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19800', '2020-12-28', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19800', '2021-01-28', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19800', '2021-03-01', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19800', '2021-03-31', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19000', '2021-06-01', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19800', '2021-06-29', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19800', '2021-08-17', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19800', '2021-10-02', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19735', '2021-12-09', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '19735', '2022-03-02', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Joaquin, Jeffrey P. ', '6204', '2022-05-03', '600', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Kasan, Edwin', '80000', '2020-08-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Kasan, Edwin', '50000', '2020-09-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Kasan, Edwin', '500', '', '200', 'ARWU');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Kasan, Edwin', '10000', '', '200', 'excess');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Labao, Mary Grace L', '20000', '2021-07-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Labao, Mary Grace L', '500', '', '100', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Labao, Mary Grace L', '5000', '2021-09-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Labao, Mary Grace L', '5000', '2021-12-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Labao, Mary Grace L', '5000', '2022-01-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Labao, Mary Grace L', '20000', '2022-03-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Labao, Mary Grace L', '14200', '2022-04-13', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lacambra, Dhee O', '15000', '2020-08-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lacambra, Dhee O', '5000', '2020-09-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lacambra, Dhee O', '10000', '2020-12-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lacambra, Dhee O', '8000', '2021-02-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '10000', '2020-08-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '10000', '2020-09-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '19800', '2020-10-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '19800', '2020-10-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '19800', '2020-10-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '19800', '2020-10-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2021-10-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2021-12-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2022-02-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2022-03-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2022-04-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2022-06-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2022-07-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2022-07-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lagahit, Joeddy Adrian Franco', '6600', '2022-08-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lamery, Eric S. ', '10000', '2020-08-31', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lamery, Eric S. ', '3300', '2020-10-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lamery, Eric S. ', '3300', '2021-01-05', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Laureano, Lilibeth Belves/Manilyn ', '5500', '2020-08-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Laureano, Lilibeth Belves/Manilyn ', '5500', '2020-09-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Laureano, Lilibeth Belves/Manilyn ', '5500', '2020-10-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Laureano, Lilibeth Belves/Manilyn ', '5500', '2020-11-20', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Laureano, Lilibeth Belves/Manilyn ', '5500', '2021-01-20', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Laureano, Lilibeth Belves/Manilyn ', '33000', '2021-02-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Laureano, Lilibeth Belves/Manilyn ', '750', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Legaspi, Eduard', '10000', '', '200', 'Barnuevo, R.');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '15000', '2020-07-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '5000', '2020-08-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '10000', '2020-10-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '10000', '2021-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '10000', '2021-02-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '5000', '2021-04-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '5000', '2021-06-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '5000', '2021-10-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '10000', '2021-12-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '5000', '2022-02-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '7500', '2022-04-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Lontiong, Darlene', '5000', '2022-06-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Loreno, Stella Merced D', '50000', '2020-08-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Loreno, Stella Merced D', '44000', '2020-09-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Loreno, Stella Merced D', '10000', '2020-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Loreno, Stella Merced D', '15000', '2020-10-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Luig, Liza F', '13300', '2020-10-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Luig, Liza F', '40000', '2021-09-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Luig, Liza F', '31700', '2021-12-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Luig, Liza F', '500', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Macuro, Larry D.', '5000', '2021-11-11', '207', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Macuro, Larry D.', '15000', '2021-11-18', '207', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Macuro, Larry D.', '15000', '2021-12-27', '207', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Macuro, Larry D.', '11200', '2022-02-03', '207', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Macuro, Larry D.', '7000', '2022-04-04', '207', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Macuro, Larry D.', '13000', '2022-05-12', '207', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Macuro, Larry D.', '13600', '2022-06-17', '207', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Macuro, Larry D.', '11200', '2022-09-23', '207', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Magbanua, Rene', '26600', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Magbanua, Rene', '6600', '2020-10-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Magbanua, Rene', '6000', '2020-11-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Magbanua, Rene', '6000', '2020-12-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Magbanua, Rene', '6600', '2021-01-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Magbanua, Rene', '3000', '2022-04-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Magdaluyo, Analyn C. ', '25000', '2021-02-13', '500', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Magdaluyo, Analyn C. ', '55000', '2021-10-09', '500', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2021-07-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2021-08-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2021-09-30', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2021-10-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2021-12-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2022-05-05', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2022-07-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2022-09-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Mansilla, Arlene', '5000', '2022-09-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Marquez, Florendo S Jr', '35000', '2020-08-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Marquez, Florendo S Jr', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Marquez, Florendo S Jr', '140000', '2020-09-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Montibon, Katrina Angelica S', '20000', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Montibon, Katrina Angelica S', '13200', '2020-09-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Montibon, Katrina Angelica S', '26400', '2021-02-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '10000', '2020-08-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '2500', '2020-09-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '2500', '2020-10-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '2500', '2020-11-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '2500', '2020-12-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '5000', '2021-03-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '7500', '2021-05-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '10000', '2021-09-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '10000', '2022-04-07', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Arriane Marie E. ', '5500', '2022-08-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Sharlene', '130000', '2020-11-19', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Morales, Sharlene', '1000', '', '200', 'DAS ');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Moralidad, Dave Alwin B. ', '5000', '2020-06-06', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Moralidad, Dave Alwin B. ', '2500', '2020-08-31', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Moralidad, Dave Alwin B. ', '2000', '2020-10-06', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Moralidad, Dave Alwin B. ', '5000', '2021-03-02', '200(1000)', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nabor, Ma. Lucy', '10000', '2020-10-20', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nabor, Ma. Lucy', '6600', '2020-10-20', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nabor, Ma. Lucy', '3300', '2021-05-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nabor, Ma. Lucy', '6000', '2021-10-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Navarro, Leizel', '6600', '2020-08-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Navarro, Leizel', '18000', '2020-12-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Navarro, Leizel', '10000', '2021-03-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Navarro, Leizel', '7000', '2021-05-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Navarro, Leizel', '10000', '2021-06-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Navarro, Leizel', '20000', '2021-06-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '65000', '2020-08-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '500', '', '200', 'NOTARY');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2020-09-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2020-10-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2020-11-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2020-12-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2021-01-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2021-02-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2021-03-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2021-04-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '5500', '2021-05-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Nido, Marites ', '11000', '2021-07-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Noble, Ramel', '6000', '2020-07-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Noble, Ramel', '4000', '2020-10-20', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Noble, Ramel', '3300', '2021-04-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Noble, Ramel', '9000', '2021-10-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ong, Madonna S. (fr. Ong, Ron Ian)', '13300', '2020-09-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ong, Madonna S. (fr. Ong, Ron Ian)', '6600', '2020-10-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ong, Madonna S. (fr. Ong, Ron Ian)', '50000', '2021-09-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ong, Madonna S. (fr. Ong, Ron Ian)', '7300', '2021-09-10', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Ong, Madonna S. (fr. Ong, Ron Ian)', '800', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Osano, Marianne E.', '15000', '2022-02-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Osano, Marianne E.', '15000', '2022-04-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, Kit Caryl C', '20000', '2021-03-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, Kit Caryl C', '5400', '2021-04-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, Kit Caryl C', '5400', '2021-05-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, Kit Caryl C', '5400', '2021-06-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, Kit Caryl C', '5400', '2021-09-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, Penena Park', '30000', '2020-08-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, Penena Park', '100000', '2020-09-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, John Bryan ', '10000', '2020-08-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, John Bryan ', '3500', '2020-10-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, John Bryan ', '3300', '2020-12-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, John Bryan ', '5000', '2022-03-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, John Bryan ', '4000', '2022-04-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Otero, John Bryan ', '10000', '2022-09-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pabila, Vanjely Tortula', '10000', '2020-08-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pabila, Vanjely Tortula', '4000', '2020-10-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pabila, Vanjely Tortula', '5000', '2020-12-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pabila, Vanjely Tortula', '5500', '2022-03-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pabila, Vanjely Tortula', '3000', '2022-04-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pabila, Vanjely Tortula', '5000', '2022-09-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pacheco, Jade B. ', '20000', '2020-07-29', '199', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pedregosa, Arlene Loreno', '10000', '2021-02-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pedregosa, Arlene Loreno', '5000', '2021-10-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pernal, Niline Gabriel', '5500', '2020-08-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pernal, Niline Gabriel', '5500', '2020-09-07', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pernal, Niline Gabriel', '5500', '2020-10-07', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pernal, Niline Gabriel', '11000', '2020-10-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pernal, Niline Gabriel', '5500', '2020-11-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pernal, Niline Gabriel', '5500', '2020-11-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Pernal, Niline Gabriel', '25500', '2021-05-27', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Robilla, Jimaris O. ', '20000', '2020-07-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Robilla, Jimaris O. ', '5000', '2020-08-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Robilla, Jimaris O. ', '5000', '2020-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Robilla, Jimaris O. ', '10000', '2020-11-19', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Robilla, Jimaris O. ', '10000', '2021-02-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sajo, Jimely', '40000', '2020-08-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sajo, Jimely', '25000', '2020-09-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sajo, Jimely', '700', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Salibio, Jessie', '207000', '2020-08-25', '210+100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Salindeho, Leonard A. ', '2500', '2020-08-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Salindeho, Leonard A. ', '2500', '2020-10-07', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Salindeho, Leonard A. ', '2500', '2021-01-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Salindeho, Leonard A. ', '12500', '2021-03-30', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Salindeho, Leonard A. ', '10000', '2021-11-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Salindeho, Leonard A. ', '10000', '2022-05-21', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '5000', '2020-07-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '20000', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '11075', '2021-01-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '22133', '2021-02-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '11070', '2021-03-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '22080', '2021-05-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '11070', '2021-07-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '22070', '2021-10-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '11070', '2022-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '13070', '2022-03-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sarino, Cyrhelle Formacion', '11070', '2022-05-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '30000', '2020-08-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2020-08-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '10000', '2020-10-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6000', '2020-10-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '7500', '2020-11-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '8000', '2021-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '7000', '2021-02-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '10000', '2021-03-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '7500', '2021-04-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6500', '2021-05-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2021-06-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '12600', '2021-07-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2021-09-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2021-10-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2021-11-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-01-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-03-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-03-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-03-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-04-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-05-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-06-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-09-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '6300', '2022-09-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Serrano, John Jeremy (Serrano, Elsie D.)', '4300', '', '200', 'refund 09/22/22');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '25006', '2020-08-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '9200', '', '200', '10/19/220');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '9200', '2021-01-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '9199', '2021-03-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '9200', '2021-05-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '9200', '2021-06-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '20003', '2021-08-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '10000', '2021-11-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '20000', '2022-03-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Sindol, Rita M. ', '12000', '2022-08-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-08-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-09-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-10-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-10-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-10-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-11-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-11-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '11200', '2020-12-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soco, Rheelen S', '21400', '2020-12-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soldevilla, Ricky', '2500', '2020-08-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soldevilla, Ricky', '2500', '2020-10-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soldevilla, Ricky', '2500', '2021-03-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soldevilla, Ricky', '2500', '2021-04-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soldevilla, Ricky', '15000', '2022-03-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soldevilla, Ricky', '7500', '2022-03-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soldevilla, Ricky', '5000', '2022-04-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '5000', '2020-07-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '30000', '2020-08-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '6600', '2020-09-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '6600', '2020-11-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '6600', '2020-12-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '6600', '2021-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '6600', '', '200', '1/29/201');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '6600', '2021-03-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '13200', '2021-04-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Soriano, Evy', '6600', '2021-12-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Susbilla, Nelfa B. ', '25000', '2020-07-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Susbilla, Nelfa B. ', '90000', '2020-09-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Susbilla, Nelfa B. ', '1200', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tadiaque, Nida', '26600', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tadiaque, Nida', '6600', '2020-10-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tadiaque, Nida', '6600', '2020-11-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tadiaque, Nida', '6600', '2020-12-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tadiaque, Nida', '6600', '2021-01-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '20000', '2020-10-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '6600', '2021-01-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '5000', '2021-03-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '6000', '2021-04-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '6000', '2021-07-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '12000', '2021-07-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '5000', '2021-07-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '6000', '2021-08-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '6000', '2021-11-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '6000', '2021-11-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '6000', '2022-07-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tesnado, Cherry', '3000', '2022-10-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '10000', '2020-09-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '3300', '2020-10-12', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '6600', '2020-12-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '3300', '2021-01-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '6600', '2021-03-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '3300', '2021-04-22', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '6600', '2021-06-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '3300', '2021-07-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '6600', '2021-09-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '6600', '2021-11-15', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '6600', '2022-01-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '9900', '2022-04-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '6600', '2022-06-16', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Tijon, Madelyn M', '10000', '2022-10-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Velasco, Earl H. ', '4500', '2021-08-05', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '10000', '2020-08-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '8000', '2020-11-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '10100', '2021-01-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '9500', '2021-02-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '9500', '2021-03-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '10000', '2021-06-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '13100', '2021-08-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '9100', '2021-10-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '10100', '2022-03-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '5150', '2022-03-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Venancio, Mae Grace', '10100', '2022-04-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '26600', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '6600', '2020-10-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '6600', '2020-11-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '6600', '2020-12-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '6600', '2021-01-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '13200', '2021-06-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '6600', '2021-09-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '6600', '2022-04-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '6600', '2022-06-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '6600', '2022-07-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Veñola, Rosemarie A', '20000', '2022-10-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '5000', '2020-09-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '5000', '2020-10-30', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '3500', '2020-12-14', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '3300', '2021-01-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '4500', '2021-04-06', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '3300', '2021-06-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '4000', '2021-07-23', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '3500', '2021-09-01', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '3000', '2021-10-08', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '3500', '2022-01-18', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '4000', '2022-02-28', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '3500', '2022-04-04', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '4500', '2022-07-27', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '3630', '2022-09-02', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Vitar, Rogie B', '4000', '2022-10-24', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '30000', '2020-07-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '10000', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '10000', '2020-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '10000', '2020-12-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '8000', '2021-01-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '12000', '2021-04-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '16000', '2021-08-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '8000', '2021-09-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '8000', '2021-11-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '6000', '2022-02-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Frondozo-Famulag)', 'Volutante, Rosfe C. ', '14000', '2022-05-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Aguirre, Rosalie C', '20000', '2020-10-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Aguirre, Rosalie C', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Aguirre, Rosalie C', '7780', '2020-12-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Aguirre, Rosalie C', '7780', '2021-01-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Aguirre, Rosalie C', '8000', '2021-03-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Aguirre, Rosalie C', '15560', '2021-04-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Aguirre, Rosalie C', '15560', '2021-06-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Aguirre, Rosalie C', '7800', '2021-08-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Alaer, Mary Jane', '20000', '2020-11-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Alaer, Mary Jane', '20000', '2020-11-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '20000', '2020-10-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2020-12-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2021-01-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '16000', '2021-03-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2021-04-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2021-06-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2021-08-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2021-09-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2021-10-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2021-11-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '16000', '2022-01-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2022-04-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2022-06-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '10000', '2022-08-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Balansag, Sherrylee C', '8000', '2022-09-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '20000', '2020-11-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '7780', '2020-12-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '7780', '2021-01-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '7780', '2021-02-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '7780', '2021-04-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '8000', '2021-06-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '5000', '2021-08-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '8000', '2021-12-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '7000', '2022-01-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '8000', '2022-02-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '8000', '2022-03-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '7000', '2022-04-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '10000', '2022-05-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '10000', '2022-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Bauson, Ivy D', '8000', '2022-11-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Belarmino, Jerryvan', '140000', '2020-12-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Belarmino, Jerryvan', '135000', '2021-02-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Belarmino, Jerryvan', '1000', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '30000', '2020-11-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2020-12-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-01-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-02-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-03-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-04-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-05-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-06-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-07-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-08-12', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-09-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-10-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '22500', '2021-11-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Boctil, Zaradane H', '3100', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '30000', '2020-12-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '18340', '2021-02-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '9170', '2021-04-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '9170', '2021-05-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '18340', '2021-06-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '18340', '2021-08-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '18340', '2022-02-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borbon, Nelson M', '9000', '2022-10-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borlaza, Jocelyn', '30000', '2020-11-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borlaza, Jocelyn', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borlaza, Jocelyn', '22500', '2020-12-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Borlaza, Jocelyn', '22500', '', '200', '2/24/202');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Burro, Antonio J', '3890', '2020-11-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Burro, Antonio J', '7780', '2021-01-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Burro, Antonio J', '8000', '2021-03-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Burro, Antonio J', '10000', '2021-06-14', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Cabuhay, Jan Mar', '20000', '2020-12-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Cabuhay, Jan Mar', '16000', '2021-02-19', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Cabuhay, Jan Mar', '15000', '2021-05-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Cabuhay, Jan Mar', '8000', '2021-07-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '20000', '2020-11-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2020-12-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2021-01-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2021-02-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2021-04-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2021-04-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2021-06-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2021-07-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2021-07-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '8000', '2021-11-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '5000', '2022-02-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Corsiga, Ma. Lizl M', '6000', '2022-06-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Dela Cruz, Richie', '20000', '2020-11-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Dela Cruz, Richie', '16000', '2021-03-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '20000', '2020-11-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '15560', '2021-01-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '15560', '2021-04-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '15560', '2021-06-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '15560', '2021-09-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '15560', '2021-12-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '15600', '2022-01-26', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '16000', '2022-03-19', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Era, Rosebeth L', '16000', '2022-08-25', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Esquierda, Juvie', '20000', '2020-12-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Esquierda, Juvie', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Esquierda, Juvie', '18340', '2021-01-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Esquierda, Juvie', '26000', '2021-03-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Esquierda, Juvie', '20000', '2021-06-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Esquierda, Juvie', '10000', '2021-09-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Esquierda, Juvie', '10000', '2022-01-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Evasco, Rhealiza C', '195000', '2021-12-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Evasco, Rhealiza C', '1000', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Evasco, Rhomel B', '25000', '', '200', 'Evasco, Rhealiza');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Evasco, Rhomel B', '170000', '2022-08-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Evasco, Rhomel B', '1000', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Falcis, Esther Antoniette F', '10000', '2020-11-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Falcis, Esther Antoniette F', '5210', '2020-12-17', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Falcis, Esther Antoniette F', '5210', '2021-02-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fegurac, Liziel Q', '5000', '2021-03-26', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fegurac, Liziel Q', '6800', '2021-05-19', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fegurac, Liziel Q', '8000', '', '100', 'Salve, Musa');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fegurac, Liziel Q', '500', '', '100', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fuasan, Mary Jane', '8000', '2020-11-27', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fuasan, Mary Jane', '500', '', '100', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fuasan, Mary Jane', '6000', '2021-01-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fuasan, Mary Jane', '3800', '2021-03-11', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fuasan, Mary Jane', '4000', '2021-06-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fullon, Jufrex S', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fullon, Jufrex S', '25000', '2020-12-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fullon, Jufrex S', '165000', '2021-01-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Fullon, Jufrex S', '1000', '', '200', 'DAS ');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Gener, Noel L', '5000', '2021-01-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Gener, Noel L', '2000', '2021-03-03', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Huinda, Concepcion C', '20000', '2021-03-22', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Huinda, Concepcion C', '20000', '2021-06-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Huinda, Concepcion C', '30000', '2022-04-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Juayno, Sheldey/Figuerda, Ma. Lizel (fr. Emboltorio, Reman B)', '10000', '2020-11-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Malata, Junife', '100000', '2021-01-29', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Malata, Junife', '1100', '', '100', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '4000', '2020-08-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '4000', '2020-10-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '12000', '2020-10-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2020-12-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2021-01-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2021-02-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2021-03-31', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2021-04-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2021-06-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2021-08-19', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2021-10-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2022-01-07', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2022-03-21', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Matociños, Christian Santos', '7000', '2022-08-16', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '20000', '2020-11-10', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2020-12-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-01-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-02-24', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-04-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-04-29', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-06-03', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-07-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-07-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-09-01', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-10-08', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '8000', '2021-11-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '10000', '2022-01-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '10000', '2022-02-28', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '10000', '2022-04-04', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '7500', '2022-05-02', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '20000', '2022-06-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Moso, Leonel B', '25000', '2022-08-15', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Ortiz, Lailyn A', '200000', '2021-07-20', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Ortiz, Lailyn A', '28000', '2021-10-14', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Perez, Arthur James A', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Perez, Arthur James A', '30000', '2020-10-30', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Perez, Arthur James A', '280000', '2020-11-18', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Perez, Arthur James A', '3250', '', '200', 'DAS ');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Prado, Jilord Laurence P', '20000', '2020-11-27', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Prado, Jilord Laurence P', '500', '', '200', 'DCS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Prado, Jilord Laurence P', '8000', '2021-01-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Prado, Jilord Laurence P', '8000', '2021-03-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Ramos, Jimmy A', '5000', '2020-11-09', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Taculin, Evelyn B', '30000', '2020-11-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Taculin, Evelyn B', '7780', '2020-11-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Taculin, Evelyn B', '1220', '2020-12-23', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Taculin, Gemma S', '7780', '2020-11-11', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Taculin, Gemma S', '232220', '2020-11-17', '200', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Taculin, Gemma S', '1000', '', '200', 'DAS');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Tubang, Juvy B', '10000', '2021-03-25', '100', '');
INSERT INTO `tmppayments` VALUES ('Norala (Lavega)', 'Tubang, Juvy B', '6000', '1900-01-00', '100', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Abatay, Carlito R.', '500', '', '600', 'DCS');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Abatay, Carlito R.', '10000', '2022-10-07', '600', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Academia, Christine A.', '30000', '2022-08-05', '200', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Academia, Christine A.', '8500', '2022-09-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Academia, Christine A.', '8500', '2022-10-13', '200', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Claveria, Charlie Miranda', '10000', '2022-06-06', '200', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Claveria, Charlie Miranda', '10000', '2022-08-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Claveria, Charlie Miranda', '10000', '2022-10-09', '200', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'David, Joanna', '25000', '2022-09-03', '700', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Jopson, Marytchel E.', '500', '', '200 (300)', 'DCS');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Jopson, Marytchel E.', '28334', '2022-06-23', '200 (300)', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Jopson, Marytchel E.', '15000', '2022-04-30', '200 (300)', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Jopson, Marytchel E.', '271666', '2022-06-29', '200 (300)', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Perez, Dominador V.', '6000', '2022-06-13', '100', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Perez, Dominador V.', '3000', '2022-09-13', '100', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Perez, Dominador V.', '3000', '2022-10-14', '100', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Sinarimbo, Fatima Andi', '200000', '2022-05-04', '500', '');
INSERT INTO `tmppayments` VALUES ('Tupi P3', 'Sumagaysay, Janice E.', '15000', '2022-07-26', '100', '');

-- ----------------------------
-- View structure for clientswithfullname
-- ----------------------------
DROP VIEW IF EXISTS `clientswithfullname`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER  VIEW `clientswithfullname` AS SELECT
	`clients`.`idclients` AS `idclients`,
	`clients`.`firstname` AS `firstname`,
	`clients`.`lastname` AS `lastname`,
	`clients`.`middlename` AS `middlename`,
	`clients`.`gender` AS `gender`,
	`clients`.`dateofbirth` AS `dateofbirth`,
	`clients`.`placeofbirth` AS `placeofbirth`,
	`clients`.`spousename` AS `spousename`,
	`clients`.`contactno` AS `contactno`,
	concat(
		`clients`.`lastname`,
		', ',
		`clients`.`firstname`,
		' ',
	IF (ISNULL(`clients`.`middlename`),'',`clients`.`middlename`)
) AS fullname
FROM
	`clients` ;

-- ----------------------------
-- View structure for expenses_view
-- ----------------------------
DROP VIEW IF EXISTS `expenses_view`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER  VIEW `expenses_view` AS SELECT
	`expenses`.*,
	concat(
		`c1`.`firstname`,
		', ',
		`c1`.`lastname`,
		' ',
		`c1`.`middlename`
	) AS `receiveby_str`,
	concat(
		`c2`.`firstname`,
		', ',
		`c2`.`lastname`,
		' ',
		`c2`.`middlename`
	) AS `releaseby_str`
FROM
	(
		(
			`expenses`
			LEFT JOIN `clients` `c1` ON (
				(
					`expenses`.`receiveby` = `c1`.`idclients`
				)
			)
		)
		LEFT JOIN `clients` `c2` ON (
			(
				`expenses`.`releaseby` = `c2`.`idclients`
			)
		)
	) 
WHERE DATEDIFF(NOW(),`daterelease`)<=0 ;

-- ----------------------------
-- View structure for view_agents
-- ----------------------------
DROP VIEW IF EXISTS `view_agents`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER  VIEW `view_agents` AS SELECT
	`id`,
	`fullname`,
	`contactno`,
	`recordstatus`
FROM
	`dicers` ;

-- ----------------------------
-- View structure for view_productdeatailswithproduct
-- ----------------------------
DROP VIEW IF EXISTS `view_productdeatailswithproduct`;
CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost`  VIEW `view_productdeatailswithproduct` AS SELECT
purchasedetails.id,
purchasedetails.blockno,
purchasedetails.lotno,
purchasedetails.area,
purchasedetails.lotprice,
purchasedetails.amortization,
purchasedetails.terms,
purchasedetails.idagent,
purchasedetails.agentpercentage,
purchasedetails.idclients,
purchasedetails.idproducts,
purchasedetails.remarks,
products.`code`,
products.location,
products.totalblockno,
products.totallotno,
products.totalarea,
products.cashprice
FROM
purchasedetails
INNER JOIN products ON purchasedetails.idproducts = products.idproduct ;

-- ----------------------------
-- Procedure structure for proc_purchasedetails_full
-- ----------------------------
DROP PROCEDURE IF EXISTS `proc_purchasedetails_full`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `proc_purchasedetails_full`()
BEGIN
	#Routine body goes here...
SELECT
purchasedetails.id,
purchasedetails.blockno,
purchasedetails.lotno,
purchasedetails.area,
purchasedetails.lotprice,
purchasedetails.amortization,
purchasedetails.terms,
purchasedetails.idagent,
purchasedetails.agentpercentage,
purchasedetails.idclients,
purchasedetails.idproducts,
purchasedetails.remarks,
CONCAT(lastname,", ",firstname," (", `code`," lot-",lotno,")") AS description,
products.`code`
FROM
purchasedetails
INNER JOIN clients ON purchasedetails.idclients = clients.idclients
INNER JOIN products ON purchasedetails.idproducts = products.idproduct;

END
;;
DELIMITER ;
