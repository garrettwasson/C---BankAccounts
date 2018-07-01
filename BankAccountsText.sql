-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema bankaccountsdb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema bankaccountsdb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `bankaccountsdb` DEFAULT CHARACTER SET utf8 ;
USE `bankaccountsdb` ;

-- -----------------------------------------------------
-- Table `bankaccountsdb`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `bankaccountsdb`.`users` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `firstName` VARCHAR(255) NULL,
  `lastName` VARCHAR(255) NULL,
  `email` VARCHAR(255) NULL,
  `password` VARCHAR(255) NULL,
  `balance` DOUBLE NULL,
  `createdAt` DATETIME NULL,
  `updatedat` DATETIME NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `bankaccountsdb`.`transactions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `bankaccountsdb`.`transactions` (
  `ID` INT NOT NULL AUTO_INCREMENT,
  `userID` INT NOT NULL,
  `total` DOUBLE NULL,
  `createdAt` DATETIME NULL,
  `updatedAt` DATETIME NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_transactions_users_idx` (`userID` ASC),
  CONSTRAINT `fk_transactions_users`
    FOREIGN KEY (`userID`)
    REFERENCES `bankaccountsdb`.`users` (`ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
