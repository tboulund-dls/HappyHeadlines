FROM node:latest as build
WORKDIR /app
COPY package*.json /app/
RUN npm install -g ionic
RUN npm install
COPY . /app
RUN npm run build --prod
FROM nginx:alpine
COPY --from=build /app/www/ /usr/share/nginx/html/www
COPY /nginx.conf  /etc/nginx/conf.d/default.conf

EXPOSE 4200
