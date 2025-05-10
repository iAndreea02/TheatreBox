-- --------------------------------------------------------
-- Host:                         localhost
-- Server version:               8.0.33 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for agentie_turism
DROP DATABASE IF EXISTS `agentie_turism`;
CREATE DATABASE IF NOT EXISTS `agentie_turism` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `agentie_turism`;

-- Dumping structure for table agentie_turism.actori
DROP TABLE IF EXISTS `actori`;
CREATE TABLE IF NOT EXISTS `actori` (
  `id_actor` int NOT NULL AUTO_INCREMENT,
  `nume` varchar(100) NOT NULL,
  `prenume` varchar(100) NOT NULL,
  `data_nasterii` date NOT NULL,
  `nationalitate` varchar(50) NOT NULL,
  PRIMARY KEY (`id_actor`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.actori: ~10 rows (approximately)
DELETE FROM `actori`;
INSERT INTO `actori` (`id_actor`, `nume`, `prenume`, `data_nasterii`, `nationalitate`) VALUES
	(1, 'Ionescu', 'Andrei', '1985-06-15', 'Romania'),
	(2, 'Popescu', 'Maria', '1990-03-22', 'Romania'),
	(3, 'Georgescu', 'Ion', '1978-11-30', 'Romania'),
	(4, 'Mihaila', 'Elena', '1980-04-18', 'Romania'),
	(5, 'Vasilescu', 'Alexandru', '1975-09-10', 'Romania'),
	(6, 'Constantin', 'Gabriela', '1992-05-05', 'Romania'),
	(7, 'Ciobanu', 'Mihai', '1988-02-01', 'Romania'),
	(8, 'Petrescu', 'Ana', '1995-07-10', 'Romania'),
	(9, 'Popa', 'Radu', '1983-11-23', 'Romania'),
	(10, 'Marin', 'Ioana', '1990-08-12', 'Romania');

-- Dumping structure for table agentie_turism.bilet
DROP TABLE IF EXISTS `bilet`;
CREATE TABLE IF NOT EXISTS `bilet` (
  `id_bilet` int NOT NULL AUTO_INCREMENT,
  `id_user` int NOT NULL,
  `id_rezervare` int NOT NULL,
  `metoda_plata` enum('Card','Cash') NOT NULL,
  `suma` int NOT NULL,
  `data_vanzarii` date NOT NULL,
  PRIMARY KEY (`id_bilet`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.bilet: ~45 rows (approximately)
DELETE FROM `bilet`;
INSERT INTO `bilet` (`id_bilet`, `id_user`, `id_rezervare`, `metoda_plata`, `suma`, `data_vanzarii`) VALUES
	(1, 2, 1, 'Card', 70, '2025-01-09'),
	(2, 2, 2, 'Card', 70, '2025-01-09'),
	(3, 5, 3, 'Cash', 25, '2025-01-10'),
	(4, 5, 4, 'Cash', 25, '2025-01-10'),
	(5, 5, 5, 'Card', 30, '2025-01-10'),
	(6, 5, 6, 'Card', 30, '2025-01-10'),
	(7, 5, 7, 'Card', 30, '2025-01-10'),
	(8, 2, 8, 'Cash', 40, '2025-01-10'),
	(9, 2, 9, 'Card', 20, '2025-01-10'),
	(10, 2, 10, 'Card', 40, '2025-01-10'),
	(11, 2, 11, 'Card', 25, '2025-01-10'),
	(12, 2, 12, 'Card', 25, '2025-01-10'),
	(13, 2, 13, 'Card', 25, '2025-01-10'),
	(14, 2, 14, 'Card', 25, '2025-01-10'),
	(15, 1, 15, 'Cash', 70, '2025-01-11'),
	(16, 1, 16, 'Cash', 70, '2025-01-11'),
	(17, 2, 17, 'Cash', 70, '2025-01-16'),
	(18, 2, 18, 'Cash', 70, '2025-01-16'),
	(19, 5, 19, 'Card', 40, '2025-01-16'),
	(20, 5, 20, 'Card', 40, '2025-01-16'),
	(21, 5, 21, 'Card', 70, '2025-01-16'),
	(22, 5, 22, 'Card', 70, '2025-01-16'),
	(23, 5, 23, 'Card', 70, '2025-01-16'),
	(24, 1, 24, 'Cash', 70, '2025-01-16'),
	(25, 1, 25, 'Cash', 70, '2025-01-16'),
	(26, 1, 26, 'Cash', 25, '2025-01-17'),
	(27, 1, 27, 'Cash', 70, '2025-04-10'),
	(28, 1, 28, 'Cash', 25, '2025-04-17'),
	(29, 1, 29, 'Card', 25, '2025-04-17'),
	(30, 1, 30, 'Card', 70, '2025-04-24'),
	(32, 1, 32, 'Card', 20, '2025-04-24'),
	(33, 1, 33, 'Card', 32, '2025-04-24'),
	(34, 1, 34, 'Cash', 32, '2025-04-24'),
	(35, 1, 35, 'Cash', 32, '2025-04-24'),
	(36, 1, 36, 'Card', 32, '2025-04-24'),
	(37, 1, 37, 'Cash', 32, '2025-04-24'),
	(38, 1, 38, 'Cash', 32, '2025-04-24'),
	(39, 1, 39, 'Cash', 32, '2025-04-24'),
	(40, 1, 40, 'Card', 64, '2025-04-29'),
	(41, 1, 41, 'Card', 64, '2025-04-29'),
	(42, 2, 42, 'Card', 90, '2025-05-07'),
	(43, 2, 43, 'Card', 90, '2025-05-07'),
	(44, 1, 44, 'Card', 45, '2025-05-08'),
	(45, 1, 45, 'Card', 45, '2025-05-08'),
	(46, 1, 46, 'Card', 70, '2025-05-09');

-- Dumping structure for table agentie_turism.distributie
DROP TABLE IF EXISTS `distributie`;
CREATE TABLE IF NOT EXISTS `distributie` (
  `id_distributie` int NOT NULL AUTO_INCREMENT,
  `id_spectacol` int NOT NULL,
  `id_actor` int NOT NULL,
  `rol` varchar(100) NOT NULL,
  PRIMARY KEY (`id_distributie`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.distributie: ~22 rows (approximately)
DELETE FROM `distributie`;
INSERT INTO `distributie` (`id_distributie`, `id_spectacol`, `id_actor`, `rol`) VALUES
	(1, 1, 1, 'Tache'),
	(2, 1, 2, 'Cațavencu'),
	(3, 1, 3, 'Zaharia Trahanache'),
	(4, 1, 4, 'Farfuridi'),
	(5, 2, 1, 'Romeo'),
	(6, 2, 2, 'Julieta'),
	(7, 2, 3, 'Mercutio'),
	(8, 2, 4, 'Benvolio'),
	(9, 2, 5, 'Tybalt'),
	(10, 3, 1, 'Prințul Siegfried'),
	(11, 3, 2, 'Odette/Odile'),
	(12, 3, 3, 'Von Rothbart'),
	(13, 4, 1, 'Regele Lear'),
	(14, 4, 2, 'Cordelia'),
	(15, 4, 3, 'Regan'),
	(16, 4, 4, 'Goneril'),
	(17, 4, 5, 'Edgar'),
	(18, 5, 1, 'Figaro'),
	(19, 5, 2, 'Susanna'),
	(20, 5, 3, 'Comtesse Almaviva'),
	(21, 5, 4, 'Conte Almaviva'),
	(22, 5, 5, 'Cherubino');

-- Dumping structure for table agentie_turism.loc
DROP TABLE IF EXISTS `loc`;
CREATE TABLE IF NOT EXISTS `loc` (
  `id_loc` int NOT NULL,
  `pozitie` varchar(3) DEFAULT NULL,
  `id_sala` int DEFAULT NULL,
  PRIMARY KEY (`id_loc`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.loc: ~63 rows (approximately)
DELETE FROM `loc`;
INSERT INTO `loc` (`id_loc`, `pozitie`, `id_sala`) VALUES
	(1, 'A1', 1),
	(2, 'A2', 1),
	(3, 'A3', 1),
	(4, 'B1', 1),
	(5, 'B2', 1),
	(6, 'C1', 1),
	(7, 'C2', 1),
	(8, 'D1', 1),
	(9, 'D2', 1),
	(10, 'A1', 2),
	(11, 'A2', 2),
	(12, 'B1', 2),
	(13, 'B2', 2),
	(14, 'B3', 2),
	(15, 'C1', 2),
	(16, 'C2', 2),
	(17, 'C3', 2),
	(18, 'D1', 2),
	(19, 'A1', 3),
	(20, 'A2', 3),
	(21, 'B1', 3),
	(22, 'B2', 3),
	(23, 'B3', 3),
	(24, 'C1', 3),
	(25, 'C2', 3),
	(26, 'C3', 3),
	(27, 'A1', 4),
	(28, 'A2', 4),
	(29, 'B1', 4),
	(30, 'B2', 4),
	(31, 'C1', 4),
	(32, 'C2', 4),
	(33, 'A1', 5),
	(34, 'A2', 5),
	(35, 'A3', 5),
	(36, 'B1', 5),
	(37, 'B2', 5),
	(38, 'B3', 5),
	(39, 'C1', 5),
	(40, 'C2', 5),
	(41, 'D1', 5),
	(42, 'D2', 5),
	(43, 'A1', 6),
	(44, 'A2', 6),
	(45, 'A3', 6),
	(46, 'A4', 6),
	(47, 'B1', 6),
	(48, 'B2', 6),
	(49, 'B3', 6),
	(50, 'C1', 6),
	(51, 'C2', 6),
	(52, 'D1', 6),
	(53, 'D2', 6),
	(54, 'E1', 6),
	(55, 'A1', 7),
	(56, 'A2', 7),
	(57, 'A3', 7),
	(58, 'B1', 7),
	(59, 'B2', 7),
	(60, 'C1', 7),
	(61, 'C2', 7),
	(62, 'C3', 7),
	(63, 'D1', 7);

-- Dumping structure for table agentie_turism.log_actiuni
DROP TABLE IF EXISTS `log_actiuni`;
CREATE TABLE IF NOT EXISTS `log_actiuni` (
  `id_log` int NOT NULL AUTO_INCREMENT,
  `id_utilizator` int NOT NULL,
  `rol` varchar(50) NOT NULL DEFAULT '',
  `operatie` varchar(50) NOT NULL DEFAULT '',
  `comanda_sql` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `detalii_operatie` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '',
  `data_operatie` date NOT NULL DEFAULT (0),
  PRIMARY KEY (`id_log`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.log_actiuni: ~37 rows (approximately)
DELETE FROM `log_actiuni`;
INSERT INTO `log_actiuni` (`id_log`, `id_utilizator`, `rol`, `operatie`, `comanda_sql`, `detalii_operatie`, `data_operatie`) VALUES
	(1, 1, 'admin', 'Import Excel', 'Multiple INSERT/UPDATE', 'Înregistrări importate: 11', '2025-05-04'),
	(2, 1, 'admin', 'Ștergere spectacol', 'DELETE CASCADE', 'ID Orar: 13', '2025-05-04'),
	(3, 1, 'admin', 'Ștergere spectacol', 'DELETE CASCADE', 'ID Orar: 14', '2025-05-06'),
	(4, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 19', '2025-05-06'),
	(5, 1, 'admin', 'Adăugare Orar', 'INSERT', 'Spectacol: Chirita in provintie, Sala: Sala Mare, Data: 2025-05-29, Ora: 17:21', '2025-05-06'),
	(6, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 9', '2025-05-06'),
	(7, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 9', '2025-05-06'),
	(8, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 9', '2025-05-06'),
	(9, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 7', '2025-05-06'),
	(10, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 4', '2025-05-06'),
	(11, 1, 'admin', 'Ștergere spectacol', 'DELETE CASCADE', 'ID Orar: 20', '2025-05-06'),
	(12, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 12', '2025-05-06'),
	(13, 1, 'admin', 'Adăugare Orar', 'INSERT', 'Spectacol: Saracu Gica, Sala: Sala Studio, Data: 2025-06-10, Ora: 15:30', '2025-05-06'),
	(14, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 16', '2025-05-06'),
	(15, 1, 'admin', 'Ștergere spectacol', 'DELETE CASCADE', 'ID Orar: 21', '2025-05-06'),
	(16, 1, 'admin', 'Ștergere spectacol', 'DELETE CASCADE', 'ID Orar: 19', '2025-05-06'),
	(17, 1, 'admin', 'Editare Orar', 'Update', 'ID Orar: 10', '2025-05-06'),
	(18, 1, 'Admin', 'Inserare Spectacol', 'INSERT INTO SPECTACOL (NUME, GEN, PRET_PERS, DESCRIERE, ID_REGIZOR) \r\n                        VALUES (@NUME, @GEN, @PRET_PERS, @DESCRIERE, @ID_REGIZOR)', 'Spectacol: Noaptea Jazz, Regizor ID: 6', '2025-05-08'),
	(19, 1, 'Admin', 'Adăugare Orar', 'INSERT', 'Spectacol: capara, Sala: Sala Clasică, Data: 2025-06-24, Ora: 18:46', '2025-05-09'),
	(20, 1, 'Admin', 'Import Excel', 'Multiple INSERT/UPDATE', 'Înregistrări importate: 14', '2025-05-10'),
	(21, 1, 'Admin', 'Import Excel', 'Multiple INSERT/UPDATE', 'Înregistrări importate: 14', '2025-05-10'),
	(22, 1, 'Admin', 'Import Excel', 'Multiple INSERT/UPDATE', 'Înregistrări importate: 14', '2025-05-10'),
	(23, 1, 'Admin', 'Inserare Spectacol', 'INSERT INTO SPECTACOL (NUME, GEN, PRET_PERS, DESCRIERE, ID_REGIZOR) \r\n                        VALUES (@NUME, @GEN, @PRET_PERS, @DESCRIERE, @ID_REGIZOR)', 'Spectacol: ceva, Regizor ID: 1', '2025-05-10'),
	(24, 1, 'Admin', 'Inserare Spectacol', 'INSERT INTO SPECTACOL (NUME, GEN, PRET_PERS, DESCRIERE, ID_REGIZOR) \r\n                        VALUES (@NUME, @GEN, @PRET_PERS, @DESCRIERE, @ID_REGIZOR)', 'Spectacol: Ceva si ceva, Regizor ID: 7', '2025-05-10'),
	(25, 1, 'Admin', 'Inserare Spectacol', 'INSERT INTO SPECTACOL (NUME, GEN, PRET_PERS, DESCRIERE, ID_REGIZOR) \r\n                        VALUES (@NUME, @GEN, @PRET_PERS, @DESCRIERE, @ID_REGIZOR)', 'Spectacol: Test, Regizor ID: 3', '2025-05-10'),
	(26, 1, 'Admin', 'Editare Orar', 'Update', 'ID Orar: 10', '2025-05-10'),
	(27, 1, 'Admin', 'Inserare Spectacol', 'INSERT INTO SPECTACOL (NUME, GEN, PRET_PERS, DESCRIERE, ID_REGIZOR) \r\n                        VALUES (@NUME, @GEN, @PRET_PERS, @DESCRIERE, @ID_REGIZOR)', 'Spectacol: Pupaza, Regizor ID: 8', '2025-05-10'),
	(28, 1, 'Admin', 'Editare Orar', 'Update', 'ID Orar: 17', '2025-05-10'),
	(29, 1, 'Admin', 'Adăugare Orar', 'INSERT', 'Spectacol: Pupaza, Sala: Sala Studio, Data: 2025-07-15, Ora: 15:33', '2025-05-10'),
	(30, 1, 'Admin', 'Editare Orar', 'Update', 'ID Orar: 17', '2025-05-10'),
	(31, 1, 'Admin', 'Editare Orar', 'UPDATE', 'ID Orar: 17', '2025-05-10'),
	(32, 1, 'Admin', 'Adăugare Orar', 'INSERT', 'Spectacol: Chirita in provintie, Sala: Sala Principală, Data: 2025-08-06, Ora: 15:37', '2025-05-10'),
	(33, 1, 'Admin', 'Editare Orar', 'Update', 'ID Orar: 15', '2025-05-10'),
	(34, 1, 'Admin', 'Editare Orar', 'EDIT', 'ID Orar: 15', '2025-05-10'),
	(35, 1, 'Admin', 'Editare Orar', 'UPDATE', 'ID Orar: 15', '2025-05-10'),
	(36, 1, 'Admin', 'UPDATE', 'UPDATE ORAR SET ID_SPEC = ?, ID_SALA = ?, DATA = ?, ORA = ? WHERE ID_ORAR = ?', 'Actualizare programare ID: 5, Spectacol ID: 1', '2025-05-10'),
	(37, 1, 'Admin', 'INSERT', 'INSERT INTO ORAR (ID_SPEC, ID_SALA, DATA, ORA) VALUES (?, ?, ?, ?)', 'Adăugare programare nouă pentru spectacolul ID: 13, Sala ID: 3', '2025-05-10');

-- Dumping structure for table agentie_turism.orar
DROP TABLE IF EXISTS `orar`;
CREATE TABLE IF NOT EXISTS `orar` (
  `id_orar` int NOT NULL AUTO_INCREMENT,
  `id_spec` int DEFAULT NULL,
  `id_sala` int DEFAULT NULL,
  `ora` varchar(45) DEFAULT NULL,
  `data` date DEFAULT NULL,
  PRIMARY KEY (`id_orar`)
) ENGINE=InnoDB AUTO_INCREMENT=77 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.orar: ~68 rows (approximately)
DELETE FROM `orar`;
INSERT INTO `orar` (`id_orar`, `id_spec`, `id_sala`, `ora`, `data`) VALUES
	(2, 2, 2, '15:00', '2025-01-16'),
	(3, 3, 4, '17:00', '2025-02-10'),
	(4, 4, 7, '18:30', '2024-12-28'),
	(5, 1, 3, '19:25', '2025-01-22'),
	(6, 3, 2, '19:00', '2025-02-11'),
	(7, 5, 1, '19:30', '2025-08-06'),
	(8, 5, 1, '19:30', '2025-04-30'),
	(9, 5, 5, '19:02', '2025-07-23'),
	(10, 1, 6, '18:20', '2025-09-04'),
	(12, 6, 2, '15:30', '2025-06-10'),
	(15, 5, 7, '20:03', '2025-09-17'),
	(16, 4, 6, '18:05', '2025-05-24'),
	(17, 6, 1, '17:30', '2025-09-01'),
	(18, 1, 1, '17:13', '2025-05-28'),
	(23, 12, 2, '15:33', '2025-07-15'),
	(24, 6, 3, '15:37', '2025-08-06'),
	(25, 13, 3, '20:43', '2025-05-29'),
	(26, 5, 7, '20:03', '1970-01-01'),
	(27, 1, 6, '18:20', '2025-04-09'),
	(28, 6, 1, '17:30', '2025-01-09'),
	(29, 5, 1, '19:30', '2025-06-08'),
	(30, 6, 3, '15:37', '2025-06-08'),
	(31, 5, 5, '19:02', '1970-01-01'),
	(32, 12, 2, '15:33', '1970-01-01'),
	(33, 6, 2, '15:30', '2025-10-06'),
	(34, 13, 3, '20:43', '1970-01-01'),
	(35, 1, 1, '17:13', '1970-01-01'),
	(36, 4, 6, '18:05', '1970-01-01'),
	(37, 5, 1, '19:30', '1970-01-01'),
	(38, 3, 2, '19:00', '2025-11-02'),
	(39, 3, 4, '17:00', '2025-10-02'),
	(40, 1, 3, '19:25', '1970-01-01'),
	(41, 2, 2, '15:00', '1970-01-01'),
	(42, 4, 7, '18:30', '1970-01-01'),
	(43, 5, 7, '20:03', '1970-01-01'),
	(44, 1, 6, '18:20', '2025-04-09'),
	(45, 6, 1, '18:30', '2025-01-09'),
	(46, 5, 1, '19:30', '2025-06-08'),
	(47, 6, 3, '15:37', '2025-06-08'),
	(48, 5, 5, '19:02', '1970-01-01'),
	(49, 12, 2, '15:33', '1970-01-01'),
	(50, 6, 2, '15:30', '2025-10-06'),
	(51, 13, 3, '20:43', '1970-01-01'),
	(52, 1, 1, '17:13', '1970-01-01'),
	(53, 4, 6, '18:05', '1970-01-01'),
	(54, 5, 1, '19:30', '1970-01-01'),
	(55, 3, 2, '19:00', '2025-11-02'),
	(56, 3, 4, '17:00', '2025-10-02'),
	(57, 1, 3, '19:25', '1970-01-01'),
	(58, 2, 2, '15:00', '1970-01-01'),
	(59, 4, 7, '18:30', '1970-01-01'),
	(60, 5, 7, '20:03', '1970-01-01'),
	(61, 1, 6, '18:20', '2025-04-09'),
	(62, 6, 1, '18:30', '2025-01-09'),
	(63, 5, 1, '19:30', '2025-06-08'),
	(64, 6, 3, '15:37', '2025-06-08'),
	(65, 5, 5, '19:02', '1970-01-01'),
	(66, 12, 2, '15:33', '1970-01-01'),
	(67, 6, 2, '15:30', '2025-10-06'),
	(68, 13, 3, '20:43', '1970-01-01'),
	(69, 1, 1, '17:13', '1970-01-01'),
	(70, 4, 6, '18:05', '1970-01-01'),
	(71, 5, 1, '19:30', '1970-01-01'),
	(72, 3, 2, '19:00', '2025-11-02'),
	(73, 3, 4, '17:00', '2025-10-02'),
	(74, 1, 3, '19:25', '1970-01-01'),
	(75, 2, 2, '15:00', '1970-01-01'),
	(76, 4, 7, '18:30', '1970-01-01');

-- Dumping structure for table agentie_turism.plata
DROP TABLE IF EXISTS `plata`;
CREATE TABLE IF NOT EXISTS `plata` (
  `id_plata` int NOT NULL,
  `tip_plata` enum('Card','Cash') DEFAULT NULL,
  `pret_total` int DEFAULT NULL,
  `id_rezervare` int DEFAULT NULL,
  `id_user` int DEFAULT NULL,
  PRIMARY KEY (`id_plata`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.plata: ~0 rows (approximately)
DELETE FROM `plata`;

-- Dumping structure for table agentie_turism.recenzie
DROP TABLE IF EXISTS `recenzie`;
CREATE TABLE IF NOT EXISTS `recenzie` (
  `ID_RECENZIE` int NOT NULL AUTO_INCREMENT,
  `ID_SPEC` int DEFAULT NULL,
  `ID_USER` int DEFAULT NULL,
  `RATING` int NOT NULL,
  `COMENTARIU` text,
  `DATA_RECENZIE` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID_RECENZIE`),
  KEY `ID_USER` (`ID_USER`),
  CONSTRAINT `recenzie_ibfk_2` FOREIGN KEY (`ID_USER`) REFERENCES `utilizatori` (`id_user`),
  CONSTRAINT `recenzie_chk_1` CHECK ((`RATING` between 1 and 5))
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.recenzie: ~3 rows (approximately)
DELETE FROM `recenzie`;
INSERT INTO `recenzie` (`ID_RECENZIE`, `ID_SPEC`, `ID_USER`, `RATING`, `COMENTARIU`, `DATA_RECENZIE`) VALUES
	(1, 1, 1, 5, 'Un spectacol care reflecta realitatea', '2025-04-24 15:34:53'),
	(2, 1, 2, 3, 'A fost okey ....dar cred ca ar trebui sa mai lucreze ', '2025-04-24 15:36:22'),
	(3, 3, 1, 4, 'Mi-a placut dar mai trebuie lucrat la el', '2025-05-10 18:44:21');

-- Dumping structure for table agentie_turism.regizori
DROP TABLE IF EXISTS `regizori`;
CREATE TABLE IF NOT EXISTS `regizori` (
  `id_regizor` int NOT NULL AUTO_INCREMENT,
  `nume` varchar(100) NOT NULL,
  `prenume` varchar(100) NOT NULL,
  `data_nasterii` date NOT NULL,
  PRIMARY KEY (`id_regizor`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.regizori: ~9 rows (approximately)
DELETE FROM `regizori`;
INSERT INTO `regizori` (`id_regizor`, `nume`, `prenume`, `data_nasterii`) VALUES
	(1, 'Ionescu', 'Andrei', '1980-05-15'),
	(2, 'Popescu', 'Maria', '1975-11-22'),
	(3, 'Georgescu', 'Ion', '1968-02-10'),
	(4, 'Mihaila', 'Elena', '1985-06-30'),
	(5, 'Vasilescu', 'Alexandru', '1970-09-01'),
	(6, 'Popa', 'George', '1980-06-10'),
	(7, 'Crin', 'George', '1989-02-02'),
	(8, 'Haghi', 'Adelin', '1889-06-12'),
	(9, 'Popa', 'Remus', '1998-02-03');

-- Dumping structure for table agentie_turism.rezervare
DROP TABLE IF EXISTS `rezervare`;
CREATE TABLE IF NOT EXISTS `rezervare` (
  `id_rezervare` int NOT NULL AUTO_INCREMENT,
  `id_orar` int NOT NULL,
  `id_loc` int NOT NULL,
  PRIMARY KEY (`id_rezervare`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.rezervare: ~45 rows (approximately)
DELETE FROM `rezervare`;
INSERT INTO `rezervare` (`id_rezervare`, `id_orar`, `id_loc`) VALUES
	(1, 3, 29),
	(2, 3, 31),
	(3, 2, 12),
	(4, 2, 13),
	(5, 5, 19),
	(6, 5, 24),
	(7, 5, 25),
	(8, 5, 21),
	(9, 7, 29),
	(10, 5, 23),
	(11, 2, 18),
	(12, 2, 10),
	(13, 2, 16),
	(14, 2, 15),
	(15, 3, 27),
	(16, 3, 28),
	(17, 4, 60),
	(18, 4, 61),
	(19, 5, 22),
	(20, 5, 26),
	(21, 6, 16),
	(22, 6, 11),
	(23, 6, 12),
	(24, 6, 14),
	(25, 6, 15),
	(26, 2, 14),
	(27, 3, 30),
	(28, 2, 17),
	(29, 2, 11),
	(30, 3, 32),
	(32, 5, 20),
	(33, 7, 9),
	(34, 7, 3),
	(35, 7, 8),
	(36, 7, 7),
	(37, 7, 2),
	(38, 7, 1),
	(39, 8, 1),
	(40, 8, 4),
	(41, 8, 7),
	(42, 16, 44),
	(43, 16, 48),
	(44, 4, 55),
	(45, 4, 56),
	(46, 6, 17);

-- Dumping structure for table agentie_turism.sala
DROP TABLE IF EXISTS `sala`;
CREATE TABLE IF NOT EXISTS `sala` (
  `id_sala` int NOT NULL,
  `id_teatru` int DEFAULT NULL,
  `nume` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id_sala`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.sala: ~7 rows (approximately)
DELETE FROM `sala`;
INSERT INTO `sala` (`id_sala`, `id_teatru`, `nume`) VALUES
	(1, 1, 'Sala Mare'),
	(2, 1, 'Sala Studio'),
	(3, 2, 'Sala Principală'),
	(4, 3, 'Sala Nouă'),
	(5, 3, 'Sala Alternativă'),
	(6, 4, 'Sala Clasică'),
	(7, 5, 'Sala Modernă');

-- Dumping structure for table agentie_turism.spectacol
DROP TABLE IF EXISTS `spectacol`;
CREATE TABLE IF NOT EXISTS `spectacol` (
  `id_spec` int NOT NULL AUTO_INCREMENT,
  `nume` varchar(45) NOT NULL,
  `gen` varchar(45) NOT NULL,
  `pret_pers` int NOT NULL,
  `descriere` varchar(200) NOT NULL,
  `id_regizor` int NOT NULL,
  PRIMARY KEY (`id_spec`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.spectacol: ~13 rows (approximately)
DELETE FROM `spectacol`;
INSERT INTO `spectacol` (`id_spec`, `nume`, `gen`, `pret_pers`, `descriere`, `id_regizor`) VALUES
	(1, 'O scrisoare pierdută', 'Comedie', 20, 'O comedie clasică românească de Ion Luca Caragiale, care satirizează lupta pentru putere și corupția din societatea politică.', 1),
	(2, 'Romeo și Julieta', 'Tragedie', 25, 'O tragică poveste de dragoste scrisă de William Shakespeare, despre doi îndrăgostiți ale căror familii rivale le împiedică fericirea.', 2),
	(3, 'Lacul Lebedelor', 'Musical', 70, 'Un spectacol de balet celebru, compus de Piotr Ilici Ceaikovski, care spune povestea prințesei Odette transformată în lebădă de un vrăjitor malefic.', 3),
	(4, 'Regele Lear', 'Dramă', 45, 'O dramă intensă de William Shakespeare, care explorează trădarea, nebunia și lupta pentru putere într-o familie regală.', 1),
	(5, 'Nunta lui Figaro', 'Operă', 32, 'O comedie muzicală de Wolfgang Amadeus Mozart, plină de intrigi și răsturnări de situație, care urmărește pregătirile pentru nunta servitorului Figaro.', 4),
	(6, 'Chirita in provintie', 'Comedie', 30, 'BLAA BLAA', 4),
	(7, 'Saracu Gica', 'Comedie', 23, 'vai si amar de el :(', 6),
	(8, 'Noaptea Jazz', 'Musical', 25, 'instrumental', 6),
	(9, 'ceva', 'Balet', 16, 'da da', 1),
	(10, 'Ceva si ceva', 'Operă', 20, 'bla bla', 7),
	(11, 'Test', 'Operă', 20, 'bla bla', 3),
	(12, 'Pupaza', 'Balet', 50, 'Ceva frumos :)', 8),
	(13, 'Gasca', 'Comedie', 90, 'dddd', 9);

-- Dumping structure for table agentie_turism.teatru
DROP TABLE IF EXISTS `teatru`;
CREATE TABLE IF NOT EXISTS `teatru` (
  `id_teatru` int NOT NULL,
  `nume` varchar(45) DEFAULT NULL,
  `adresa` varchar(45) DEFAULT NULL,
  `oras` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`id_teatru`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.teatru: ~5 rows (approximately)
DELETE FROM `teatru`;
INSERT INTO `teatru` (`id_teatru`, `nume`, `adresa`, `oras`) VALUES
	(1, 'Teatrul National', 'Strada Principala 123', 'Bucuresti'),
	(2, 'Teatrul de Comedie', 'Strada Veseliei 45', 'Cluj-Napoca'),
	(3, 'Teatrul Dramatic', 'Bulevardul Libertătii 89', 'Timisoara'),
	(4, 'Teatrul Mic', 'Strada Linistita 12', 'Iasi'),
	(5, 'Teatrul Mare', 'Piata Centrala 34', 'Constanta');

-- Dumping structure for view agentie_turism.user_ticket
DROP VIEW IF EXISTS `user_ticket`;
-- Creating temporary table to overcome VIEW dependency errors
CREATE TABLE `user_ticket` (
	`id_user` INT NOT NULL,
	`NUME_SPECT` VARCHAR(1) NOT NULL COLLATE 'utf8mb4_0900_ai_ci',
	`NUME_TEATRU` VARCHAR(1) NULL COLLATE 'utf8mb4_0900_ai_ci',
	`data` DATE NULL,
	`ora` VARCHAR(1) NULL COLLATE 'utf8mb4_0900_ai_ci'
) ENGINE=MyISAM;

-- Dumping structure for table agentie_turism.utilizatori
DROP TABLE IF EXISTS `utilizatori`;
CREATE TABLE IF NOT EXISTS `utilizatori` (
  `id_user` int NOT NULL AUTO_INCREMENT,
  `nume` varchar(20) DEFAULT NULL,
  `pren` varchar(20) DEFAULT NULL,
  `email` varchar(45) NOT NULL,
  `parola` varchar(50) DEFAULT NULL,
  `rol` enum('Admin','User') DEFAULT NULL,
  `tel` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id_user`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `tel_UNIQUE` (`tel`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table agentie_turism.utilizatori: ~5 rows (approximately)
DELETE FROM `utilizatori`;
INSERT INTO `utilizatori` (`id_user`, `nume`, `pren`, `email`, `parola`, `rol`, `tel`) VALUES
	(1, 'Radu', 'Andreea', 'ra@yahoo.com', '1234', 'Admin', '0751503648'),
	(2, 'Cojan', 'Alin', 'ca@yahoo.com', '2003', 'User', '0784339422'),
	(5, 'Popa', 'Adelina', 'pa@yahoo.com', '0000', 'User', '0784536412'),
	(6, 'Popandache', 'Ghita', 'pg@yahoo.com', '10_lei', 'User', '0777777777'),
	(8, 'Petrea', 'Ana', 'pam@yahoo.com', '2003', 'User', '0712345678');

-- Removing temporary table and create final VIEW structure
DROP TABLE IF EXISTS `user_ticket`;
CREATE ALGORITHM=UNDEFINED SQL SECURITY DEFINER VIEW `user_ticket` AS select distinct `u`.`id_user` AS `id_user`,`s`.`nume` AS `NUME_SPECT`,`t`.`nume` AS `NUME_TEATRU`,`o`.`data` AS `data`,`o`.`ora` AS `ora` from ((((((`utilizatori` `u` join `bilet` `b` on((`b`.`id_user` = `u`.`id_user`))) join `rezervare` `r` on((`r`.`id_rezervare` = `b`.`id_rezervare`))) join `orar` `o` on((`o`.`id_orar` = `r`.`id_orar`))) join `spectacol` `s` on((`s`.`id_spec` = `o`.`id_spec`))) join `sala` `ss` on((`ss`.`id_sala` = `o`.`id_sala`))) join `teatru` `t` on((`t`.`id_teatru` = `ss`.`id_teatru`)))
;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
