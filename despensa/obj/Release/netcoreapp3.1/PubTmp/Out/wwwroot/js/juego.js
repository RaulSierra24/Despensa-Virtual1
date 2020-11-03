/*Vamos a leer el teclado*/

/*Está funcion vamos a reconocer el teclado*/

document.addEventListener('keydown', function(evento){

    if(evento.keyCode==32){/*Cuando el usuario presiona un espacio haga esto*/
  
      /*Que escriba por pantalla salta*/
  
      if(trex.saltando==false){
        if(nivel.muerto==false){
          console.log("Salta");
          saltar();
        }else{
          console.log("else");
          nivel.muerto=false;
            nivel.velocidad=9;
            aux=1;
            contador=0;
            cactus.x=ancho+100;
            nivel.marcador=0;
        }

  
      }
  
    }
  
  });
  
  
  
  
  
  /*Vamos a cargar las imagenes*/
  
  var imgRex, imgNube, imgCactus, imgSuelo;
  
  function cargaImagenes(){
  
    imgRex= new Image();/*Vamos a decirle que laimagen del rex sea igual a una nueva imagen*/
    
    imgRex2= new Image();
    imgRex3= new Image();
    

    imgNube= new Image();
  
    imgCactus= new Image();
  
    imgSuelo= new Image();
  
  
  
      imgRex.src = "/img/rex.png";/*Indicamos la ruta del objeto*/
      imgRex2.src ="/img/rex2.png";
      imgRex3.src ="/img/rex3.png";
      imgNube.src ="/img/rex4.png";
  
      imgCactus.src ="/img/cactus.png";
  
      imgSuelo.src ="/img/suelo3.png";
  
  }
  
  var ancho = 700;
  
  var alto= 300;
  var suelo=210;
  
  var canvas,ctx;

  var nivel = {velocidad:9,puntuacion:0, muerto:false,marcador:0,record:0};
  
  /*Vamoa a inicializar*/
  
  function inicializa(){
    nivel.muerto=true;
            nivel.velocidad=0;
            aux=52;
            contador==31;
            
  
    canvas=document.getElementById('canvas');/*Esta variable canvas vamos a decirle que sea igual al elemento del html llamado canvas*/
  
    ctx= canvas.getContext('2d');/*El contextto te dice como funciona la pizarra en 2d*/
  
    cargaImagenes();
  
  }
  
  
  
  /*Vamos a borrar la pizarra*/
  
  function borrarCanvas(){/*Para borrar el canvas lo más sencillo es borrarle pa altura y anchura*/
  
    canvas.width= ancho;
  
    canvas.height= alto;
  
  }
  
  
  
  var trex = {y: suelo, vy:0,gravedad:2, salto:28, vymax:9, saltando: false};/*Pos 1 en donde se encuentra el trex, Pos 2, velocidad vertical cuanto sube los pixeles, 
  
  una gravedad de 2 y un salto de 20 pixeles y cada vez que pase un fotograma le restamos 2*/
  
  /*Vamos hacer la función que dibuja la imagen del rex*/


  
  function dibujaRex(){
  
    ctx.drawImage(imgRex,0,0,93,102,100,trex.y,75,75);/*Para dibujar la imagen tenemos que cargar el contexto y tenemos que utilizar el atributo ctx.drawImage();*/
  
  }
  
  function dibujaRex2(){
  
    ctx.drawImage(imgRex2,0,0,93,102,100,trex.y,75,75);/*Para dibujar la imagen tenemos que cargar el contexto y tenemos que utilizar el atributo ctx.drawImage();*/
  
  }
  function dibujaRex3(){
  
    ctx.drawImage(imgRex3,0,0,93,102,100,trex.y,75,75);/*Para dibujar la imagen tenemos que cargar el contexto y tenemos que utilizar el atributo ctx.drawImage();*/
  
  }
  function dibujaRex4(){
  
    ctx.drawImage(imgNube,0,0,93,102,100,trex.y,75,75);/*Para dibujar la imagen tenemos que cargar el contexto y tenemos que utilizar el atributo ctx.drawImage();*/
  
  }


  var cactus ={x: ancho+100, y:suelo};
  
  function dibujarcactus(){
    ctx.drawImage(imgCactus,0,0,52,101,cactus.x,cactus.y,40,75);
  }
  
  
  function logicacactus(){
    if(cactus.x < -100){
        cactus.x = ancho+100;
    }else{
      cactus.x -= nivel.velocidad;
    }
  }


  var suelog ={x: 0, y:suelo+28};

  function dibujarsuelo(){
    ctx.drawImage(imgSuelo,suelog.x,0,700,28,0,suelog.y,700,30);
  }
  function logicasuelo(){
    if(suelog.x > 700){
        suelog.x = 0;
    }else{
      suelog.x += nivel.velocidad;
    }
  }
  

  /*Vamos hacer la funcion saltar*/
  
  function saltar(){
  
    trex.saltando=true;/*Tenemos que activarlo*/
  
    trex.vy=trex.salto;/*La velocidad vertical es igual a 28px*/
  
  }
  
  /*Vamos a hacer una funcion del comportamiento de la gravedad*/
  
  function gravedad(){/*Consiste en el movimiento de saltar */
  
    if(trex.saltando==true){/*Comprobar si el dinosaurio está saltando, se refiere que está en el aire, sino está en el aire no hay que 
  
      aplicarle la gravedad*/
  
      /*Entre más valla restanto el dibujo trex va ir subiendo para arriba*/
  
      if(trex.y-trex.vy-trex.gravedad>suelo){/*Si trex es mayor a 250 haga esto*/
  
          trex.saltando=false;/*Así deja de saltar*/
  
          trex.vy=0;/*Porque ha llegado al suelo y se ha parado*/
  
          trex.y=suelo;/*Para que queda exactamente en el suelo*/
  
      }else{
  
        trex.vy-=trex.gravedad;/*Esto es para que autodecrementado en 2 en 2, esto sube para arriba*/
  
        trex.y-=trex.vy;/*Le restamos la velocidad*/
  
      }
  
    }
  
  }
  

  function colision(){
      //cactus.x
      //trex.y
      if(cactus.x >= 100 && cactus.x <= 165){
        if(trex.y >= suelo){
            nivel.muerto=true;
            if(nivel.record<nivel.marcador){
              nivel.record=nivel.marcador;
            }
            nivel.velocidad=0;
            aux=50;
            contador==31;
        }

      }
  }
  

  function puntuacion(){
    ctx.font = "30px impact";
    ctx.fillStyle= '#555555';
    ctx.fillText(`Record: `+`${nivel.record}`,500,50);
    ctx.fillText(`${nivel.marcador}`,600,90);
    
    if(nivel.muerto==true){
      ctx.font="60px impact";
      if(aux==52){
        ctx.fillText(`INICIAR JUEGO`,200,150);
      }else{
        ctx.fillText(`FIN DEL JUEGO`,200,150);

      }
      
    }
  }
  
  //-----------------------------------------------------------------
  
  /*Bucle Principal*/
  
  var FPS=60;
  
  setInterval(function(){/*Nos dice cada cuanto tiempo tiene que ejecutarse una funcion concreta*/
  
    principal();
  
  },1000/FPS);
  
  var contador=0;
  var aux=1;
  function principal(){/*Aquí vamos a llamar todo este el bucle principal*/
  contador++;
    console.log("principal");
    
    borrarCanvas();
    gravedad(); 
    colision()
    logicasuelo();
    logicacactus();
    dibujarsuelo();
    dibujarcactus();

    if(trex.saltando==true){
      dibujaRex();
    }else{
      if(aux<=2){
        dibujaRex();
        aux++;
      }else if(aux<=4){
        dibujaRex2();
        aux++;
      }else if(aux<=6){
        dibujaRex();
        aux++;
      }else if(aux<=8){
        dibujaRex3();
        aux++;
      }else if(aux<=10){
        dibujaRex();
        aux=1;
      }else if(aux==50){
        dibujaRex4();
      }
    }
    if(nivel.muerto==false){
      if(contador==30){
        console.log("mas 1");
        nivel.marcador++;
        nivel.velocidad=nivel.velocidad+0.1;
        contador=0;
      }
    }
    
    puntuacion();
  
  }