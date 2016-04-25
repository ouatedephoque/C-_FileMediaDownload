-- phpMyAdmin SQL Dump
-- version 4.1.14
-- http://www.phpmyadmin.net
--
-- Client :  127.0.0.1
-- Généré le :  Lun 25 Avril 2016 à 19:21
-- Version du serveur :  5.6.17
-- Version de PHP :  5.5.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données :  `bd_fmd`
--

-- --------------------------------------------------------

--
-- Structure de la table `film`
--

CREATE TABLE IF NOT EXISTS `film` (
  `FilmId` int(11) NOT NULL AUTO_INCREMENT,
  `FilmTitle` varchar(100) NOT NULL,
  `FilmLink` text NOT NULL,
  `FilmPourcent` int(11) NOT NULL,
  `FilmExtension` varchar(4) NOT NULL,
  PRIMARY KEY (`FilmId`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=6 ;

--
-- Contenu de la table `film`
--

INSERT INTO `film` (`FilmId`, `FilmTitle`, `FilmLink`, `FilmPourcent`, `FilmExtension`) VALUES
(5, 'Star Wars VII', 'http://dl5.downloadha.com/hosein/Movie/March 2016/Star.Wars.Episode.VII.The.Force.Awakens.2015.720p.BrRip.x265-PSA_www.Downloadha.com_.mkv', 0, 'mkv');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
