# SF Movies Challenge

## Descripci�n
Aplicaci�n web que muestra las locaciones de pel�culas filmadas en San Francisco, implementada con una API en .NET 9 (Clean Architecture) y un frontend en Vue.js.

## Estructura del Proyecto
```
SFMovies/
??? SFMovies.Api/           # API Backend (.NET 9)
??? SFMovies.Application/   # Capa de aplicaci�n
??? SFMovies.Domain/        # Capa de dominio
??? SFMovies.Infrastructure/# Capa de infraestructura
??? SFMovies.Tests/         # Tests unitarios e integraci�n
??? docker-compose.yml      # Configuraci�n Docker Compose
??? README.md

# El frontend Vue.js debe crearse en un directorio aparte, fuera de esta soluci�n:
# Ejemplo:
# ??? sfmovies-frontend/    # Frontend (Vue.js, proyecto independiente)
```

## Requisitos Previos
- Docker Desktop
- Git

## Configuraci�n y Ejecuci�n

### 1. Clonar el Repositorio Backend
```bash
git clone <url-repositorio-backend>
cd SFMovies
```

### 2. Crear el Proyecto Frontend Independiente
En una carpeta aparte (no dentro de la soluci�n .NET):
```bash
npm create vue@latest sfmovies-frontend
cd sfmovies-frontend
npm install axios vuetify leaflet @mdi/font pinia vue-router
```

Configura el archivo `.env` del frontend:
```
VITE_API_URL=http://localhost:5001/api
```

Agrega el Dockerfile y nginx.conf seg�n las instrucciones del backend.

### 3. Ejecutar la Aplicaci�n (Backend + Frontend)
Coloca ambos proyectos (backend y frontend) en carpetas separadas, al mismo nivel, y usa el `docker-compose.yml` del backend para levantar ambos servicios:
```bash
docker-compose up --build
```

### 4. Acceder a la Aplicaci�n
- Frontend: http://localhost:8080
- API Swagger: http://localhost:5001/swagger

## Tecnolog�as Utilizadas

### Backend
- .NET 9
- Clean Architecture
- JWT Authentication
- MediatR (CQRS)
- Serilog
- Entity Framework Core
- xUnit (Testing)

### Frontend (independiente)
- Vue.js 3
- Vite
- Vuetify
- Axios
- Vue Router
- Pinia (State Management)
- Leaflet (Mapas)

## Funcionalidades Principales
- Visualizaci�n de locaciones de pel�culas en mapa
- B�squeda por t�tulo de pel�cula
- Filtrado por a�o
- Autenticaci�n de usuarios (JWT)
- Roles de usuario (Admin/Viewer)

## Estructura de la API
- `GET /api/movielocations` - Obtener todas las locaciones
- `GET /api/movielocations/{id}` - Obtener locaci�n espec�fica
- `POST /api/auth/login` - Autenticaci�n de usuarios
- `POST /api/movielocations` - Agregar nueva locaci�n (requiere rol Admin)

## Tests
Los tests del backend se pueden ejecutar con:
```bash
dotnet test
```

---

**Nota:** El frontend Vue.js es un proyecto independiente y debe crearse y mantenerse fuera de la soluci�n .NET. El archivo `docker-compose.yml` permite levantar ambos servicios juntos para facilitar la integraci�n y despliegue en cualquier entorno con Docker.