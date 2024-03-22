/*create table Usuario(
 IdUsuario int primary key identity(1,1),
 Nombre Varchar(100),
 Fecha_Creacion datetime,
 Usuario Varchar (20),
 Password Varchar(500),
 Estatus bit,
 Acciones Varchar(500)
)

create table FormasFarmauceuticas(
 IdFormaFarmaucetica int primary key identity(1,1),
 Nombre Varchar(100)
)

create table Medicamentos(
 IdMedicamento int primary key identity(1,1),
 Nombre Varchar(100),
 Concentracion Varchar(100),
 Precio Varchar (20),
 Presentacion Varchar(500),
 Habilitado bit,
 Acciones Varchar(500),
 IdFormaFarmaucetica int,
 FOREIGN KEY (IdFormaFarmaucetica) REFERENCES FormasFarmauceuticas(IdFormaFarmaucetica)
)

create PROCEDURE sp_alta_usuario(
@Nombre Varchar(100),
@Fecha_Creacion datetime,
@Usuario Varchar (20),
@Password Varchar(500),
@Estatus bit,
@Acciones Varchar(500),
@Exito bit output,
@Mensaje Varchar (20) output
)
AS
BEGIN
 IF (NOT EXISTS(SELECT Usuario FROM Usuario WHERE Usuario = @Usuario))
  BEGIN
    INSERT INTO Usuario (Nombre,Fecha_Creacion,Usuario,Password,Estatus,Acciones)
	VALUES (@Nombre,@Fecha_Creacion,@Usuario,@Password,@Estatus,@Acciones)
	SET @Exito = 1
	SET @Mensaje = 'Se ha dado de alta al usuario'
  END
  ELSE
   BEGIN
    SET @Exito = 0
	SET @Mensaje = 'El Usuario ya existe'
   END
END



create PROCEDURE sp_actualisa_usuario(
@IdUsuario int,
@Nombre Varchar(100),
@Usuario Varchar (20),
@Password Varchar(500),
@Estatus bit,
@Acciones Varchar(500),
@Exito bit output,
@Mensaje Varchar (20) output
)
AS
BEGIN
 IF (EXISTS(SELECT Usuario FROM Usuario WHERE IdUsuario = @IdUsuario))
  BEGIN
    UPDATE Usuario
	SET Nombre= @Nombre,
	Usuario= @Usuario,
	Password= @Password,
	Estatus = @Estatus,
	Acciones = @Acciones
	WHERE  IdUsuario = @IdUsuario
	SET @Exito = 1
	SET @Mensaje = 'Se actualizado al usuario'
  END
  ELSE
   BEGIN
    SET @Exito = 0
	SET @Mensaje = 'El Usuario no existe'
   END
END

create PROCEDURE sp_elimina_usuario(
@IdUsuario int,
@Exito bit output,
@Mensaje Varchar (20) output
)
AS
BEGIN
 IF (EXISTS(SELECT Usuario FROM Usuario WHERE IdUsuario = @IdUsuario))
  BEGIN
    DELETE Usuario
	WHERE  IdUsuario = @IdUsuario
	SET @Exito = 1
	SET @Mensaje = 'Se ha eliminado al usuario'
  END
  ELSE
   BEGIN
    SET @Exito = 0
	SET @Mensaje = 'El Usuario no existe'
   END
END


CREATE PROCEDURE sp_Valida_Usuario(
@Usuario Varchar (20),
@Password Varchar(500)
)
AS
BEGIN
 IF (EXISTS(SELECT Usuario FROM Usuario WHERE Usuario = @Usuario AND Password = @Password))
   BEGIN
     SELECT IdUsuario FROM Usuario WHERE Usuario = @Usuario AND Password = @Password
   END
  ELSE
    SELECT '0'  
END




CREATE PROCEDURE sp_ConsultaUsuarios(
@Ignorar int = 0,
@Cantidad int = 10,
@Filtro varchar(100)
)
AS
BEGIN
 SELECT IdUsuario,Nombre,Usuario,Fecha_Creacion,Password,Estatus,Acciones FROM Usuario
 ORDER BY IdUsuario
 OFFSET @Ignorar ROWS
 FETCH NEXT @Cantidad ROWS ONLY
END

create PROCEDURE sp_alta_medicamento(
 @Nombre Varchar(100),
 @Concentracion Varchar(100),
 @Precio Varchar (20),
 @Presentacion Varchar(500),
 @Habilitado bit,
 @Acciones Varchar(500),
 @IdFormaFarmaucetica int,
 @Exito bit output,
 @Mensaje Varchar (20) output
)
AS
BEGIN
 IF (NOT EXISTS(SELECT Nombre FROM Medicamentos WHERE Nombre = @Nombre))
  BEGIN
    INSERT INTO Medicamentos (Nombre,Concentracion,Precio,Presentacion,Habilitado,Acciones,IdFormaFarmaucetica)
	VALUES (@Nombre,@Concentracion,@Precio,@Presentacion,@Habilitado,@Acciones,@IdFormaFarmaucetica)
	SET @Exito = 1
	SET @Mensaje = 'Se ha dado de alta el medicamento'
  END
  ELSE
   BEGIN
    SET @Exito = 0
	SET @Mensaje = 'El medicamento ya existe'
   END
END



create PROCEDURE sp_ConsultaMedicamentos(
@Ignorar int = 0,
@Cantidad int = 10,
@Filtro varchar(100)
)
AS
BEGIN
 SELECT IdMedicamento,Nombre,Concentracion,Precio,Presentacion,Habilitado,Acciones,IdFormaFarmaucetica FROM Medicamentos
 ORDER BY IdMedicamento
 OFFSET @Ignorar ROWS
 FETCH NEXT @Cantidad ROWS ONLY
END

create PROCEDURE sp_elimina_Medicamento(
@IdMedicamento int,
@Exito bit output,
@Mensaje Varchar (20) output
)
AS
BEGIN
 IF (EXISTS(SELECT Nombre FROM Medicamentos WHERE IdMedicamento = @IdMedicamento))
  BEGIN
    DELETE Medicamentos
	WHERE  IdMedicamento = @IdMedicamento
	SET @Exito = 1
	SET @Mensaje = 'Se ha eliminado el medicamento'
  END
  ELSE
   BEGIN
    SET @Exito = 0
	SET @Mensaje = 'El medicamento no existe'
   END
END



create PROCEDURE sp_actualiza_medicamento(
@IdMedicamento int,
@Nombre Varchar(100),
 @Concentracion Varchar(100),
 @Precio Varchar (20),
 @Presentacion Varchar(500),
 @Habilitado bit,
 @Acciones Varchar(500),
 @IdFormaFarmaucetica int,
 @Exito bit output,
 @Mensaje Varchar (20) output
)
AS
BEGIN
 IF (EXISTS(SELECT Nombre FROM Medicamentos WHERE IdMedicamento = @IdMedicamento))
  BEGIN
    UPDATE Medicamentos
	SET Nombre= @Nombre,
	Concentracion= @Concentracion,
	Precio= @Precio,
	Presentacion = @Presentacion,
	Habilitado = @Habilitado,
	Acciones = @Acciones,
	IdFormaFarmaucetica = @IdFormaFarmaucetica
	WHERE  IdMedicamento = @IdMedicamento
	SET @Exito = 1
	SET @Mensaje = 'Se actualizado el medicamento'
  END
  ELSE
   BEGIN
    SET @Exito = 0
	SET @Mensaje = 'El medicamento no existe'
   END
END

insert into FormasFarmauceuticas(Nombre) values('Sólido')
insert into FormasFarmauceuticas(Nombre) values('Liquido')
insert into FormasFarmauceuticas(Nombre) values('semisólido')
insert into FormasFarmauceuticas(Nombre) values('gaseoso')

*/