'use strict';

const http = require('http');
const socket = require('socket.io');
const server = http.createServer();
const port = 3000;


// var io = socket(server, {
//     pingInterval: 10000,
//     pingTimeout: 5000
// });

const io = require("socket.io")(server, {
    cors: {
      origin: "http://localhost:3000",
      methods: ["GET", "POST"],
      allowEIO3: true,
      //pingInterval: 10000,
      //pingTimeout: 5000
    }
  });

io.use((socket, next) => {
    next();
    // if (socket.handshake.query.token === "UNITY") {
    //     next();
    // } else {
    //     next(new Error("Authentication error"));
    // }
});
var clients = 0;
io.on('connection', socket => {
  console.log('connection');
  clients++;
  console.log(clients)
  io.sockets.emit('broadcast',{ description: clients + ' clients connected!'});

  socket.on('disconnect', function () {
     clients--;
     io.sockets.emit('broadcast',{ description: clients + ' clients connected!'});
  });

//     //console.log(socket)
// //   setTimeout(() => {
//   socket.emit('connection', {date: new Date().getTime(), data: "Hello Unity"})
// //   }, 1000);

// //   socket.on('connection', (data) => {
// //     console.log('hello', data);
// //     socket.emit('connection', {date: new Date().getTime(), data: "Hello Unity"})
// //   });
//   socket.on('hello', (data) => {
//     console.log('hello', data);
//     socket.emit('hello', {date: new Date().getTime(), data: data});
//   });

  socket.on('spin', (data) => {
    console.log('spin');
    //socket.emit('spin', {date: new Date().getTime(), data: data});
    io.sockets.emit('spin', {date: new Date().getTime(), data: data});

  });
  socket.on('listado_estudiantes', (data) => {
    console.log('listado_estudiantes');
    //socket.emit('listado_estudiantes', {date: new Date().getTime(), data: data});
    io.sockets.emit('listado_estudiantes', {date: new Date().getTime(), data: data});
  });

  socket.on('estudiantes_evaluados', (data) => {
    console.log('estudiantes_evaluados');
    io.sockets.emit('estudiantes_evaluados', {date: new Date().getTime(), data: data});
  });
  

//   socket.on('class', (data) => {
//     console.log('class', data);
//     socket.emit('class', {date: new Date().getTime(), data: data});
//   });
});


server.listen(port, () => {
  console.log('listening on *:' + port);
});