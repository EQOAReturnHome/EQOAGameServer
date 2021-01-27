-- MySQL dump 10.13  Distrib 8.0.13, for Win64 (x86_64)
--
-- Host: 192.168.1.4    Database: eqoabase
-- ------------------------------------------------------
-- Server version	5.5.5-10.3.23-MariaDB-0+deb10u1

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `AccountInfo`
--

DROP TABLE IF EXISTS `AccountInfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `AccountInfo` (
  `accountid` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(32) NOT NULL,
  `pwhash` binary(60) NOT NULL,
  `acctstatus` int(11) DEFAULT NULL,
  `acctlevel` int(11) DEFAULT NULL,
  `acctcreation` datetime DEFAULT NULL,
  `lastlogin` datetime DEFAULT NULL,
  `ipaddress` varchar(16) DEFAULT NULL,
  `firstname` varchar(32) DEFAULT NULL,
  `unknown1` varchar(32) DEFAULT NULL,
  `midinitial` varchar(16) DEFAULT NULL,
  `lastname` varchar(32) DEFAULT NULL,
  `unknown2` varchar(32) DEFAULT NULL,
  `countryAB` varchar(16) DEFAULT NULL,
  `zip` varchar(16) DEFAULT NULL,
  `birthday` varchar(16) DEFAULT NULL,
  `birthyear` varchar(16) DEFAULT NULL,
  `birthmon` varchar(16) DEFAULT NULL,
  `sex` varchar(16) DEFAULT NULL,
  `email` varchar(128) DEFAULT NULL,
  `result` int(11) DEFAULT NULL,
  `subtime` int(11) DEFAULT NULL,
  `partime` int(11) DEFAULT NULL,
  `subfeatures` int(11) DEFAULT NULL,
  `gamefeatures` int(11) DEFAULT NULL,
  PRIMARY KEY (`accountid`,`username`),
  UNIQUE KEY `username_UNIQUE` (`username`),
  UNIQUE KEY `accountid_UNIQUE` (`accountid`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AccountInfo`
--

