# MongoDbApp
﻿Actividad 1
Diseño y operaciones CRUD en Bases de datos NoSQLTarea

Actividad 2 
 Consultas en Bases de datos NoSQL 
	








 
Ingeniería de Software. 
Bases De Datos Avanzadas
Mary Luz Rubiano Acosta 








Mauricio Bello Cogollo
Jhonny Alejandro Sanchez Quintana



Enero 16  2022. 
Tabla de Contenidos

Planteamiento	3
Caso Práctico	4
Algunos Comandos utilizados	4
Requerimientos	4
Bibliografía	6

 
Planteamiento
Desarrollar el modelo de una base de datos MongoDb que permita la gestión de los participantes a un torneo deportivo, (deportistas, entrenadores, árbitros, encuentros deportivos, resultados y tabla de posiciones). Ustedes escogen el tipo de evento deportivo que deseen trabajar, el cual debe ser real y tener disponible los resultados y detalles del torneo deportivo.
Subir la actividad en el enlace que corresponde para la entrega de la tarea, indicando el link del repositorio Git y del video.

 
Caso Práctico
Se instalas el motor de basase de datos  mongoDB

 

Algunos Comandos utilizados
show dbs:  lista las bases de datos existentes
db: obtiene el nombre de la base de datos actual
use: crea una nueva base de datos: use TorneoDeportivo
show collections: obtiene todos las collections “tablas”
db.dropDatabase() borra la base de datos actual

db.Deportistas.insert({nombre:"Mauricio",numero:1,posision:"DC",documento:"A1234567", fechaTex:"2022-01-16",idEquipo:"1234567890987654"})

db.createCollection("Arbitros")
db.Arbitros.insert({nombre:"Jhonny",numero:1,documento:"A1234567"})

db.createCollection("Entrenadores")
db.Entrenadores.insert({nombre:"Juan",documento:"A1234567",fechaTex:"2022-01-16",idEquipo: "1234567890987654"})

db.createCollection("Equipos")
db.Equipos.insert({nombreEquipo:"Colombia",fechaTex:"2022-01-16"})

db.createCollection("Temporadas")
db.Temporadas.insert({temporada:"Ibero 2022",fechaTex:"2022-01-16",fechaInicio:"2022-01-16", fechaFin:"2022-05-16", idEquipo: "1234567890987654"})

db.EncuentrosDeportivos.find({_id:"61e313e89746b6c6b700ad3c"})

Requerimientos  
Evento deportivo de futbol:  
consiste la parametrización de un torneo de futbol así:  se registras los equipos con su respectivo CRUD,( "Crear, Leer, Actualizar y Borrar"), gestión de los entrenadores de un equipo, los jugadores de un equipo, las temporadas o torneos, dando lugar a los diferentes eventos o encuentros deportivos donde interactuaran los jugadores por equipo y los árbitros por cada evento.
Finalmente, al ejecutar un evento se tendrá los resultados por equipo y por jugador.
 


Bibliografía

Sarasa, A. (2016). Introducción a las bases de datos NoSQL usando MongoDB. Editorial UOC.
			
