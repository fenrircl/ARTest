$(function () {
  });
  const socket = io('http://socketio.integritic.cl:3000', { 
    transports: ['websocket'] ,
    query: {
        "token": "UNITY"
    },         
});
  var DATOS_COLEGIO = {}
  getDatosColegio=async(id="5ad50cfb92efa2e6567d44d0",promocion=2022)=>{
    const config = {
    headers:{
        'Content-Type':  'application/json',
        'API_KEY' : "",
    },
    withCredentials: false,
    };
    const url = "https://api.gateway.integritic.cl/je/getDatosColegio/"+id;  

    return new Promise((resolve)=>{
        axios.get(url, config)
        .then(response=> {
          console.log(response)
          let res = response.data
          console.log(res.inst[0])
          for(let x=0;x<res.inst[0].ALUMNOS.length;x++){
            res.inst[0].ALUMNOS[x].ALUMNOCURSO = res.inst[0].ALUMNOS[x].ALUMNOCURSO.filter(A=>A.PROMOCION === promocion)[0];
            res.inst[0].ALUMNOS[x].NOMBRE_RULETA = res.inst[0].ALUMNOS[x].NOMBRES.split(" ")[0]+" "+res.inst[0].ALUMNOS[x].APELLIDOPATERNO+" "+res.inst[0].ALUMNOS[x].APELLIDOMATERNO
          }               
          DATOS_COLEGIO = res.inst[0]
          resolve(res.inst[0]);

          console.log(res)
        })
        .catch(err=> console.log(err))

    // http.get<any>("https://api.gateway.integritic.cl/je/getDatosColegio/"+id,httpOptions).subscribe(response => {
    //     console.log(response.inst[0])
    //     for(let x=0;x<response.inst[0].ALUMNOS.length;x++){
    //     response.inst[0].ALUMNOS[x].ALUMNOCURSO = response.inst[0].ALUMNOS[x].ALUMNOCURSO.filter(A=>A.PROMOCION === promocion)[0];
    //     response.inst[0].ALUMNOS[x].NOMBRE_RULETA = response.inst[0].ALUMNOS[x].NOMBRES.split(" ")[0]+" "+response.inst[0].ALUMNOS[x].APELLIDOPATERNO+" "+response.inst[0].ALUMNOS[x].APELLIDOMATERNO
    //     }
    //     resolve(response.inst[0]);
    // }, response_err=>{
    //     console.log(response_err.error)
    // });
    });
}

cargarWS=()=>{
  


         socket.on('news', function (data) {
        console.log(data);
        socket.emit('my other event', { my: 'data' });

        socket.on('broadcast',function(data){
            document.body.innerHTML = '';
            document.write(data.description);
        });

    });
    //var socket = io();
    $('form').submit(function(){
      socket.emit('spin', $('#m').val());
      $('#m').val('');
      return false;
    });
    socket.on('estudiantes_evaluados', function(msg){
      actualizar_evaluados(msg);
    });
}
cargar_datos_string=async()=>{
  return new Promise((resolve)=>{
     let coso = JSON.parse(DATOS_COLEGIO_STRING)
      resolve(coso);
    })
    .catch(err=> console.log(err))

};

var cargar_datos=async()=>{
    //DATOS_COLEGIO = await getDatosColegio(); 
    DATOS_COLEGIO = await cargar_datos_string();
    getCursos();
    setTimeout(() => {
        cargar_select();
        setTimeout(() => {
            changeLetra();
        }, 1000);

    }, 1000);
    
}
cargar_datos();
$(document).ready(function() { 
    /* code here */
    
});

$(window).on('load', function() {
    console.log('All assets are loaded')
    console.log(DATOS_COLEGIO   )
    cargarWS();

})

