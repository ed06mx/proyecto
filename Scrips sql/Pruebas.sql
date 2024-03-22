--'9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08' test

declare @Exito bit, @Mensaje  Varchar (20)


exec sp_alta_usuario 'Edgar', now, 'uno','9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08',1,'saludos',
 @Exito output, @Mensaje output

 select @Exito, @Mensaje


 select * from Usuario

 select * from FormasFarmauceuticas

 select * from Medicamentos