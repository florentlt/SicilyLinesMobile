-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost:3306
-- Généré le : lun. 31 mars 2025 à 04:24
-- Version du serveur : 8.0.30
-- Version de PHP : 8.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `sicilylinesmobile`
--

-- --------------------------------------------------------

--
-- Structure de la table `bateau`
--

CREATE TABLE `bateau` (
  `IDBATEAU` int NOT NULL,
  `NOM` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `LONGUEUR` decimal(10,2) DEFAULT NULL,
  `LARGEUR` decimal(10,2) DEFAULT NULL,
  `VITESSE` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `bateau`
--

INSERT INTO `bateau` (`IDBATEAU`, `NOM`, `LONGUEUR`, `LARGEUR`, `VITESSE`) VALUES
(1, 'Marco', 30.00, 8.00, 25),
(2, 'Mantegna', 28.00, 7.50, 24),
(3, 'Antioco', 29.00, 8.00, 23),
(4, 'Platone', 27.00, 7.00, 22),
(5, 'Eduardo', 31.00, 9.00, 26);

-- --------------------------------------------------------

--
-- Structure de la table `categorie`
--

CREATE TABLE `categorie` (
  `IDCATEGORIE` int NOT NULL,
  `LIBELLE` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `categorie`
--

INSERT INTO `categorie` (`IDCATEGORIE`, `LIBELLE`) VALUES
(1, 'Passager'),
(2, 'Véh.inf.2m'),
(3, 'Véh.sup.2m');

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

CREATE TABLE `client` (
  `IDCLIENT` int NOT NULL,
  `CP` char(32) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `ADRESSE` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `VILLE` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `EMAIL` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `MDP` varchar(255) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `NOM` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `PRENOM` varchar(100) COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `client`
--

INSERT INTO `client` (`IDCLIENT`, `CP`, `ADRESSE`, `VILLE`, `EMAIL`, `MDP`, `NOM`, `PRENOM`) VALUES
(1, '75001', '5 Rue de Rivoli', 'Paris', 'flo@mail.com', '$2b$12$JbSLzNki//kySfSv4ITsiOXZef6cXUF04sHGKlVWipP.ty6Sr2/xO', 'LOUET', 'Florent'),
(2, '69002', '10 Rue de la République', 'Lyon', 'jean.dujardin@mail.com', '$2b$12$QGO.9LcUerF9.ZCzaFGaP.N8q9H.D2cSZQarc92oLhsyOOuyP5Htq', 'DUJARDIN', 'Jean'),
(3, '13001', '15 Boulevard de la Liberté', 'Marseille', 'frederic.chopin@mail.com', 'azer', 'CHOPIN', 'Frederic'),
(4, '59000', '20 Rue Faidherbe', 'Lille', 'erik.satie@mail.com', 'azer', 'SATIE', 'Erik');

-- --------------------------------------------------------

--
-- Structure de la table `contenir`
--

CREATE TABLE `contenir` (
  `IDBATEAU` int NOT NULL,
  `IDCATEGORIE` int NOT NULL,
  `CAPACITEMAX` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `contenir`
--

INSERT INTO `contenir` (`IDBATEAU`, `IDCATEGORIE`, `CAPACITEMAX`) VALUES
(1, 1, 300),
(1, 2, 15),
(1, 3, 5),
(2, 1, 276),
(2, 2, 10),
(2, 3, 3),
(3, 1, 250),
(3, 2, 8),
(3, 3, 2),
(4, 1, 155),
(4, 2, 5),
(4, 3, 1);

-- --------------------------------------------------------

--
-- Structure de la table `equipement`
--

CREATE TABLE `equipement` (
  `IDEQUIPEMENT` int NOT NULL,
  `LIBELLE` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `equipement`
--

INSERT INTO `equipement` (`IDEQUIPEMENT`, `LIBELLE`) VALUES
(1, 'Accès Handicapé'),
(2, 'Bar'),
(3, 'Pont Promenade'),
(4, 'Salon Vidéo');

-- --------------------------------------------------------

--
-- Structure de la table `liaison`
--

CREATE TABLE `liaison` (
  `IDLIAISON` int NOT NULL,
  `IDSECTEUR` int NOT NULL,
  `IDPORTDEPART` int NOT NULL,
  `IDPORTARRIVEE` int NOT NULL,
  `DUREE` time DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `liaison`
--

INSERT INTO `liaison` (`IDLIAISON`, `IDSECTEUR`, `IDPORTDEPART`, `IDPORTARRIVEE`, `DUREE`) VALUES
(1, 1, 2, 3, '02:30:00'),
(2, 2, 3, 4, '04:00:00'),
(11, 3, 8, 3, '00:50:00'),
(15, 1, 1, 2, '01:30:00'),
(16, 2, 4, 5, '01:25:00'),
(17, 2, 4, 6, '01:45:00'),
(19, 2, 4, 7, '02:05:00'),
(21, 4, 9, 11, '00:30:00'),
(24, 1, 1, 3, '04:00:00'),
(25, 3, 8, 6, '00:40:00'),
(30, 4, 9, 10, '02:30:00');

-- --------------------------------------------------------

--
-- Structure de la table `periode`
--

CREATE TABLE `periode` (
  `IDPERIODE` int NOT NULL,
  `DATEDEBUT` date DEFAULT NULL,
  `DATEFIN` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `periode`
--

INSERT INTO `periode` (`IDPERIODE`, `DATEDEBUT`, `DATEFIN`) VALUES
(1, '2021-09-01', '2022-06-15'),
(2, '2022-06-16', '2022-09-15'),
(3, '2022-09-16', '2023-05-31');

-- --------------------------------------------------------

--
-- Structure de la table `port`
--

CREATE TABLE `port` (
  `IDPORT` int NOT NULL,
  `NOM` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `port`
--

INSERT INTO `port` (`IDPORT`, `NOM`) VALUES
(1, 'Palerme'),
(2, 'Ustica'),
(3, 'Lipari'),
(4, 'Messine'),
(5, 'Stromboli'),
(6, 'Vulcano'),
(7, 'Panarea'),
(8, 'Milazzo'),
(9, 'Trapani'),
(10, 'Pantelleria'),
(11, 'Favignagna');

-- --------------------------------------------------------

--
-- Structure de la table `proposer`
--

CREATE TABLE `proposer` (
  `IDBATEAU` int NOT NULL,
  `IDEQUIPEMENT` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `proposer`
--

INSERT INTO `proposer` (`IDBATEAU`, `IDEQUIPEMENT`) VALUES
(1, 1),
(1, 2),
(1, 4),
(2, 1),
(2, 2),
(3, 1),
(3, 3),
(4, 1),
(4, 4);

-- --------------------------------------------------------

--
-- Structure de la table `reservation`
--

CREATE TABLE `reservation` (
  `IDRESERVATION` int NOT NULL,
  `IDLIAISON` int NOT NULL,
  `IDTRAVERSEE` int NOT NULL,
  `IDCLIENT` int NOT NULL,
  `RECAPITULATIF` char(64) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `reservation`
--

INSERT INTO `reservation` (`IDRESERVATION`, `IDLIAISON`, `IDTRAVERSEE`, `IDCLIENT`, `RECAPITULATIF`) VALUES
(1, 1, 541202, 1, 'Réservation pour 2 Adultes'),
(2, 2, 541203, 2, 'Réservation pour 1 Adulte et 1 Voiture'),
(3, 1, 36, 2, 'Réservation pour 3 Adultes'),
(4, 21, 541207, 1, 'Réservation pour 2 Adultes et 1 Enfant'),
(5, 24, 541208, 1, 'Réservation pour 1 Adulte et 2 Juniors'),
(6, 30, 541210, 2, 'Réservation pour 2 Adultes et 1 Véhicule inférieur à 4m');

-- --------------------------------------------------------

--
-- Structure de la table `secteur`
--

CREATE TABLE `secteur` (
  `IDSECTEUR` int NOT NULL,
  `LIBELLE` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `secteur`
--

INSERT INTO `secteur` (`IDSECTEUR`, `LIBELLE`) VALUES
(1, 'Palerme'),
(2, 'Messine'),
(3, 'Milazzo'),
(4, 'Trapani');

-- --------------------------------------------------------

--
-- Structure de la table `selectionner`
--

CREATE TABLE `selectionner` (
  `IDTYPE` int NOT NULL,
  `IDRESERVATION` int NOT NULL,
  `QUANTITE` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `selectionner`
--

INSERT INTO `selectionner` (`IDTYPE`, `IDRESERVATION`, `QUANTITE`) VALUES
(1, 1, '2 Adultes');

-- --------------------------------------------------------

--
-- Structure de la table `tarifier`
--

CREATE TABLE `tarifier` (
  `IDLIAISON` int NOT NULL,
  `IDPERIODE` int NOT NULL,
  `IDTYPE` int NOT NULL,
  `TARIF` decimal(13,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `tarifier`
--

INSERT INTO `tarifier` (`IDLIAISON`, `IDPERIODE`, `IDTYPE`, `TARIF`) VALUES
(1, 1, 1, 20.00);

-- --------------------------------------------------------

--
-- Structure de la table `traversee`
--

CREATE TABLE `traversee` (
  `IDLIAISON` int NOT NULL,
  `IDTRAVERSEE` int NOT NULL,
  `IDBATEAU` int NOT NULL,
  `DATETRAVERSEE` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `HEURE` time DEFAULT NULL,
  `LIBELLE` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `traversee`
--

INSERT INTO `traversee` (`IDLIAISON`, `IDTRAVERSEE`, `IDBATEAU`, `DATETRAVERSEE`, `HEURE`, `LIBELLE`) VALUES
(1, 36, 1, '2022-10-02', '10:00:00', 'Traversee1'),
(1, 541202, 1, '2021-09-22', '09:00:00', 'Traversee2'),
(1, 541204, 1, '2026-04-01', '10:30:00', 'Traversée1'),
(1, 541211, 4, '2024-02-02', '18:00:00', 'Traversée12'),
(2, 541203, 2, '2021-09-22', '17:00:00', 'Traversee3'),
(2, 541205, 2, '2026-04-01', '14:00:00', 'Traversée2'),
(2, 541208, 1, '2024-05-01', '10:30:00', 'Traversée13'),
(11, 541206, 3, '2026-02-02', '16:00:00', 'Traversée3'),
(11, 541209, 2, '2024-05-01', '14:00:00', 'Traversée14'),
(15, 541207, 4, '2026-02-02', '18:00:00', 'Traversée4'),
(15, 541210, 3, '2024-08-02', '16:00:00', 'Traversée15'),
(16, 541204, 1, '2026-05-01', '10:30:00', 'Traversée5'),
(16, 541211, 4, '2024-08-02', '18:00:00', 'Traversée16'),
(17, 541205, 2, '2026-05-01', '14:00:00', 'Traversée6'),
(19, 541206, 3, '2026-08-02', '16:00:00', 'Traversée7'),
(21, 541207, 4, '2026-08-02', '18:00:00', 'Traversée8'),
(24, 541208, 1, '2024-04-01', '10:30:00', 'Traversée9'),
(25, 541209, 2, '2024-04-01', '14:00:00', 'Traversée10'),
(30, 541210, 3, '2024-02-02', '16:00:00', 'Traversée11');

-- --------------------------------------------------------

--
-- Structure de la table `type`
--

CREATE TABLE `type` (
  `IDTYPE` int NOT NULL,
  `IDCATEGORIE` int NOT NULL,
  `LIBELLE` char(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Déchargement des données de la table `type`
--

INSERT INTO `type` (`IDTYPE`, `IDCATEGORIE`, `LIBELLE`) VALUES
(1, 1, 'Adulte'),
(2, 1, 'Junior 8 à 18 ans'),
(3, 1, 'Enfant 0 à 7 ans'),
(4, 2, 'Voiture longueur inférieure à 4m');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `bateau`
--
ALTER TABLE `bateau`
  ADD PRIMARY KEY (`IDBATEAU`);

--
-- Index pour la table `categorie`
--
ALTER TABLE `categorie`
  ADD PRIMARY KEY (`IDCATEGORIE`);

--
-- Index pour la table `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`IDCLIENT`);

--
-- Index pour la table `contenir`
--
ALTER TABLE `contenir`
  ADD PRIMARY KEY (`IDBATEAU`,`IDCATEGORIE`),
  ADD KEY `I_FK_CONTENIR_BATEAU` (`IDBATEAU`),
  ADD KEY `I_FK_CONTENIR_CATEGORIE` (`IDCATEGORIE`);

--
-- Index pour la table `equipement`
--
ALTER TABLE `equipement`
  ADD PRIMARY KEY (`IDEQUIPEMENT`);

--
-- Index pour la table `liaison`
--
ALTER TABLE `liaison`
  ADD PRIMARY KEY (`IDLIAISON`),
  ADD KEY `I_FK_LIAISON_SECTEUR` (`IDSECTEUR`),
  ADD KEY `I_FK_LIAISON_PORT` (`IDPORTDEPART`),
  ADD KEY `I_FK_LIAISON_PORT1` (`IDPORTARRIVEE`);

--
-- Index pour la table `periode`
--
ALTER TABLE `periode`
  ADD PRIMARY KEY (`IDPERIODE`);

--
-- Index pour la table `port`
--
ALTER TABLE `port`
  ADD PRIMARY KEY (`IDPORT`);

--
-- Index pour la table `proposer`
--
ALTER TABLE `proposer`
  ADD PRIMARY KEY (`IDBATEAU`,`IDEQUIPEMENT`),
  ADD KEY `I_FK_PROPOSER_BATEAU` (`IDBATEAU`),
  ADD KEY `I_FK_PROPOSER_EQUIPEMENT` (`IDEQUIPEMENT`);

--
-- Index pour la table `reservation`
--
ALTER TABLE `reservation`
  ADD PRIMARY KEY (`IDRESERVATION`),
  ADD KEY `I_FK_RESERVATION_TRAVERSEE` (`IDLIAISON`,`IDTRAVERSEE`),
  ADD KEY `I_FK_RESERVATION_CLIENT` (`IDCLIENT`);

--
-- Index pour la table `secteur`
--
ALTER TABLE `secteur`
  ADD PRIMARY KEY (`IDSECTEUR`);

--
-- Index pour la table `selectionner`
--
ALTER TABLE `selectionner`
  ADD PRIMARY KEY (`IDTYPE`,`IDRESERVATION`),
  ADD KEY `I_FK_SELECTIONNER_TYPE` (`IDTYPE`),
  ADD KEY `I_FK_SELECTIONNER_RESERVATION` (`IDRESERVATION`);

--
-- Index pour la table `tarifier`
--
ALTER TABLE `tarifier`
  ADD PRIMARY KEY (`IDLIAISON`,`IDPERIODE`,`IDTYPE`),
  ADD KEY `I_FK_TARIFIER_LIAISON` (`IDLIAISON`),
  ADD KEY `I_FK_TARIFIER_PERIODE` (`IDPERIODE`),
  ADD KEY `I_FK_TARIFIER_TYPE` (`IDTYPE`);

--
-- Index pour la table `traversee`
--
ALTER TABLE `traversee`
  ADD PRIMARY KEY (`IDLIAISON`,`IDTRAVERSEE`),
  ADD KEY `I_FK_TRAVERSEE_LIAISON` (`IDLIAISON`),
  ADD KEY `I_FK_TRAVERSEE_BATEAU` (`IDBATEAU`);

--
-- Index pour la table `type`
--
ALTER TABLE `type`
  ADD PRIMARY KEY (`IDTYPE`),
  ADD KEY `I_FK_TYPE_CATEGORIE` (`IDCATEGORIE`);

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `contenir`
--
ALTER TABLE `contenir`
  ADD CONSTRAINT `FK_CONTENIR_BATEAU` FOREIGN KEY (`IDBATEAU`) REFERENCES `bateau` (`IDBATEAU`),
  ADD CONSTRAINT `FK_CONTENIR_CATEGORIE` FOREIGN KEY (`IDCATEGORIE`) REFERENCES `categorie` (`IDCATEGORIE`);

--
-- Contraintes pour la table `liaison`
--
ALTER TABLE `liaison`
  ADD CONSTRAINT `FK_LIAISON_PORT` FOREIGN KEY (`IDPORTDEPART`) REFERENCES `port` (`IDPORT`),
  ADD CONSTRAINT `FK_LIAISON_PORT1` FOREIGN KEY (`IDPORTARRIVEE`) REFERENCES `port` (`IDPORT`),
  ADD CONSTRAINT `FK_LIAISON_SECTEUR` FOREIGN KEY (`IDSECTEUR`) REFERENCES `secteur` (`IDSECTEUR`);

--
-- Contraintes pour la table `proposer`
--
ALTER TABLE `proposer`
  ADD CONSTRAINT `FK_PROPOSER_BATEAU` FOREIGN KEY (`IDBATEAU`) REFERENCES `bateau` (`IDBATEAU`),
  ADD CONSTRAINT `FK_PROPOSER_EQUIPEMENT` FOREIGN KEY (`IDEQUIPEMENT`) REFERENCES `equipement` (`IDEQUIPEMENT`);

--
-- Contraintes pour la table `reservation`
--
ALTER TABLE `reservation`
  ADD CONSTRAINT `FK_RESERVATION_CLIENT` FOREIGN KEY (`IDCLIENT`) REFERENCES `client` (`IDCLIENT`),
  ADD CONSTRAINT `FK_RESERVATION_TRAVERSEE` FOREIGN KEY (`IDLIAISON`,`IDTRAVERSEE`) REFERENCES `traversee` (`IDLIAISON`, `IDTRAVERSEE`);

--
-- Contraintes pour la table `selectionner`
--
ALTER TABLE `selectionner`
  ADD CONSTRAINT `FK_SELECTIONNER_RESERVATION` FOREIGN KEY (`IDRESERVATION`) REFERENCES `reservation` (`IDRESERVATION`),
  ADD CONSTRAINT `FK_SELECTIONNER_TYPE` FOREIGN KEY (`IDTYPE`) REFERENCES `type` (`IDTYPE`);

--
-- Contraintes pour la table `tarifier`
--
ALTER TABLE `tarifier`
  ADD CONSTRAINT `FK_TARIFIER_LIAISON` FOREIGN KEY (`IDLIAISON`) REFERENCES `liaison` (`IDLIAISON`),
  ADD CONSTRAINT `FK_TARIFIER_PERIODE` FOREIGN KEY (`IDPERIODE`) REFERENCES `periode` (`IDPERIODE`),
  ADD CONSTRAINT `FK_TARIFIER_TYPE` FOREIGN KEY (`IDTYPE`) REFERENCES `type` (`IDTYPE`);

--
-- Contraintes pour la table `traversee`
--
ALTER TABLE `traversee`
  ADD CONSTRAINT `FK_TRAVERSEE_BATEAU` FOREIGN KEY (`IDBATEAU`) REFERENCES `bateau` (`IDBATEAU`),
  ADD CONSTRAINT `FK_TRAVERSEE_LIAISON` FOREIGN KEY (`IDLIAISON`) REFERENCES `liaison` (`IDLIAISON`);

--
-- Contraintes pour la table `type`
--
ALTER TABLE `type`
  ADD CONSTRAINT `FK_TYPE_CATEGORIE` FOREIGN KEY (`IDCATEGORIE`) REFERENCES `categorie` (`IDCATEGORIE`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
