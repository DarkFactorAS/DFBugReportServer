DROP TABLE IF EXISTS `bugreports`;
CREATE TABLE `bugreports` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `title` varchar(100) NOT NULL DEFAULT '',
  `message` varchar(300) NOT NULL DEFAULT '',
  `email` varchar(100) NOT NULL DEFAULT '',
  `clientName` varchar(50) NOT NULL DEFAULT '',
  `clientVersion` varchar(25) NOT NULL DEFAULT '',
  `created` datetime NOT NULL,
  `updated` datetime NOT NULL,
  PRIMARY KEY (`id`)
);

DROP TABLE IF EXISTS `logtable`;
CREATE TABLE `logtable` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `created` datetime NOT NULL,
  `loglevel` int(11) NOT NULL,
  `groupname` varchar(100) NOT NULL DEFAULT '',
  `message` varchar(1024) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
);