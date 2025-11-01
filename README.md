# Clima Medellín - Juan Pablo Soto

Aplicación web en ASP.NET Core Razor Pages (.NET 8) que muestra el clima en Medellín y alrededores con animaciones y tarjetas. Diseñada como entrega de trabajo práctico.

Contenido del repositorio

* Dockerfile
* Código fuente (WebApplication1/)
* wwwroot/ con CSS y JS para animaciones
* .env.example con la variable OPENWEATHER\_API\_KEY=YOUR\_KEY
* DEPLOYMENT\_AND\_SUBMISSION.txt con el detalle del proceso
* screenshots/ (añadir hasta 4 imágenes)

Requisitos

* Docker instalado
* (Opcional) Docker Compose
* API key de OpenWeatherMap

Cómo ejecutar

1. Crear archivo .env en la raíz del proyecto con tu clave:

   OPENWEATHER\_API\_KEY=TU\_API\_KEY

2. Construir y ejecutar con Docker (sin compose):

   sudo docker build -t webapp:1.0 .
   sudo docker run -d --name webapp -p 80:80 --env-file .env webapp:1.0

   o con Docker Compose (si tienes docker-compose.yml):

   sudo docker compose up --build -d

3. Verificar que esté corriendo:

   docker ps
   docker logs -f webapp
   curl -I http://localhost:80

   Publicar la imagen (opcional)

* Docker Hub:
  docker login -u TU\_USUARIO
  docker tag webapp:1.0 TU\_USUARIO/mi-proyecto:latest
  docker push TU\_USUARIO/mi-proyecto:latest
* GitHub Container Registry (GHCR):
  echo PAT | docker login ghcr.io -u TU\_USUARIO --password-stdin
  docker tag webapp:1.0 ghcr.io/TU\_USUARIO/mi-proyecto:v1
  docker push ghcr.io/TU\_USUARIO/mi-proyecto:v1

  Notas

  