LOCK TABLES `AccountInfo` WRITE;
/*!40000 ALTER TABLE `AccountInfo` DISABLE KEYS */;
INSERT INTO `AccountInfo` VALUES (1,'Leukocyte',_binary '$2b$10$KTXm9ZVYfNPx1FMJVIFHEOqYH4d6piuurgR44JAOevQ7UTJHS4PZG',1,1,'2020-05-27 01:02:15','2020-06-08 16:11:29','67.249.128.46','m','','g','c','','US','13420','1','1977','Jan','Male','abc@abc.com',0,2592000,2592000,4,3),(2,'beebs',_binary '$2b$10$KWgbvH6jp4Wu0.mFoIvEo.hloEDrE28rJlRHhKaBhIfQcc5dII4nm',1,1,'2020-05-27 01:05:25','2020-07-31 18:15:06','71.84.180.35','a','','a','a','','US','33333','1','1977','Jan','Male','a@a.com',0,2592000,2592000,4,3),(3,'go',_binary '$2b$10$0Z/hW76zkQi2JbdDrjuqKOKhBf5p6yYVdBwhcUJkYaOPQJQM3qmC.',1,1,'2020-05-27 01:15:55','2020-08-01 21:45:37','73.251.113.151','aaaa','','aaaa','','','US','24486','1','1977','Jan','Male','',0,2592000,2592000,4,3);
/*!40000 ALTER TABLE `AccountInfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Characters`
--

DROP TABLE IF EXISTS `Characters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Characters` (
  `charName` varchar(32) NOT NULL,
  `accountid` int(11) NOT NULL,
  `serverid` int(11) NOT NULL AUTO_INCREMENT,
  `modelid` bigint(20) NOT NULL,
  `tclass` int(11) NOT NULL,
  `race` int(11) NOT NULL,
  `humType` varchar(12) NOT NULL,
  `level` int(11) NOT NULL,
  `haircolor` int(11) NOT NULL,
  `hairlength` int(11) NOT NULL,
  `hairstyle` int(11) NOT NULL,
  `faceoption` int(11) NOT NULL,
  `classIcon` int(11) NOT NULL,
  `totalXP` int(11) NOT NULL,
  `debt` int(11) NOT NULL,
  `breath` int(11) NOT NULL,
  `tunar` int(11) NOT NULL,
  `bankTunar` int(11) NOT NULL,
  `unusedTP` int(11) NOT NULL,
  `totalTP` int(11) NOT NULL,
  `world` int(11) NOT NULL DEFAULT 0,
  `x` float(17,12) NOT NULL,
  `y` float(17,12) NOT NULL,
  `z` float(17,12) NOT NULL,
  `facing` float(17,12) NOT NULL,
  `unknown` float(17,12) NOT NULL DEFAULT 0.000000000000,
  `strength` int(4) NOT NULL,
  `stamina` int(4) NOT NULL,
  `agility` int(4) NOT NULL,
  `dexterity` int(4) NOT NULL,
  `wisdom` int(4) NOT NULL,
  `intel` int(4) NOT NULL,
  `charisma` int(4) NOT NULL,
  `currentHP` int(11) NOT NULL,
  `maxHP` int(11) NOT NULL,
  `currentPower` int(11) NOT NULL,
  `maxPower` int(11) NOT NULL,
  `unk42_1` int(11) NOT NULL DEFAULT 0,
  `healot` int(11) NOT NULL,
  `powerot` int(11) NOT NULL,
  `ac` int(11) NOT NULL,
  `unk42_2` int(11) NOT NULL DEFAULT 0,
  `unk42_3` int(11) NOT NULL DEFAULT 0,
  `unk42_4` int(11) NOT NULL DEFAULT 0,
  `unk42_5` int(11) NOT NULL DEFAULT 0,
  `unk42_6` int(11) NOT NULL DEFAULT 0,
  `unk42_7` int(11) NOT NULL DEFAULT 0,
  `unk42_8` int(11) NOT NULL DEFAULT 0,
  `poisonr` int(11) NOT NULL,
  `diseaser` int(11) NOT NULL,
  `firer` int(11) NOT NULL,
  `coldr` int(11) NOT NULL,
  `lightningr` int(11) NOT NULL,
  `arcaner` int(11) NOT NULL,
  `fishing` int(11) NOT NULL,
  `base_strength` int(4) NOT NULL,
  `base_stamina` int(4) NOT NULL,
  `base_agility` int(4) NOT NULL,
  `base_dexterity` int(4) NOT NULL,
  `base_wisdom` int(4) NOT NULL,
  `base_intel` int(4) NOT NULL,
  `base_charisma` int(4) NOT NULL,
  `currentHP2` int(11) NOT NULL,
  `baseHP` int(11) NOT NULL,
  `currentPower2` int(11) NOT NULL,
  `basePower` int(11) NOT NULL,
  `unk42_9` int(11) NOT NULL DEFAULT 0,
  `healot2` int(11) NOT NULL,
  `powerot2` int(11) NOT NULL,
  `unk42_10` int(11) NOT NULL DEFAULT 0,
  `unk42_11` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`serverid`,`charName`),
  UNIQUE KEY `serverid_UNIQUE` (`serverid`),
  UNIQUE KEY `charName_UNIQUE` (`charName`),
  KEY `CharAccount_idx` (`accountid`),
  KEY `CharModel_idx` (`modelid`),
  CONSTRAINT `accountid` FOREIGN KEY (`accountid`) REFERENCES `AccountInfo` (`accountid`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `modelid` FOREIGN KEY (`modelid`) REFERENCES `characterModel` (`modelid`)
) ENGINE=InnoDB AUTO_INCREMENT=1260600 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Characters`
--

LOCK TABLES `Characters` WRITE;
/*!40000 ALTER TABLE `Characters` DISABLE KEYS */;
INSERT INTO `Characters` VALUES ('Dallasdevin',2,1224955,1893243078,0,0,'Other',60,6,3,3,0,0,180923385,0,255,500000000,100000000,10,350,0,25334.179687500000,54.125904083252,15748.388671875000,-1.546252727509,0.000000000000,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0),('Beebster',2,1260510,-511108617,7,1,'Other',60,7,2,3,2,7,25029476,1,255,345647345,35000000,15,234,0,25321.500000000000,54.200000762939,15746.799804687500,0.000000000000,0.000000000000,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0),('Leukocyte',2,1260511,1282385202,10,5,'Other',60,6,3,3,0,20,45834723,0,255,0,25000,20,12,0,25321.500000000000,54.200000762939,15746.799804687500,0.000000000000,0.000000000000,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0),('Aaaaa',2,1260583,1640644319,6,6,'Other',1,2,2,3,2,6,0,0,255,5000,0,20,475,0,13115.000000000000,54.399398803711,4316.310058593750,2.924910068512,0.000000000000,75,70,70,80,50,55,55,1000,1000,500,500,0,20,10,0,0,0,0,0,0,0,0,10,10,10,10,10,10,0,75,70,70,80,50,55,55,1000,1000,500,500,0,20,10,0,0),('Cccc',3,1260589,1640644319,6,6,'Other',1,2,2,3,2,6,0,0,255,5000,0,20,475,0,13115.000000000000,54.399398803711,4316.310058593750,2.924910068512,0.000000000000,75,70,70,80,50,55,55,1000,1000,500,500,0,20,10,0,0,0,0,0,0,0,0,10,10,10,10,10,10,0,75,70,70,80,50,55,55,1000,1000,500,500,0,20,10,0,0),('Bbbbb',3,1260593,-1436875705,9,4,'Other',1,0,1,2,0,9,0,0,255,5000,0,20,475,0,15445.900390625000,82.967201232910,8743.750000000000,1.505380034447,0.000000000000,70,70,55,55,85,50,70,1000,1000,500,500,0,20,10,0,0,0,0,0,0,0,0,10,10,10,10,10,10,0,70,70,55,55,85,50,70,1000,1000,500,500,0,20,10,0,0),('Beebs',2,1260594,-511108617,14,1,'Other',1,2,3,1,0,14,0,0,255,5000,0,20,475,0,18909.000000000000,53.875999450684,6567.839843750000,0.027663400397,0.000000000000,50,60,55,70,80,80,60,1000,1000,500,500,0,20,10,0,0,0,0,0,0,0,0,10,10,10,10,10,10,0,50,60,55,70,80,80,60,1000,1000,500,500,0,20,10,0,0),('Aweesome',2,1260599,-2071956336,3,9,'Other',1,2,3,1,0,3,0,0,255,5000,0,-15,350,0,9379.490234375000,30.382200241089,7134.560058593750,1.799309968948,0.000000000000,95,95,65,65,55,60,55,1000,1000,500,500,0,20,10,0,0,0,0,0,0,0,0,10,10,10,10,10,10,0,90,90,60,60,50,55,50,1000,1000,500,500,0,20,10,0,0);
/*!40000 ALTER TABLE `Characters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Hotkeys`
--

DROP TABLE IF EXISTS `Hotkeys`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Hotkeys` (
  `hotkeyid` int(11) NOT NULL AUTO_INCREMENT,
  `serverid` int(11) NOT NULL,
  `direction` varchar(10) NOT NULL,
  `Nlabel` varchar(128) DEFAULT NULL,
  `Nmessage` varchar(128) DEFAULT NULL,
  `Wlabel` varchar(128) DEFAULT NULL,
  `Wmessage` varchar(128) DEFAULT NULL,
  `Elabel` varchar(128) DEFAULT NULL,
  `Emessage` varchar(128) DEFAULT NULL,
  `Slabel` varchar(128) DEFAULT NULL,
  `Smessage` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`hotkeyid`),
  UNIQUE KEY `hotkeyid_UNIQUE` (`hotkeyid`),
  KEY `serverid_hotkey_idx` (`serverid`),
  CONSTRAINT `serverid_hotkey` FOREIGN KEY (`serverid`) REFERENCES `Characters` (`serverid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Hotkeys`
--

LOCK TABLES `Hotkeys` WRITE;
/*!40000 ALTER TABLE `Hotkeys` DISABLE KEYS */;
INSERT INTO `Hotkeys` VALUES (1,1260510,'Main Menu',' ','Responses',' ','Options',' ','Group',' ','Communicate'),(2,1260510,'N','feel yhe knife','I\'ll rip your flesh \'till there\'s no breath dismembered destiny','number 2','As soon as life has left your corpse i\'ll make you part of me','number 3','No emotion death is all I seek',' ','More Responses'),(3,1260510,'W',' ','Chat Mode',' ','Pet Command',' ','Grouping',' ','Chat Filter'),(4,1260510,'WN','Normal Say','|14','Guild Speak','|17','Group Speak','|15','Shout','|16'),(5,1260510,'E',' ','Attacking',' ','Creation',' ','Readiness',' ','Important!'),(6,1260510,'EN','Pulling','I am attacking %4','Assist Target','|22','Assist Leader','|21','Assist Me','I am attacking %4, please assist me!'),(7,1260510,'S',' ','Player',' ','Navigation',' ','Ask Help',' ','Action'),(8,1260510,'SN','Hail','Hail %4','Tell','|19','Reply','|18','Goodbye','Goodbye %4'),(9,1260510,'WW','Passive','|24',' ','More Pet Commands','Defensive','|25','Aggressive','|26'),(10,1260510,'EW','Invite','|4',' ','Organization','Need Group','%2 %3 seeking group!',' ','Hunting'),(11,1260510,'SW','North','North.','West','West.','East','East.','South','South.'),(12,1260510,'WE','Invite','|4','Boot Member','|5','Group Speak','|15','Leave Group','|6'),(13,1260510,'EE','Ready?','Ready?','Health/Power','Low on health / power.','Good to Go!','I am good to go!','Break 1min','I need a short break, be right back in a minute!'),(14,1260510,'SE','Request','Do you have spare time to answer a question?','Where to find?','Where can I find ...','Where am I? ','I am lost, where am I?','Where Town?','Where can I find the nearest town with merchants?'),(15,1260510,'NS','Thanks','|31','Nevermind','Never mind.',' ','Greetings',' ','Notifications'),(16,1260510,'WS','Ignore Target','|7','Ignore Guild','|12','Ignore Shouts','|10',' ','Restore Commands'),(17,1260510,'ES','Retreat!','RUN! This is a battle we cannot win!','Link Dead!','Someone just went Link Dead!','Peel!','HELP! I\'m being attacked, peel it off of me!','Medic!','MEDIC! I need healing badly!'),(18,1260510,'SS','Wave','|0','Point','|2','Cheer','|3','Bow','|1'),(19,1260510,'WWW','Neutral','|27','Pet Attack','|30','Pet Backoff','|29','Dismiss Pet','|28'),(20,1260510,'EWW','Request Roll','Roll for loot please!','Loot up!','Loot up if you want this.','Want Group?','Would you like to group?','Roll 0-99','|23'),(21,1260510,'NSE','Good','Good.','Bad','Bad.','How\'s it going?','How\'s it going?','Okay','Okay'),(22,1260510,'EWS','Follow me','Follow me.','New Hunt','Let\'s find another place to hunt.','Puller?','Who wants to handle pulling?','Where hunt?','Where shall we hunt?'),(23,1260510,'NSS','Sorry, Busy','Sorry, I am busy at this moment','Quit','I must leave and am logging out of Tunaria, goodbye!','Long Break','I need to take a break and will not be present for a while.','Keyboard','It will be difficult for me to respond sometimes (no keyboard)'),(24,1260511,'Main Menu',' ','Responses',' ','Options',' ','Group',' ','Communicate'),(25,1260511,'N','feel yhe knife','I\'ll rip your flesh \'till there\'s no breath dismembered destiny','number 2','As soon as life has left your corpse i\'ll make you part of me','number 3','No emotion death is all I seek',' ','More Responses'),(26,1260511,'W',' ','Chat Mode',' ','Pet Command',' ','Grouping',' ','Chat Filter'),(27,1260511,'WN','Normal Say','|14','Guild Speak','|17','Group Speak','|15','Shout','|16'),(28,1260511,'E',' ','Attacking',' ','Creation',' ','Readiness',' ','Important!'),(29,1260511,'EN','Pulling','I am attacking %4','Assist Target','|22','Assist Leader','|21','Assist Me','I am attacking %4, please assist me!'),(30,1260511,'S',' ','Player',' ','Navigation',' ','Ask Help',' ','Action'),(31,1260511,'SN','Hail','Hail %4','Tell','|19','Reply','|18','Goodbye','Goodbye %4'),(32,1260511,'WW','Passive','|24',' ','More Pet Commands','Defensive','|25','Aggressive','|26'),(33,1260511,'EW','Invite','|4',' ','Organization','Need Group','%2 %3 seeking group!',' ','Hunting'),(34,1260511,'SW','North','North.','West','West.','East','East.','South','South.'),(35,1260511,'WE','Invite','|4','Boot Member','|5','Group Speak','|15','Leave Group','|6'),(36,1260511,'EE','Ready?','Ready?','Health/Power','Low on health / power.','Good to Go!','I am good to go!','Break 1min','I need a short break, be right back in a minute!'),(37,1260511,'SE','Request','Do you have spare time to answer a question?','Where to find?','Where can I find ...','Where am I? ','I am lost, where am I?','Where Town?','Where can I find the nearest town with merchants?'),(38,1260511,'NS','Thanks','|31','Nevermind','Never mind.',' ','Greetings',' ','Notifications'),(39,1260511,'WS','Ignore Target','|7','Ignore Guild','|12','Ignore Shouts','|10',' ','Restore Commands'),(40,1260511,'ES','Retreat!','RUN! This is a battle we cannot win!','Link Dead!','Someone just went Link Dead!','Peel!','HELP! I\'m being attacked, peel it off of me!','Medic!','MEDIC! I need healing badly!'),(41,1260511,'SS','Wave','|0','Point','|2','Cheer','|3','Bow','|1'),(42,1260511,'WWW','Neutral','|27','Pet Attack','|30','Pet Backoff','|29','Dismiss Pet','|28'),(43,1260511,'EWW','Request Roll','Roll for loot please!','Loot up!','Loot up if you want this.','Want Group?','Would you like to group?','Roll 0-99','|23'),(44,1260511,'NSE','Good','Good.','Bad','Bad.','How\'s it going?','How\'s it going?','Okay','Okay'),(45,1260511,'EWS','Follow me','Follow me.','New Hunt','Let\'s find another place to hunt.','Puller?','Who wants to handle pulling?','Where hunt?','Where shall we hunt?'),(46,1260511,'NSS','Sorry, Busy','Sorry, I am busy at this moment','Quit','I must leave and am logging out of Tunaria, goodbye!','Long Break','I need to take a break and will not be present for a while.','Keyboard','It will be difficult for me to respond sometimes (no keyboard)'),(70,1224955,'Main Menu','Responses',' ','Options',' ','Group',' ','Communicate',' '),(71,1224955,'N','Yes','Yes.','No','No.','I don\'t know','','More Responses',' '),(72,1224955,'W','Chat Mode',' ','Pet Command',' ','Grouping',' ','Chat Filter',' '),(73,1224955,'WN','Normal Say','|14','Guild Speak','|17','Group Speak','|15','Shout','|16'),(74,1224955,'E','Attacking',' ','Creation',' ','Readiness',' ','Important!',' '),(75,1224955,'EN','Pulling','I am attacking %4','Assist Target','|22','Assist Leader','|21','Assist Me','I am attacking %4, please assist me!'),(76,1224955,'S','Player',' ','Navigation',' ','Ask Help++','','Action',' '),(77,1224955,'SN','Hail','Heya %4','Tell','|19','Reply','|18','More',''),(78,1224955,'WW','Passive','|24','More Pet Commands',' ','Defensive','|25','Aggressive','|26'),(79,1224955,'EW','Invite','|4','Organization',' ','Need Group','%2 Savage %3 seeking group!','Hunting',' '),(80,1224955,'SW','North','North.','West','West.','East','East.','South','South.'),(81,1224955,'NE','Me dunno','Me dunno, me just stoopid Troll.','Maybe','Maybe.','I don\'t know','I don\'t know.','I don\'t care','I don\'t care.'),(82,1224955,'WE','Invite','|4','Boot Member','|5','Group Speak','|15','Leave Group','|6'),(83,1224955,'EE','Ready?','Ready?','Health/Power','Low on health / power.','Good to Go!','I am good to go!','Break 1min','I need a short break, be right back in a minute!'),(84,1224955,'SE','Hotkeys','','LFD ...','','Ask Help','','WTB ...',''),(85,1224955,'SEN','Duel Invite','|34','Good Fight','Good fight %4','Surrender','|35','Good luck','Good luck %4'),(86,1224955,'NS','Thanks','Thanks!','Never mind','Never mind.','Greetings',' ','Notifications',' '),(87,1224955,'WS','Ignore Target','|7','Ignore Guild','|12','Ignore Shouts','|10','Restore Commands',' '),(88,1224955,'ES','Retreat!','RUN! This is a battle we cannot win!','Link Dead!','Someone just went Link Dead!','Peel!','HELP! I\'m being attacked, peel it off of me!','Medic!','MEDIC! I need healing badly!'),(89,1224955,'SS','Wave','|0','Point','|2','Cheer','|3','Bow','|1'),(90,1224955,'WWW','Neutral','|27','Pet Attack','|30','Pet Backoff','|29','Dismiss Pet','|28'),(91,1224955,'EWW','Request Roll','Roll for loot please!','Loot up!','Loot up if you want this.','Want Group?','Would you like to group?','Roll 0-99','|23'),(92,1224955,'SEW','Seeking duel','%2 %3 seeking duel!','Anyone duel?','Anyone care to duel?','Dudd FH','%4, Dudd from Ferran\'s Hope says hello.','Ready?','Ready %4?'),(93,1224955,'SEE','Request','Do you have spare time to answer a question?','Where to find?','Where can I find ...','Where am I?','I am lost, where am I?','Where Town?','Where can I find the nearest town with merchants?'),(94,1224955,'NSE','Good','Good.','Bad','Bad.','How\'s it?','How\'s it going?','Okay','Okay'),(95,1224955,'SNS','/R Hail','/R Hail %4','Follow','|31','Duel','%2 %3 seeking duel!','Goodbye','Goodbye %4'),(96,1224955,'EWS','Follow me','Follow me.','New Hunt','Let\'s find another place to hunt.','Puller?','Who wants to handle pulling?','Where hunt?','Where shall we hunt?'),(97,1224955,'SES','','','','','','','',''),(98,1224955,'NSS','Sorry, busy','Sorry, I am busy at this moment.','Quit','I must leave and am logging out of Tunaria, goodbye!','Long Break','I need to take a break and will not be present for a while.','Keyboard','It will be difficult for me to respond sometimes (no keyboard).'),(99,1224955,'WSS','Privacy List','|9','Restore guild','|13','Restore shout','|11','Stop ignoring','|8');
/*!40000 ALTER TABLE `Hotkeys` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Spells`
--

DROP TABLE IF EXISTS `Spells`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `Spells` (
  `serverid` int(11) NOT NULL,
  `spellid` int(11) NOT NULL AUTO_INCREMENT,
  `addedorder` int(11) NOT NULL,
  `onHotBar` int(11) NOT NULL,
  `whereonBar` int(11) NOT NULL,
  `unk1` int(11) NOT NULL,
  `showhide` int(11) NOT NULL,
  PRIMARY KEY (`spellid`),
  KEY `spell_serverid_idx` (`serverid`),
  CONSTRAINT `spell_serverid` FOREIGN KEY (`serverid`) REFERENCES `Characters` (`serverid`),
  CONSTRAINT `spell_spellid` FOREIGN KEY (`spellid`) REFERENCES `spellPattern` (`spellid`)
) ENGINE=InnoDB AUTO_INCREMENT=6619 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Spells`
--

LOCK TABLES `Spells` WRITE;
/*!40000 ALTER TABLE `Spells` DISABLE KEYS */;
INSERT INTO `Spells` VALUES (1224955,178,3,0,-1,0,1),(1224955,179,0,1,8,0,1),(1224955,180,5,0,-1,0,1),(1224955,181,7,0,-1,0,1),(1224955,182,55,0,-1,0,1),(1224955,183,4,0,-1,0,1),(1224955,184,6,0,-1,0,1),(1224955,185,11,0,-1,0,1),(1224955,186,10,0,-1,0,1),(1224955,187,14,0,-1,0,1),(1224955,356,9,1,7,0,1),(1224955,357,12,0,-1,0,1),(1224955,358,13,0,-1,0,1),(1224955,671,21,0,-1,0,1),(1224955,672,33,0,-1,0,1),(1224955,674,19,0,-1,0,1),(1224955,675,25,0,-1,0,1),(1224955,676,30,0,-1,0,1),(1224955,678,24,0,-1,0,1),(1224955,679,57,0,-1,0,1),(1224955,681,20,0,-1,0,1),(1224955,682,58,1,2,0,1),(1224955,683,15,0,-1,0,1),(1224955,684,22,0,-1,0,1),(1224955,685,26,0,-1,0,1),(1224955,686,1,0,-1,0,1),(1224955,687,51,0,-1,0,1),(1224955,1542,2,0,-1,0,1),(1224955,1588,8,0,-1,0,1),(1224955,1615,27,1,3,0,1),(1224955,1616,34,0,-1,0,1),(1224955,1722,23,0,-1,0,1),(1224955,1723,16,0,-1,0,1),(1224955,1725,29,0,-1,0,1),(1224955,2083,65,0,-1,0,1),(1224955,2675,56,0,-1,0,1),(1224955,2775,40,0,-1,0,1),(1224955,2776,61,0,-1,0,1),(1224955,2777,54,1,1,0,1),(1224955,2778,62,0,-1,0,1),(1224955,2844,52,0,-1,0,1),(1224955,3090,60,0,-1,0,1),(1224955,3091,42,1,4,0,1),(1224955,3915,39,1,6,0,1),(1224955,4090,18,1,0,0,1),(1224955,4321,28,0,-1,0,1),(1224955,4332,41,1,9,0,1),(1224955,4335,64,0,-1,0,1),(1224955,4386,31,0,-1,0,1),(1224955,4412,32,0,-1,0,1),(1224955,5246,59,0,-1,0,1),(1224955,5873,63,0,-1,0,1),(1224955,6247,53,1,5,0,1),(1224955,6257,17,0,-1,0,1),(1224955,6569,36,0,-1,0,1),(1224955,6576,49,0,-1,0,1),(1224955,6587,43,0,-1,0,1),(1224955,6594,35,0,-1,0,1),(1224955,6595,37,0,-1,0,1),(1224955,6599,38,0,-1,0,1),(1224955,6605,44,0,-1,0,1),(1224955,6610,46,0,-1,0,1),(1224955,6611,45,0,-1,0,1),(1224955,6613,48,0,-1,0,1),(1224955,6617,47,0,-1,0,1),(1224955,6618,50,0,-1,0,1);
/*!40000 ALTER TABLE `Spells` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `charInventory`
--

DROP TABLE IF EXISTS `charInventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `charInventory` (
  `itemid` int(11) NOT NULL AUTO_INCREMENT,
  `serverid` int(11) NOT NULL,
  `stackleft` int(11) NOT NULL,
  `remainHP` int(11) NOT NULL,
  `remaincharge` int(11) NOT NULL,
  `patternid` int(11) NOT NULL,
  `equiploc` int(11) NOT NULL,
  `location` int(11) NOT NULL,
  `listnumber` int(11) NOT NULL,
  PRIMARY KEY (`itemid`),
  UNIQUE KEY `itemid_UNIQUE` (`itemid`),
  KEY `inventory_serverid_idx` (`serverid`),
  KEY `inventory_patternid` (`patternid`),
  CONSTRAINT `inventory_patternid` FOREIGN KEY (`patternid`) REFERENCES `itemPattern` (`patternid`) ON UPDATE CASCADE,
  CONSTRAINT `inventory_serverid` FOREIGN KEY (`serverid`) REFERENCES `Characters` (`serverid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `charInventory`
--

LOCK TABLES `charInventory` WRITE;
/*!40000 ALTER TABLE `charInventory` DISABLE KEYS */;
INSERT INTO `charInventory` VALUES (1,1224955,1,0,0,31001,2,1,0),(2,1224955,1,0,0,31002,12,1,1),(3,1224955,1,0,0,31003,14,1,2),(4,1224955,1,0,0,31000,19,1,3),(5,1224955,1,0,0,31004,11,1,4),(6,1260510,1,0,0,31005,2,1,0),(7,1260510,1,0,0,31006,15,1,1),(8,1260510,1,0,0,31007,19,1,2),(9,1260510,1,0,0,31008,11,1,3),(10,1260511,1,0,0,31005,2,1,0),(11,1260511,1,0,0,31006,15,1,1),(12,1260511,1,0,0,31007,19,1,2),(13,1260511,1,0,0,31008,11,1,4);
/*!40000 ALTER TABLE `charInventory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `characterModel`
--

DROP TABLE IF EXISTS `characterModel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `characterModel` (
  `sex` varchar(16) NOT NULL,
  `modelid` bigint(20) NOT NULL AUTO_INCREMENT,
  `race` varchar(8) NOT NULL,
  PRIMARY KEY (`modelid`)
) ENGINE=InnoDB AUTO_INCREMENT=3783858680 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `characterModel`
--

LOCK TABLES `characterModel` WRITE;
/*!40000 ALTER TABLE `characterModel` DISABLE KEYS */;
INSERT INTO `characterModel` VALUES ('Male',-2071956336,'OGR'),('Female',-1977112783,'HLF'),('Female',-1935979719,'DELF'),('Male',-1545912350,'HLF'),('Male',-1449366763,'GNO'),('Male',-1436875705,'DWF'),('Female',-1396700337,'OGR'),('Female',-1001728746,'BAR'),('Male',-657100808,'ELF'),('Female',-511108617,'ELF'),('Female',223572789,'HUM'),('Male',839650119,'ERU'),('Female',889978302,'DWF'),('Female',1279296764,'GNO'),('Male',1282385202,'TRL'),('Female',1306732289,'TRL'),('Female',1327119550,'ERU'),('Male',1640644319,'BAR'),('Male',1893243078,'HUM'),('Male',2128125354,'DELF');
/*!40000 ALTER TABLE `characterModel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `defaultCharacter`
--

DROP TABLE IF EXISTS `defaultCharacter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `defaultCharacter` (
  `defaultCharacterID` int(11) NOT NULL AUTO_INCREMENT,
  `tclass` varchar(6) NOT NULL,
  `race` varchar(8) NOT NULL,
  `humType` varchar(12) NOT NULL,
  `level` int(11) NOT NULL,
  `tunar` int(11) NOT NULL,
  `bankTunar` int(11) NOT NULL,
  `unusedTP` int(11) NOT NULL,
  `totalTP` int(11) NOT NULL,
  `x` float NOT NULL,
  `z` float NOT NULL,
  `y` float DEFAULT NULL,
  `facing` float NOT NULL,
  `world` int(11) DEFAULT NULL,
  `strength` int(11) DEFAULT NULL,
  `stamina` int(11) DEFAULT NULL,
  `agility` int(11) DEFAULT NULL,
  `dexterity` int(11) DEFAULT NULL,
  `wisdom` int(11) DEFAULT NULL,
  `intel` int(11) DEFAULT NULL,
  `charisma` int(11) DEFAULT NULL,
  PRIMARY KEY (`defaultCharacterID`)
) ENGINE=InnoDB AUTO_INCREMENT=76 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `defaultCharacter`
--

LOCK TABLES `defaultCharacter` WRITE;
/*!40000 ALTER TABLE `defaultCharacter` DISABLE KEYS */;
INSERT INTO `defaultCharacter` VALUES (1,'CL','HUM','Freeport',1,5000,0,20,475,25477.1,53.501,15601.4,3.15526,0,65,65,60,55,80,55,75),(2,'BRD','HUM','Freeport',1,5000,0,20,475,25416.2,65.9091,15606.2,3.22916,0,60,75,80,65,55,55,65),(3,'ALC','HUM','Freeport',1,5000,0,20,475,25296.8,70.9619,15551.2,3.49787,0,55,65,60,65,75,80,55),(4,'WAR','HUM','Freeport',1,5000,0,20,475,25486.5,61.3651,15547,-1.61104,0,75,80,65,65,60,55,55),(5,'WIZ','HUM','Freeport',1,5000,0,20,475,25300.8,54.27,15558.6,2.38991,0,65,60,65,75,55,80,55),(6,'SK','HUM','Freeport',1,5000,0,20,475,25478.1,53.501,15607.3,1.27313,0,75,80,65,65,55,60,55),(7,'RGE','HUM','Freeport',1,5000,0,20,475,25474.4,53.501,15612.9,4.31982,0,65,65,75,80,55,55,60),(8,'NEC','HUM','Freeport',1,5000,0,20,475,25466.1,60.2276,15710.8,5.36427,0,65,75,55,65,60,80,55),(9,'MAG','HUM','Freeport',1,5000,0,20,475,25289.7,63.7276,15552.5,5.49659,0,55,65,75,60,65,80,55),(10,'ENC','HUM','Freeport',1,5000,0,20,475,25299.8,63.7276,15559,-0.610595,0,55,55,65,65,60,80,75),(11,'WAR','HUM','Qeynos',1,5000,0,20,475,4243.7,58.1338,17169,-1.55227,0,75,80,65,65,60,55,55),(12,'RAN','HUM','Qeynos',1,5000,0,20,475,9005.05,56.8153,12749.2,3.50485,0,65,65,75,80,60,55,55),(13,'PAL','HUM','Qeynos',1,5000,0,20,475,4225.26,58.4463,17325,-0.611862,0,75,80,65,65,60,55,55),(14,'MNK','HUM','Qeynos',1,5000,0,20,475,5593.73,58.5088,17627,-1.53023,0,65,75,65,80,60,55,55),(15,'BRD','HUM','Qeynos',1,5000,0,20,475,4555,58.2126,17108,1.23694,0,60,75,80,65,55,55,65),(16,'RGE','HUM','Qeynos',1,5000,0,20,475,4499.56,57.8586,17115,1.58916,0,65,65,75,80,55,55,60),(17,'DRD','HUM','Qeynos',1,5000,0,20,475,9386.06,61.1802,12557,2.33274,0,65,55,55,75,80,60,65),(18,'CL','HUM','Qeynos',1,5000,0,20,475,4287.73,57.8967,17287,-2.75868,0,65,65,60,55,80,55,75),(19,'MAG','HUM','Qeynos',1,5000,0,20,475,4224.5,62.0449,17085,5.11527,0,55,65,75,60,65,80,55),(20,'ENC','HUM','Qeynos',1,5000,0,20,475,4224.5,62.0449,17085,5.11527,0,55,55,65,65,60,80,75),(21,'WIZ','HUM','Qeynos',1,5000,0,20,475,4224.5,62.0449,17085,5.11527,0,65,60,65,75,55,80,55),(22,'ALC','HUM','Qeynos',1,5000,0,20,475,4453.6,58.2187,17295,2.47965,0,55,65,60,65,75,80,55),(23,'SHA','BAR','Other',1,5000,0,20,475,13298,65.1573,4310.94,-0.750984,0,65,80,60,60,75,55,60),(24,'WAR','BAR','Other',1,5000,0,20,475,13298,65.1573,4310.94,-0.750984,0,65,80,60,60,75,55,60),(25,'RGE','BAR','Other',1,5000,0,20,475,13115,54.3994,4316.31,2.92491,0,75,70,70,80,50,55,55),(26,'ALC','DELF','Other',1,5000,0,20,475,25195.1,6.59631,8845.75,-0.42117,0,50,65,55,70,75,90,50),(27,'CL','DELF','Other',1,5000,0,20,475,24775.6,7.29475,8814.61,-1.59202,0,60,65,55,60,80,65,70),(28,'ENC','DELF','Other',1,5000,0,20,475,25006.8,7.51975,8756.37,-2.32213,0,50,55,60,70,60,90,70),(29,'MAG','DELF','Other',1,5000,0,20,475,25002.1,7.51975,8745.99,0.635421,0,50,65,70,65,65,90,50),(30,'NEC','DELF','Other',1,5000,0,20,475,24630.1,5.76662,8837.69,-1.8864,0,60,75,50,70,60,90,50),(31,'RGE','DELF','Other',1,5000,0,20,475,25316.7,1.20725,8766.33,-1.52219,0,60,65,70,85,55,65,55),(32,'SK','DELF','Other',1,5000,0,20,475,24659.8,5.76662,8836.48,-5.12263,0,70,80,60,70,55,70,50),(33,'WAR','DELF','Other',1,5000,0,20,475,25220.1,7.35246,8626.18,-0.00439516,0,70,80,60,70,60,65,50),(34,'WIZ','DELF','Other',1,5000,0,20,475,24971,7.51975,8746.44,-0.892634,0,60,60,60,80,55,90,50),(35,'CL','DWF','Other',1,5000,0,20,475,15445.9,82.9672,8743.75,1.50538,0,70,70,55,55,85,50,70),(36,'PAL','DWF','Other',1,5000,0,20,475,15437.3,82.9634,8727.31,2.32993,0,80,85,60,65,65,50,50),(37,'WAR','DWF','Other',1,5000,0,20,475,15190.8,99.9204,8767.33,-1.53629,0,80,85,60,65,65,50,50),(38,'RGE','DWF','Other',1,5000,0,20,475,15382.3,88.5664,8687.14,-2.35616,0,70,70,70,80,60,50,55),(39,'ALC','ELF','Other',1,5000,0,20,475,18909,53.876,6567.84,0.0276634,0,50,60,55,70,80,80,60),(40,'BRD','ELF','Other',1,5000,0,20,475,18126.4,75.5557,7246.11,1.24227,0,55,70,75,70,60,55,70),(41,'CL','ELF','Other',1,5000,0,20,475,19173.3,63.3994,6306.74,0.475139,0,60,60,55,60,85,55,80),(42,'DRD','ELF','Other',1,5000,0,20,475,18344.5,75.5557,7318.33,-1.70491,0,60,50,50,80,85,60,70),(43,'ENC','ELF','Other',1,5000,0,20,475,18888,63.3994,6544.83,-3.1319,0,50,50,60,70,65,80,80),(44,'MAG','ELF','Other',1,5000,0,20,475,18892,63.3994,6568.56,0.0108012,0,50,60,70,65,70,80,60),(45,'PAL','ELF','Other',1,5000,0,20,475,18985.5,63.3994,6356.56,-2.35888,0,70,85,60,70,65,55,60),(46,'RAN','ELF','Other',1,5000,0,20,475,18380.2,75.5557,7378.39,-2.40724,0,60,60,70,85,65,55,60),(47,'RGE','ELF','Other',1,5000,0,20,475,18216.6,75.5557,7294.82,-2.91637,0,60,60,70,85,60,55,65),(48,'WIZ','ELF','Other',1,5000,0,20,475,18892.5,63.3994,6559.51,2.19348,0,60,55,60,80,60,80,60),(49,'ALC','ERU','Other',1,5000,0,20,475,4809.2,54.8135,21427,-2.21056,0,50,60,55,65,80,95,50),(50,'CL','ERU','Other',1,5000,0,20,475,4735.44,66.1885,21599.5,3.43546,0,60,60,55,55,85,70,70),(51,'ENC','ERU','Other',1,5000,0,20,475,4561.02,120.376,21707.4,1.60734,0,50,50,60,65,65,95,70),(52,'MAG','ERU','Other',1,5000,0,20,475,4440.21,169.564,21840.8,3.13725,0,50,60,70,60,70,95,50),(53,'PAL','ERU','Other',1,5000,0,20,475,4727.37,54.126,21654.9,1.11078,0,70,75,60,65,65,70,50),(54,'SK','ERU','Other',1,5000,0,20,475,4683.29,57.0635,21726,0.0455922,0,70,75,60,65,60,75,50),(55,'NEC','ERU','Other',1,5000,0,20,475,4685.45,57.0635,21693,1.55153,0,60,70,50,65,65,95,50),(56,'WIZ','ERU','Other',1,5000,0,20,475,4191,108.813,21517.4,3.09498,0,60,55,60,75,60,95,50),(57,'ALC','GNO','Other',1,5000,0,20,475,23864.6,32.7401,6243.07,0.361819,0,50,60,65,70,70,90,50),(58,'CL','GNO','Other',1,5000,0,20,475,23587.8,32.626,6107.76,2.31424,0,60,60,65,60,75,65,70),(59,'ENC','GNO','Other',1,5000,0,20,475,23757.3,32.626,6123.12,-1.86151,0,50,50,70,70,55,90,70),(60,'MAG','GNO','Other',1,5000,0,20,475,23778.7,32.626,6101.91,-3.0061,0,50,60,80,65,60,90,50),(61,'NEC','GNO','Other',1,5000,0,20,475,23695.5,36.0213,6541.96,2.41319,0,60,70,60,70,55,90,50),(62,'RGE','GNO','Other',1,5000,0,20,475,23677.9,25.3447,6390.02,1.93538,0,60,60,80,85,50,65,55),(63,'WAR','GNO','Other',1,5000,0,20,475,23478.1,37.2401,6209.86,0.722441,0,70,75,70,70,55,65,50),(64,'WIZ','GNO','Other',1,5000,0,20,475,23808.8,32.7401,6305.64,-5.47559,0,60,55,70,80,50,90,50),(65,'DRD','HLF','Other',1,5000,0,20,475,18885.9,58.5947,10860.8,2.5023,0,60,60,60,85,75,55,60),(66,'RGE','HLF','Other',1,5000,0,20,475,18918.9,58.5947,11002.5,0.892183,0,60,70,80,90,50,50,55),(67,'WAR','HLF','Other',1,5000,0,20,475,18776.3,62.376,10959.6,-3.06876,0,70,85,70,75,55,50,50),(68,'CL','HLF','Other',1,5000,0,20,475,18921.1,58.5947,11246.8,3.17584,0,60,70,65,65,75,50,70),(69,'SK','OGR','Other',1,5000,0,20,475,9379.49,30.3822,7134.56,1.79931,1,90,90,60,60,50,55,50),(70,'WAR','OGR','Other',1,5000,0,20,475,9377.67,53.1885,7385.81,-1.53102,1,90,90,60,60,55,50,50),(71,'SHA','OGR','Other',1,5000,0,20,475,8739.3,79.126,7345.37,-1.54821,1,70,85,60,55,75,50,60),(72,'NEC','OGR','Other',1,5000,0,20,475,9378.09,30.3822,7123.08,-1.15251,1,80,85,50,60,55,75,50),(73,'WAR','TRL','Other',1,5000,0,20,475,25407.7,70.0135,31410.3,-1.93956,0,90,90,60,60,55,50,50),(74,'SK','TRL','Other',1,5000,0,20,475,25628,75.001,31591,5.2606,0,90,90,60,60,50,55,50),(75,'SHA','TRL','Other',1,5000,0,20,475,25266.9,63.751,31424.6,-1.72244,0,70,85,60,55,5,50,60);
/*!40000 ALTER TABLE `defaultCharacter` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `defaultSpells`
--

DROP TABLE IF EXISTS `defaultSpells`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `defaultSpells` (
  `def_spell_id` int(11) NOT NULL AUTO_INCREMENT,
  `tclass` varchar(6) NOT NULL,
  `spellid` int(11) NOT NULL,
  `addedorder` int(11) NOT NULL,
  `onHotBar` int(11) NOT NULL,
  `whereonBar` int(11) NOT NULL,
  `unk1` int(11) NOT NULL,
  `showhide` int(11) NOT NULL,
  PRIMARY KEY (`def_spell_id`),
  KEY `defspell_spellid_idx` (`spellid`),
  CONSTRAINT `defspell_spellid` FOREIGN KEY (`spellid`) REFERENCES `Spells` (`spellid`)
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `defaultSpells`
--

LOCK TABLES `defaultSpells` WRITE;
/*!40000 ALTER TABLE `defaultSpells` DISABLE KEYS */;
INSERT INTO `defaultSpells` VALUES (1,'WAR',178,3,1,1,0,1),(2,'WAR',179,0,1,0,0,1),(3,'CL',178,3,1,1,0,1),(4,'CL',179,0,1,0,0,1),(5,'SK',178,3,1,1,0,1),(6,'SK',179,0,1,0,0,1),(7,'RAN',178,3,1,1,0,1),(8,'RAN',179,0,1,0,0,1),(9,'PAL',178,3,1,1,0,1),(10,'PAL',179,0,1,0,0,1),(11,'MNK',178,3,1,1,0,1),(12,'MNK',179,0,1,0,0,1),(13,'BRD',178,3,1,1,0,1),(14,'BRD',179,0,1,0,0,1),(15,'RGE',178,3,1,1,0,1),(16,'RGE',179,0,1,0,0,1),(17,'DRD',178,3,1,1,0,1),(18,'DRD',179,0,1,0,0,1),(19,'SHA',178,3,1,1,0,1),(20,'SHA',179,0,1,0,0,1),(21,'MAG',178,3,1,1,0,1),(22,'MAG',179,0,1,0,0,1),(23,'NEC',178,3,1,1,0,1),(24,'NEC',179,0,1,0,0,1),(25,'ENC',178,3,1,1,0,1),(26,'ENC',179,0,1,0,0,1),(27,'WIZ',178,3,1,1,0,1),(28,'WIZ',179,0,1,0,0,1),(29,'ALC',178,3,1,1,0,1),(30,'ALC',179,0,1,0,0,1);
/*!40000 ALTER TABLE `defaultSpells` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `defaultcharInventory`
--

DROP TABLE IF EXISTS `defaultcharInventory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `defaultcharInventory` (
  `itemid` int(11) NOT NULL AUTO_INCREMENT,
  `tclass` varchar(6) NOT NULL,
  `stackleft` int(11) NOT NULL,
  `remainHP` int(11) NOT NULL,
  `remaincharge` int(11) NOT NULL,
  `patternid` int(11) NOT NULL,
  `equiploc` int(11) NOT NULL,
  `location` int(11) NOT NULL,
  `listnumber` int(11) NOT NULL,
  PRIMARY KEY (`itemid`),
  KEY `defcharinv_patternid_idx` (`patternid`),
  CONSTRAINT `defcharinv_patternid` FOREIGN KEY (`patternid`) REFERENCES `itemPattern` (`patternid`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `defaultcharInventory`
--

LOCK TABLES `defaultcharInventory` WRITE;
/*!40000 ALTER TABLE `defaultcharInventory` DISABLE KEYS */;
INSERT INTO `defaultcharInventory` VALUES (1,'WAR',1,0,0,31002,-1,1,0),(2,'RAN',1,0,0,31002,-1,1,0),(3,'PAL',1,0,0,31002,-1,1,0),(4,'SK',1,0,0,31002,-1,1,0),(5,'MNK',1,0,0,31002,-1,1,0),(6,'BRD',1,0,0,31002,-1,1,0),(7,'RGE',1,0,0,31002,-1,1,0),(8,'DRD',1,0,0,31002,-1,1,0),(9,'SHA',1,0,0,31002,-1,1,0),(10,'CL',1,0,0,31002,-1,1,0),(11,'MAG',1,0,0,31002,-1,1,0),(12,'NEC',1,0,0,31002,-1,1,0),(13,'ENC',1,0,0,31002,-1,1,0),(14,'WIZ',1,0,0,31002,-1,1,0),(15,'ALC',1,0,0,31002,-1,1,0);
/*!40000 ALTER TABLE `defaultcharInventory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `itemPattern`
--

DROP TABLE IF EXISTS `itemPattern`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `itemPattern` (
  `patternid` int(11) NOT NULL AUTO_INCREMENT,
  `patternfam` int(11) NOT NULL,
  `unk1` int(11) NOT NULL,
  `itemicon` bigint(20) NOT NULL,
  `unk2` int(11) NOT NULL,
  `equipslot` int(11) NOT NULL,
  `unk3` int(11) NOT NULL,
  `trade` int(11) NOT NULL,
  `rent` int(11) NOT NULL,
  `unk4` int(11) NOT NULL,
  `attacktype` int(11) NOT NULL,
  `weapondamage` int(11) NOT NULL,
  `unk5` int(11) NOT NULL,
  `levelreq` int(11) NOT NULL,
  `maxstack` int(11) NOT NULL,
  `maxhp` int(11) NOT NULL,
  `duration` int(11) NOT NULL,
  `classuse` int(11) NOT NULL,
  `raceuse` int(11) NOT NULL,
  `procanim` int(11) NOT NULL,
  `lore` int(11) NOT NULL,
  `unk6` int(11) NOT NULL,
  `craft` int(11) NOT NULL,
  `itemname` varchar(32) NOT NULL,
  `itemdesc` varchar(256) NOT NULL,
  `model` bigint(20) NOT NULL,
  `color` bigint(20) NOT NULL,
  `str` int(11) DEFAULT NULL,
  `sta` int(11) DEFAULT NULL,
  `agi` int(11) DEFAULT NULL,
  `wis` int(11) DEFAULT NULL,
  `dex` int(11) DEFAULT NULL,
  `cha` int(11) DEFAULT NULL,
  `int` int(11) DEFAULT NULL,
  `HPMAX` int(11) DEFAULT NULL,
  `POWMAX` int(11) DEFAULT NULL,
  `PoT` int(11) DEFAULT NULL,
  `HoT` int(11) DEFAULT NULL,
  `AC` int(11) DEFAULT NULL,
  `PR` int(11) DEFAULT NULL,
  `DR` int(11) DEFAULT NULL,
  `FR` int(11) DEFAULT NULL,
  `CR` int(11) DEFAULT NULL,
  `LR` int(11) DEFAULT NULL,
  `AR` int(11) DEFAULT NULL,
  PRIMARY KEY (`patternid`),
  UNIQUE KEY `patternid_UNIQUE` (`patternid`)
) ENGINE=InnoDB AUTO_INCREMENT=31009 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `itemPattern`
--

LOCK TABLES `itemPattern` WRITE;
/*!40000 ALTER TABLE `itemPattern` DISABLE KEYS */;
INSERT INTO `itemPattern` VALUES (31000,1,0,-220605916,0,1,0,1,1,0,0,0,0,60,1,18500,75,0,0,0,0,0,0,'Wind Golem Gloves','Basic Wind golem gloves',4,1073774847,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123),(31001,1,0,1394465037,0,21,0,1,1,0,0,0,0,60,1,18500,75,0,0,0,0,0,0,'Ceremonial Vestments','Rediculously Rare Robe, most would kill to have it',0,4294967295,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123),(31002,1,0,-1604900612,0,14,0,1,1,0,1,999,0,60,1,18500,75,0,0,0,0,0,0,'Khal Sword','A gemless sword dropped by the Khal Warlord\'s',1857495629,0,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123),(31003,1,0,848266163,0,15,0,1,1,0,1,999,0,60,1,18500,75,0,0,0,0,0,0,'Sapphire\'s Sword','Rare sword of Sapphire',-1269925226,0,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123),(31004,1,0,549277255,0,13,0,1,1,0,0,0,0,60,1,18500,75,0,0,0,0,0,0,'Wind Golem Boots','Basic wind golem boots',4,1073774847,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123),(31005,1,0,-1149496641,0,21,0,1,1,0,0,0,0,60,1,18500,75,0,0,0,0,0,0,'Khal Healer Robe','Rare robe from the legendary Khal Warlord',2,4205785855,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123),(31006,1,0,-2055321559,0,16,0,1,1,0,4,999,0,60,1,18500,75,0,0,0,0,0,0,'Staff of the Jade Forest','Legendary Staff from Nagafen',724832212,0,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123),(31007,1,0,76743497,0,1,0,1,1,0,0,0,0,60,1,18500,75,0,0,0,0,0,0,'Healer gloves','These gloves are for healers',2,8388863,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123),(31008,1,0,1731843454,0,13,0,1,1,0,0,0,0,60,1,18500,75,0,0,0,0,0,0,'Healer boots','These boots are for healers',2,8388863,13,65,65,76,56,0,112,0,12,987,0,62,23,12,0,0,927,123);
/*!40000 ALTER TABLE `itemPattern` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `spellPattern`
--

DROP TABLE IF EXISTS `spellPattern`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `spellPattern` (
  `spellid` int(11) NOT NULL AUTO_INCREMENT,
  `abilitylvl` int(11) NOT NULL,
  `unk2` int(11) NOT NULL,
  `unk3` int(11) NOT NULL,
  `range` int(11) NOT NULL,
  `casttime` int(11) NOT NULL,
  `power` int(11) NOT NULL,
  `iconColor` bigint(20) NOT NULL,
  `icon` bigint(20) NOT NULL,
  `scope` int(11) NOT NULL,
  `recast` int(11) NOT NULL,
  `eqprequire` int(11) NOT NULL,
  `spellname` varchar(32) NOT NULL,
  `spelldesc` varchar(256) NOT NULL,
  PRIMARY KEY (`spellid`),
  UNIQUE KEY `spellid_UNIQUE` (`spellid`)
) ENGINE=InnoDB AUTO_INCREMENT=6619 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `spellPattern`
--

LOCK TABLES `spellPattern` WRITE;
/*!40000 ALTER TABLE `spellPattern` DISABLE KEYS */;
INSERT INTO `spellPattern` VALUES (178,1,0,0,5,0,8,486870879,-1265863830,1,30,63,'Quick Strike','This quick strike will allow you to do more damage.'),(179,3,0,0,5,0,23,486870879,638503769,1,6,255,'Kick','An attack that allows you to kick your enemy.'),(180,4,0,0,5,1,24,-1809115846,-630227479,0,4,63,'Concentration','An offensive stance that increases your dexterity.'),(181,7,0,0,5,0,168,-1809115846,1028836133,0,120,255,'Furious Defense','A defensive stance that lowers your offense.'),(182,8,0,0,5,0,48,-1809115846,638503769,0,120,255,'Sprint','Allows you to run faster for a short period of time.'),(183,5,0,0,15,0,19,486870879,-49534593,1,5,63,'Taunt','Allows you to gain the attention of your enemy.'),(184,8,0,0,15,1,72,593297002,-630227479,2,90,255,'Call to Action','A buff that increases the defense and hit points of your group.'),(185,12,0,0,5,0,72,-1809115846,-630227479,0,60,63,'Attention','An offensive stance that increases your dexterity.'),(186,12,0,0,5,3,72,-1809115846,-1664015648,0,2,255,'Elemental Toughness','Coats you in a shell that protects you from the elements.'),(187,20,0,0,5,0,150,486870879,-1265863830,1,90,63,'Critical Strike','This quick strike will allow you to do more damage.'),(356,15,0,0,5,0,113,486870879,638503769,1,6,255,'Stomp','An attack that allows you to stomp on your enemy.'),(357,13,0,0,5,0,98,486870879,-1265863830,1,30,63,'Rapid Strike','This quick strike will allow you to do more damage.'),(358,16,0,0,15,1,144,593297002,-630227479,2,90,255,'Call to Arms','A buff that increases the defense and hit points of your group.'),(671,24,0,0,15,1,216,593297002,-630227479,2,90,255,'Call to Combat','A buff that increases the defense and hit points of your group.'),(672,44,0,0,15,1,396,593297002,-630227479,2,90,255,'Call to War','A buff that increases the defense and hit points of your group.'),(674,24,0,0,15,0,90,486870879,-49534593,1,5,63,'Provoke','Allows you to gain the attention of your enemy.'),(675,34,0,0,15,0,128,486870879,-49534593,1,5,63,'Bait','Allows you to gain the attention of your enemy.'),(676,44,0,0,15,0,165,486870879,-49534593,1,5,63,'Incite','Allows you to gain the attention of your enemy.'),(678,34,0,0,5,0,204,-1809115846,-630227479,0,90,63,'Focus','An offensive stance that increases your dexterity.'),(679,49,0,0,5,0,294,-1809115846,-630227479,0,90,63,'Focused Attention','An offensive stance that increases your dexterity.'),(681,29,0,0,5,0,218,486870879,-1265863830,1,90,63,'Critical Assault','This quick strike will allow you to do more damage.'),(682,49,0,0,5,0,368,486870879,-1265863830,1,90,63,'Critical Flurry','This quick strike will allow you to do more damage.'),(683,20,0,0,15,1,240,593297002,1920746744,2,90,255,'Iron Will','A buff that increases the armor of your group.'),(684,29,0,0,15,1,348,593297002,1920746744,2,90,255,'Iron Resolve','A buff that increases the armor of your group.'),(685,39,0,0,15,1,468,593297002,1920746744,2,90,255,'Iron Conviction','A buff that increases the armor of your group.'),(686,1,0,0,1,3,6,-1809115846,-630227479,0,5,255,'Fortitude','A defensive stance that increases your stamina.'),(687,4,0,0,1,3,24,-1809115846,1920746744,0,4,255,'Tough Skin','A buff that increases your armor.'),(1542,1,0,0,5,60,25,-1809115846,2072956978,0,5,255,'Return Home','This ability will return you to your bind point.'),(1588,20,0,0,5,1,120,-1809115846,-630227479,0,300,255,'Bellow','A buff that increases your hit points.'),(1615,39,0,0,5,0,293,486870879,-1265863830,1,90,63,'Critical Barrage','This quick strike will allow you to do more damage.'),(1616,16,0,0,5,0,96,-1809115846,638503769,0,240,255,'Dash','Allows you to run faster for a short period of time.'),(1722,40,0,0,5,1,240,-1809115846,-630227479,0,300,255,'Howl','A buff that increases your hit points.'),(1723,30,0,0,5,1,180,-1809115846,-630227479,0,300,255,'Roar','A buff that increases your hit points.'),(1725,49,0,0,5,1,294,-1809115846,-630227479,0,300,255,'Warcry','A buff that increases your hit points.'),(2083,47,0,0,5,0,564,-1809115846,1028836133,0,120,255,'Frantic Defense','A defensive stance that lowers your offense.'),(2675,46,0,0,5,1,322,486870879,-1265863830,1,30,255,'Rend','A slashing attack rends your enemies armor, making it susceptible to attack.'),(2775,55,0,0,15,0,206,486870879,-49534593,1,5,63,'Goad','Allows you to gain the attention of your enemy.'),(2776,55,0,0,15,1,495,593297002,-630227479,2,90,255,'Call to Victory','A buff that increases the defense and hit points of your group.'),(2777,60,0,0,5,0,450,486870879,-1265863830,1,90,63,'Critical Attack','This quick strike will allow you to do more damage.'),(2778,60,0,0,5,0,360,-1809115846,-630227479,0,36,63,'Diligence','An offensive stance that increases your dexterity.'),(2844,1,0,0,5,3,10,-1809115846,-466420384,0,9,255,'Remove Illusion','This ability removes the current illusion on you.'),(3090,57,0,0,5,0,342,429768225,1920746744,0,12,255,'Mithril Skin','Increases armor class.'),(3091,54,0,0,5,1,405,486870879,-1265863830,1,30,255,'Slash','A slashing attack tears your enemies armor, making it susceptible to attack.'),(3915,51,0,0,5,0,318,486870879,638503769,1,6,255,'Boot','A swift kick to a tender area is often all you need.'),(4090,60,0,0,5,1,20,-1809115846,-630227479,0,600,255,'Enrage','Greatly increases strength and dexterity for a short time.'),(4321,30,0,0,5,3,0,-1809115846,-568605204,0,10,255,'Werewolf Form','Assume the form of the werewolf.'),(4332,30,0,0,5,0,50,486870879,-861288699,1,30,255,'Bite','You sink your fangs into your enemies.'),(4335,40,0,0,30,4,450,593297002,-2143509564,2,5,255,'Growl of Battle','Your steady growl can be heard over the sounds of battle encouraging your companions to fight with more fervor.'),(4386,40,0,0,5,3,25,-1809115846,-568605204,0,300,255,'Feral Wolf Form','Assume the form of a feral wolf.'),(4412,50,0,0,5,3,25,-1809115846,-568605204,0,300,255,'Dire Wolf Form','Assume the form of a dire wolf.'),(5246,49,0,0,15,1,588,593297002,1920746744,2,90,255,'Iron Resolution','A buff that increases the armor of your group.'),(5873,60,0,0,5,0,242,486870879,-1265863830,0,45,255,'Protection of Marr','The protection of Marr allows your skills to hit with greater precision and effect.'),(6247,54,0,0,5,1,250,486870879,638503769,1,6,255,'Dropkick','An attack that favors strong, offensively-minded Warriors.'),(6257,60,0,0,5,5,250,-1809115846,-630227479,0,1,255,'Minotaur\'s Persistence','You take on the attributes of a Minotaur.'),(6569,1,0,0,5,12,0,-1809115846,-862007142,0,1,255,'Summon: Undead Mammoth','Summons an undead mammoth companion.'),(6576,1,0,0,5,12,0,-1809115846,-862007142,0,1,255,'Summon: Moss Snake','Summons a moss snake companion.'),(6587,1,0,0,5,12,0,-1809115846,-862007142,0,1,255,'Summon: Baby Sandswiss','Summons a Baby Sandswiss companion.'),(6594,1,0,0,5,12,0,-1809115846,-862007142,0,1,255,'Summon: Badger','Summons a badger companion.'),(6595,1,0,0,5,12,0,-1809115846,-862007142,0,1,255,'Summon: Squishy Cube','Summons a squishy cube companion.'),(6599,1,0,0,5,12,0,-1809115846,-862007142,0,1,255,'Summon: Tiny Jester','Summons a tiny jester companion..'),(6605,1,0,0,5,12,0,-1809115846,-862007142,0,1,255,'Summon: White Rat','Summons a white rat companion..'),(6610,1,0,0,0,2,0,-1013573000,-1569239993,0,0,255,'Illusion: Rat',' '),(6611,1,0,0,0,2,0,-1013573000,-1569239993,0,0,255,'Illusion: Gelatinous Cube',' '),(6613,1,0,0,5,12,0,-1809115846,-862007142,0,1,255,'Summon: Sand Giant','Summons a sand giant companion.'),(6617,1,0,0,30,2,0,1758916955,2057020990,1,10,255,'Sand Blast Effect','A blast of sand effect.'),(6618,1,0,0,0,5,0,-1013573000,-1569239993,0,0,255,'Illusion: Land Shark',' ');
/*!40000 ALTER TABLE `spellPattern` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `weaponHotBar`
--

DROP TABLE IF EXISTS `weaponHotBar`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `weaponHotBar` (
  `weaponsetID` int(11) NOT NULL AUTO_INCREMENT,
  `serverid` int(11) NOT NULL,
  `hotbarname` varchar(64) NOT NULL,
  `weaponID` int(11) NOT NULL,
  `secondaryID` int(11) NOT NULL,
  PRIMARY KEY (`weaponsetID`),
  UNIQUE KEY `weaponsetID_UNIQUE` (`weaponsetID`),
  KEY `weaponhotbar_serverid_idx` (`serverid`),
  CONSTRAINT `weaponhotbar_serverid` FOREIGN KEY (`serverid`) REFERENCES `Characters` (`serverid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `weaponHotBar`
--

LOCK TABLES `weaponHotBar` WRITE;
/*!40000 ALTER TABLE `weaponHotBar` DISABLE KEYS */;
INSERT INTO `weaponHotBar` VALUES (1,1224955,'Perfect Combo',31002,31003),(2,1224955,'Combo Perfect',31003,31002);
/*!40000 ALTER TABLE `weaponHotBar` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'eqoabase'
--

--
-- Dumping routines for database 'eqoabase'
--
/*!50003 DROP PROCEDURE IF EXISTS `CheckName` */;
ALTER DATABASE `eqoabase` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `CheckName`(in CharacterName varchar(32))
BEGIN
SELECT charName FROM Characters where charName = CharacterName;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
ALTER DATABASE `eqoabase` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci ;
/*!50003 DROP PROCEDURE IF EXISTS `CreateCharacter` */;
ALTER DATABASE `eqoabase` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `CreateCharacter`(in charName varchar(32), AccountID int, ModelID bigint, TClass int,
			Race int, HumType varchar(12), Level int, HairColor int, HairLength int, HairStyle int, FaceOption int, ClassIcon int, TotalXP int, Debt int, Breath int, Tunar int, BankTunar int, UnusedTP int, TotalTP int, X float(17,12),
			Y float(17,12), Z float(17,12), Facing float(17,12), Strength int, Stamina int, Agility int, Dexterity int, Wisdom int, Intelligence int, Charisma int, CurrentHP int, MaxHP int, CurrentPower int, MaxPower int, Healot int, Powerot int,
			Ac int, PoisonR int, DiseaseR int, FireR int, ColdR int, LightningR int, ArcaneR int, Fishing int, Base_Strength int, Base_Stamina int, Base_Agility int, Base_Dexterity int, Base_Wisdom int,
			Base_Intelligence int, Base_Charisma int, CurrentHP2 int, BaseHP int, CurrentPower2 int, basePower int, Healot2 int, Powerot2 int)
