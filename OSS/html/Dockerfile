# Showcase OSS
# OSS - no tag

FROM nginx:stable-alpine
LABEL author="Johannes Sim / Renzo Veldkamp"
ENV REFRESHED_AT 2017-09-01

COPY  index.html *.png /usr/share/nginx/html/

# COPY  nginx.conf /etc/nginx/nginx.conf

# build:  docker build -t centricms/html .
# run:    docker run --name html -d -p 4200:80 centricms/html
# run centric config: docker run --name html -v C:/Data/GitRepos/MicroContainer/OSS/html/conf:/etc/nginx/conf.d/:ro -d -p 4200:80 centricms/html
# delete: docker rm -f  centricms/html
# push:   docker push centricms/html
# pull:   docker pull centricms/html
# in:     docker exec -it centricms/html /bin/sh