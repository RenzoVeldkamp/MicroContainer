var http = require("http");

http.createServer(function(request, response) {
  response.writeHead(200, {"Content-Type": "text/plain"});
  response.write("Hello World Centric");
  response.end();
}).listen(8080);
console.log('Server running at http://localhost:xyxy/');