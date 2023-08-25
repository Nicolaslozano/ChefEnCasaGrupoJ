CREATE SCHEMA IF NOT EXISTS `chefencasagrupoj` DEFAULT CHARACTER SET utf8;
USE `chefencasagrupoj`;


CREATE TABLE IF NOT EXISTS `usuario` (
  `idusuario` INT NOT NULL,
  `nombre` VARCHAR(45) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `usuario` VARCHAR(45) NOT NULL,
  `contraseña` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idusuario`));



CREATE TABLE IF NOT EXISTS `receta` (
  `idReceta` INT NOT NULL,
  `titulo` VARCHAR(45) NOT NULL,
  `descripcion` VARCHAR(45) NOT NULL,
  `tiempoPreparacion` INT NOT NULL,
  PRIMARY KEY (`idReceta`));


CREATE TABLE IF NOT EXISTS `foto` (
  `idfoto` INT NOT NULL,
  `fotoUrl` VARCHAR(45) NOT NULL,
  `receta_idReceta` INT NOT NULL,
  PRIMARY KEY (`idfoto`, `receta_idReceta`),
  INDEX `fk_foto_receta1_idx` (`receta_idReceta` ASC) VISIBLE,
  CONSTRAINT `fk_foto_receta1`
    FOREIGN KEY (`receta_idReceta`)
    REFERENCES `receta` (`idReceta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE TABLE IF NOT EXISTS `categoria` (
  `idcategoria` INT NOT NULL,
  `nombreCategoria` VARCHAR(45) NOT NULL,
  `receta_idReceta` INT NOT NULL,
  PRIMARY KEY (`idcategoria`, `receta_idReceta`),
  INDEX `fk_categoria_receta1_idx` (`receta_idReceta` ASC) VISIBLE,
  CONSTRAINT `fk_categoria_receta1`
    FOREIGN KEY (`receta_idReceta`)
    REFERENCES `receta` (`idReceta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE TABLE IF NOT EXISTS `ingrediente` (
  `idingrediente` INT NOT NULL,
  `nombreIngrediente` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idingrediente`));


CREATE TABLE IF NOT EXISTS `ingredientesRecetas` (
  `receta_idReceta` INT NOT NULL,
  `ingrediente_idingrediente` INT NOT NULL,
  PRIMARY KEY (`receta_idReceta`, `ingrediente_idingrediente`),
  INDEX `fk_receta_has_ingrediente_ingrediente1_idx` (`ingrediente_idingrediente` ASC) VISIBLE,
  INDEX `fk_receta_has_ingrediente_receta1_idx` (`receta_idReceta` ASC) VISIBLE,
  CONSTRAINT `fk_receta_has_ingrediente_receta1`
    FOREIGN KEY (`receta_idReceta`)
    REFERENCES `receta` (`idReceta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_receta_has_ingrediente_ingrediente1`
    FOREIGN KEY (`ingrediente_idingrediente`)
    REFERENCES `ingrediente` (`idingrediente`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

    
CREATE TABLE IF NOT EXISTS `recetaDeUsuario` (
  `usuario_idusuario` INT NOT NULL,
  `receta_idReceta` INT NOT NULL,
  PRIMARY KEY (`usuario_idusuario`, `receta_idReceta`),
  INDEX `fk_usuario_has_receta_receta1_idx` (`receta_idReceta` ASC) VISIBLE,
  INDEX `fk_usuario_has_receta_usuario1_idx` (`usuario_idusuario` ASC) VISIBLE,
  CONSTRAINT `fk_usuario_has_receta_usuario1`
    FOREIGN KEY (`usuario_idusuario`)
    REFERENCES `usuario` (`idusuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_usuario_has_receta_receta1`
    FOREIGN KEY (`receta_idReceta`)
    REFERENCES `receta` (`idReceta`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);



CREATE TABLE IF NOT EXISTS `recetaFavoritas` (
  `idrecetaFavoritas` INT NOT NULL,
  `recetasFavoritascol` VARCHAR(45) NULL,
  `usuario_idusuario` INT NOT NULL,
  PRIMARY KEY (`idrecetaFavoritas`, `usuario_idusuario`),
  INDEX `fk_recetaFavoritas_usuario1_idx` (`usuario_idusuario` ASC) VISIBLE,
  CONSTRAINT `fk_recetaFavoritas_usuario1`
    FOREIGN KEY (`usuario_idusuario`)
    REFERENCES `usuario` (`idusuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);



CREATE TABLE IF NOT EXISTS `suscripcion` (
  `idsuscripcion` INT NOT NULL,
  `nombreUsuario` VARCHAR(45) NOT NULL,
  `usuario_idusuario` INT NOT NULL,
  PRIMARY KEY (`idsuscripcion`, `usuario_idusuario`),
  INDEX `fk_suscripcion_usuario1_idx` (`usuario_idusuario` ASC) VISIBLE,
  CONSTRAINT `fk_suscripcion_usuario1`
    FOREIGN KEY (`usuario_idusuario`)
    REFERENCES `usuario` (`idusuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


