CREATE SCHEMA IF NOT EXISTS `chefencasagrupoj` DEFAULT CHARACTER SET utf8;
USE `chefencasagrupoj`;


CREATE TABLE IF NOT EXISTS usuario (
  idusuario INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(45) NOT NULL,
  email VARCHAR(45) NOT NULL,
  user VARCHAR(45) NOT NULL,
  password VARCHAR(45) NOT NULL,
  PRIMARY KEY (idusuario)
);

CREATE TABLE IF NOT EXISTS `categoria` (
  `idcategoria` INT NOT NULL,
  `nombreCategoria` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idcategoria`));

INSERT INTO `categoria` VALUES (1,'Postres'),(2,'Veganas'),(3,'Resposteria'),(4,'Bebidas'),(5,'Regionales');

CREATE TABLE IF NOT EXISTS `receta` (
  `idreceta` INT NOT NULL AUTO_INCREMENT,
  `titulo` VARCHAR(45) NOT NULL,
  `descripcion` VARCHAR(100) NOT NULL,
  `tiempoPreparacion` INT NOT NULL,
  `ingredientes` VARCHAR(200) NOT NULL,
  `pasos` VARCHAR(600) NOT NULL,
  `url_foto1` TEXT NULL DEFAULT NULL,
  `url_foto2` TEXT NULL DEFAULT NULL,
  `url_foto3` TEXT NULL DEFAULT NULL,
  `url_foto4` TEXT NULL DEFAULT NULL,
  `url_foto5` TEXT NULL DEFAULT NULL,
  `usuario_idusuario` INT NOT NULL,
  `categoria_idcategoria` INT NOT NULL,
  PRIMARY KEY (`idreceta`),
  INDEX `fk_receta_usuario1_idx` (`usuario_idusuario` ASC),
  CONSTRAINT `fk_receta_usuario1`
    FOREIGN KEY (`usuario_idusuario`)
    REFERENCES `usuario` (`idusuario`),
  INDEX `fk_receta_categoria_idx` (`categoria_idcategoria` ASC),
  CONSTRAINT `fk_receta_categoria1`
    FOREIGN KEY (`categoria_idcategoria`)
    REFERENCES `categoria` (`idcategoria`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);



CREATE TABLE IF NOT EXISTS `recetaFavoritas` (
  `idrecetaFavoritas` INT NOT NULL,
  `recetasFavoritascol` INT NULL,
  `usuario_idusuario1` INT NOT NULL,
  PRIMARY KEY (`idrecetaFavoritas`, `usuario_idusuario1`),
  INDEX `fk_recetaFavoritas_usuario1_idx` (`usuario_idusuario1` ASC),
  CONSTRAINT `fk_recetaFavoritas_usuario1`
    FOREIGN KEY (`usuario_idusuario1`)
    REFERENCES `usuario` (`idusuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);



CREATE TABLE IF NOT EXISTS `suscripcion` (
  `idsuscripcion` INT NOT NULL,
  `usuario_idusuario` INT NOT NULL,
  PRIMARY KEY (`idsuscripcion`, `usuario_idusuario`),
  INDEX `fk_suscripcion_usuario1_idx` (`usuario_idusuario` ASC) ,
  CONSTRAINT `fk_suscripcion_usuario1`
    FOREIGN KEY (`usuario_idusuario`)
    REFERENCES `usuario` (`idusuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);
