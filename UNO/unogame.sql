-- phpMyAdmin SQL Dump
-- version 4.2.7.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jan 24, 2016 at 02:05 PM
-- Server version: 5.6.20
-- PHP Version: 5.5.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `unogame`
--

-- --------------------------------------------------------

--
-- Table structure for table `card`
--

CREATE TABLE IF NOT EXISTS `card` (
`CardID` int(11) NOT NULL,
  `Type` varchar(25) NOT NULL,
  `Color` varchar(25) NOT NULL,
  `Number` int(11) NOT NULL
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=109 ;

--
-- Dumping data for table `card`
--

INSERT INTO `card` (`CardID`, `Type`, `Color`, `Number`) VALUES
(1, 'draw4Wild', 'None', -1),
(2, 'wild', 'None', -1),
(3, 'draw4Wild', 'None', -1),
(4, 'wild', 'None', -1),
(5, 'draw4Wild', 'None', -1),
(6, 'wild', 'None', -1),
(7, 'draw4Wild', 'None', -1),
(8, 'wild', 'None', -1),
(9, 'skip', 'Blue', -1),
(10, 'skip', 'Blue', -1),
(11, 'skip', 'Green', -1),
(12, 'skip', 'Green', -1),
(13, 'skip', 'Red', -1),
(14, 'skip', 'Red', -1),
(15, 'skip', 'Yellow', -1),
(16, 'skip', 'Yellow', -1),
(17, 'reverse', 'Blue', -1),
(18, 'reverse', 'Blue', -1),
(19, 'reverse', 'Green', -1),
(20, 'reverse', 'Green', -1),
(21, 'reverse', 'Red', -1),
(22, 'reverse', 'Red', -1),
(23, 'reverse', 'Yellow', -1),
(24, 'reverse', 'Yellow', -1),
(25, 'draw2', 'Blue', -1),
(26, 'draw2', 'Blue', -1),
(27, 'draw2', 'Green', -1),
(28, 'draw2', 'Green', -1),
(29, 'draw2', 'Red', -1),
(30, 'draw2', 'Red', -1),
(31, 'draw2', 'Yellow', -1),
(32, 'draw2', 'Yellow', -1),
(33, 'normal', 'Blue', 0),
(34, 'normal', 'Green', 0),
(35, 'normal', 'Red', 0),
(36, 'normal', 'Yellow', 0),
(37, 'normal', 'Yellow', 1),
(38, 'normal', 'Yellow', 1),
(39, 'normal', 'Yellow', 2),
(40, 'normal', 'Yellow', 2),
(41, 'normal', 'Yellow', 3),
(42, 'normal', 'Yellow', 3),
(43, 'normal', 'Yellow', 4),
(44, 'normal', 'Yellow', 4),
(45, 'normal', 'Yellow', 5),
(46, 'normal', 'Yellow', 5),
(47, 'normal', 'Yellow', 6),
(48, 'normal', 'Yellow', 6),
(49, 'normal', 'Yellow', 7),
(50, 'normal', 'Yellow', 7),
(51, 'normal', 'Yellow', 8),
(52, 'normal', 'Yellow', 8),
(53, 'normal', 'Yellow', 9),
(54, 'normal', 'Yellow', 9),
(55, 'normal', 'Red', 1),
(56, 'normal', 'Red', 1),
(57, 'normal', 'Red', 2),
(58, 'normal', 'Red', 2),
(59, 'normal', 'Red', 3),
(60, 'normal', 'Red', 3),
(61, 'normal', 'Red', 4),
(62, 'normal', 'Red', 4),
(63, 'normal', 'Red', 5),
(64, 'normal', 'Red', 5),
(65, 'normal', 'Red', 6),
(66, 'normal', 'Red', 6),
(67, 'normal', 'Red', 7),
(68, 'normal', 'Red', 7),
(69, 'normal', 'Red', 8),
(70, 'normal', 'Red', 8),
(71, 'normal', 'Red', 9),
(72, 'normal', 'Red', 9),
(73, 'normal', 'Green', 1),
(74, 'normal', 'Green', 1),
(75, 'normal', 'Green', 2),
(76, 'normal', 'Green', 2),
(77, 'normal', 'Green', 3),
(78, 'normal', 'Green', 3),
(79, 'normal', 'Green', 4),
(80, 'normal', 'Green', 4),
(81, 'normal', 'Green', 5),
(82, 'normal', 'Green', 5),
(83, 'normal', 'Green', 6),
(84, 'normal', 'Green', 6),
(85, 'normal', 'Green', 7),
(86, 'normal', 'Green', 7),
(87, 'normal', 'Green', 8),
(88, 'normal', 'Green', 8),
(89, 'normal', 'Green', 9),
(90, 'normal', 'Green', 9),
(91, 'normal', 'Blue', 1),
(92, 'normal', 'Blue', 1),
(93, 'normal', 'Blue', 2),
(94, 'normal', 'Blue', 2),
(95, 'normal', 'Blue', 3),
(96, 'normal', 'Blue', 3),
(97, 'normal', 'Blue', 4),
(98, 'normal', 'Blue', 4),
(99, 'normal', 'Blue', 5),
(100, 'normal', 'Blue', 5),
(101, 'normal', 'Blue', 6),
(102, 'normal', 'Blue', 6),
(103, 'normal', 'Blue', 7),
(104, 'normal', 'Blue', 7),
(105, 'normal', 'Blue', 8),
(106, 'normal', 'Blue', 8),
(107, 'normal', 'Blue', 9),
(108, 'normal', 'Blue', 9);

-- --------------------------------------------------------

--
-- Table structure for table `deck`
--

CREATE TABLE IF NOT EXISTS `deck` (
  `GameID` int(11) NOT NULL,
  `Type` varchar(25) NOT NULL,
  `Color` varchar(25) NOT NULL,
  `Number` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `gameinfo`
--

CREATE TABLE IF NOT EXISTS `gameinfo` (
  `GameID` int(11) NOT NULL,
  `PlayerUserName` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `move`
--

CREATE TABLE IF NOT EXISTS `move` (
`ID` int(11) NOT NULL,
  `Username` varchar(25) NOT NULL,
  `GameID` int(11) NOT NULL,
  `Type` varchar(50) NOT NULL,
  `Card` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE IF NOT EXISTS `players` (
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `GamesWon` int(11) DEFAULT NULL,
  `GamesPlayed` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `players`
--

INSERT INTO `players` (`Username`, `Password`, `GamesWon`, `GamesPlayed`) VALUES
('alireza', '123456', NULL, NULL),
('maja', 'maja', NULL, NULL),
('sam', 'sam', NULL, NULL),
('samin', 'samin', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `savegame`
--

CREATE TABLE IF NOT EXISTS `savegame` (
  `GameID` int(11) NOT NULL,
  `Username` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `card`
--
ALTER TABLE `card`
 ADD PRIMARY KEY (`CardID`);

--
-- Indexes for table `deck`
--
ALTER TABLE `deck`
 ADD PRIMARY KEY (`GameID`,`Type`,`Color`,`Number`);

--
-- Indexes for table `gameinfo`
--
ALTER TABLE `gameinfo`
 ADD PRIMARY KEY (`GameID`,`PlayerUserName`), ADD KEY `fk_gameinfo` (`PlayerUserName`);

--
-- Indexes for table `move`
--
ALTER TABLE `move`
 ADD PRIMARY KEY (`ID`), ADD KEY `fk_moveusername` (`Username`), ADD KEY `fk_movegameID` (`GameID`), ADD KEY `fk_cardid` (`Card`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
 ADD PRIMARY KEY (`Username`);

--
-- Indexes for table `savegame`
--
ALTER TABLE `savegame`
 ADD PRIMARY KEY (`GameID`,`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `card`
--
ALTER TABLE `card`
MODIFY `CardID` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=109;
--
-- AUTO_INCREMENT for table `move`
--
ALTER TABLE `move`
MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `deck`
--
ALTER TABLE `deck`
ADD CONSTRAINT `fk_fordeckgameid` FOREIGN KEY (`GameID`) REFERENCES `savegame` (`GameID`);

--
-- Constraints for table `gameinfo`
--
ALTER TABLE `gameinfo`
ADD CONSTRAINT `fk_gameinfousername` FOREIGN KEY (`PlayerUserName`) REFERENCES `players` (`Username`);

--
-- Constraints for table `move`
--
ALTER TABLE `move`
ADD CONSTRAINT `fk_cardid` FOREIGN KEY (`Card`) REFERENCES `card` (`CardID`),
ADD CONSTRAINT `fk_movegameID` FOREIGN KEY (`GameID`) REFERENCES `savegame` (`GameID`),
ADD CONSTRAINT `fk_moveusername` FOREIGN KEY (`Username`) REFERENCES `players` (`Username`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
