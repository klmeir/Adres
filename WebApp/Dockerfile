# Paso 1: Utiliza una imagen base con Node.js para construir la aplicación Angular
FROM node:20.15.1 as build

# Establece el directorio de trabajo en el contenedor
WORKDIR /app

# Copia el archivo `package.json` y `package-lock.json` (si está disponible)
#COPY package.json ./
COPY ["package.json", "./"]

# Instala las dependencias del proyecto
RUN npm install

# Copia los archivos y carpetas restantes al directorio de trabajo en el contenedor
COPY . .

# Construye la aplicación para producción
RUN npm run build -- --configuration=production

# Paso 2: Utiliza una imagen base de Nginx para servir la aplicación
FROM nginx:alpine

# Copia los archivos de construcción desde el paso anterior al directorio de Nginx para servir los archivos estáticos
COPY --from=build /app/dist/web-app/browser/* /usr/share/nginx/html/

# Expone el puerto 80 para acceder a la aplicación
EXPOSE 80

# Copiar la configuración personalizada de NGINX
COPY nginx.conf /etc/nginx/nginx.conf

# Cuando el contenedor inicia, ejecuta Nginx y mantiene el proceso en primer plano
CMD ["nginx", "-g", "daemon off;"]
