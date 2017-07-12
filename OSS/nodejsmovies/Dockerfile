# Showcase
# nodejsmovies - no tag

FROM node:8-alpine
MAINTAINER Johannes Sim / Renzo Veldkamp
ENV REFRESHED_AT 2017-07-12

# Create app directory
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Install app dependencies
COPY package.json /usr/src/app/

# RUN npm install
CMD [ "npm", "install" ]

#COPY node_modules/* /usr/src/app/node_modules/

# Bundle app source
COPY . /usr/src/app/

RUN ls -lastr /usr/src/app/

EXPOSE 8080
CMD [ "npm", "movies" ]

# build:  docker build -t centricms/nodejsmovies .
# run:    docker run --name nodejsmovies -d -p 8090:4201 centricms/nodejsmovies
# delete: docker rm -f nodejsmovies
# push:   docker push centricms/nodejsmovies
# pull:   docker pull centricms/nodejsmovies
# in:     docker exec -it nodejsmovies /bin/sh