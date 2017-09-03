# Showcase Static Website
# with tag

LABEL MAINTAINER="Johannes Sim / Remco Veldkamp"
ENV REFRESHED_AT 2017-09-03

FROM nginx:stable-alpine
COPY index.html *.png /usr/share/nginx/html/


# build:  docker image build -t centricms/staticws:latest .

# push:   docker push centricms/staticws:latest
# pull:   docker pull centricms/staticws:latest

# run:    docker container run --name staticws -d -p 4200:80 centricms/staticws:latest
# Azure:  docker container run --name staticws -d -p 8901:80 centricms/staticws:latest

# delete: docker container rm -f  centricms/staticws:latest
# in:     docker container exec -it centricms/staticws:latest /bin/sh