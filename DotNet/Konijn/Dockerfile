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

EXPOSE 15671 15672

# BUILD: docker image build -t centricms/rabbitmq:latest .

# RUN:  docker container run -d -p 5672:5672 -p 15673:15672 --network dockercompose12231365961002848029_default --hostname konijn --name konijn centricms/rabbitmq:latest
# RUN:   docker container run -d -p 5672:5672 15672:15672  --hostname local-rabbit --name renzo-rabbit centricms/rabbitmq:latest
