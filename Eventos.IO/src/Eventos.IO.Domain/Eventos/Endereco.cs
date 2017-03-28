using Eventos.IO.Domain.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eventos.IO.Domain.Eventos
{
    public class Endereco : Entity<Endereco>
    {
        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string CEP { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public Guid? EventoId { get; set; }

        //EF propriedades de navegação
        public virtual Evento Evento { get; private set; }

        public Endereco(Guid id, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, Guid? eventoId)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            CEP = cep;
            Cidade = cidade;
            Estado = estado;
            EventoId = eventoId;
        }

        //Construtor EF
        protected Endereco()
        {

        }

        public override bool EhValido()
        {
            RuleFor(c => c.Logradouro)
                .NotEmpty().WithMessage("O logradouro precisa ser fornecido")
                .Length(2, 150).WithMessage("o logradouro precisar ter entre 2 e 150 caracteres");

            RuleFor(c => c.Bairro)
                .NotEmpty().WithMessage("O bairro precisa ser fornecido")
                .Length(2, 150).WithMessage("o bairro precisar ter entre 2 e 150 caracteres");

            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("O cep precisa ser fornecido")
                .Length(8).WithMessage("o cep precisar ter 8 caracteres");

            RuleFor(c => c.Cidade)
                .NotEmpty().WithMessage("O cidade precisa ser fornecido")
                .Length(2, 150).WithMessage("o cidade precisar ter entre 2 e 150 caracteres");

            RuleFor(c => c.Estado)
                .NotEmpty().WithMessage("O estado precisa ser fornecido")
                .Length(2, 150).WithMessage("o estado precisar ter entre 2 e 150 caracteres");

            RuleFor(c => c.Numero)
                .NotEmpty().WithMessage("O numero precisa ser fornecido")
                .Length(1, 10).WithMessage("o numero precisar ter entre 1 e 10 caracteres");

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
