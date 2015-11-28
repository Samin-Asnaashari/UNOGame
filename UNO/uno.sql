-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Nov 29, 2015 at 12:12 AM
-- Server version: 10.1.8-MariaDB
-- PHP Version: 5.6.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `uno`
--

-- --------------------------------------------------------

--
-- Table structure for table `moves`
--

CREATE TABLE `moves` (
  `ID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Game ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `players`
--

CREATE TABLE `players` (
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `GamesWon` int(11) DEFAULT NULL,
  `GamesPlayed` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `savedgames`
--

CREATE TABLE `savedgames` (
  `Game ID` int(11) NOT NULL,
  `Username` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `moves`
--
ALTER TABLE `moves`
  ADD PRIMARY KEY (`ID`,`Username`),
  ADD UNIQUE KEY `Game ID` (`Game ID`),
  ADD KEY `Username` (`Username`);

--
-- Indexes for table `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`Username`);

--
-- Indexes for table `savedgames`
--
ALTER TABLE `savedgames`
  ADD PRIMARY KEY (`Game ID`,`Username`),
  ADD KEY `PlayerUserNameSaved` (`Username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `moves`
--
ALTER TABLE `moves`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `moves`
--
ALTER TABLE `moves`
  ADD CONSTRAINT `PlayerUserName` FOREIGN KEY (`Username`) REFERENCES `players` (`Username`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `savedgames`
--
ALTER TABLE `savedgames`
  ADD CONSTRAINT `GameID` FOREIGN KEY (`Game ID`) REFERENCES `moves` (`Game ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `PlayerUserNameSaved` FOREIGN KEY (`Username`) REFERENCES `players` (`Username`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
