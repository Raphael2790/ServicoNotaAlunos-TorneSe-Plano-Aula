namespace TorneSe.ServicoNotaAluno.Domain.Utils;

public static class Constants
{
    public static class ApplicationMessages
    {
        public const string SEM_MENSAGENS_NA_FILA = "Não existem mensagens a serem procesaadas.";
    }

    public static class ValidationMessages
    {
        public const string ALUNO_INEXISTENTE = "O aluno informado não existe.";
        public const string PROFESSOR_INEXISTENTE = "O professor informado não existe";
        public const string DISCIPLINA_INEXISTENTE = "A disciplina da atividade não existe";
        public const string ALUNO_INATIVO = "O aluno informado não pode receber nota porque está inativo.";
        public const string ALUNO_NAO_MATRICULADO = "O aluno não está matriculado na disciplina";
        public const string PROFESSOR_INATIVO = "O professor informado não pode atribuir notas pois está inativo";
        public const string PROFESSOR_SUPLENTE_NAO_PODE_DAR_NOTA = "O professor informado é um professor suplente e não pode atribuir notas aos alunos";
        public const string PROFESSOR_NAO_PODE_DAR_NOTA_DISCIPLINA = "O professor informado não é titular dessa disciplina";
        public const string DISCIPLINA_TIPO_ENCONTRO = "A disciplina é do tipo encontro e não pode receber notas";
        public const string DISCIPLINA_FECHADA = "A disciplina da atividade não pode receber mais lançamentos de notas";
        public const string ALUNO_JA_POSSUI_ATIVIDADE_SEMELHANTE_CANCELADA = "O aluno ja possui uma atividade cancelada por retentativa do mesmo tipo";
    }

    public static class ExceptionMessages
    {
        
    }
}