INSERT INTO Characters(charName, accountid, modelid, tclass, race, humType, level, haircolor, hairlength, hairstyle, faceoption, classIcon, totalXP, debt,  
			breath, tunar, bankTunar, unusedTP, totalTP, x, y, z, facing, strength, stamina, agility, dexterity, wisdom, intel, charisma, currentHP, maxHP, currentPower,
			maxPower, healot, powerot, ac, poisonr, diseaser, firer, coldr, lightningr, arcaner, fishing, base_strength, base_stamina, base_agility, base_dexterity,
			base_wisdom, base_intel, base_charisma, currentHP2, baseHP, currentPower2, basePower, healot2, powerot2) VALUES (charName, AccountID, ModelID, TClass,
			Race, HumType, Level, HairColor, HairLength, HairStyle, FaceOption, ClassIcon, TotalXP, Debt, Breath, Tunar, BankTunar, UnusedTP, TotalTP, X,
			Y, Z, Facing, Strength, Stamina, Agility, Dexterity, Wisdom, Intelligence, Charisma, CurrentHP, MaxHP, CurrentPower, MaxPower, Healot, Powerot,
			Ac, PoisonR, DiseaseR, FireR, ColdR, LightningR, ArcaneR, Fishing, Base_Strength, Base_Stamina, Base_Agility, Base_Dexterity, Base_Wisdom,
			Base_Intelligence, Base_Charisma, CurrentHP2, BaseHP, CurrentPower2, basePower, Healot2, Powerot2) ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
