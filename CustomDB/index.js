"use strict";
exports.__esModule = true;
var jsonServer = require("json-server");
var dns = require("dns");
var os = require("os");
var server = jsonServer.create();
var router = jsonServer.router('db.json');
var middlewares = jsonServer.defaults();
server.use(middlewares);
server.use(function (req, res, next) {
    console.log(req);
    next();
});
var port = 3000;
server.use(router);
dns.lookup(os.hostname(), function (error, address, deviceFamily) {
    if (error) {
        console.log(error);
        return;
    }
    var listening = server.listen(Number(port), address, function () {
        console.log("Server running at " + address + " on port " + port);
    });
});
