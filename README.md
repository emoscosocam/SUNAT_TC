# SUNAT_TC
Proyecto de SSIS para extraer el Tipo de Cambio de la SUNAT y grabarlo en una tabla

Hecho con Visual Studio CE 2015 y SQL Server 2016 DE

* El paquete tiene un ScriptComponent, el cual se conecta a la página de la SUNAT.
* El código se puede ver en ScriptComponent.vb y alternativament en ScriptComponent.cs
* No se usan referencias a librerías de terceros.

* Adicionalmente se incluyen un proyecto Windows Forms en VB y C# para probar el código de carga.

Nota: La SUNAT cambió el link para extraer el tipo de cambio. Ahora es: https://e-consulta.sunat.gob.pe/cl-at-ittipcam/tcS01Alias

Setup:
- La base de datos de prueba se debe de llamar Test_DB
- Las tablas STG_SUNAT_TC y M_SUNAT_TC se pueden crear con Script_Crear_Tablas.sql
- Luego se debe de ejecutar el script Proc_STG_SUNAT_TC.sql para crear el stored procedure.
- La cadena de conexión para la base de datos se puede setear en el archivo de configuración Inicializacion.dtsConfig
- El archivo Importar_SUNAT_TC.bat puede ser usado para ejecutar el paquete desde la línea de comando.

--> Una vez ejecutado el paquete, el tipo de cambio extraido se encontrará en la tabla M_SUNAT_TC

Reconocimiento: 
   Me basé en código C# de Afu Tse Mundaca como figura en su blog:
      http://r3xet.blogspot.pe/2013/12/obtener-el-tipo-de-cambio-de-sunat-del.html
  
  Muchas gracias Afu por tu aporte!


Programación (scheduling): 
   Está pensado que el package se ejecute todos los días a las 8:30am; 
   pero recomiendo que también se ejecute a las 9am en caso hubiera un atraso en actualizarse el TC.


