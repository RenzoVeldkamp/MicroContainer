# Showcase
# Static WebSite - no tag

FROM nginx:stable-alpine
MAINTAINER Johannes Sim
ENV REFRESHED_AT 2017-06-05
COPY  index.html *.png /usr/share/nginx/html/


# build:  docker build -t sim007/staticws .
# run:    docker run --name staticws -d -p 8090:80 sim007/staticws
# delete: docker rm -f staticws
# push:   docker push sim007/staticws
# in:     docker exec -it staticws /bin/sh