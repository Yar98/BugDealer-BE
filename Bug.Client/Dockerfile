FROM node:12.0 as build-step
WORKDIR /app
COPY package.json /app
RUN npm install -g npm@latest -g
RUN npm install --production

FROM nginx:1.17.1-alpine
COPY --from=build-step /app /usr/share/nginx/html
