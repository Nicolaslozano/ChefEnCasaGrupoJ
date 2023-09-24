CREATE SCHEMA IF NOT EXISTS `chefencasagrupoj` DEFAULT CHARACTER SET utf8;
USE `chefencasagrupoj`;



CREATE TABLE IF NOT EXISTS usuario (
  idusuario INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(45) NOT NULL,
  email VARCHAR(45) NOT NULL,
  user VARCHAR(45) NOT NULL,
  password VARCHAR(45) NOT NULL,
  popular INT NOT NULL,
  PRIMARY KEY (idusuario),
      INDEX `idx_user` (`user`)
);

CREATE TABLE IF NOT EXISTS `categoria` (
  `idcategoria` INT NOT NULL,
  `nombreCategoria` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idcategoria`),
	INDEX `idx_nombreCategoria` (`nombreCategoria`)
);

INSERT INTO `categoria` VALUES (1,'Postres'),(2,'Veganas'),(3,'Resposteria'),(4,'Bebidas'),(5,'Regionales');

CREATE TABLE IF NOT EXISTS `receta` (
  `idreceta` INT NOT NULL AUTO_INCREMENT,
  `titulo` VARCHAR(45) NOT NULL,
  `descripcion` VARCHAR(600) NOT NULL,
  `tiempoPreparacion` INT NOT NULL,
  `ingredientes` VARCHAR(200) NOT NULL,
  `pasos` VARCHAR(600) NOT NULL,
  `url_foto1` TEXT NULL DEFAULT NULL,
  `url_foto2` TEXT NULL DEFAULT NULL,
  `url_foto3` TEXT NULL DEFAULT NULL,
  `url_foto4` TEXT NULL DEFAULT NULL,
  `url_foto5` TEXT NULL DEFAULT NULL,
  `usuario_user` VARCHAR(45) NOT NULL,
  `nombreCategoria1` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idreceta`),
  INDEX `fk_receta_usuario1_idx` (`usuario_user` ASC),
  CONSTRAINT `fk_receta_usuario1`
    FOREIGN KEY (`usuario_user`)
    REFERENCES `usuario` (`user`),
  INDEX `fk_receta_categoria_idx` (`nombreCategoria1` ASC),
  CONSTRAINT `fk_receta_categoria1`
    FOREIGN KEY (`nombreCategoria1`)
    REFERENCES `categoria` (`nombreCategoria`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);



CREATE TABLE IF NOT EXISTS `recetaFavoritas` (
  `idrecetaFavoritas` INT NOT NULL AUTO_INCREMENT,
  `recetasFavoritascol` INT NULL,
  `usuario_userfav` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idrecetaFavoritas`),
  INDEX `fk_suscripcion_user_idx` (`usuario_userfav` ASC),
  CONSTRAINT `fk_recetaFavoritas_usuario1`
    FOREIGN KEY (`usuario_userfav`)
    REFERENCES `usuario` (`user`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);



CREATE TABLE IF NOT EXISTS `suscripcion` (
  `idsuscripcion` INT NOT NULL AUTO_INCREMENT,
  `followed_user` VARCHAR(45) NOT NULL,
  `my_user` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idsuscripcion`),
  INDEX `fk_suscripcion_usuario1_idx` (`my_user` ASC),
  CONSTRAINT `fk_suscripcion_user1`
    FOREIGN KEY (`my_user`)
    REFERENCES `usuario` (`user`)
    ON UPDATE NO ACTION
);