ALTER DATABASE `eqoabase` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci ;
/*!50003 DROP PROCEDURE IF EXISTS `DeleteCharacter` */;
ALTER DATABASE `eqoabase` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `DeleteCharacter`(in cServerID int)
DELETE FROM Characters WHERE serverid = cServerID ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
ALTER DATABASE `eqoabase` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci ;
/*!50003 DROP PROCEDURE IF EXISTS `GetAccountCharacters` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `GetAccountCharacters`(in pAccountID int)
SELECT charName, serverID, modelID, tClass, race, humType, level, hairColor, hairLength, hairStyle, faceOption,
classIcon, totalXP, debt, breath, tunar, bankTunar, unusedTP, totalTP, world, x, y, z, facing,
strength, stamina, agility, dexterity, wisdom, intel, charisma, currentHP, maxHP, currentPower, maxPower, healot, powerot, ac,
poisonr, diseaser, firer, coldr, lightningr, arcaner, fishing, base_Strength, base_Stamina, base_Agility,
base_Dexterity, base_Wisdom, base_Intel, base_Charisma, currentHP2, baseHP, currentPower2, basePower, healot2, powerot2
 FROM Characters c WHERE accountid = pAccountID ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetCharacterGear` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `GetCharacterGear`(in pAccountID int)
BEGIN
SELECT c.charName, ci.stackleft, ci.remainHP, ci.remaincharge, ci.equiploc, ci.location, ci.listnumber, ip.patternid, ip.patternfam, ip.itemicon, ip.equipslot, ip.attacktype, ip.weapondamage,
       ip.maxhp, ip.trade, ip.rent, ip.craft, ip.lore, ip.levelreq, ip.maxstack, ip.itemname, ip.itemdesc, ip.duration, ip.classuse, ip.raceuse, ip.procanim, ip.str, ip.sta, ip.agi, ip.dex, ip.wis,
       ip.int, ip.cha, ip.HPMAX, ip.POWMAX, ip.PoT, ip.HoT, ip.AC, ip.PR, ip.DR, ip.FR, ip.CR, ip.LR, ip.AR, ip.model, ip.color from charInventory ci JOIN Characters c ON c.serverid = ci.serverid
                                JOIN itemPattern ip ON ip.patternid = ci.patternid WHERE c.accountid = pAccountID ORDER BY ci.listnumber;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetCharHotkeys` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `GetCharHotkeys`(in charID int)
BEGIN
SELECT HK.direction, HK. Nlabel, HK.Nmessage, HK.Wlabel, HK.Wmessage, HK.Elabel, HK.Emessage, HK.Slabel, HK.smessage
FROM Hotkeys HK WHERE HK.serverid = 1224955;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetCharModel` */;
ALTER DATABASE `eqoabase` CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = '' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `GetCharModel`(in RaceType varchar(8), ClassType varchar(6), HumType varchar(12), SexType varchar(16))
BEGIN
select dc.*, cm.modelid FROM defaultCharacter dc INNER JOIN characterModel cm ON dc.race = cm.race 
WHERE dc.race = RaceType and dc.tclass = ClassType and dc.humType= HumType and cm.sex = SexType;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
ALTER DATABASE `eqoabase` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci ;
/*!50003 DROP PROCEDURE IF EXISTS `GetCharSpells` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `GetCharSpells`(in charID int)
BEGIN
SELECT S.SpellID,S.AddedOrder,S.OnHotBar,
S.WhereOnBar, S.Unk1, S.ShowHide, SP.AbilityLvl, SP.Unk2,
SP.Unk3, SP.Range, SP.CastTime, SP.Power, SP.IconColor, SP.Icon,
SP.Scope, SP.Recast, SP.EqpRequire, SP.SpellName, SP.SpellDesc
FROM Spells S JOIN spellPattern SP ON S.SpellID=SP.SpellID WHERE S.ServerID = charID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `GetCharWeaponHotbar` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`fooUser`@`%` PROCEDURE `GetCharWeaponHotbar`(in charID int)
BEGIN
SELECT HB.hotbarname, HB.weaponID, HB.secondaryID
FROM weaponHotBar HB WHERE HB.ServerID = charID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-01-27 15:09:04