cursos_institucion = [];
letras_curso = [];
CURSO_INSTITUCION = "";
LETRA_CURSO = "";
DATOS_COLEGIO;
ESTUDIANTES_ACTUALES = [];
LISTADO_ESTUDIANTES = []
cargar_select=()=>{
    console.log("cargar_select")
    //return;
    let html_cursos = "";
    let html_letras = "";
    //console.log(DATOS_COLEGIO.CURSOS)
    for (let x = 0; x < cursos_institucion.length; x++) {
        //(const element = array[x];
        html_cursos += `<option  value="${cursos_institucion[x].id}">${cursos_institucion[x].name}</option>`
        
    }
    for (let x = 0; x < letras_curso.length; x++) {
        //(const element = array[x];
        html_letras += `<option  value="${cursos_institucion[x].id}">${letras_curso[x].name}</option>`
        
    }
    $("#cursos_institucion").html(html_cursos)
    $("#letra_curso").html(html_letras)

}

 getCursos=async()=>{
    //DATOS_COLEGIO = await getDatosColegio();
    console.error(DATOS_COLEGIO);
    for(let x=0;x<DATOS_COLEGIO.CURSOS.length;x++){
      cursos_institucion.push({
        id: DATOS_COLEGIO.CURSOS[x]._id,
        name: DATOS_COLEGIO.CURSOS[x].NOMBRE,
      });
    }
    CURSO_INSTITUCION = cursos_institucion[0].id;
    changeCurso();
  }
  
  changeCurso=()=>{
    console.error("curso "+CURSO_INSTITUCION);
    letras_curso = [];
    var curso = DATOS_COLEGIO.CURSOS.filter(A=>A._id === CURSO_INSTITUCION)[0];
    var letras = curso.DETALLE;
    for(let x=0;x<letras.length;x++)letras_curso.push({id: letras[x], name: letras[x]});
    LETRA_CURSO = letras_curso[0].id;

    if(LETRA_CURSO != ""){
      //CARGAR ESTUDIANTES
      //cargar_select();
      if($("#cursos_institucion " ).val())
      CURSO_INSTITUCION = $("#cursos_institucion" ).val();
      changeLetra();
    }
  }

  changeLetra=()=>{

    if(CURSO_INSTITUCION != ""){
      //CARGAR ESTUDIANTES
      //cargar_select();
      //LETRA_CURSO = $("#letra_curso " ).text();
      LETRA_CURSO = $( "#letra_curso option:selected" ).text();
      cargarListado();
    }
    console.error("letra "+LETRA_CURSO);

  }

  cargarListado=()=>{
    console.log("cargarListado")
    $("#listado_estudiantes").empty()
    ESTUDIANTES_ACTUALES = []
    LISTADO_ESTUDIANTES = []
    var estudiantes = this.DATOS_COLEGIO.ALUMNOS.filter(A=>A.ALUMNOCURSO && A.ALUMNOCURSO.ID_CURSO == this.CURSO_INSTITUCION && A.ALUMNOCURSO.DETALLE == this.LETRA_CURSO && A.ALUMNOCURSO.ESTADO=="1");
    ESTUDIANTES_ACTUALES = estudiantes
    let html= "<ul class='list-group'>"
    for(let x=0;x<this.ESTUDIANTES_ACTUALES.length;x++){
        //console.log(this.ESTUDIANTES_ACTUALES[x].ALUMNOCURSO)
        LISTADO_ESTUDIANTES.push({
          index: x,
          NOMBRE: this.ESTUDIANTES_ACTUALES[x].NOMBRE_RULETA,
          MOSTRAR: true,
            LISTA: this.ESTUDIANTES_ACTUALES[x].ALUMNOCURSO.NUMERO_LISTA
        });
    }
    for (let i = 0; i < LISTADO_ESTUDIANTES.length; i++) {
      //console.log(LISTADO_ESTUDIANTES[i].MOSTRAR)
      let checked = `${i<5?"checked":""}`
      html+=`
      <li class="list-group-item"><input  data-id="${i}" lista="${LISTADO_ESTUDIANTES[i].LISTA}" type="checkbox" class=" form-check-input me-1 mr-5" ${checked} style="cursor: pointer;"> <b>${i+1}</b>-. ${LISTADO_ESTUDIANTES[i].NOMBRE}<span id="${LISTADO_ESTUDIANTES[i].LISTA}">
      <span class="badge rounded-pill bg-danger"></span>
      </i>
      </span></li>
      `;
      
    }
     html +=`</ul>`

     $("#listado_estudiantes").html(html)
   
  }
LISTADO_ESTUDIANTES_A_EVALUAR = []
var array_listado = []

enviarListado=()=>{
    LISTADO_ESTUDIANTES_A_EVALUAR=[]
    array_listado=[]
    $("[data-id]").each(function() {
      var lista = $(this).attr("lista")
      //console.log(lista)
      //console.log($(this))
      if(this.checked){
        LISTADO_ESTUDIANTES_A_EVALUAR.push(LISTADO_ESTUDIANTES[lista])
        array_listado.push(lista)
      }
      
      //console.log(this.text + ' ' + this.value);
  });
  console.error(array_listado)
  if(array_listado.length>0){
    console.log("listado_estudiantes")
    socket.emit('listado_estudiantes', array_listado); 
    socket.emit('respuesta_correcta', $('input[name=evaluacion]:checked').val()); 
    
  }
  resetearEvaluados();
}
actualizar_evaluados=(obj)=>{
  console.log(obj)
  let respuesta = $('input[name=evaluacion]:checked').val()==obj.data.split(",")[1]?"success":"danger"
  let html = `<span class="badge rounded-pill bg-${respuesta}">Evaluado ${obj.data}</span>`

  if(obj.data){
    let id = obj.data.split(",")[0]
    console.error(id)
    $("#"+id).html(html)
  }
}
resetearEvaluados=()=>{
  $(".rounded-pill").each(function() {
    $(this).html(null)
  })
}