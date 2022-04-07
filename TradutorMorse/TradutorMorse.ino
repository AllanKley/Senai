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

        Serial.print(" -. ");
      
        break;

      case 'b':

        traco();
        ponto();
        ponto();
        ponto();
        Serial.print(" -... ");
        break;

      case 'c':

        traco();
        ponto();
        traco();
        ponto();
        Serial.print(" -.-. ");
        break;

      case 'd':

        traco();
        ponto();
        ponto();
        Serial.print(" -.. ");
        break;

      case 'e':

        ponto();
        Serial.print(" . ");
        break;

      case 'f':

        ponto();
        ponto();
        traco();
        ponto();
        Serial.print(" ..-. ");
        break;

      case 'g':

        traco();
        traco();
        ponto();
        Serial.print(" --. ");
        break;
      
      case 'h':

        ponto();
        ponto();
        ponto();
        ponto();
        Serial.print(" .... ");
        break;
      
      case 'i':

        ponto();
        ponto();
        Serial.print(" .. ");
        break;
      
      case 'j':

        ponto();
        traco();
        traco();
        traco();
        Serial.print(" .--- ");
        break;
      
      case 'k':

        traco();
        ponto();
        traco();
        Serial.print(" -.- ");
        break;
      
      case 'l':

        ponto();
        traco();
        ponto();
        ponto();
        Serial.print(" .-.. ");
        break;

      case 'm':

        traco();
        traco();
        Serial.print(" -- ");
        break;

      case 'n':

        traco();
        ponto();
        Serial.print(" -. ");
        break;

      case 'o':

        traco();
        traco();
        traco();

        Serial.print(" --- ");
        
        break;

      case 'p':

        ponto();
        traco();
        traco();
        ponto();
        Serial.print(" .--. ");
        break;

      case 'q':

        traco();
        traco();
        ponto();
        traco();
        Serial.print(" --.- ");
        break;

      case 'r':

        ponto();
        traco();
        ponto();
        Serial.print(" .-. ");
        break;

      case 's':

        ponto();
        ponto();
        ponto();

        Serial.print(" ... ");
        break;

      case 't':

        traco();
        Serial.print(" -- ");
        break;

      case 'u':

        ponto();
        ponto();
        traco();
        Serial.print(" ..- ");
        break;

      case 'v':

        ponto();
        ponto();
        ponto();
        traco();
        Serial.print(" ...- ");
        break;

      case 'w':

        ponto();
        traco();
        traco();
        Serial.print(" .-- ");
        break;

      case 'x':

        traco();
        ponto();
        ponto();
        traco();
        Serial.print(" -..- ");
        break;

      case 'y':

        traco();
        ponto();
        traco();
        traco();
        Serial.print(" -.-- ");
        break;

      case 'z':

        traco();
        traco();
        ponto();
        ponto();
        Serial.print(" --.. ");
        break;
    }
  }
  Serial.println("\n");
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
