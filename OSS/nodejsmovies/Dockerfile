# Showcase
# nodejsmovies - no tag

FROM node:8-alpine
MAINTAINER Johannes Sim / Renzo Veldkamp
ENV REFRESHED_AT 2017-07-12

# Create app directory
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Bundle app source
COPY . /usr/src/app/

# Install app dependencies
RUN npm install

EXPOSE 4201
CMD [ "npm", "run", "movies" ]

# build:  docker build --no-cache -t centricms/nodejsmovies .
# run:    docker run --name nodejsmovies -d -p 8090:4201 centricms/nodejsmovies
# delete: docker rm -f nodejsmovies
# push:   docker push centricms/nodejsmovies
# pull:   docker pull centricms/nodejsmovies
# in:     docker exec -it nodejsmovies /bin/sh