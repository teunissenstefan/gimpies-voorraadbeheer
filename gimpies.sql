-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Gegenereerd op: 10 nov 2017 om 17:12
-- Serverversie: 10.1.19-MariaDB
-- PHP-versie: 5.6.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `gimpies`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `artikelen`
--

CREATE TABLE `artikelen` (
  `id` bigint(11) NOT NULL,
  `beschrijving` varchar(40) NOT NULL,
  `aantal` int(11) NOT NULL,
  `maat` int(4) NOT NULL,
  `prijs` varchar(11) NOT NULL,
  `verkocht` bigint(20) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `artikelen`
--

INSERT INTO `artikelen` (`id`, `beschrijving`, `aantal`, `maat`, `prijs`, `verkocht`) VALUES
(23, 'Adidas', 500, 40, '39.99', 0),
(24, 'Converse', 500, 40, '39.99', 0),
(25, 'Crocs', 500, 40, '39.99', 500),
(26, 'Nike', 500, 40, '39.99', 0),
(27, 'Puma', 500, 40, '39.99', 0),
(28, 'Skechers', 500, 40, '39.99', 0),
(29, 'Vans', 500, 40, '39.99', 0),
(30, 'Airness', 500, 40, '39.99', 0),
(31, 'Anta Sports', 500, 40, '39.99', 0),
(32, 'Ari Football', 500, 40, '39.99', 0),
(33, 'Banana Republic', 500, 40, '39.99', 0),
(34, 'Calvin Klein', 500, 40, '39.99', 0),
(35, 'Cole Haan', 500, 40, '39.99', 0),
(36, 'DC Shoes', 500, 40, '39.99', 0),
(37, 'Legea', 500, 40, '39.99', 0),
(38, 'Lescon', 500, 40, '39.99', 10),
(39, 'Lugz', 500, 40, '39.99', 0),
(40, 'Moon Boot', 500, 40, '39.99', 0),
(41, 'Nocona Boots', 500, 40, '49.99', 0),
(42, 'Nine West', 500, 40, '49.99', 0),
(43, 'Pacific Brands', 500, 40, '49.99', 0),
(44, 'Real United', 500, 41, '59.99', 0),
(45, 'Red or Dead', 500, 42, '49.99', 0),
(46, 'Reebok', 500, 39, '59.99', 0),
(47, 'Rock Engineered Design', 500, 39, '39.99', 10),
(48, 'Rossi Boots', 500, 40, '49.99', 0),
(49, 'Servis Shoes', 500, 40, '49.99', 0),
(50, 'Toms Shoes', 500, 42, '59.99', 0),
(51, 'Acorn', 500, 40, '49.99', 1);

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `globalvars`
--

CREATE TABLE `globalvars` (
  `id` int(11) NOT NULL,
  `var` varchar(255) NOT NULL,
  `value` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `globalvars`
--

INSERT INTO `globalvars` (`id`, `var`, `value`) VALUES
(1, 'MAX_LOGIN_TRIES', '3'),
(2, 'WACHTWOORD', 'qwe');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `log`
--

CREATE TABLE `log` (
  `id` int(11) NOT NULL,
  `medewerkerid` bigint(20) NOT NULL,
  `inlog` tinyint(1) NOT NULL,
  `tijd` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `log`
--

INSERT INTO `log` (`id`, `medewerkerid`, `inlog`, `tijd`) VALUES
(407, 1, 1, '2017-11-10 11:36:07'),
(408, 1, 0, '2017-11-10 11:36:09'),
(409, 2, 1, '2017-11-10 11:36:13'),
(410, 2, 0, '2017-11-10 11:36:15'),
(411, 1, 1, '2017-11-10 11:50:27'),
(412, 1, 0, '2017-11-10 11:50:32'),
(413, 1, 1, '2017-11-10 11:51:22'),
(414, 1, 0, '2017-11-10 11:51:30'),
(415, 1, 1, '2017-11-10 11:52:57'),
(416, 1, 1, '2017-11-10 11:54:09'),
(417, 1, 0, '2017-11-10 11:54:18'),
(418, 1, 1, '2017-11-10 11:55:17'),
(419, 1, 1, '2017-11-10 12:00:59'),
(420, 1, 0, '2017-11-10 12:01:07'),
(421, 1, 1, '2017-11-10 12:01:19'),
(422, 1, 0, '2017-11-10 12:01:30'),
(423, 1, 1, '2017-11-10 12:04:59'),
(424, 1, 0, '2017-11-10 12:05:08'),
(425, 1, 1, '2017-11-10 12:13:57'),
(426, 1, 1, '2017-11-10 12:23:29'),
(427, 1, 1, '2017-11-10 12:23:49'),
(428, 1, 0, '2017-11-10 12:24:14'),
(429, 1, 1, '2017-11-10 12:24:32'),
(430, 1, 0, '2017-11-10 12:24:50'),
(431, 1, 1, '2017-11-10 12:27:16'),
(432, 1, 0, '2017-11-10 12:27:21'),
(433, 1, 1, '2017-11-10 13:10:25'),
(434, 1, 0, '2017-11-10 13:10:53'),
(435, 1, 1, '2017-11-10 13:11:57'),
(436, 1, 0, '2017-11-10 13:12:16'),
(437, 1, 1, '2017-11-10 13:12:48'),
(438, 1, 0, '2017-11-10 13:13:00'),
(439, 1, 1, '2017-11-10 13:15:19'),
(440, 1, 0, '2017-11-10 13:15:31'),
(441, 1, 1, '2017-11-10 13:17:32'),
(442, 1, 0, '2017-11-10 13:17:38'),
(443, 1, 1, '2017-11-10 13:18:33'),
(444, 1, 0, '2017-11-10 13:18:45'),
(445, 1, 1, '2017-11-10 13:31:46'),
(446, 1, 0, '2017-11-10 13:32:49'),
(447, 1, 1, '2017-11-10 15:51:21'),
(448, 1, 1, '2017-11-10 15:53:20'),
(449, 1, 1, '2017-11-10 15:55:12'),
(450, 1, 1, '2017-11-10 15:56:01'),
(451, 1, 1, '2017-11-10 16:02:39'),
(452, 1, 0, '2017-11-10 16:11:22'),
(453, 1, 1, '2017-11-10 16:12:32'),
(454, 1, 0, '2017-11-10 16:15:46'),
(455, 1, 1, '2017-11-10 16:26:51'),
(456, 1, 0, '2017-11-10 16:27:57'),
(457, 1, 1, '2017-11-10 16:31:05'),
(458, 1, 0, '2017-11-10 16:31:17'),
(459, 1, 1, '2017-11-10 16:31:21'),
(460, 1, 0, '2017-11-10 16:31:26'),
(461, 1, 1, '2017-11-10 16:31:27'),
(462, 1, 0, '2017-11-10 16:31:28'),
(463, 1, 1, '2017-11-10 16:33:47'),
(464, 1, 0, '2017-11-10 16:34:35'),
(465, 1, 1, '2017-11-10 16:38:50'),
(466, 1, 0, '2017-11-10 16:38:55'),
(467, 1, 1, '2017-11-10 16:39:11'),
(468, 1, 1, '2017-11-10 16:40:50'),
(469, 1, 0, '2017-11-10 16:41:04'),
(470, 1, 1, '2017-11-10 16:42:31'),
(471, 1, 1, '2017-11-10 16:43:00'),
(472, 1, 0, '2017-11-10 16:44:36'),
(473, 1, 1, '2017-11-10 16:45:23'),
(474, 1, 1, '2017-11-10 16:46:04'),
(475, 1, 0, '2017-11-10 16:46:38'),
(476, 1, 1, '2017-11-10 16:47:49'),
(477, 1, 0, '2017-11-10 16:48:37'),
(478, 1, 1, '2017-11-10 16:49:34'),
(479, 1, 0, '2017-11-10 16:50:25'),
(480, 1, 1, '2017-11-10 16:50:32'),
(481, 1, 1, '2017-11-10 16:52:18'),
(482, 1, 0, '2017-11-10 16:53:20'),
(483, 1, 1, '2017-11-10 16:54:07'),
(484, 1, 1, '2017-11-10 16:54:56'),
(485, 1, 1, '2017-11-10 16:55:47'),
(486, 1, 1, '2017-11-10 16:56:53'),
(487, 1, 1, '2017-11-10 16:57:02'),
(488, 1, 0, '2017-11-10 16:58:09'),
(489, 1, 1, '2017-11-10 16:58:11'),
(490, 1, 0, '2017-11-10 16:59:33'),
(491, 1, 1, '2017-11-10 16:59:51'),
(492, 1, 0, '2017-11-10 17:01:04'),
(493, 1, 1, '2017-11-10 17:02:46'),
(494, 1, 0, '2017-11-10 17:02:51'),
(495, 1, 1, '2017-11-10 17:02:58'),
(496, 1, 0, '2017-11-10 17:03:03'),
(497, 1, 1, '2017-11-10 17:03:41'),
(498, 1, 0, '2017-11-10 17:03:46'),
(499, 1, 1, '2017-11-10 17:03:53'),
(500, 1, 0, '2017-11-10 17:04:01'),
(501, 1, 1, '2017-11-10 17:04:38'),
(502, 1, 0, '2017-11-10 17:06:19'),
(503, 1, 1, '2017-11-10 17:08:13'),
(504, 1, 0, '2017-11-10 17:08:44'),
(505, 10, 1, '2017-11-10 17:08:48'),
(506, 10, 0, '2017-11-10 17:09:00'),
(507, 1, 1, '2017-11-10 17:09:02'),
(508, 1, 0, '2017-11-10 17:09:15'),
(509, 1, 1, '2017-11-10 17:10:19'),
(510, 1, 0, '2017-11-10 17:10:51'),
(511, 13, 1, '2017-11-10 17:10:53'),
(512, 13, 0, '2017-11-10 17:11:12'),
(513, 1, 1, '2017-11-10 17:11:14'),
(514, 1, 0, '2017-11-10 17:11:34');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `ranks`
--

CREATE TABLE `ranks` (
  `rankid` bigint(20) NOT NULL,
  `title` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `ranks`
--

INSERT INTO `ranks` (`rankid`, `title`) VALUES
(1, 'Verkoopmedewerker'),
(2, 'Magazijnmedewerker'),
(3, 'Admin');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `sales`
--

CREATE TABLE `sales` (
  `id` bigint(20) NOT NULL,
  `userid` bigint(20) NOT NULL,
  `artikelid` bigint(20) NOT NULL,
  `aantal` bigint(20) NOT NULL,
  `euro` varchar(11) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `sales`
--

INSERT INTO `sales` (`id`, `userid`, `artikelid`, `aantal`, `euro`, `date`) VALUES
(30, 1, 51, 1, '49,99', '2017-11-10 16:58:30'),
(31, 1, 38, 10, '399,9', '2017-11-10 16:58:59'),
(32, 1, 25, 500, '19995', '2017-11-10 17:00:20'),
(33, 13, 47, 10, '399,9', '2017-11-10 17:11:00');

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `werknemers`
--

CREATE TABLE `werknemers` (
  `id` bigint(11) NOT NULL,
  `username` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `rank` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Gegevens worden geëxporteerd voor tabel `werknemers`
--

INSERT INTO `werknemers` (`id`, `username`, `password`, `rank`) VALUES
(1, 'admin', 'admin', 3),
(2, 'stefan', 'qwe', 1),
(3, 'pizza', 'pizza', 2),
(13, 'test', 'test', 1);

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `artikelen`
--
ALTER TABLE `artikelen`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `globalvars`
--
ALTER TABLE `globalvars`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `log`
--
ALTER TABLE `log`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `ranks`
--
ALTER TABLE `ranks`
  ADD PRIMARY KEY (`rankid`);

--
-- Indexen voor tabel `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `werknemers`
--
ALTER TABLE `werknemers`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `artikelen`
--
ALTER TABLE `artikelen`
  MODIFY `id` bigint(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=52;
--
-- AUTO_INCREMENT voor een tabel `globalvars`
--
ALTER TABLE `globalvars`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- AUTO_INCREMENT voor een tabel `log`
--
ALTER TABLE `log`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=515;
--
-- AUTO_INCREMENT voor een tabel `ranks`
--
ALTER TABLE `ranks`
  MODIFY `rankid` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT voor een tabel `sales`
--
ALTER TABLE `sales`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;
--
-- AUTO_INCREMENT voor een tabel `werknemers`
--
ALTER TABLE `werknemers`
  MODIFY `id` bigint(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
