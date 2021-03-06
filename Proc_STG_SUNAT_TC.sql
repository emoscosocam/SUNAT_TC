USE [Test_DB]
GO
/****** Object:  StoredProcedure [dbo].[Proc_STG_SUNAT_TC]    Script Date: 31/03/2017 5:10:32 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[Proc_STG_SUNAT_TC]

as

/*
	Escrito por Ernesto Moscoso Cam
	Octubre 2016

	El propósito de este stored procedure es poblar la tabla M_SUNAT_TC 
	desde la tabla STG_SUNAT_TC, de tal manera que los dias sin dato tienen
	el TC del dia anterior.

	La tabla STG_SUNAT_TC tiene filas únicamente por cada Fecha donde sí hay un TC,
	pero en la tabla M_SUNAT_TC no hay fechas faltantes. Tiene filas ya sean con un valor de TC real o con un valor estimado.

*/

declare @UltimoDiaConDatos date -- Ultimo día en M_SUNAT_TC donde Estimado = 0
declare @NumDiasAdelantados tinyint = 5 -- Se van a estimar hasta 5 días en el futuro
declare @UltimoDiaAdelantado as date -- DateAdd(day, @NumDiasAdelantados, getdate())

-- Una tabla auxiliar
declare @TC table (
	Fecha date,
	Compra money,
	Venta  money
)

-- Se obtiene la última fecha con un valor real
select	@UltimoDiaConDatos = max(Fecha) from M_SUNAT_TC where Estimado = 0

if @UltimoDiaConDatos is null -- esto se da cuando la tabla está vacía
begin
	select	
		@UltimoDiaConDatos = min(Fecha) -- Se toma la mínima fecha de lo que esté en la tabla Stage
	from STG_SUNAT_TC                   -- Se supone que este procedure se llama después de poblar la tabla STG_SUNAT_TC
	

	insert into M_SUNAT_TC -- Se inserta la única fila desde la tabla Stage
	select
		tc.Fecha,
		tc.Compra,
		tc.Venta,
		0 as Estimado
	from STG_SUNAT_TC as tc
	where
		tc.Fecha = @UltimoDiaConDatos
end
else
begin
	delete M_SUNAT_TC where Fecha > @UltimoDiaConDatos -- Se eliminan todos los días después de @UltimoDiaConDatos,
	                                                   -- es decir, todos los que tienen Estimado = 1 después de esa fecha
end


set @UltimoDiaAdelantado = DateAdd(day, @NumDiasAdelantados, getdate()); 


-- Una lista de fechas
declare @CalendarioAux table(
	Fecha date
)

declare @FecAux date = @UltimoDiaConDatos

--  @CalendarioAux contendrá todas las fechas desde @UltimoDiaConDatos + 1 hasta @UltimoDiaAdelantado
while @FecAux <= @UltimoDiaAdelantado
begin
	set @FecAux =DateAdd(day, 1, @FecAux) -- Se incremente la fecha en un día
	insert into @CalendarioAux values (@FecAux)	-- Se agrega la nueva fecha
end


-- la tabla @TC contendrá todos los días desde la fecha @UltimoDiaConDatos hasta la fecha @UltimoDiaAdelantado
insert into @TC
select
	tc.Fecha,
	tc.Compra,
	tc.Venta
from M_SUNAT_TC as tc
where tc.Fecha = @UltimoDiaConDatos -- Entonces es fila contiene un valor real, no estimado

union all

select
	cd.Fecha,
	tc.Compra, -- Inicialmente este valor puede ser Nulo
	tc.Venta   -- Inicialmente este valor puede ser Nulo
from @CalendarioAux as cd
left  join STG_SUNAT_TC as tc on tc.Fecha = cd.Fecha -- Se usa Left Join porque no necesariamente todas las fechas tendrán un TC	
where
	cd.Fecha > @UltimoDiaConDatos and  cd.Fecha <= @UltimoDiaAdelantado;
	



-- Se usa un CTE para aplicar recursividad
with cte as
(
	select -- La primera fila (que tiene un valor real)...
		x.Fecha,
		x.Compra, 
		x.Venta,
		0 as Estimado -- Es valor real
	from @TC as x
	where
		x.Fecha = @UltimoDiaConDatos

	union all

	select -- Aquí se aplica la recursividad
		x.Fecha,
		isnull(x.Compra, z.Compra) as Compra,  -- x se refiere al día actual, z se refiere al día anterior...
		isnull(x.Venta, z.Venta) as Venta,     -- "Si el TC para esta fecha es nulo, entonces poner el TC del día anterior"
		iif(x.Venta is null, 1, 0) as Estimado -- "Si el TC para esta fecha es nulo, entonces Estimado = 1
	from @TC as x
	inner join cte as z on z.Fecha = DATEADD(day, -1, x.Fecha) -- Se hace un join con el mismo cte pero con un offset de un día
	                                                         
)
insert into M_SUNAT_TC -- Finalmente se insertan las filas trabajadas
	select
		Fecha,
		Compra, 
		Venta,
		Estimado -- Si Estimado = 0, entonces el valor del TC es el del día anterior
	from cte
	where
		cte.Fecha > @UltimoDiaConDatos and  cte.Fecha <= @UltimoDiaAdelantado;


