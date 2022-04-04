SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Vasti Gabriela Flores
-- Create date: 04 Abril 2022
-- Description:	Busca una coincidencia en la tabla de carros
-- =============================================
CREATE PROCEDURE sp_search_car
	-- Add the parameters for the stored procedure here
	@search VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT id, concat(id, ' - ' , car_make, ' - ' , car_model, ' - ' , car_model_year)[description]
	, car_make, car_model, car_model_year
	FROM cars WHERE CONCAT(id, ' - ' , car_make, ' - ' , car_model, ' - ' , car_model_year) LIKE '%' + @search + '%';
END
GO