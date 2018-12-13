-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Dec 14, 2018 at 12:15 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `recipebox`
--

-- --------------------------------------------------------

--
-- Table structure for table `recipes_ingredients`
--

CREATE TABLE `recipes_ingredients` (
  `id` int(11) NOT NULL,
  `recipe_id` int(11) NOT NULL,
  `ingredient_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `recipes_ingredients`
--

INSERT INTO `recipes_ingredients` (`id`, `recipe_id`, `ingredient_id`) VALUES
(1, 1, 1),
(3, 2, 1),
(4, 2, 3),
(5, 3, 1),
(6, 3, 2),
(7, 3, 3),
(8, 3, 4),
(9, 3, 6),
(10, 3, 5),
(11, 4, 7),
(12, 4, 9),
(18, 1, 2),
(19, 1, 4),
(20, 4, 8),
(22, 5, 3);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `recipes_ingredients`
--
ALTER TABLE `recipes_ingredients`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `recipes_ingredients`
--
ALTER TABLE `recipes_ingredients`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
