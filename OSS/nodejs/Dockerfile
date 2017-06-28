# Showcase
# NodeJS - no tag

FROM node:8-alpine
MAINTAINER Johannes Sim
ENV REFRESHED_AT 2017-06-28

# Create app directory
RUN mkdir -p /usr/src/app
WORKDIR /usr/src/app

# Install app dependencies
COPY package.json /usr/src/app/
RUN npm install

# Bundle app source
COPY . /usr/src/app

EXPOSE 8080
CMD [ "npm", "start" ]

# build:  docker build -t centricms/nodejs .
# run:    docker run --name nodejs -d -p 8090:8080 centricms/nodejs
# delete: docker rm -f nodejs
# push:   docker push centricms/nodejs
# pull:   docker pull centricms/nodejs
# in:     docker exec -it nodejs bin/sh