SGTB - Sistema de Gestión de Transporte de Buses
Proyecto de software desarrollado para la gestion integral de un sistema de transporte, optimizando el control de rutas, autobuses, conductores, viajes y emision de boletos.

Caracteristicas Principales
Gestion de Buses: Control de unidades, numeros de placa y modelos.

Gestion de Conductores: Registro y administracion de personal al volante.

Gestion de Rutas: Configuracion de trayectos, origenes y destinos.

Control de Viajes: Programacion de salidas asignando ruta, bus, conductor, fecha, hora, precio y asientos disponibles.

Emision de Boletos: Venta y asociacion directa con los IDs de viajes activos.

Modulo de Consultas: Busqueda y filtrado dinamico de informacion.

Tecnologias Utilizadas
Backend / Frontend: ASP.NET Core MVC (C#)

Base de Datos: Microsoft SQL Server (SSMS)

Control de Versiones: Git y GitHub

Documentacion y Reportes: WPS Office / Microsoft Word

Base de Datos y Configuracion (Backup / Restore)
El sistema utiliza Microsoft SQL Server. Para restaurar la base de datos a partir del respaldo completo incluido en el proyecto, se pueden ejecutar las siguientes consultas SQL en SSMS:



BACKUP DATABASE [SistemaTransporteDB]
TO DISK = 'C:\RespaldoSGTB\SistemaTransporteDB_Backup.bak'
WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME = 'Respaldo Completo SGTB';


RESTORE DATABASE [SistemaTransporteDB]
FROM DISK = 'C:\RespaldoSGTB\SistemaTransporteDB_Backup.bak'
WITH REPLACE;
Estructura del Repositorio
Controllers/: Logica de controlacion para Rutas, Buses, Conductores, Viajes y Boletos.

Views/: Interfaces de usuario en Razor (.cshtml).

Models/: Modelos de datos y conexion con la base de datos.

Documentacion/: Archivos de reporte de pruebas y validacion (V&V).

Autor

EMANUEL JOSUÉ VELÁSQUEZ LÓPEZ.

Desarrollado para la defensa y entrega del proyecto academico.
