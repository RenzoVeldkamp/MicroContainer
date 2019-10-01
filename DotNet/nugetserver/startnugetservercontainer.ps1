# run container:
# docker run --detach=true --publish 5000:80 --env NUGET_API_KEY=centricms --env BASE_URL=/nuget --volume ${PWD}/nuget/database:/var/www/db --volume ${PWD}/nuget/packages:/var/www/packagefiles --name nuget-server sunside/simple-nuget-server

# push package:
# dotnet nuget push .\DierenHok.Communication\DierenHok.Communication.1.0.0.nupkg -s http://localhost:5000 --api-key centricms

