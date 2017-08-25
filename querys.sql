

CREATE TABLE [dbo].[tb_local](
	[codLocal] [int] identity,
	[nombreLocal] [nvarchar](50) NULL,
	[fechaAperturaLocal] [date] NULL,
	[responsableLocal] [nvarchar](50) NULL,
	[distritoLocal] [nvarchar](50) NULL,
	[direccionLocal] [nvarchar](50) NULL,
	[latitudLocal] [nvarchar](50) NULL,
	[longitudLocal] [nvarchar](50) NULL,
 CONSTRAINT [PK_tb_local] PRIMARY KEY CLUSTERED 
(
	[codLocal] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

insert into tb_local (nombreLocal,fechaAperturaLocal,responsableLocal,distritoLocal,direccionLocal,latitudLocal,longitudLocal) values('Local A',GETDATE(),'Mulder','Lima','Los olvidados de dios','-11.507337257137694','-76.13296882656255')
insert into tb_local (nombreLocal,fechaAperturaLocal,responsableLocal,distritoLocal,direccionLocal,latitudLocal,longitudLocal) values('Local B',GETDATE(),'Alan','xxx','xxxx','-8.630304229722206','-77.79190437343755')
insert into tb_local (nombreLocal,fechaAperturaLocal,responsableLocal,distritoLocal,direccionLocal,latitudLocal,longitudLocal) values('Local C',GETDATE(),'Ollanta','xxx','ccccc','-8.075947714971415','-74.67178718593755')
insert into tb_local (nombreLocal,fechaAperturaLocal,responsableLocal,distritoLocal,direccionLocal,latitudLocal,longitudLocal) values('Local D',GETDATE(),'Fujimory','xxx','vvvv','-10.072028138697476','-72.50748054531255')
insert into tb_local (nombreLocal,fechaAperturaLocal,responsableLocal,distritoLocal,direccionLocal,latitudLocal,longitudLocal) values('Local E',GETDATE(),'Veronica','xxx','bbbbb','-11.604209978629505','-70.10147468593755')


GO
CREATE TABLE [dbo].[tb_encuesta](
	[codEncuesta] [int] identity,
	[nombreEncuesta] [nvarchar](50) NULL,
	[fechaInicioEncuesta] [date] NULL,
	[fechaFinEncuesta] [date] NULL,
	[cantidadClientesEncuestados] [int] NULL,
	[codLocal] [int] NULL,
 CONSTRAINT [PK_tb_encuesta] PRIMARY KEY CLUSTERED 
(
	[codEncuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

insert into tb_encuesta (nombreEncuesta,fechaInicioEncuesta,fechaFinEncuesta,cantidadClientesEncuestados,codLocal) values ('Encuesta 1',GETDATE(),GETDATE(),5,1)
insert into tb_encuesta (nombreEncuesta,fechaInicioEncuesta,fechaFinEncuesta,cantidadClientesEncuestados,codLocal) values ('Encuesta 2',GETDATE(),GETDATE(),6,2)
insert into tb_encuesta (nombreEncuesta,fechaInicioEncuesta,fechaFinEncuesta,cantidadClientesEncuestados,codLocal) values ('Encuesta 3',GETDATE(),GETDATE(),8,3)
insert into tb_encuesta (nombreEncuesta,fechaInicioEncuesta,fechaFinEncuesta,cantidadClientesEncuestados,codLocal) values ('Encuesta 4',GETDATE(),GETDATE(),7,4)
insert into tb_encuesta (nombreEncuesta,fechaInicioEncuesta,fechaFinEncuesta,cantidadClientesEncuestados,codLocal) values ('Encuesta 5',GETDATE(),GETDATE(),5,5)

go
CREATE TABLE [dbo].[tb_medioComunicacion](
	[codMedioComunicacion] [int] identity,
	[nombreMedioComunicacion] [nvarchar](50) NULL,
	[costoUnitarioMedioComunicacion] [decimal](10, 5) NULL
 CONSTRAINT [PK_tb_medioComunicacion] PRIMARY KEY CLUSTERED 
(
	[codMedioComunicacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

insert into tb_medioComunicacion (nombreMedioComunicacion,costoUnitarioMedioComunicacion) values ('Facebook',20.00)
insert into tb_medioComunicacion (nombreMedioComunicacion,costoUnitarioMedioComunicacion) values ('twitter',15.00)
insert into tb_medioComunicacion (nombreMedioComunicacion,costoUnitarioMedioComunicacion) values ('instagram',13.00)
insert into tb_medioComunicacion (nombreMedioComunicacion,costoUnitarioMedioComunicacion) values ('gmail',12.00)
insert into tb_medioComunicacion (nombreMedioComunicacion,costoUnitarioMedioComunicacion) values ('outlook',11.00)
insert into tb_medioComunicacion (nombreMedioComunicacion,costoUnitarioMedioComunicacion) values ('whatsapp',18.00)

GO
CREATE TABLE [dbo].[tb_resultadoEncuesta](
	[codResultadoEncuesta] [int] identity,
	[puntajeCaracteristicaCombo] [int] NULL,
	[codEncuesta] [int] NULL,
	[codMedioComunicacion] [int] null,
 CONSTRAINT [PK_tb_resultadoEncuesta] PRIMARY KEY CLUSTERED 
(
	[codResultadoEncuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (50,1,1)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (70,1,2)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (10,1,3)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (20,1,4)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (60,1,5)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (60,1,6)

insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (20,2,1)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (30,2,2)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (40,2,3)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (50,2,4)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (60,2,5)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (60,2,6)

insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (50,3,1)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (10,3,2)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (10,3,3)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (20,3,4)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (60,3,5)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (10,3,6)

insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (50,4,1)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (30,4,2)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (10,4,3)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (20,4,4)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (30,4,5)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (30,4,6)

insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (50,5,1)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (80,5,2)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (10,5,3)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (20,5,4)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (90,5,5)
insert into tb_resultadoEncuesta (puntajeCaracteristicaCombo,codEncuesta,codMedioComunicacion) values (10,5,6)


go

alter procedure sp_Lista_Medios_X_Locales
as
begin

	select R.puntajeCaracteristicaCombo,	   
		   (select (R.puntajeCaracteristicaCombo*100)/sum(R1.puntajeCaracteristicaCombo) 
			from tb_resultadoEncuesta R1
			where R1.codMedioComunicacion  = R.codMedioComunicacion
			group by R1.codMedioComunicacion) as 'Porcentaje',
		   R.codEncuesta,
		   E.nombreEncuesta,
		   R.codMedioComunicacion,
		   MC.nombreMedioComunicacion,
		   MC.costoUnitarioMedioComunicacion,
		   L.codLocal,
		   L.nombreLocal,
		   L.latitudLocal,
		   L.longitudLocal 
	from tb_resultadoEncuesta R
	inner join tb_encuesta E on R.codEncuesta= E.codEncuesta
	inner join tb_medioComunicacion MC on MC.codMedioComunicacion= R.codMedioComunicacion
	inner join tb_local L on L.codLocal=E.codLocal
	
end


go
alter procedure sp_ListaMedioComunicaion
as
begin
	select 
	sum(R1.puntajeCaracteristicaCombo) sumatoria,
	COUNT(R1.puntajeCaracteristicaCombo) cantidad,
	((sum(R1.puntajeCaracteristicaCombo)/COUNT(R1.puntajeCaracteristicaCombo))*100)/sum(R1.puntajeCaracteristicaCombo) promedio ,
	R1.codMedioComunicacion,
	MC.nombreMedioComunicacion,
	MC.costoUnitarioMedioComunicacion
	from tb_resultadoEncuesta R1
	inner join tb_medioComunicacion MC on MC.codMedioComunicacion= R1.codMedioComunicacion
	group by R1.codMedioComunicacion,MC.nombreMedioComunicacion,MC.costoUnitarioMedioComunicacion
end


select * from tb_medioComunicacion




