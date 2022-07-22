﻿using FluentResults;
using GeradorTestes.Dominio;
using GeradorTestes.Dominio.ModuloMateria;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeradorTestes.Aplicacao.ModuloMateria
{
    public class ServicoMateria
    {
        private IRepositorioMateria repositorioMateria;
        private IContextoPersistencia contexto;

        public ServicoMateria(IRepositorioMateria repositorioMateria, IContextoPersistencia contexto)
        {
            this.repositorioMateria = repositorioMateria;
            this.contexto = contexto;
        }

        public Result<Materia> Inserir(Materia materia)
        {
            Log.Logger.Debug("Tentando inserir materia... {@m}", materia);

            Result resultado = ValidarMateria(materia);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioMateria.Inserir(materia);

                contexto.GravarDados();

                Log.Logger.Information("Materia {MateriaId} inserida com sucesso", materia.Id);

                return Result.Ok(materia);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar inserir a Materia";

                Log.Logger.Error(ex, msgErro + " {MateriaId}", materia.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<Materia> Editar(Materia materia)
        {
            Log.Logger.Debug("Tentando editar materia... {@m}", materia);

            var resultado = ValidarMateria(materia);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                repositorioMateria.Editar(materia);

                contexto.GravarDados();

                Log.Logger.Information("Materia {MateriaId} editada com sucesso", materia.Id);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar editar a Materia";

                Log.Logger.Error(ex, msgErro + " {MateriaId}", materia.Id);

                return Result.Fail(msgErro);
            }

            return Result.Ok(materia);
        }

        public Result Excluir(Materia materia)
        {
            Log.Logger.Debug("Tentando excluir materia... {@m}", materia);

            try
            {
                repositorioMateria.Excluir(materia);

                contexto.GravarDados();

                Log.Logger.Information("Materia {MateriaId} excluída com sucesso", materia.Id);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar excluir a Materia";

                Log.Logger.Error(ex, msgErro + " {MateriaId}", materia.Id);

                return Result.Fail(msgErro);
            }
        }

        public Result<List<Materia>> SelecionarTodos()
        {
            Log.Logger.Debug("Tentando selecionar materias...");

            try
            {
                var materias = repositorioMateria.SelecionarTodos(incluirDisciplina:true);

                Log.Logger.Information("Matérias selecionadas com sucesso");

                return Result.Ok(materias);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar todas as Materias";

                Log.Logger.Error(ex, msgErro);

                return Result.Fail(msgErro);
            }
        }

        public Result<Materia> SelecionarPorId(Guid id)
        {
            try
            {
                var materia = repositorioMateria.SelecionarPorId(id);

                if (materia == null)
                {
                    Log.Logger.Warning("Matéria {MateriaId} não encontrada", id);

                    return Result.Fail("Matéria não encontrada");
                }

                Log.Logger.Information("Matéria {MateriaId} selecionada com sucesso", id);

                return Result.Ok(materia);
            }
            catch (Exception ex)
            {
                string msgErro = "Falha no sistema ao tentar selecionar o Materia";

                Log.Logger.Error(ex, msgErro + " {MateriaId}", id);

                return Result.Fail(msgErro);
            }
        }

        #region Métodos privados
        private Result ValidarMateria(Materia materia)
        {
            var validador = new ValidadorMateria();

            var resultadoValidacao = validador.Validate(materia);

            var erros = new List<Error>();

            foreach (var validationFailure in resultadoValidacao.Errors)
            {
                Log.Logger.Warning(validationFailure.ErrorMessage);

                erros.Add(new Error(validationFailure.ErrorMessage));
            }

            if (NomeDuplicado(materia))
                erros.Add(new Error($"Este nome \"{materia.Nome}\" já está cadastrado no sistema"));

            if (erros.Any())
                return Result.Fail(erros);

            return Result.Ok();
        }

        private bool NomeDuplicado(Materia materia)
        {
            var materiaEncontrado = repositorioMateria.SelecionarMateriaPorNome(materia.Nome);

            return materiaEncontrado != null &&
                   materiaEncontrado.Nome == materia.Nome &&
                   materiaEncontrado.Id != materia.Id;
        }

        #endregion
    }
}
