# MAINTAINER Renzo Veldkamp
# Centric, november 2018

FROM rabbitmq:3.7-alpine

RUN rabbitmq-plugins enable --offline rabbitmq_management

# extract "rabbitmqadmin" from inside the "rabbitmq_management-X.Y.Z.ez" plugin zipfile
# see https://github.com/docker-library/rabbitmq/issues/207
RUN set -eux; \
	erl -noinput -eval ' \
		{ ok, AdminBin } = zip:foldl(fun(FileInArchive, GetInfo, GetBin, Acc) -> \
			case Acc of \
				"" -> \
					case lists:suffix("/rabbitmqadmin", FileInArchive) of \
						true -> GetBin(); \
						false -> Acc \
					end; \
				_ -> Acc \
			end \
		end, "", init:get_plain_arguments()), \
		io:format("~s", [ AdminBin ]), \
		init:stop(). \
	' -- /plugins/rabbitmq_management-*.ez > /usr/local/bin/rabbitmqadmin; \
	[ -s /usr/local/bin/rabbitmqadmin ]; \
	chmod +x /usr/local/bin/rabbitmqadmin; \
	apk add --no-cache python; \
	rabbitmqadmin --version

EXPOSE 15671 15672 15673

# http://localhost:4000/api/apen geeft alle apen uit de apenhok-container(s):

# BUILD: docker image build -t centricms/rabbitmq:latest .

# NETWORK: docker network create rabbitnetwork

# RUN: docker container run -d -p 15673:15672 --network rabbitnetwork --hostname konijn --name konijn centricms/rabbitmq:latest

# BUILD apenhok (in de solution folder!!): docker image build -t centricms/apenhok:latest -f .\ApenHok\Dockerfile .
# RUN apenhok1: docker container run -d --name apenhok1 --network rabbitnetwork -v C:\data\temp\vol1:/apenvolume centricms/apenhok:latest
# RUN apenhok2: docker container run -d --name apenhok2 --network rabbitnetwork -v C:\data\temp\vol2:/apenvolume centricms/apenhok:latest

# BUILD apencontainer: docker image build -t centricms/apencontainer:latest .
# RUN apencontainer: docker container run -d -p 4000:80 --name apencontainer --network rabbitnetwork centricms/apencontainer:latest

# NuGet server container met (2) volumes:
# docker container run --detach=true --publish 5000:80 --env NUGET_API_KEY=Centric2018 --volume C:\Data\GitRepos\MicroContainer\DotNet\NuGetDatabase:/var/www/db --volume C:\Data\GitRepos\MicroContainer\DotNet\NuGetPackages:/var/www/packagefiles --name nuget-server --hostname nugetserver sunside/simple-nuget-server