using System;

namespace RoleTopMVC.Models
{
    public class Evento
    {
        public string NomeEvento {get;set;}
        public string DescricaoEvento {get;set;}
        public Iluminacao Iluminacao {get;set;}
        public Som Som {get;set;}
        
        public Evento(){
            this.Iluminacao = new Iluminacao();
            this.Som = new Som();
        }
    }    
}