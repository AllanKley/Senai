#include <stdio.h>
#include <ctype.h>

String PalavraEscolhida;

void setup()
{
  //Define a porta do led como saida
  pinMode(12, OUTPUT);
  Serial.begin(9600); 
}

void loop(){
  
  Serial.println("Digite a palavra desejada:");
  
  while (Serial.available() <= 0)
  {}    
  
  PalavraEscolhida = Serial.readString();
  Serial.println(PalavraEscolhida);
    
  
  int tamanhoPalavra = sizeof(PalavraEscolhida)/sizeof(PalavraEscolhida[0]);

  for(int i = 0; i < tamanhoPalavra; i++){
    switch(PalavraEscolhida[i]){
      case 'a':
      
        ponto();
        traco();
      
        break;

      case 'b':

        traco();
        ponto();
        ponto();
        ponto();
        
        break;

      case 'c':

        traco();
        ponto();
        traco();
        ponto();
        
        break;

      case 'd':

        traco();
        ponto();
        ponto();
        
        break;

      case 'e':

        ponto();
      
        break;

      case 'f':

        ponto();
        ponto();
        traco();
        ponto();
        
        break;

      case 'g':

        traco();
        traco();
        ponto();
        
        break;
      
      case 'h':

        ponto();
        ponto();
        ponto();
        
        break;
      
      case 'i':

        ponto();
        ponto();
        
        break;
      
      case 'j':

        ponto();
        traco();
        traco();
        traco();
        break;
      
      case 'k':

        traco();
        ponto();
        traco();
        
        break;
      
      case 'l':

        ponto();
        traco();
        ponto();
        ponto();
        
        break;

      case 'm':

        traco();
        traco();
        
        break;

      case 'n':

        traco();
        ponto();
        
        break;

      case 'o':

        traco();
        traco();
        traco();
        
        break;

      case 'p':

        ponto();
        traco();
        traco();
        ponto();
        
        break;

      case 'q':

        traco();
        traco();
        ponto();
        traco();
        
        break;

      case 'r':

        ponto();
        traco();
        ponto();
        
        break;

      case 's':

        ponto();
        ponto();
        ponto();
        break;

      case 't':

        traco();
        
        break;

      case 'u':

        ponto();
        ponto();
        traco();
        
        break;

      case 'v':

        ponto();
        ponto();
        ponto();
        traco();
        
        break;

      case 'w':

        ponto();
        traco();
        traco();
        
        break;

      case 'x':

        traco();
        ponto();
        ponto();
        traco();
        
        break;

      case 'y':

        traco();
        ponto();
        traco();
        traco();
        
        break;

      case 'z':

        traco();
        traco();
        ponto();
        ponto();
        
        break;
    }
  }

  delay(1000);
}

void ponto(){
  digitalWrite(12, HIGH);
  delay(100);
  digitalWrite(12, LOW);
  delay(100);
}

void traco(){
  digitalWrite(12, HIGH);
  delay(300);
  digitalWrite(12, LOW);
  delay(300);
}
